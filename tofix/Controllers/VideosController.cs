﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Threading.Tasks;
using System.Web.Mvc;
using tofix.YoutubeDataPuller;

namespace tofix.Models
{
    public class VideosController : Controller
    {
        private LighthouseTest1Entities db = new LighthouseTest1Entities();
        private static Random randomFinder = new Random();

        // GET: Videos
        public ActionResult Index(int? id)
        {
            var videos = db.Videos.Include(v => v.Category).Where(v => v.videoCatagory==id);
            return PartialView(videos.ToList());
        }

        // GET: Videos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                id = db.Videos.OrderByDescending(v => Guid.NewGuid()).First().ID;

            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // GET: Videos/Create
        public ActionResult Create()
        {
            ViewBag.videoCatagory = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Videos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ReviewScoreTotal,Description,videoCatagory,videoLink")] Video video)
        {
            var videoPartsToBreak = await YTDataPuller.GetVideoInfo(video.videoLink);

            video.youtubeLinkAPI = YTDataPuller.ParseFromUrl(video.videoLink);
            video.videoName = videoPartsToBreak.Title;
            video.videoSelfDescription = videoPartsToBreak.Description;
            var image = videoPartsToBreak.Thumbnails.Maxres ?? videoPartsToBreak.Thumbnails.Standard ??
                videoPartsToBreak.Thumbnails.High ?? videoPartsToBreak.Thumbnails.Medium ?? videoPartsToBreak.Thumbnails.Default__;
            video.videoImageUrl = image.Url;

            video.ReviewScoreTotal = 0;
        
            if (ModelState.IsValid)
            {
                db.Videos.Add(video);
                db.SaveChanges();
                return RedirectToAction("Details","Videos",new { id=video.ID});
            }

            ViewBag.videoCatagory = new SelectList(db.Categories, "ID", "Name", video.videoCatagory);
            return View(video);
        }

        // GET: Videos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            ViewBag.videoCatagory = new SelectList(db.Categories, "ID", "Name", video.videoCatagory);
            return View(video);
        }

        // POST: Videos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,youtubeLinkAPI,ReviewScoreTotal,ReviewScoreVotes,Description,videoCatagory,videoLink,videoName,videoImage,videoSelfDescription,videoLength")] Video video)
        {
            if (ModelState.IsValid)
            {
                db.Entry(video).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.videoCatagory = new SelectList(db.Categories, "ID", "Name", video.videoCatagory);
            return View(video);
        }

        // GET: Videos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Video video = db.Videos.Find(id);
            if (video == null)
            {
                return HttpNotFound();
            }
            return View(video);
        }

        // POST: Videos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Video video = db.Videos.Find(id);
            
            
            db.Videos.Remove(video);
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
