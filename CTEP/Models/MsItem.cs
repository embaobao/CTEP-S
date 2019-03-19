using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;
using Newtonsoft.Json;


namespace CTEP.Models
{
    
    public class MsItem 
    {
        public int StatusNum { get; set; }

        public string title { get; set; }

        public object body { get; set; }
        public MsItem(object val) {
            StatusNum = 0;
            title = "jsonString";
            body = JsonConvert.SerializeObject(val);
        }
        public MsItem(string title,object val)
        {
            StatusNum = 0;
            this.title = title;
            body = JsonConvert.SerializeObject(val);
        }

    }
}