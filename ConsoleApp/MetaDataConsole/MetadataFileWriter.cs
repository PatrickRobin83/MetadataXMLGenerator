/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   MetadataFileWriter.cs
 *   Date			:   2020-02-05 15:56:24
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using MetaDataXMLGenerator.MetaDataConsole.Config;
using MetaDataXMLGenerator.MetaDataConsole.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Xml;

namespace MetadataXMLGenerator.MetaDataConsole
{
    /// <summary>
    /// This Class 
    /// </summary>
    public class MetadataFileWriter
    {

        #region Fields

        private IniReader _iniReader = new IniReader(Helper.SettingsIniFile);

        #endregion

        #region Properties
        /// <summary>
        /// Path where to put the metadata.xml
        /// </summary>
        public string PathForMetadataXml { get; set; }

        #endregion

        #region Constructor

        #endregion

        #region Methods

        /// <summary>
        /// Writes the Metadata.xml file
        /// </summary>
        /// <param name="entries"></param>
        public void WriteMetadataXml(List<MetaDataEntry> entries)
        {
            if (File.Exists(PathForMetadataXml + "\\Metadata.xml"))
            {
                File.Delete(PathForMetadataXml + "\\Metadata.xml");
            }

            try
            {
                if (entries.Count > 0)
                {
                    Console.WriteLine($"File created in: {PathForMetadataXml} ");
                    Console.WriteLine();
                    XmlDocument doc = new XmlDocument();
                    XmlNode declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", "yes");
                    doc.AppendChild(declaration);

                    XmlNode docRoot = doc.CreateElement("Directory");
                    foreach (MetaDataEntry entry in entries)
                    {
                        if (!string.IsNullOrEmpty(entry.SystemTagValue))
                        {
                            XmlNode fileElement = doc.CreateElement("File");
                            XmlAttribute attribute = doc.CreateAttribute("name");
                            attribute.Value = entry.FileName;
                            XmlNode systemTags = doc.CreateElement("SystemTags");
                            fileElement.AppendChild(systemTags);
                            XmlNode systemTagNode = doc.CreateElement("SystemTag");
                            systemTags.AppendChild(systemTagNode);
                            XmlAttribute systemTag = doc.CreateAttribute("name");
                            XmlAttribute value = doc.CreateAttribute("value");
                            systemTag.Value = entry.SystemTagName;
                            value.Value = entry.SystemTagValue;
                            systemTagNode.Attributes.Append(systemTag);
                            systemTagNode.Attributes.Append(value);
                            fileElement.Attributes.Append(attribute);
                            docRoot.AppendChild(fileElement);
                        }
                    }
                    doc.AppendChild(docRoot);
                    doc.Save(PathForMetadataXml + "\\Metadata.xml");

                    Thread.Sleep(100);
                }
            }
            catch (UnauthorizedAccessException ex)    
            {
                Console.Error.WriteLine(ex.Message);
            }
        }

        #endregion

    }
}