﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace CarConnect.Utility
{
    internal class DbConnUtil
    {
        private static IConfiguration _iconfiguration;

        static DbConnUtil()
        {
            GetAppSettingsFile();
        }

        private static void GetAppSettingsFile()
        {
            var builder = new ConfigurationBuilder()
                         .SetBasePath(Directory.GetCurrentDirectory())// sets the base path where configuration file is located
                         .AddJsonFile("appsettings.json");// configuration should include  the configuration file named as appsettings,json 
            _iconfiguration = builder.Build();//build creates an Iconfiguration object which has data from Appsettings file



        }

        public static string GetConnString()
        {
            return _iconfiguration.GetConnectionString("LocalConnectionString");
        }
    }
}