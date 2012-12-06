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
MONODEVELOP_APP?=/Applications/MonoDevelop.app
CONFIGURATION?=Debug
BUILD_NUMBER?=0
APP_SUPPORT_DIR=~/Library/Application\ Support/MonoDevelop-3.0/LocalInstall/Addins

# Set the directories
ADDINS_DIR=$(CURDIR)/addins
BUILD_DIR=$(CURDIR)/build
DIST_DIR=$(CURDIR)/dist
REPOSITORY_DIR=$(DIST_DIR)/repository
TOOLS_DIR=$(CURDIR)/tools

# Compute the version
ADDIN_VERSION=3.5
DATE_REFERENCE=$(shell date -j -f "%Y-%m-%d" "2007-07-01" "+%s")
DATE_TODAY=$(shell date "+%s")
REVISION_NUMBER=$(shell echo "($(DATE_TODAY) - $(DATE_REFERENCE)) / 86400" | bc)

# Set the tools
CAT=cat
CP=cp -f
CPC=rsync -a
MKDIR=mkdir -p
RMRF=rm -Rf
SED=sed
XBUILD=xbuild /verbosity:minimal /p:Configuration=$(CONFIGURATION) /p:OutDir=$(BUILD_DIR)/
MDTOOL=$(MONODEVELOP_APP)/Contents/MacOS/mdtool
MD_CONFIG_PATH=$(MONODEVELOP_APP)/Contents/MacOS/lib/pkgconfig

# Addin descriptors
MONOBJC_ADDINS= \
	MonoDevelop.Monobjc

# ----------------------------------------
# Targets
# ----------------------------------------

all: $(MONODEVELOP_APP)
	$(MKDIR) "$(BUILD_DIR)"
	$(MKDIR) "$(REPOSITORY_DIR)"
	
	for i in $(MONOBJC_ADDINS); do \
		$(CAT) $(ADDINS_DIR)/$$i/$$i.xml | $(SED) -e "s/0.0.0.0/$(ADDIN_VERSION).$(REVISION_NUMBER).$(BUILD_NUMBER)/g" > $(ADDINS_DIR)/$$i/addin.xml; \
	done
	(PKG_CONFIG_PATH=$(MD_CONFIG_PATH) $(XBUILD))

clean:
	for i in $(MONOBJC_ADDINS); do \
		$(CAT) $(ADDINS_DIR)/$$i/$$i.xml > $(ADDINS_DIR)/$$i/addin.xml; \
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
		$(CP) -R $(BUILD_DIR) $(APP_SUPPORT_DIR)/$$i.$(ADDIN_VERSION).$(REVISION_NUMBER).$(BUILD_NUMBER); \
	done

$(MONODEVELOP_APP):
	$(error Cannot found MonoDevelop application)

# ----------------------------------------
# Phony Targets
# ----------------------------------------

.PHONY: \
	all \
	clean \
	repository \
	local
