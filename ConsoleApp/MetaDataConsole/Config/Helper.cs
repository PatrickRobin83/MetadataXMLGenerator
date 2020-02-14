/*
 * -----------------------------------------------------------------------------
 *	 
 *   Filename		:   Helper.cs
 *   Date			:   2020-02-06 11:35:35
 *   All rights reserved
 * 
 * -----------------------------------------------------------------------------
 * @author     Patrick Robin <support@rietrob.de>
 * @Version      1.0.0
 */

using System;
using System.Net;

namespace MetaDataXMLGenerator.MetaDataConsole.Config
{
    /// <summary>
    /// This class contains all stuff for setting up
    /// </summary>
    public static class Helper
    {

        #region Fields

        #endregion

        #region Properties
        ///  <summary>
        /// path of the settings.ini
        /// </summary>
        public static string SettingsIniFile = "resources\\Settings.ini";
        

        #endregion

        #region Constructor

        #endregion

        #region Methods
        /// <summary>
        /// Converts the given string into the right url format.
        /// </summary>
        /// <param name="stringToConvert"></param>
        /// <returns></returns>
        public static string convert(string stringToConvert)
        {
            if (stringToConvert == null) throw new ArgumentNullException(nameof(stringToConvert));
            string url = stringToConvert;

            url = url.Replace("\\", "/");
            url = WebUtility.UrlEncode(stringToConvert);
            //string url = Uri.EscapeDataString(stringToConvert);
           
           // url = url.Replace("%2F", "/");
            url = url.Replace("+", "%20");
            url = url.Replace("%5C", "/");

            return url;
        }

        #endregion
    }
}