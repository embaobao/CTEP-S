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
    public class BandTabsController : BaseController
    {
        // GET: BandTabs
        public ActionResult Index()
        {
            return View(db.BandTabs.ToList());
        }

        // GET: BandTabs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandTabs bandTabs = db.BandTabs.Find(id);
            if (bandTabs == null)
            {
                return HttpNotFound();
            }
            return View(bandTabs);
        }

        // GET: BandTabs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BandTabs/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,BandiId,type,UserId")] BandTabs bandTabs)
        {
            if (ModelState.IsValid)
            {
                db.BandTabs.Add(bandTabs);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bandTabs);
        }

        // GET: BandTabs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandTabs bandTabs = db.BandTabs.Find(id);
            if (bandTabs == null)
            {
                return HttpNotFound();
            }
            return View(bandTabs);
        }

        // POST: BandTabs/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,BandiId,type,UserId")] BandTabs bandTabs)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bandTabs).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bandTabs);
        }

        // GET: BandTabs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BandTabs bandTabs = db.BandTabs.Find(id);
            if (bandTabs == null)
            {
                return HttpNotFound();
            }
            return View(bandTabs);
        }

        // POST: BandTabs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BandTabs bandTabs = db.BandTabs.Find(id);
            db.BandTabs.Remove(bandTabs);
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
