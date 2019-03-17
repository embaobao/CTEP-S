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
    public class AccountInfoesController : BaseController
    {
        

        // GET: AccountInfoes
        public ActionResult Index()
        {
            return View(db.AccountInfo.ToList());
        }

        // GET: AccountInfoes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfo.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // GET: AccountInfoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountInfoes/Create
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,CreateTime,address,UsedTime,LastTime")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.AccountInfo.Add(accountInfo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(accountInfo);
        }

        // GET: AccountInfoes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfo.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // POST: AccountInfoes/Edit/5
        // 为了防止“过多发布”攻击，请启用要绑定到的特定属性，有关 
        // 详细信息，请参阅 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,CreateTime,address,UsedTime,LastTime")] AccountInfo accountInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(accountInfo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(accountInfo);
        }

        // GET: AccountInfoes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AccountInfo accountInfo = db.AccountInfo.Find(id);
            if (accountInfo == null)
            {
                return HttpNotFound();
            }
            return View(accountInfo);
        }

        // POST: AccountInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AccountInfo accountInfo = db.AccountInfo.Find(id);
            db.AccountInfo.Remove(accountInfo);
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
