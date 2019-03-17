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
    public class ClassNumsController : Controller
    {
        private CTEBdbEntities db = new CTEBdbEntities();

        // GET: ClassNums
        public ActionResult Index()
        {
            return View(db.ClassNum.ToList());
        }

        // GET: ClassNums/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassNum classNum = db.ClassNum.Find(id);
            if (classNum == null)
            {
                return HttpNotFound();
            }
            return View(classNum);
        }

        // GET: ClassNums/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClassNums/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,name,upid")] ClassNum classNum)
        {
            if (ModelState.IsValid)
            {
                db.ClassNum.Add(classNum);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(classNum);
        }

        // GET: ClassNums/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassNum classNum = db.ClassNum.Find(id);
            if (classNum == null)
            {
                return HttpNotFound();
            }
            return View(classNum);
        }

        // POST: ClassNums/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,name,upid")] ClassNum classNum)
        {
            if (ModelState.IsValid)
            {
                db.Entry(classNum).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(classNum);
        }

        // GET: ClassNums/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ClassNum classNum = db.ClassNum.Find(id);
            if (classNum == null)
            {
                return HttpNotFound();
            }
            return View(classNum);
        }

        // POST: ClassNums/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ClassNum classNum = db.ClassNum.Find(id);
            db.ClassNum.Remove(classNum);
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
