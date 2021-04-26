using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace tofix.Models
{
    public class ReviewResponsesController : Controller
    {
        private LighthouseTest1Entities db = new LighthouseTest1Entities();

        // GET: ReviewResponses
        public ActionResult Index()
        {
            var reviewResponses = db.ReviewResponses.Include(r => r.Review).Include(r => r.UserData);
            return View(reviewResponses.ToList());
        }

        // GET: ReviewResponses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewResponse reviewResponse = db.ReviewResponses.Find(id);
            if (reviewResponse == null)
            {
                return HttpNotFound();
            }
            return View(reviewResponse);
        }

        // GET: ReviewResponses/Create
        public ActionResult Create()
        {
            ViewBag.reviewID = new SelectList(db.Reviews, "ID", "BodyText");
            ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName");
            return View();
        }

        // POST: ReviewResponses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,reviewID,userID,Response,BodyText")] ReviewResponse reviewResponse)
        {
            if (ModelState.IsValid)
            {
                db.ReviewResponses.Add(reviewResponse);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.reviewID = new SelectList(db.Reviews, "ID", "BodyText", reviewResponse.reviewID);
            ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName", reviewResponse.userID);
            return View(reviewResponse);
        }

        // GET: ReviewResponses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewResponse reviewResponse = db.ReviewResponses.Find(id);
            if (reviewResponse == null)
            {
                return HttpNotFound();
            }
            ViewBag.reviewID = new SelectList(db.Reviews, "ID", "BodyText", reviewResponse.reviewID);
            ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName", reviewResponse.userID);
            return View(reviewResponse);
        }

        // POST: ReviewResponses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,reviewID,userID,Response,BodyText")] ReviewResponse reviewResponse)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reviewResponse).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.reviewID = new SelectList(db.Reviews, "ID", "BodyText", reviewResponse.reviewID);
            ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName", reviewResponse.userID);
            return View(reviewResponse);
        }

        // GET: ReviewResponses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ReviewResponse reviewResponse = db.ReviewResponses.Find(id);
            if (reviewResponse == null)
            {
                return HttpNotFound();
            }
            return View(reviewResponse);
        }

        // POST: ReviewResponses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ReviewResponse reviewResponse = db.ReviewResponses.Find(id);
            db.ReviewResponses.Remove(reviewResponse);
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
