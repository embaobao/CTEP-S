using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CTEP.Controllers
{
    public class EvaluController : Controller
    {
        // GET: Evalu
        public ActionResult Index()
        {
            return View();
        }

        // GET: Evalu/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Evalu/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Evalu/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evalu/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Evalu/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Evalu/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Evalu/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}
