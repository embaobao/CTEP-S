using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CTEP.Models;

namespace CTEP.Controllers
{
    public class CTEPSysCofigsController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: CTEPSysCofigs
        public ActionResult Index()
        {
            return View(db.CTEPSysCofig.ToList());
        }

        // GET: CTEPSysCofigs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTEPSysCofig cTEPSysCofig = db.CTEPSysCofig.Find(id);
            if (cTEPSysCofig == null)
            {
                return HttpNotFound();
            }
            return View(cTEPSysCofig);
        }

        // GET: CTEPSysCofigs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CTEPSysCofigs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,AdminId,AdminPw,SysDescription,EmaiTemplate,WebAddress,SysAddress,img")] CTEPSysCofig cTEPSysCofig)
        {
            if (ModelState.IsValid)
            {
                db.CTEPSysCofig.Add(cTEPSysCofig);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(cTEPSysCofig);
        }

        // GET: CTEPSysCofigs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTEPSysCofig cTEPSysCofig = db.CTEPSysCofig.Find(id);
            if (cTEPSysCofig == null)
            {
                return HttpNotFound();
            }
            return View(cTEPSysCofig);
        }

        // POST: CTEPSysCofigs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,AdminId,AdminPw,SysDescription,EmaiTemplate,WebAddress,SysAddress,img")] CTEPSysCofig cTEPSysCofig)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cTEPSysCofig).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cTEPSysCofig);
        }

        // GET: CTEPSysCofigs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CTEPSysCofig cTEPSysCofig = db.CTEPSysCofig.Find(id);
            if (cTEPSysCofig == null)
            {
                return HttpNotFound();
            }
            return View(cTEPSysCofig);
        }

        // POST: CTEPSysCofigs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CTEPSysCofig cTEPSysCofig = db.CTEPSysCofig.Find(id);
            db.CTEPSysCofig.Remove(cTEPSysCofig);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
