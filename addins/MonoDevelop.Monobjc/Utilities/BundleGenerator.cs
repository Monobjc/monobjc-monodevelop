//
// This file is part of Monobjc, a .NET/Objective-C bridge
// Copyright (C) 2007-2011 - Laurent Etiemble
//
// Monobjc is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// any later version.
//
// Monobjc is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with Monobjc.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Monobjc.Tools.Generators;
using MonoDevelop.Core;
using MonoDevelop.Projects;

namespace MonoDevelop.Monobjc.Utilities
{
    public static class BundleGenerator
    {
        public static void Generate(IProgressMonitor monitor, BuildResult result, MonobjcProject project, ConfigurationSelector configuration, String outputDirectory, bool native)
        {
            // Infer application name from configuration
            String applicationName = project.GetApplicationName(configuration);
            
            // Create the bundle maker
            BundleMaker maker = new BundleMaker(applicationName, outputDirectory);

            // Compile the XIB files
            BuildHelper.CompileXIBFiles(monitor, project, maker, result);
            if (result.ErrorCount > 0)
            {
                monitor.ReportError(GettextCatalog.GetString("Failed to compile XIB files"), null);
                return;
            }

            // Copy the output and dependencies
            BuildHelper.CopyOutputFiles(monitor, project, configuration, maker);

            // Copy the content files
            BuildHelper.CopyContentFiles(monitor, project, configuration, maker);
            
            // Create the Info.plist
            BuildHelper.CreateInfoPList(monitor, project, configuration, maker);

            if (native)
            {
                GenerateNative(monitor, result, project, configuration, maker);
            }
            else
            {
                // Copy the Monobjc assemblies
                BuildHelper.CopyMonobjcAssemblies(monitor, project, configuration, maker);
                
                // Write the native runtime
                monitor.BeginTask(GettextCatalog.GetString("Copying native launcher..."), 0);
                maker.WriteRuntime(project.TargetOSVersion);
                monitor.EndTask();
            }

            BuildHelper.CombineArtwork(monitor, project, maker);
            BuildHelper.EncryptContentFiles(monitor, project, configuration, maker);

            // Perform the signing
            BuildHelper.SignBundle(monitor, project, maker);
            BuildHelper.SignNativeBinaries(monitor, project, maker);
        }
        
        public static void Archive(IProgressMonitor monitor, BuildResult result, MonobjcProject project, ConfigurationSelector configuration, String outputDirectory)
        {
            monitor.BeginTask(GettextCatalog.GetString("Archiving..."), 0);
            
            // Infer application name from configuration
            String applicationName = project.GetApplicationName(configuration);
            
            // Create the bundle maker
            BundleMaker maker = new BundleMaker(applicationName, outputDirectory);
            
            // Archive the application
            BuildHelper.ArchiveBundle(monitor, project, maker);
            
            monitor.EndTask();
        }
        
        private static void GenerateNative(IProgressMonitor monitor, BuildResult result, MonobjcProject project, ConfigurationSelector configuration, BundleMaker maker)
        {
            // Create a directory for generation
            String tempDir = Path.Combine(project.GetOutputFileName(configuration).ParentDirectory, ".native");
            Directory.CreateDirectory(tempDir);

            // Build a list of all folders to visit when collecting managed references
            String mainAssembly = project.GetOutputFileName(configuration);
            String configurationDir = Path.GetDirectoryName(mainAssembly);
            List<String> searchDirs = new List<String>();
            searchDirs.Add(configurationDir);

            // For each reference, add its base dir
            foreach (ProjectReference reference in project.References)
            {
                String[] files = reference.GetReferencedFileNames(configuration);
                foreach (string file in files)
                {
                    String dir = Path.GetDirectoryName(file);
                    searchDirs.Add(dir);
                }
            }

            // Remove redundant entries
            searchDirs = searchDirs.Distinct().ToList();

            // Collect all the assemblies
            monitor.BeginTask(GettextCatalog.GetString("Collecting assemblies..."), 0);
            ManagedReferenceCollector collector = new ManagedReferenceCollector();
            collector.Logger = new BuildLogger(monitor, result);
            collector.SearchDirectories = searchDirs;
            monitor.EndTask();

            // Collect the main assembly references
            List<String> assemblies = new List<String>();
            assemblies.AddRange(collector.Collect(mainAssembly));

            // Remove redundant entries
            assemblies = assemblies.Distinct().ToList();

            // Generate the embedded executable
            monitor.BeginTask(GettextCatalog.GetString("Generating native code..."), 0);
            NativeCodeGenerator codeGenerator = new NativeCodeGenerator();
            codeGenerator.Logger = new BuildLogger(monitor, result);
            codeGenerator.Assemblies = assemblies;
            codeGenerator.DeveloperToolsFolder = DeveloperToolsDesktopApplication.DeveloperToolsFolder;
            codeGenerator.TargetOSVersion = project.TargetOSVersion;
            codeGenerator.TargetArchitecture = project.TargetOSArch;

            // We embed the machine.config file; it depends on the target framework
            int version = (int) project.TargetFramework.ClrVersion;
            switch (version)
            {
                case 2: // ClrVersion.Net_2_0:
                    codeGenerator.MachineConfiguration = "/Library/Frameworks/Mono.framework/Home/etc/mono/2.0/machine.config";
                    break;
                case 4: // ClrVersion.Net_4_0:
                    codeGenerator.MachineConfiguration = "/Library/Frameworks/Mono.framework/Home/etc/mono/4.0/machine.config";
                    break;
                case 5: // ClrVersion.Net_4_5:
                    codeGenerator.MachineConfiguration = "/Library/Frameworks/Mono.framework/Home/etc/mono/4.5/machine.config";
                    break;
            }

            // Launch the generation
            String executableFile = codeGenerator.Generate(tempDir);
            String libraryFile = Path.Combine(tempDir, "libmonobjc.dylib");
            monitor.EndTask();

            // Copy the native parts into the bundle
            monitor.BeginTask(GettextCatalog.GetString("Copying native code..."), 0);
            maker.CopyTo(executableFile, maker.MacOSDirectory);
            maker.CopyTo(libraryFile, maker.MacOSDirectory);
            monitor.EndTask();

            // Change the paths
            executableFile = maker.Combine(maker.MacOSDirectory, executableFile);
            libraryFile = maker.Combine(maker.MacOSDirectory, libraryFile);

            // Relocate the libraries
            monitor.BeginTask(GettextCatalog.GetString("Relocating native code..."), 0);
            NativeCodeRelocator relocator = new NativeCodeRelocator();
            relocator.Logger = new BuildLogger(monitor, result);
            relocator.DependencyPattern = new List<string> {"Mono.framework"};
            relocator.Relocate(executableFile, maker.MacOSDirectory);
            relocator.Relocate(libraryFile, maker.MacOSDirectory);
            monitor.EndTask();
        }
    }
}
