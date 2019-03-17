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
    public class GradsController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: Grads
        public ActionResult Index()
        {
            return View(db.Grads.ToList());
        }

        // GET: Grads/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grads grads = db.Grads.Find(id);
            if (grads == null)
            {
                return HttpNotFound();
            }
            return View(grads);
        }

        // GET: Grads/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Grads/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,name,upid")] Grads grads)
        {
            if (ModelState.IsValid)
            {
                db.Grads.Add(grads);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(grads);
        }

        // GET: Grads/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grads grads = db.Grads.Find(id);
            if (grads == null)
            {
                return HttpNotFound();
            }
            return View(grads);
        }

        // POST: Grads/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,name,upid")] Grads grads)
        {
            if (ModelState.IsValid)
            {
                db.Entry(grads).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(grads);
        }

        // GET: Grads/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Grads grads = db.Grads.Find(id);
            if (grads == null)
            {
                return HttpNotFound();
            }
            return View(grads);
        }

        // POST: Grads/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Grads grads = db.Grads.Find(id);
            db.Grads.Remove(grads);
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
