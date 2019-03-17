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
    public class UserBandisController : BaseController
    {
        //private CTEPdbEntities db = new CTEPdbEntities();

        // GET: UserBandis
        public ActionResult Index()
        {
            return View(db.UserBandis.ToList());
        }

        // GET: UserBandis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBandis userBandis = db.UserBandis.Find(id);
            if (userBandis == null)
            {
                return HttpNotFound();
            }
            return View(userBandis);
        }

        // GET: UserBandis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserBandis/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,EvalutionFormId,type")] UserBandis userBandis)
        {
            if (ModelState.IsValid)
            {
                db.UserBandis.Add(userBandis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(userBandis);
        }

        // GET: UserBandis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBandis userBandis = db.UserBandis.Find(id);
            if (userBandis == null)
            {
                return HttpNotFound();
            }
            return View(userBandis);
        }

        // POST: UserBandis/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,EvalutionFormId,type")] UserBandis userBandis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userBandis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(userBandis);
        }

        // GET: UserBandis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserBandis userBandis = db.UserBandis.Find(id);
            if (userBandis == null)
            {
                return HttpNotFound();
            }
            return View(userBandis);
        }

        // POST: UserBandis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserBandis userBandis = db.UserBandis.Find(id);
            db.UserBandis.Remove(userBandis);
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
