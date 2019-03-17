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
    public class UserSetTabsController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: UserSetTabs
        public ActionResult Index()
        {
            return View(db.UserSetTabs.ToList());
        }

        // GET: UserSetTabs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSetTabs userSetTabs = db.UserSetTabs.Find(id);
            if (userSetTabs == null)
            {
                return HttpNotFound();
            }
            return View(userSetTabs);
        }

        // GET: UserSetTabs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserSetTabs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userSet")] UserSetTabs userSetTabs)
        {
            if (ModelState.IsValid)
            {
                db.UserSetTabs.Add(userSetTabs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userSetTabs);
        }

        // GET: UserSetTabs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSetTabs userSetTabs = db.UserSetTabs.Find(id);
            if (userSetTabs == null)
            {
                return HttpNotFound();
            }
            return View(userSetTabs);
        }

        // POST: UserSetTabs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userSet")] UserSetTabs userSetTabs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userSetTabs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userSetTabs);
        }

        // GET: UserSetTabs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserSetTabs userSetTabs = db.UserSetTabs.Find(id);
            if (userSetTabs == null)
            {
                return HttpNotFound();
            }
            return View(userSetTabs);
        }

        // POST: UserSetTabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserSetTabs userSetTabs = db.UserSetTabs.Find(id);
            db.UserSetTabs.Remove(userSetTabs);
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
