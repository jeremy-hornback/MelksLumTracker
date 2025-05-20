using System;
using System.IO;
using System.Xml;

namespace MelksLuminanceTracker
{
	public static class Util
	{
        private static string mBasePath = null;

		public static void LogError(Exception ex)
		{
			try
			{
				using (StreamWriter writer = new StreamWriter(Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\Asheron's Call\" + Globals.PluginName + " errors.txt", true))
				{
					writer.WriteLine("============================================================================");
					writer.WriteLine(DateTime.Now.ToString());
					writer.WriteLine("Error: " + ex.Message);
					writer.WriteLine("Source: " + ex.Source);
					writer.WriteLine("Stack: " + ex.StackTrace);
					if (ex.InnerException != null)
					{
						writer.WriteLine("Inner: " + ex.InnerException.Message);
						writer.WriteLine("Inner Stack: " + ex.InnerException.StackTrace);
					}
					writer.WriteLine("============================================================================");
					writer.WriteLine("");
					writer.Close();
				}
			}
			catch
			{
			}
		}

		public static void WriteToChat(string message)
		{
			try
			{
				Globals.Host.Actions.AddChatText("<{" + Globals.PluginName + "}>: " + message, 5);
			}
			catch (Exception ex) { LogError(ex); }
		}
        
        public static string FullPath(string fileName)
		{
			return System.IO.Path.Combine(BasePath, fileName);
		}

        public static void SaveXml(XmlDocument doc, string filePath)
		{
			XmlWriterSettings writerSettings = new XmlWriterSettings();
			writerSettings.Indent = true;
			writerSettings.IndentChars = "    ";
			using (XmlWriter writer = XmlWriter.Create(filePath, writerSettings))
			{
				doc.Save(writer);
			}
		}

        public static string BasePath
		{
			get
			{
				if (mBasePath == null)
				{
					mBasePath = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString();
					int slash = mBasePath.LastIndexOf('\\');
					if (slash > 0)
						mBasePath = mBasePath.Substring(0, slash + 1);
				}
				return mBasePath;
			}
			set
			{
				if (value.EndsWith("\\"))
					mBasePath = value;
				else
					mBasePath = value + "\\";
			}
		}
	}
    
}
