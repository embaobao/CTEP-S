using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;
using System.Runtime.Remoting.Messaging;
using System.Data.Entity;

namespace CTEP.Controllers
{
    public class BaseController : Controller
    {
        public CTEBdbEntities db
        {
            get {
                CTEBdbEntities db = CallContext.GetData("db") as CTEBdbEntities;
                if (db == null)
                {
                    db = new CTEBdbEntities();
                    CallContext.SetData("db", db);
                }
                return db;
            }
        }
    }
}