using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTEP.Models
{
    /// <summary>
    /// 消息项
    /// </summary>
    /// <typeparam name="V">值</typeparam>
    public class MsItem <V>
    {
        int StatusNum { get; set; }

        string title { get; set; }

        V body { get; set; }
        public MsItem(V val) {
            StatusNum = 0;
            title = "MS";
            body = val;
        }

    }
}