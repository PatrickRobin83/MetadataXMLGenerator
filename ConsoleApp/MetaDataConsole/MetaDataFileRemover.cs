/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   MetaDataFileRemover.cs
 *   Date			:   2020-02-17 14:54:05
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Documents;

namespace MetaDataXMLGenerator.MetaDataConsole
{
    /// <summary>
    /// This class is for removing the Metadata.xml from every Folder.
    /// </summary>
    public class MetaDataFileRemover
    {
        

        #region Fields

        #endregion

        #region Properties

        #endregion

        #region Constructor

        #endregion

        #region Methods

        public void RemoveMetadataXmlFromFolder(List<string> allFolder)
        {

            Console.WriteLine($"{allFolder.Count} Files found.");
            int i = 1;
            foreach (string path in allFolder)
            {
                if (File.Exists(path + "\\Metadata.xml"))
                {
                    Console.WriteLine("**************************************************************************************************");

                    File.Delete(path + "\\Metadata.xml");

                    Console.WriteLine($"{i} Files from {allFolder.Count} Files processed.");
                    Console.WriteLine("");
                    Console.WriteLine("**************************************************************************************************");
                    Thread.Sleep(100);
                }
                
                i++;
            }
        }

        #endregion

    }
}