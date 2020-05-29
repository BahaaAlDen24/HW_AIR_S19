using HW_AIR_S19.Models.Indexing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW_AIR_S19.Models.MatchingModels
{
    public class ExtenedBooleanMoldel 
    {
        public static Dictionary<double, AQUESTION> ArabicSearch(string Query)
        {
            Dictionary<double, AQUESTION> RankedQuestions = new Dictionary<double, AQUESTION>();

            var Questions = BooleanModel.ArabicSearch(Query);

            foreach (AQUESTION Question in Questions)
            {

                // rank the document against the search terms
                var rank = VectorModel.ACosineSimilarity(Query, Question);

                while (RankedQuestions.ContainsKey(rank))
                    rank += 0.00001;

                // record the score so we can rank and return it
                RankedQuestions.Add(rank, Question);
            }

            // sort by score and return
            return RankedQuestions;
        }

        public static Dictionary<double, EQUESTION> EnglishSearch(string Query)
        {
            Dictionary<double, EQUESTION> RankedQuestions = new Dictionary<double, EQUESTION>();

            var Questions = BooleanModel.EnglishSearch(Query);

            foreach (EQUESTION Question in Questions)
            {
                // rank the document against the search terms
                var rank = VectorModel.ECosineSimilarity(Query, Question);

                while (RankedQuestions.ContainsKey(rank))
                    rank += 0.00001;

                // record the score so we can rank and return it
                RankedQuestions.Add(rank, Question);
            }

            // sort by score and return
            return RankedQuestions;
        }
    }
}