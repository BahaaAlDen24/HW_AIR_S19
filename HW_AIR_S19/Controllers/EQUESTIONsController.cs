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
    public class EQUESTIONsController : Controller
    {
        private AIR_S19Entities1 db = new AIR_S19Entities1();

        // GET: EQUESTIONs
        public ActionResult Index()
        {
            return View(db.EQUESTIONs.ToList());
        }

        // GET: EQUESTIONs/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUESTION eQUESTION = db.EQUESTIONs.Find(id);
            if (eQUESTION == null)
            {
                return HttpNotFound();
            }
            return View(eQUESTION);
        }

        // GET: EQUESTIONs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EQUESTIONs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,VALUE,ANSWER,Indexed")] EQUESTION eQUESTION)
        {
            if (ModelState.IsValid)
            {
                eQUESTION.ID = Guid.NewGuid();
                db.EQUESTIONs.Add(eQUESTION);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(eQUESTION);
        }

        // GET: EQUESTIONs/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUESTION eQUESTION = db.EQUESTIONs.Find(id);
            if (eQUESTION == null)
            {
                return HttpNotFound();
            }
            return View(eQUESTION);
        }

        // POST: EQUESTIONs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,VALUE,ANSWER,Indexed")] EQUESTION eQUESTION)
        {
            if (ModelState.IsValid)
            {
                eQUESTION.Indexed = 0;
                db.Entry(eQUESTION).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(eQUESTION);
        }

        // GET: EQUESTIONs/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EQUESTION eQUESTION = db.EQUESTIONs.Find(id);
            if (eQUESTION == null)
            {
                return HttpNotFound();
            }
            return View(eQUESTION);
        }

        // POST: EQUESTIONs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            EQUESTION eQUESTION = db.EQUESTIONs.Find(id);
            db.EQUESTIONs.Remove(eQUESTION);
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
