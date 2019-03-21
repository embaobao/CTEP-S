using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTEP.Models
{
    public class Tools
    {


        public static string BaseUrl = "http://localhost:55810/";
        /// <summary>
        /// 对象转化成Json
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object Json<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        /// <summary>
        /// Json 反序列化成对象
        /// </summary>
        /// <typeparam name="T">转化的类型</typeparam>
        /// <param name="JsonString">Json 的 序列化对象</param>
        /// <returns>反序列化成的对象</returns>
        public static T ToObject<T>(string JsonString)
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


        /// <summary>
        /// 转化成Base64
        /// </summary>
        /// <typeparam name="T">要转化的对象类型</typeparam>
        /// <param name="val">对象</param>
        /// <returns>转化成Base64的字符串</returns>
        public static string ToBase64<T>(T val)
        {

            return Convert.ToBase64String(System.Text.Encoding.Default.GetBytes(Json<T>(val).ToString())).Replace("+", "%2B");

        }
        /// <summary>
        /// 把字符串转化成对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="b64">Base 64 字符</param>
        /// <returns>转化成的对象</returns>
        public static T FromBase64<T>(string b64)
        {
            return ToObject<T>(System.Text.Encoding.Default.GetString(Convert.FromBase64String(b64.Trim().ToString().Replace("%2B", "+"))));
        }


    }
}