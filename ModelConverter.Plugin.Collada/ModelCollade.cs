﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModelConverter.Model;
using System.Reflection;
using System.Xml;

namespace ModelConverter.Plugin.Collada
{
    public class ModelCollade:IPlugin
    {
        public string Name { get { return "COLLADA"; } }
        public Dictionary<string, string> fileExtensions
        {
            get
            {
                Dictionary<string, string> extensionsDic = new Dictionary<string, string>();
                extensionsDic.Add("dae", "COLLADA");
                return extensionsDic;
            }
        }
		public Version PluginVersion { get { return Assembly.GetExecutingAssembly().GetName().Version; } }
        public string Creator { get { return "Sven Tatter"; } }
        public string About { get { return "Support for COLLADA Exchange Format"; } }

        public bool canRead { get { return false; } }
        public bool canWrite { get { return false; } }
        public bool supportReadTexture { get { return false; } }
        public bool supportWriteTexture { get { return false; } }
        public bool supportModelAnimation { get { return false; } }
        public bool supportModelAnimationNormal { get { return false; } }
        public bool supportModelAnimationTexture { get { return false; } }

        public BaseModel Read(string filePath, out List<LogMessage> Log)
        {
            XmlDocument document = new XmlDocument();
            document.Load(filePath);
            XmlNode UpVector = document.SelectSingleNode("//asset/up_axis");
            XmlNodeList MaterialLibrary = document.SelectNodes("//library_materials");
            XmlNodeList ImagesLibrary = document.SelectNodes("//library_images");
            XmlNodeList GeometriesLibrary = document.SelectNodes("//library_geometries");
            XmlNodeList AnimationsLibrary = document.SelectNodes("//library_animations");

            foreach (XmlNode Mesh in document.SelectNodes("//library_geometries/geometry/mesh"))
            {
                //Mesh.SelectNodes(
            }

            throw new NotImplementedException();
        }

        public void Write(string filePath, BaseModel model, out List<LogMessage> Log)
        {
            throw new NotImplementedException();
        }
    }
}