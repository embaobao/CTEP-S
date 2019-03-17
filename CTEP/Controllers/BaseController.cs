using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;
using System.Runtime.Remoting.Messaging;

namespace CTEP.Controllers
{
    public class BaseController : Controller
    {
        public CTEPEntities db
        {
            get {
                CTEPEntities db = CallContext.GetData("db") as CTEPEntities;
                if (db == null)
                {
                    db = new CTEPEntities();
                    CallContext.SetData("db", db);

                }
                return db;
            }
        }
    }
}