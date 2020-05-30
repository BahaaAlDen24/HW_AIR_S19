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
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult EBuildIndex(string Query)
        {
            Models.Indexing.Index.BuildEnglishIndex();

            var QuestionCount  = Models.Indexing.Index.db.EQUESTIONs.Count();
            var TermsCount = Models.Indexing.Index.db.ETERMs.Count();

            var JsonRes = new
            {
                QuestionCount = QuestionCount,
                TermsCount = TermsCount
            };
            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ABuildIndex(string Query)
        {
            Models.Indexing.Index.BuildArabicIndex();

            var QuestionCount = Models.Indexing.Index.db.AQUESTIONs.Count();
            var TermsCount = Models.Indexing.Index.db.ATERMs.Count();

            var JsonRes = new
            {
                QuestionCount = QuestionCount ,
                TermsCount = TermsCount
            };

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EBooleanModelSearch(string Query)
        {
            List<EQUESTION> Result = BooleanModel.EnglishSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.VALUE,
                Answer = Q.ANSWER,
                Rank = 1
            });

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ABooleanModelSearch(string Query)
        {
            List<AQUESTION> Result = BooleanModel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.VALUE,
                Answer = Q.ANSWER,
                Rank = 1
            });

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult EExtendBooleanModelSearch(string Query)
        {
            Dictionary<double, EQUESTION> Result = ExtenedBooleanMoldel.EnglishSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Math.Round(Q.Key,3)
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AExtendBooleanModelSearch(string Query)
        {
            Dictionary<double, AQUESTION> Result = ExtenedBooleanMoldel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Math.Round(Q.Key, 3)
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult EVectorSpaceModelSearch(string Query)
        {
            try { 
            Dictionary<double, EQUESTION> Result = VectorModel.EnglishSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Math.Round(Q.Key, 3)
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(e.Message, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public JsonResult AVectorSpaceModelSearch(string Query)
        {
            Dictionary<double, AQUESTION> Result = VectorModel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Math.Round(Q.Key, 3)
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult ETokenize(string Query)
        {
            IDictionary<string, int> Result = Models.TextProcessing.Helper.EnglishTokenize(Query);

            var JsonRes = Result.Select(Q => new {
                Term = Q.Key
            });

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult ATokenize(string Query)
        {
            IDictionary<string, int> Result = Models.TextProcessing.Helper.EnglishTokenize(Query);

            var JsonRes = Result.Select(Q => new {
                Term = Q.Key
            });

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
        }

    }
}