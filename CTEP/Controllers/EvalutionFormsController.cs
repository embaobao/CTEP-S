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
    public class EvalutionFormsController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: EvalutionForms
        public ActionResult Index()
        {
            return View(db.EvalutionForms.ToList());
        }

        // GET: EvalutionForms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionForms evalutionForms = db.EvalutionForms.Find(id);
            if (evalutionForms == null)
            {
                return HttpNotFound();
            }
            return View(evalutionForms);
        }

        // GET: EvalutionForms/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EvalutionForms/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreateId,CourseId,author,score,FormStatus,title,body,img,CreateTime,EStartTime,EEndTime,Eweek,BandiId")] EvalutionForms evalutionForms)
        {
            if (ModelState.IsValid)
            {
                db.EvalutionForms.Add(evalutionForms);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(evalutionForms);
        }

        // GET: EvalutionForms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionForms evalutionForms = db.EvalutionForms.Find(id);
            if (evalutionForms == null)
            {
                return HttpNotFound();
            }
            return View(evalutionForms);
        }

        // POST: EvalutionForms/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreateId,CourseId,author,score,FormStatus,title,body,img,CreateTime,EStartTime,EEndTime,Eweek,BandiId")] EvalutionForms evalutionForms)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evalutionForms).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(evalutionForms);
        }

        // GET: EvalutionForms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EvalutionForms evalutionForms = db.EvalutionForms.Find(id);
            if (evalutionForms == null)
            {
                return HttpNotFound();
            }
            return View(evalutionForms);
        }

        // POST: EvalutionForms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EvalutionForms evalutionForms = db.EvalutionForms.Find(id);
            db.EvalutionForms.Remove(evalutionForms);
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
