﻿using System.Web;
using System.Web.Mvc;

namespace BastardFat.ProcessSniffer.Server
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}