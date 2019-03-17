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
    public class SchoolBandisTabsController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: SchoolBandisTabs
        public ActionResult Index()
        {
            return View(db.SchoolBandisTabs.ToList());
        }

        // GET: SchoolBandisTabs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolBandisTabs schoolBandisTabs = db.SchoolBandisTabs.Find(id);
            if (schoolBandisTabs == null)
            {
                return HttpNotFound();
            }
            return View(schoolBandisTabs);
        }

        // GET: SchoolBandisTabs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolBandisTabs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,UniversityName,AcademyName,GradeNum,ClassNum")] SchoolBandisTabs schoolBandisTabs)
        {
            if (ModelState.IsValid)
            {
                db.SchoolBandisTabs.Add(schoolBandisTabs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(schoolBandisTabs);
        }

        // GET: SchoolBandisTabs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolBandisTabs schoolBandisTabs = db.SchoolBandisTabs.Find(id);
            if (schoolBandisTabs == null)
            {
                return HttpNotFound();
            }
            return View(schoolBandisTabs);
        }

        // POST: SchoolBandisTabs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,UniversityName,AcademyName,GradeNum,ClassNum")] SchoolBandisTabs schoolBandisTabs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(schoolBandisTabs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(schoolBandisTabs);
        }

        // GET: SchoolBandisTabs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SchoolBandisTabs schoolBandisTabs = db.SchoolBandisTabs.Find(id);
            if (schoolBandisTabs == null)
            {
                return HttpNotFound();
            }
            return View(schoolBandisTabs);
        }

        // POST: SchoolBandisTabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SchoolBandisTabs schoolBandisTabs = db.SchoolBandisTabs.Find(id);
            db.SchoolBandisTabs.Remove(schoolBandisTabs);
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
