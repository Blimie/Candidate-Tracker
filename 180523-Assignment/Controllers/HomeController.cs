using _180523_Assignment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _180523_Assignment.Controllers
{
    public class HomeController : Controller
    {
        CandidatesRepository repo = new CandidatesRepository(Properties.Settings.Default.ConStr);

        public ActionResult Index()
        {
            if (TempData["Message"] != null)
            {
                ViewBag.message = (string)TempData["Message"];
            }
            return View();
        }
        public ActionResult AddCandidate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCandidate(Candidate candidate)
        {
            candidate.Status = Status.Pending;
            repo.AddCandidate(candidate);
            TempData["Message"] = "Candidate successfully added!";
            return Redirect("/");
        }
        public ActionResult Pending()
        {
            var candidates = repo.GetCandidatesByStatus(Status.Pending);
            return View(candidates);
        }
        public ActionResult Confirmed()
        {
            var candidates = repo.GetCandidatesByStatus(Status.Confirmed);
            return View(candidates);
        }
        public ActionResult Refused()
        {
            var candidates = repo.GetCandidatesByStatus(Status.Refused);
            return View(candidates);
        }
        public ActionResult ViewDetails(int id)
        {
            var candidate = repo.GetById(id);
            return View(candidate);
        }
        [HttpPost]
        public void Confirm(int id)
        {
            repo.SetStatus(id, Status.Confirmed);
        }
        [HttpPost]
        public void Refuse(int id)
        {      
            repo.SetStatus(id, Status.Refused);
        }
        public ActionResult GetCounts()
        {
            var refusedCount = repo.GetCandidateCountByStatus(Status.Refused);
            var confirmedCount = repo.GetCandidateCountByStatus(Status.Confirmed);
            var pendingCount = repo.GetCandidateCountByStatus(Status.Pending);
            return Json(new { Refused = refusedCount, Confirmed = confirmedCount, Pending = pendingCount }, JsonRequestBehavior.AllowGet);
        }
    }
}