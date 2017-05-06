using DbLink;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VacuumBase.Classes
{
    public static class GlobalVariables
    {
        public static string UrlHost { get; set; }

        public static string FullPath { get; set; }

        public static string UrlStop { get; set; }

        public static Connect Link { get; set; }

        public static string GetFullUrl(string str)
        {
            return GlobalVariables.UrlHost + str;
        }

    }
}