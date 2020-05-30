using HW_AIR_S19.Models;
using HW_AIR_S19.Models.MatchingModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            try { 
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
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult ABuildIndex(string Query)
        {
            try { 
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
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult EBooleanModelSearch(string Query)
        {
            try { 
                List<EQUESTION> Result = BooleanModel.EnglishSearch(Query);

                var JsonRes = Result.Select(Q => new {
                    Question = Q.VALUE,
                    Answer = Q.ANSWER,
                    Rank = 1
                });

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ABooleanModelSearch(string Query)
        {
            try { 
                List<AQUESTION> Result = BooleanModel.ArabicSearch(Query);

                var JsonRes = Result.Select(Q => new {
                    Question = Q.VALUE,
                    Answer = Q.ANSWER,
                    Rank = 1
                });

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult EExtendBooleanModelSearch(string Query)
        {
            try { 
                Dictionary<double, EQUESTION> Result = ExtenedBooleanMoldel.EnglishSearch(Query);

                var JsonRes = Result.Select(Q => new {
                    Question = Q.Value.VALUE,
                    Answer = Q.Value.ANSWER,
                    Rank = Math.Round(Q.Key,3)
                }).OrderByDescending(R => R.Rank);

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AExtendBooleanModelSearch(string Query)
        {
            try { 
            Dictionary<double, AQUESTION> Result = ExtenedBooleanMoldel.ArabicSearch(Query);

            var JsonRes = Result.Select(Q => new {
                Question = Q.Value.VALUE,
                Answer = Q.Value.ANSWER,
                Rank = Math.Round(Q.Key, 3)
            }).OrderByDescending(R => R.Rank);

            return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }

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

                var line = GetLineNumber(e);

                var JsonRes = new {
                    Message = e.Message ,
                    innerExeption  = e.InnerException.Message ,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString() ,
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);

            }
        }


        [HttpPost]
        public JsonResult AVectorSpaceModelSearch(string Query)
        {
            try { 
                Dictionary<double, AQUESTION> Result = VectorModel.ArabicSearch(Query);

                var JsonRes = Result.Select(Q => new {
                    Question = Q.Value.VALUE,
                    Answer = Q.Value.ANSWER,
                    Rank = Math.Round(Q.Key, 3)
                }).OrderByDescending(R => R.Rank);

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult ETokenize(string Query)
        {
            try
            {
                IDictionary<string, int> Result = Models.TextProcessing.Helper.EnglishTokenize(Query);

                var JsonRes = Result.Select(Q => new {
                    Term = Q.Key
                });

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult ATokenize(string Query)
        {
            try { 
                IDictionary<string, int> Result = Models.TextProcessing.Helper.EnglishTokenize(Query);

                var JsonRes = Result.Select(Q => new {
                    Term = Q.Key
                });

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {

                var line = GetLineNumber(e);

                var JsonRes = new
                {
                    Message = e.Message,
                    innerExeption = e.InnerException.Message,
                    factor3 = e.Data.ToString(),
                    factor4 = e.InnerException.ToString(),
                    line = line
                };

                return Json(JsonRes, JsonRequestBehavior.AllowGet);
            }
        }

        public int GetLineNumber(Exception ex)
        {
            var lineNumber = 0;
            const string lineSearch = ":line ";
            var index = ex.StackTrace.LastIndexOf(lineSearch);
            if (index != -1)
            {
                var lineNumberText = ex.StackTrace.Substring(index + lineSearch.Length);
                if (int.TryParse(lineNumberText, out lineNumber))
                {
                }
            }
            return lineNumber;
        }


    }
}