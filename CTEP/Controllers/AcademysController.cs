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
    public class AcademysController : BaseController
    {
       

        // GET: Academys
        public ActionResult Index()
        {
            return View(db.Academys.ToList());
        }

        // GET: Academys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academys academys = db.Academys.Find(id);
            if (academys == null)
            {
                return HttpNotFound();
            }
            return View(academys);
        }

        // GET: Academys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Academys/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,upid")] Academys academys)
        {
            if (ModelState.IsValid)
            {
                db.Academys.Add(academys);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(academys);
        }

        // GET: Academys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academys academys = db.Academys.Find(id);
            if (academys == null)
            {
                return HttpNotFound();
            }
            return View(academys);
        }

        // POST: Academys/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,upid")] Academys academys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(academys).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(academys);
        }

        // GET: Academys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Academys academys = db.Academys.Find(id);
            if (academys == null)
            {
                return HttpNotFound();
            }
            return View(academys);
        }

        // POST: Academys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Academys academys = db.Academys.Find(id);
            db.Academys.Remove(academys);
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
