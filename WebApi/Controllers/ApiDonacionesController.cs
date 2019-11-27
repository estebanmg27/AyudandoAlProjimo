using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApi.Controllers
{
    public class ApiDonacionesController : Controller
    {
        // GET: ApiDonaciones
        public ActionResult Index()
        {
            return View();
        }

        // GET: ApiDonaciones/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ApiDonaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ApiDonaciones/Create
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

        // GET: ApiDonaciones/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ApiDonaciones/Edit/5
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

        // GET: ApiDonaciones/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ApiDonaciones/Delete/5
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
