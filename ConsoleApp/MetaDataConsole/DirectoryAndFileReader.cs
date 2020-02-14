/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   DirectoryAndFileReader.cs
 *   Date			:   2020-02-05 16:22:52
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

namespace MetadataXMLGenerator.MetaDataConsole
{
    public class DirectoryAndFileReader
    {
        
        #region Fields
        /// <summary>
        /// IniFile reader instance
        /// </summary>
        private IniReader _iniReader = new IniReader(Helper.SettingsIniFile);
        /// <summary>
        /// Metadata.xml writer instance
        /// </summary>
        private MetadataFileWriter _fileWriter = new MetadataFileWriter();
        /// <summary>
        /// Temporary List of Folders from the parent folder
        /// </summary>
        private List<string> _folderPathes;
        /// <summary>
        /// List of all Folders including the root Folder from Settings.ini file
        /// </summary>
        private List<string> _allFolders = new List<string>();
        /// <summary>
        /// Rootpath from the ini file. Where to start searching
        /// </summary>
        private string _rootPath;

        #endregion

        #region Properties

        #endregion

        #region Constructor
        /// <summary>
        /// Default Constructor with the Root Path initialization loaded from Settings.ini File
        /// </summary>
        public DirectoryAndFileReader()
        {
            _rootPath = _iniReader.Read("Rootpath", "Local");
        }

        #endregion

        #region Methods
        /// <summary>
        /// Starts the Metadata Generation Process
        /// </summary>
        public void Run()
        {
            try
            {
                _allFolders.Add(_rootPath);
                getFoldersFromRootPath(_rootPath);
                getFilesInFolder(_allFolders);

                Console.WriteLine("[FINISH]");
                Console.WriteLine();
                Thread.Sleep(1500);
                Console.Write("Bitte eine beliebige Taste drücken um das Programm zu beenden.....");
                Console.ReadKey();
                
            }
            catch (DirectoryNotFoundException directoryNotFound)
            {

                Console.WriteLine(directoryNotFound.Message);
                Console.ReadKey();
            }
        }

        /// <summary>
        /// gets the patzh from all Folders which are in the root path
        /// </summary>
        /// <param name="rootPath"></param>
        private void getFoldersFromRootPath(string rootPath)
        {
            _folderPathes = new List<string>();

            DirectoryInfo di = new DirectoryInfo(rootPath);
            
            if (di.GetDirectories().Length > 0)
            {
                foreach (string folderPath in Directory.EnumerateDirectories(rootPath))
                {
                    _allFolders.Add(folderPath);
                    _folderPathes.Add(folderPath);
                }
            }
            foreach (string folder in _folderPathes)
            {
                getFoldersFromRootPath(folder);
            }
        }

        /// <summary>
        /// get all Filenames inside a folder and adds it into a List
        /// </summary>
        /// <param name="folder"></param>
        private void getFilesInFolder(List<string> folder)
        {
            int i = 1;
            foreach (string rootFolder in _allFolders)
            {
                Console.WriteLine("**************************************************************************************************");
                List<MetaDataEntry> metaDataEntries = new List<MetaDataEntry>();

                DirectoryInfo di = new DirectoryInfo(rootFolder);

                if (di.GetFiles().Length > 0)
                {
                    foreach (FileInfo fileInfo in di.GetFiles())
                    {
                        if (!fileInfo.Name.EndsWith(".xml") && !fileInfo.Name.EndsWith(".ffs_db"))
                        {
                            string tmp_Name = fileInfo.FullName.Remove(0, _rootPath.Length);

                            string uriPath = Helper.convert(tmp_Name);

                            MetaDataEntry entry = new MetaDataEntry(fileInfo.Name, _iniReader.Read("RootPath", "Web") + uriPath);
                            metaDataEntries.Add(entry);

                        }
                    }
                    _fileWriter.PathForMetadataXml = rootFolder;
                    _fileWriter.WriteMetadataXml(metaDataEntries);
                }
                
                Console.WriteLine($"{i} von {_allFolders.Count} verarbeitet.");
                Console.WriteLine("");
                Console.WriteLine("**************************************************************************************************");

                i++;
               
            }
        }

        #endregion

    }
}