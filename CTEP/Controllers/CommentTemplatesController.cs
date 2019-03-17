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
    public class CommentTemplatesController : BaseController
    {
        //private CTEPEntities db = new CTEPEntities();

        // GET: CommentTemplates
        public ActionResult Index()
        {
            return View(db.CommentTemplates.ToList());
        }

        // GET: CommentTemplates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentTemplates commentTemplates = db.CommentTemplates.Find(id);
            if (commentTemplates == null)
            {
                return HttpNotFound();
            }
            return View(commentTemplates);
        }

        // GET: CommentTemplates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CommentTemplates/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreateId,CreteTime,EContent")] CommentTemplates commentTemplates)
        {
            if (ModelState.IsValid)
            {
                db.CommentTemplates.Add(commentTemplates);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(commentTemplates);
        }

        // GET: CommentTemplates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentTemplates commentTemplates = db.CommentTemplates.Find(id);
            if (commentTemplates == null)
            {
                return HttpNotFound();
            }
            return View(commentTemplates);
        }

        // POST: CommentTemplates/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreateId,CreteTime,EContent")] CommentTemplates commentTemplates)
        {
            if (ModelState.IsValid)
            {
                db.Entry(commentTemplates).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(commentTemplates);
        }

        // GET: CommentTemplates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CommentTemplates commentTemplates = db.CommentTemplates.Find(id);
            if (commentTemplates == null)
            {
                return HttpNotFound();
            }
            return View(commentTemplates);
        }

        // POST: CommentTemplates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CommentTemplates commentTemplates = db.CommentTemplates.Find(id);
            db.CommentTemplates.Remove(commentTemplates);
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
