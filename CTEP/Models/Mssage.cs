using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CTEP.Models
{
    public class Mssage
    {

        /// <summary>
        ///  MssageType
        ///  erro 0
        /// </summary>
        public const int errMs = 0;


        /// <summary>
        ///  exp: ClassRandom: 0  Attribution : 0  ErroActive: 0
        /// user 0
        /// userinfo 1
        /// 0 NOT Seclct
        /// 1 id
        /// 2 email
        /// 3 role
        /// 4 status
        /// Null 0
        /// Other 1
        /// 
        /// </summary>
        public const int ErrEmailNull = 020;
        public const int ErrIdNull = 010;
        public const int UserNull = 000;
        public const int UserInfoNull = 100;

        //public Dictionary<int, string> MsTypecomparisontable = new Dictionary<int, string>(); = new Dictionary<int, string>();= new Dictionary<int, string>();
        public Dictionary<int, string> Mscomparisontable = null;

        public Dictionary<int, string> Typecomparisontable = null;
        public Mssage(int msType)
        {
            if (msType == 0)
            {
                Mscomparisontable = new Dictionary<int, string>();
                Mscomparisontable.Add(ErrEmailNull, "ErrEmailNull");
                Mscomparisontable.Add(ErrIdNull, "ErrIdNull");
                Mscomparisontable.Add(UserNull, "UserNull");
                Mscomparisontable.Add(UserInfoNull, "UserInfoNull");
            }
            
        }
        public Mssage()
        {

        }



        public int  MssageType  { get; set; }
   

        public int errNum { get; set; }

        public static string ErroMS(int errType)
        {

            return new Mssage(0).ErrMS(errType);
        }
        public string ErrMS(int errType) {
            string ms = "";
            Mscomparisontable.TryGetValue(errType,out ms);
            return ms;
        }
        
    }
}