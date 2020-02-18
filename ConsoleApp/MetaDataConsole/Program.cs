using MetadataXMLGenerator.MetaDataConsole;
using System;
using MetaDataXMLGenerator.MetaDataConsole.Config;


namespace MetaDataConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            DirectoryAndFileReader directoryAndFileReader = new DirectoryAndFileReader();
            IniReader iniReader = new IniReader(Helper.SettingsIniFile);

            if (string.IsNullOrEmpty(directoryAndFileReader.RootPath) || string.IsNullOrEmpty(iniReader.Read("RootPath","Web")))
            {
                Console.WriteLine("Please add a valid and existing path to the ..\\Resources\\Settings.ini file");
                Console.WriteLine();
                Console.WriteLine("Press any Key to exit ...");
                Console.ReadKey();
            }
            else
            {
                directoryAndFileReader.Run();
            }
        }
    }
}
