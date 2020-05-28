using HW_AIR_S19.Models.Indexing;
using HW_AIR_S19.Models.TextProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW_AIR_S19.Models.MatchingModels
{
    public class BooleanModel 
    {
        public static List<AQUESTION> ArabicSearch(string Query)
        {
            string AndTerms = "";
            string OrTerms = "";
            string NotTerms = "";

            List<AQUESTION> MatchedQuestions = new List<AQUESTION>();

            BooleanQuerySpliter(Query, ref AndTerms, ref OrTerms, ref NotTerms);

            if (String.IsNullOrEmpty(AndTerms))
            {
                IDictionary<string, Int32> AndStemmedTerms = AndTerms.ArabicTokenize();
                MatchedQuestions = GetATermsQuestions(AndStemmedTerms);
            }

            if (String.IsNullOrEmpty(OrTerms))
            {
                IDictionary<string, Int32> OrStemmedTerms = OrTerms.ArabicTokenize();
                MatchedQuestions.AddRange(GetATermsQuestions(OrStemmedTerms));
            }

            if (String.IsNullOrEmpty(NotTerms))
            {
                IDictionary<string, Int32> NotStemmedTerms = NotTerms.ArabicTokenize();

                List<AQUESTION> ToRemoveQuestions = GetATermsQuestions(NotStemmedTerms);

                foreach (AQUESTION AQ in ToRemoveQuestions)
                {
                    MatchedQuestions.Remove(AQ);
                }
            }

            return MatchedQuestions;
        }

        public static List<EQUESTION> EnglishSearch(string Query)
        {
            string AndTerms = "";
            string OrTerms = "";
            string NotTerms = "";

            List<EQUESTION> MatchedQuestions = new List<EQUESTION>();

            BooleanQuerySpliter(Query, ref AndTerms,ref  OrTerms,ref NotTerms);

            if (String.IsNullOrEmpty(AndTerms)) { 
                IDictionary<string, Int32> AndStemmedTerms = AndTerms.EnglishTokenize();
                MatchedQuestions = GetETermsQuestions(AndStemmedTerms);
            }

            if (String.IsNullOrEmpty(OrTerms))
            {
                IDictionary<string, Int32> OrStemmedTerms = OrTerms.EnglishTokenize();
                MatchedQuestions.AddRange(GetETermsQuestions(OrStemmedTerms));
            }

            if (String.IsNullOrEmpty(NotTerms))
            {
                IDictionary<string, Int32> NotStemmedTerms = NotTerms.EnglishTokenize();

                List<EQUESTION>  ToRemoveQuestions = GetETermsQuestions(NotStemmedTerms);

                foreach(EQUESTION EQ in ToRemoveQuestions)
                {
                    MatchedQuestions.Remove(EQ);
                }
            }

            return MatchedQuestions; 
        }

        public static void BooleanQuerySpliter(string Query,ref string AndTerms, ref string OrTerms, ref string NotTerms)
        {
            Query = Query.ToLower();

            var Spliters = Query.Split(new string[] { " or " }, StringSplitOptions.None);

            if (Spliters.Length == 1)
            {
                AndTerms = Spliters[0];
            }
            else if (Spliters.Length > 1)
            {
                AndTerms = Spliters[0];
                OrTerms = Spliters[1];

                var Spliters2 = OrTerms.Split(new string[] { " not " }, StringSplitOptions.None);

                if (Spliters2.Length == 1)
                {
                    OrTerms = Spliters[0];
                }
                else if (Spliters2.Length > 1)
                {
                    OrTerms = Spliters2[0];
                    NotTerms = Spliters2[1];
                }
            }
        }


        public static List<EQUESTION> GetETermsQuestions(IDictionary<string, Int32> ETerms)
        {
            List<EQUESTION> Questions = new List<EQUESTION>();

            List<ETERM> Terms = new List<ETERM>();

            foreach (string key in ETerms.Keys)
            {
                ETERM Term = Index.db.ETERMs.Where(T => T.VALUE == key).FirstOrDefault<ETERM>();

                if (Term != null && (Questions.Count == 0))
                {
                    Questions = (List<EQUESTION>)Term.EQUESTIONTERMs;

                }else if (Term != null && ( Questions.Count > 0 ))
                {
                    List<EQUESTION>  NewQuestions = (List<EQUESTION>)Term.EQUESTIONTERMs;

                    Questions = (from NewQuestion in NewQuestions
                                 join OldQuestion in Questions
                                 on NewQuestion.ID equals OldQuestion.ID
                                 select NewQuestion).ToList();
                }
            }

            return Questions;
        }


        public static List<AQUESTION> GetATermsQuestions(IDictionary<string, Int32> ATerms)
        {
            List<AQUESTION> Questions = new List<AQUESTION>();

            List<ATERM> Terms = new List<ATERM>();

            foreach (string key in ATerms.Keys)
            {
                ATERM Term = Index.db.ATERMs.Where(T => T.VALUE == key).FirstOrDefault<ATERM>();

                if (Term != null && (Questions.Count == 0))
                {
                    Questions = (List<AQUESTION>)Term.AQUESTIONTERMs;

                }
                else if (Term != null && (Questions.Count > 0))
                {
                    List<AQUESTION> NewQuestions = (List<AQUESTION>)Term.AQUESTIONTERMs;

                    Questions = (from NewQuestion in NewQuestions
                                 join OldQuestion in Questions
                                 on NewQuestion.ID equals OldQuestion.ID
                                 select NewQuestion).ToList();
                }
            }

            return Questions;
        }
    }
}