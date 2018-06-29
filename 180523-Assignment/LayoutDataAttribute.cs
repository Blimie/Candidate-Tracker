using _180523_Assignment.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _180523_Assignment
{
    public class LayoutDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            CandidatesRepository repo = new CandidatesRepository(Properties.Settings.Default.ConStr);
            filterContext.Controller.ViewBag.Confirmed = repo.GetCandidateCountByStatus(Status.Confirmed);
            filterContext.Controller.ViewBag.Pending = repo.GetCandidateCountByStatus(Status.Pending);
            filterContext.Controller.ViewBag.Refused = repo.GetCandidateCountByStatus(Status.Refused);
            base.OnActionExecuting(filterContext);
           
        }
    }
}