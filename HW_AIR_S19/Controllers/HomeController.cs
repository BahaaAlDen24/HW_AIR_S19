using HW_AIR_S19.Models.MatchingModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HW_AIR_S19.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Models.Indexing.Index.BuildEnglishIndex();
            return View();
        }

        public ActionResult EBuildIndex(string Query)
        {
            Models.Indexing.Index.BuildEnglishIndex();
            return View();
        }

        public ActionResult ABuildIndex(string Query)
        {
            Models.Indexing.Index.BuildArabicIndex();
            return View();
        }

        public ActionResult EBooleanModelSearch(string Query)
        {
            var Result = BooleanModel.EnglishSearch(Query);
            return View();
        }

        public ActionResult ABooleanModelSearch(string Query)
        {
            var Result = BooleanModel.ArabicSearch(Query);
            return View();
        }

        public ActionResult EExtendBooleanModelSearch(string Query)
        {
            var Result = ExtenedBooleanMoldel.EnglishSearch(Query);
            return View();
        }

        public ActionResult AExtendBooleanModelSearch(string Query)
        {
            var Result = ExtenedBooleanMoldel.ArabicSearch(Query);
            return View();
        }

        public ActionResult EVectorSpaceModelSearch(string Query)
        {
            var Result = VectorModel.EnglishSearch(Query);
            return View();
        }

        public ActionResult AVectorSpaceModelSearch(string Query)
        {
            var Result = VectorModel.EnglishSearch(Query);
            return View();
        }

    }
}