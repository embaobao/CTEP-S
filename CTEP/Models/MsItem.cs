using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Controllers;
using CTEP.Models;

namespace CTEP.Models
{
    /// <summary>
    /// 消息项
    /// </summary>
    /// <typeparam name="V">值</typeparam>
    public class MsItem <V>:BaseController
    {
        int StatusNum { get; set; }

        string title { get; set; }

        JsonResult body { get; set; }
        public MsItem(V val) {
            StatusNum = 0;
            title = "MS";
            body = Json(val);
        }

    }
}