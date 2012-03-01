using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MonoDevelop.Core;
using MonoDevelop.Projects.Extensions;
using MonoDevelop.Projects.Formats.MSBuild;

namespace MonoDevelop.Monobjc
{
	public class MonobjcProjectMigrationHandler : IDotNetSubtypeMigrationHandler
	{
		public IEnumerable<String> FilesToBackup (String filename)
		{
			return new String[]{ filename };
		}

		public bool Migrate (IProjectLoadProgressMonitor monitor, MSBuildProject project, String fileName, String language)
		{
			// Change IB file to use "InterfaceDefinition" build action.
			IList<MSBuildItem> items = project.GetAllItems().Where(item => item.Name == "Page").ToList();
			foreach(MSBuildItem item in items) {
				XmlElement newElement = project.doc.CreateElement (MonobjcProject.InterfaceDefinition);
				newElement.InnerXml = item.Element.InnerXml;
				foreach (System.Xml.XmlAttribute attribute in item.Element.Attributes) {
					newElement.SetAttribute (attribute.Name, attribute.Value);
				}
				item.Element.ParentNode.ReplaceChild (newElement, item.Element);
			}
			return true;
		}
	}
}
