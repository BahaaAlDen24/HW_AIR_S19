using HW_AIR_S19.Models;
using HW_AIR_S19.Models.MatchingModels;
using Newtonsoft.Json;
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

        public JsonResult EBooleanModelSearch(string Query)
        {
            List<EQUESTION> Result = BooleanModel.EnglishSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.VALUE,
                Answer = Q.ANSWER
            });

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ABooleanModelSearch(string Query)
        {
            List<AQUESTION> Result = BooleanModel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.VALUE,
                Answer = Q.ANSWER
            });

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EExtendBooleanModelSearch(string Query)
        {
            Dictionary<double, EQUESTION> Result = ExtenedBooleanMoldel.EnglishSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Q.Key
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AExtendBooleanModelSearch(string Query)
        {
            Dictionary<double, AQUESTION> Result = ExtenedBooleanMoldel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Q.Key
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EVectorSpaceModelSearch(string Query)
        {
            Dictionary<double, EQUESTION> Result = VectorModel.EnglishSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Q.Key
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AVectorSpaceModelSearch(string Query)
        {
            Dictionary<double, AQUESTION> Result = VectorModel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Q.Key
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }
    }
}