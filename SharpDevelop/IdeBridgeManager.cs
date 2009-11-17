// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Matthew Ward" email="mrward@users.sourceforge.net"/>
//     <version>$Revision: 3096 $</version>
// </file>

using System;
using System.IO;
using System.Reflection;
using System.Resources;

using ICSharpCode.Core;
using ICSharpCode.SharpDevelop;
using ICSharpCode.SharpDevelop.Commands;
using ICSharpCode.SharpDevelop.Project;
//using ICSharpCode.SharpDevelop.Sda;
using ICSharpCode.SharpDevelop.Gui;

namespace IdeBridge
{
	public sealed class IdeBridgeManager
	{
		IdeBridgeManager()
		{
		}

		public static void Init()
		{
			IdeBridgeManager manager = new IdeBridgeManager();
			Assembly exe = manager.GetType().Assembly;

			string rootPath = Path.GetDirectoryName(exe.Location);
			string configDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "IdeBridge");
			string dataDirectory = Path.Combine(rootPath, "data");

			CoreStartup startup = new CoreStartup("IdeBridge");
			startup.ConfigDirectory = configDirectory;
			startup.DataDirectory = dataDirectory;
			startup.PropertiesName = "IdeBridge";

			startup.StartCoreServices();

			ResourceService.RegisterNeutralStrings(new ResourceManager("Resources.StringResources", exe));
			ResourceService.RegisterNeutralImages(new ResourceManager("Resources.BitmapResources", exe));

			StringParser.RegisterStringTagProvider(new SharpDevelopStringTagProvider());

			string addInFolder = Path.Combine(rootPath, "AddIns");
			startup.AddAddInsFromDirectory(addInFolder);
			startup.RunInitialization();
		}
	}
}
