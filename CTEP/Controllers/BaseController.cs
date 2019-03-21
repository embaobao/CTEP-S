using System.Web.Mvc;
using CTEP.Models;
using System.Runtime.Remoting.Messaging;
using System;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;
using System.Data.Entity;

namespace CTEP.Controllers
{
    public class BaseController : Controller
    {
        public CTEBdbEntities db
        {
            get
            {
                CTEBdbEntities db = CallContext.GetData("db") as CTEBdbEntities;
                if (db == null)
                {
                    db = new CTEBdbEntities();
                    CallContext.SetData("db", db);
                }
                return db;
            }
        }
        public int EFormsNumForId(int? id)
        {

            int num = 0;


            try
            {
                if (id < 0)
                {
                    return num;
                }

                num = db.EvalutionForms.Where(x => x.id == id).Count();
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }
        public int CTNumForId(int? id)
        {
            int num = 0;

            try
            {
                if (id < 0)
                {
                    return num;
                }
                num = db.CourseTemplates.Where(x => x.id == id).Count();
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }
        public int SBNumForId(int? id)
        {
            int num = 0;

            try
            {
                if (id < 0)
                {
                    return num;
                }
                num = db.SchoolBandisTabs.Where(x => x.id == id).Count();
            }
            catch (Exception)
            {
                num = -1;
            }
            return num;
        }

        /// <summary>
        /// 对象转化成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public object JsonData<T>(T obj)
        {
            return Json(obj).Data;
        }
        public DateTime TimeNow
        {
            get => DateTime.Now;
        }


        public T ExecuteController<T>()
        {

            //var result = Controller.SetBandsiSchool0(sB);
            //return result;
            return DependencyResolver.Current.GetService<T>();
        }



        /// <summary>
        /// Json 反序列化成对象
        /// </summary>
        /// <typeparam name="T">转化的类型</typeparam>
        /// <param name="JsonString">Json 的 序列化对象</param>
        /// <returns>反序列化成的对象</returns>
        public T ToObject<T>(string JsonString)
        {
            T data = default;
            try
            {
                data = JsonConvert.DeserializeObject<T>(JsonString);
            }
            catch (Exception)
            {
                return data;
            }
            return data;
        }

        public bool AddData<T>(T val)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(val).State = EntityState.Added;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }
        public bool ChangeData<T>(T val)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(val).State = EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        public bool DeletedData<T>(T val)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(val).State = EntityState.Deleted;
                    db.SaveChanges();
                }

            }
            catch (Exception)
            {

                return false;
            }

            return true;

        }

        [Obsolete]
        public bool SendMail(Users user)
        {
            return new EMail(user.email).Send(new Users { email = user.email,pw=user.pw,role=user.role,status=1 });
        }

        

        /// <summary>
        /// 转化成Base64
        /// </summary>
        /// <typeparam name="T">要转化的对象类型</typeparam>
        /// <param name="val">对象</param>
        /// <returns>转化成Base64的字符串</returns>
        public  string ToBase64<T>(T val)
        {
            
            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(JsonData<T>(val).ToString())).Replace("+", "%2B");

        }
        /// <summary>
        /// 把字符串转化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="b64">Base 64 字符</param>
        /// <returns>转化成的对象</returns>
        public T FromBase64<T>(string b64)
        {
            return ToObject<T>( System.Text.Encoding.Default.GetString(Convert.FromBase64String(b64.Trim().ToString().Replace("%2B", "+"))) ) ;
        }









        //_______________________!
    }
}