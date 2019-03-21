using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace CTEP.Models
{

    public class MsItem 
    {
        public int StatusNum { get; set; }

        public string title { get; set; }

        public object body { get; set; }

        public MsItem()
        {
        }
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
        public MsItem(int status,string title, object val)
        {
            this.StatusNum = status;
            this.title = title;
            this.body = JsonConvert.SerializeObject(val);
        }

        public object Json(object data)
        {
            JavaScriptSerializer javaScriptSerializer = null;
            if (data != null)
            {
                javaScriptSerializer = new JavaScriptSerializer();
                return javaScriptSerializer.Serialize(data);
            }
            else
            {
                return string.Empty;
            }
        }

        public object GetJson() {
            return JsonConvert.SerializeObject(this);
        }

        public void SetS(int num) {
            this.StatusNum = num;
        }
        public void SetT(string t)
        {
            this.title = t;
        }
        public void SetB(object b)
        {
            this.body = b;
        }

    }

    public class MsNonJson : MsItem
    {
       
        public MsNonJson(object body) {
            this.StatusNum = 0;
            this.title = "";
            this.body = body;
        }
        public MsNonJson(string t,object body)
        {
            this.StatusNum = 0;
            this.title = t;
            this.body = body;
        }
        public MsNonJson(int s,string t, object body)
        {
            this.StatusNum = s;
            this.title = t;
            this.body = body;
        }
    }
    public class MsJson : MsItem
    {
        public MsJson(object body)
        {
            this.StatusNum = 0;
            this.title = "";
            this.body = Json(body);
        }
        public MsJson(string t, object body)
        {
            this.StatusNum = 0;
            this.title = t;
            this.body = Json(body); 
        }
        public MsJson(int s, string t, object body)
        {
            this.StatusNum = s;
            this.title = t;
            this.body = Json(body);
        }

       
    }
}