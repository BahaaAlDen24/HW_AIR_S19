using HW_AIR_S19.Models.Indexing;
using HW_AIR_S19.Models.TextProcessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HW_AIR_S19.Models.MatchingModels
{
    public class VectorModel 
    {
        public static Dictionary<double, AQUESTION> ArabicSearch(string Query)
        {
            Dictionary<double, AQUESTION> RankedQuestions = new Dictionary<double, AQUESTION>();

            foreach (AQUESTION Question in Index.db.AQUESTIONs)
            {

                // rank the document against the search terms
                var rank = ACosineSimilarity(Query, Question);

                // record the score so we can rank and return it
                RankedQuestions.Add(rank, Question);
            }

            // sort by score and return
            return (Dictionary<double, AQUESTION>)RankedQuestions.OrderByDescending(s => s.Key);
        }

        public static Dictionary<double, EQUESTION> EnglishSearch(string Query)
        {
            Dictionary<double, EQUESTION> RankedQuestions = new Dictionary<double, EQUESTION>();
            
            foreach (EQUESTION Question in Index.db.EQUESTIONs)
            {

                // rank the document against the search terms
                var rank = ECosineSimilarity(Query, Question);

                // record the score so we can rank and return it
                RankedQuestions.Add(rank, Question);
            }

            // sort by score and return
            return (Dictionary<double, EQUESTION>)RankedQuestions.OrderByDescending(s => s.Key);
        }


        public static Double[] CreateQueryFrequencyVector(List<string> superset, IDictionary<string, Int32> ETerms)
        {
            Dictionary<string, Double> keyset = new Dictionary<string, Double>();

            foreach (var key in superset)
                keyset.Add(key, 0);

            foreach (string key in ETerms.Keys)
            {
                keyset[key] = (double)ETerms[key];
            }

            return keyset.Values.ToArray();
        }

        public static Double[] CreateEQuestionFrequencyVector(List<string> superset, EQUESTION Question)
        {
            Dictionary<string, Double> keyset = new Dictionary<string, Double>();

            foreach (var key in superset)
                keyset.Add(key, 0);

            foreach (EQUESTIONTERM QT in Question.EQUESTIONTERMs)
            {
                keyset[QT.ETERM.VALUE] = Convert.ToDouble(QT.WEIGHT);
            }

            return keyset.Values.ToArray();
        }


        public static Double[] CreateAQuestionFrequencyVector(List<string> superset, AQUESTION Question)
        {
            Dictionary<string, Double> keyset = new Dictionary<string, Double>();

            foreach (var key in superset)
                keyset.Add(key, 0);

            foreach (AQUESTIONTERM QT in Question.AQUESTIONTERMs)
            {
                keyset[QT.ATERM.VALUE] = Convert.ToDouble(QT.WEIGHT);
            }

            return keyset.Values.ToArray();
        }


        public static double DotProduct(Double[] vectorOne, Double[] vectorTwo)
        {
            var sum = 0d;
            for (int i = 0; i < vectorOne.Length; i++)
                sum += vectorOne[i] * vectorTwo[i];
            return sum;
        }

        public static Double ECosineSimilarity(string query, EQUESTION Question)
        {
            var QueryTerms = query.EnglishTokenize();

            List<String> superset = new List<string>();

            foreach (var term in QueryTerms)
            {
                superset.Add(term.Key);
            }

            foreach (EQUESTIONTERM QT in Question.EQUESTIONTERMs)
            {
                string temp = QT.ETERM.VALUE;

                if(!superset.Contains(temp))
                    superset.Add(temp);
            }

            // normalize documents into term vectors for comparison
            var vectorOne = CreateQueryFrequencyVector(superset, QueryTerms);
            var vectorTwo = CreateEQuestionFrequencyVector(superset, Question);

            // calculate the dot product of the two vectors ((V1[0] * V2[0]) + (V1[1] * V2[1]) ... + (V1[n] * V2[n])) 
            var dotProduct = DotProduct(vectorOne, vectorTwo);
            // calculate the product of the vector magnatudes (Sqrt(Sum(V1) * Sum(V2)))
            var productOfMagnitudes = ProductOfMagnitudes(vectorOne, vectorTwo);

            // return dot product normalized by the product of magnatudes
            return dotProduct / productOfMagnitudes;
        }

        public static Double ACosineSimilarity(string query, AQUESTION Question)
        {
            var QueryTerms = query.ArabicTokenize();

            List<String> superset = new List<string>();

            foreach (var term in QueryTerms)
            {
                superset.Add(term.Key);
            }

            foreach (AQUESTIONTERM QT in Question.AQUESTIONTERMs)
            {
                string temp = QT.ATERM.VALUE;

                if (!superset.Contains(temp))
                    superset.Add(temp);
            }

            // normalize documents into term vectors for comparison
            var vectorOne = CreateQueryFrequencyVector(superset, QueryTerms);
            var vectorTwo = CreateAQuestionFrequencyVector(superset, Question);

            // calculate the dot product of the two vectors ((V1[0] * V2[0]) + (V1[1] * V2[1]) ... + (V1[n] * V2[n])) 
            var dotProduct = DotProduct(vectorOne, vectorTwo);
            // calculate the product of the vector magnatudes (Sqrt(Sum(V1) * Sum(V2)))
            var productOfMagnitudes = ProductOfMagnitudes(vectorOne, vectorTwo);

            // return dot product normalized by the product of magnatudes
            return dotProduct / productOfMagnitudes;
        }


        public static double ProductOfMagnitudes(Double[] vectorOne, Double[] vectorTwo)
        {
            var sumOne = 0d;
            var sumTwo = 0d;

            for (int i = 0; i < vectorOne.Length; i++)
            {
                sumOne += System.Math.Pow(vectorOne[i], 2);
                sumTwo += System.Math.Pow(vectorTwo[i], 2);
            }

            var product = sumOne * sumTwo;

            return System.Math.Sqrt(product);
        }

    }
}