using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HW_AIR_S19.Models;

namespace HW_AIR_S19.Controllers
{
    public class AQUESTIONsController : Controller
    {
        private AIR_S19Entities1 db = new AIR_S19Entities1();

        // GET: AQUESTIONs
        public ActionResult Index()
        {
            return View(db.AQUESTIONs.ToList());
        }

        // GET: AQUESTIONs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AQUESTION aQUESTION = db.AQUESTIONs.Find(id);
            if (aQUESTION == null)
            {
                return HttpNotFound();
            }
            return View(aQUESTION);
        }

        // GET: AQUESTIONs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AQUESTIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VALUE,ANSWER,Indexed")] AQUESTION aQUESTION)
        {
            if (ModelState.IsValid)
            {
                aQUESTION.ID = Guid.NewGuid();
                aQUESTION.Indexed = 0;
                db.AQUESTIONs.Add(aQUESTION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aQUESTION);
        }

        // GET: AQUESTIONs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AQUESTION aQUESTION = db.AQUESTIONs.Find(id);
            if (aQUESTION == null)
            {
                return HttpNotFound();
            }
            return View(aQUESTION);
        }

        // POST: AQUESTIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VALUE,ANSWER,Indexed")] AQUESTION aQUESTION)
        {
            if (ModelState.IsValid)
            {
                aQUESTION.Indexed = 0;
                db.Entry(aQUESTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aQUESTION);
        }

        // GET: AQUESTIONs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AQUESTION aQUESTION = db.AQUESTIONs.Find(id);
            if (aQUESTION == null)
            {
                return HttpNotFound();
            }
            return View(aQUESTION);
        }

        // POST: AQUESTIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            AQUESTION aQUESTION = db.AQUESTIONs.Find(id);
            db.AQUESTIONs.Remove(aQUESTION);
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
