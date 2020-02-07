using MetadataXMLGenerator.MetaDataConsole;
using MetaDataXMLGenerator.MetaDataConsole.Config;
using System;
using System.Collections.Generic;
using System.IO;


namespace MetaDataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryAndFileReader directoryAndFileReader = new DirectoryAndFileReader();
            directoryAndFileReader.Run();
            
        }
    }
}
