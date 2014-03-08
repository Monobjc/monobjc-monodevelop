## =======================================
##  __  __                   _     _
## |  \/  | ___  _ __   ___ | |__ (_) ___ 
## | |\/| |/ _ \| '_ \ / _ \| '_ \| |/ __|
## | |  | | (_) | | | | (_) | |_) | | (__ 
## |_|  |_|\___/|_| |_|\___/|_.__// |\___|
##                              |__/
## Top level makefile
## =======================================

# ----------------------------------------
# Variables
# ----------------------------------------

# Set the default parameters
VERSION?=4.2
CONFIGURATION?=Debug
BUILD_NUMBER?=0
IDE_APP?=/Applications/Xamarin\ Studio.app
APP_SUPPORT_DIR?=~/Library/Application\ Support/XamarinStudio-4.0/LocalInstall/Addins

# Set the directories
ADDINS_DIR=$(CURDIR)/addins
BUILD_DIR=$(CURDIR)/build
DIST_DIR=$(CURDIR)/dist
REPOSITORY_DIR=$(DIST_DIR)/repository
TOOLS_DIR=$(CURDIR)/tools

# Compute the version
DATE_REFERENCE=$(shell date -j -f "%Y-%m-%d" "2007-07-01" "+%s")
DATE_TODAY=$(shell date "+%s")
REVISION_NUMBER=$(shell echo "($(DATE_TODAY) - $(DATE_REFERENCE)) / 86400" | bc)
MD_PROPERTIES=MD_$(shell echo "$(VERSION)" | sed -e "s/\./_/g")

# Set the tools
CAT=cat
CP=cp -f
CPC=rsync -a
MKDIR=mkdir -p
RMRF=rm -Rf
SED=sed
XBUILD=xbuild /verbosity:minimal /p:Configuration=$(CONFIGURATION) /p:OutDir=$(BUILD_DIR)/
MDTOOL=$(IDE_APP)/Contents/MacOS/mdtool
MD_CONFIG_PATH=$(IDE_APP)/Contents/MacOS/lib/pkgconfig

# Addin descriptors
MONOBJC_ADDINS= \
	MonoDevelop.Monobjc

# ----------------------------------------
# Targets
# ----------------------------------------

all: $(IDE_APP)
	$(MKDIR) "$(BUILD_DIR)"
	$(MKDIR) "$(REPOSITORY_DIR)"
	
	for i in $(MONOBJC_ADDINS); do \
        $(CAT) $(ADDINS_DIR)/$$i/$$i.xml | $(SED) -e "s/@VERSION@/$(VERSION)/g" -e "s/@REVISION@/$(REVISION_NUMBER).$(BUILD_NUMBER)/g" > $(ADDINS_DIR)/$$i/addin.xml; \
        $(CAT) $(ADDINS_DIR)/$$i/$$i.csproj.tmpl | $(SED) -e "s/@VERSION@/$(VERSION)/g" -e "s/@PROPERTIES@/$(MD_PROPERTIES)/g" > $(ADDINS_DIR)/$$i/$$i.csproj; \
	done
	(PKG_CONFIG_PATH=$(MD_CONFIG_PATH) $(XBUILD))

clean:
	for i in $(MONOBJC_ADDINS); do \
        $(CAT) $(ADDINS_DIR)/$$i/$$i.xml > $(ADDINS_DIR)/$$i/addin.xml; \
        $(CAT) $(ADDINS_DIR)/$$i/$$i.csproj.tmpl | $(SED) -e "s/@VERSION@/3.0/g" > $(ADDINS_DIR)/$$i/$$i.csproj; \
	done
	$(RMRF) "$(BUILD_DIR)"
	$(RMRF) "$(DIST_DIR)"

repository: all $(MONOBJC_ADDINS_DESCRIPTOR_DIST)
	for i in $(MONOBJC_ADDINS); do \
		$(CPC) $(ADDINS_DIR)/$$i/addin.xml $(BUILD_DIR)/$$i.xml; \
		$(MDTOOL) setup pack $(BUILD_DIR)/$$i.xml -d:$(REPOSITORY_DIR); \
	done
	$(MDTOOL) setup rep-build $(REPOSITORY_DIR)

local: repository
	for i in $(MONOBJC_ADDINS); do \
		$(CP) -R $(BUILD_DIR) $(APP_SUPPORT_DIR)/$$i.$(VERSION).$(REVISION_NUMBER).$(BUILD_NUMBER); \
	done

$(IDE_APP):
	$(error Cannot found MonoDevelop application)

# ----------------------------------------
# Phony Targets
# ----------------------------------------

.PHONY: \
	all \
	clean \
	repository \
	local
