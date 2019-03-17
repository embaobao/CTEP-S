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
    public class CourseTemplatesController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: CourseTemplates
        public ActionResult Index()
        {
            return View(db.CourseTemplates.ToList());
        }

        // GET: CourseTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTemplates courseTemplates = db.CourseTemplates.Find(id);
            if (courseTemplates == null)
            {
                return HttpNotFound();
            }
            return View(courseTemplates);
        }

        // GET: CourseTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CourseTemplates/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreaterId,CourseStatus,title,body,img,CourseScore,bandinID")] CourseTemplates courseTemplates)
        {
            if (ModelState.IsValid)
            {
                db.CourseTemplates.Add(courseTemplates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(courseTemplates);
        }

        // GET: CourseTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTemplates courseTemplates = db.CourseTemplates.Find(id);
            if (courseTemplates == null)
            {
                return HttpNotFound();
            }
            return View(courseTemplates);
        }

        // POST: CourseTemplates/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreaterId,CourseStatus,title,body,img,CourseScore,bandinID")] CourseTemplates courseTemplates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(courseTemplates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(courseTemplates);
        }

        // GET: CourseTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CourseTemplates courseTemplates = db.CourseTemplates.Find(id);
            if (courseTemplates == null)
            {
                return HttpNotFound();
            }
            return View(courseTemplates);
        }

        // POST: CourseTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CourseTemplates courseTemplates = db.CourseTemplates.Find(id);
            db.CourseTemplates.Remove(courseTemplates);
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
