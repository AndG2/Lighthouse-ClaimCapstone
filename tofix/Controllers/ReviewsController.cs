using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using tofix.Models;
using tofix.Models.ViewModels;

namespace tofix.Controllers
{
    public class ReviewsController : Controller
    {
        private LighthouseTest1Entities db = new LighthouseTest1Entities();

        // GET: Reviews
        public ActionResult Index(int? id)
        {
            var reviews = db.Reviews.Where(r => r.videoID==id);
            return PartialView(reviews.ToList());
        }

        // GET: Reviews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // GET: Reviews/Create
        public ActionResult Create(int? id)
        {
            var activeUser = User.Identity.GetUserId();
            var model = new CreateReviewViewModel();
            model.userID = activeUser;
            model.videoID = id.Value;

            ViewBag.allEmojis = db.ReactionEmojis.ToList();
            //ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName");
            //ViewBag.videoID = new SelectList(db.Videos, "ID", "youtubeLinkAPI");
            
            return View(model);
        }

        // POST: Reviews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateReviewViewModel reviewVM)
        {
           
            if (ModelState.IsValid)
            {
                var review = new Review
                {
                    BodyText = reviewVM.BodyText,
                    ReactionLink = reviewVM.ReactionLink,
                    userID = reviewVM.userID,
                    videoID =reviewVM.videoID
                };
                var emojiList = new List<ReactionEmoji>();
                foreach(var emojiID in reviewVM.ReactionEmojiIDs)
                {
                    var emoji = db.ReactionEmojis.Find(emojiID);
                    emojiList.Add(emoji);
                }
                review.ReactionEmojis = emojiList;
                
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Details","Videos",new {id=review.videoID});
            }

            ViewBag.allEmojis = db.ReactionEmojis.ToList();
            //ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName", review.userID);
            //ViewBag.videoID = new SelectList(db.Videos, "ID", "youtubeLinkAPI", review.videoID);
            return View(reviewVM);
        }

        // GET: Reviews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName", review.userID);
            ViewBag.videoID = new SelectList(db.Videos, "ID", "youtubeLinkAPI", review.videoID);
            return View(review);
        }

        // POST: Reviews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,BodyText,ReactionLink,userID,videoID")] Review review)
        {
            if (ModelState.IsValid)
            {
                db.Entry(review).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userID = new SelectList(db.UserDatas, "ID", "DisplayName", review.userID);
            ViewBag.videoID = new SelectList(db.Videos, "ID", "youtubeLinkAPI", review.videoID);
            return View(review);
        }

        // GET: Reviews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Reviews.Find(id);
            if (review == null)
            {
                return HttpNotFound();
            }
            return View(review);
        }

        // POST: Reviews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var deleteFromVideo = db.Reviews.Find(id).videoID;
            Review review = db.Reviews.Find(id);
            //var review2 =db.Reviews.Include(r => r.ReviewResponses).FirstOrDefault(r => r.ID==id);
            
            //await db.ReviewResponses.Where(r => r.reviewID == id).ForEachAsync<ReviewResponse>(
            //    r => db.ReviewResponses.Remove(r)
            //);

            db.Reviews.Remove(review);
            db.SaveChanges();
            return RedirectToAction("Details" ,"Videos", new { id = deleteFromVideo }); // action, controller, new(id=value)
            //return View("Index");
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
