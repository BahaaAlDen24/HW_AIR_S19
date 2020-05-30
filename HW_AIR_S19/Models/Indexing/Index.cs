using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HW_AIR_S19.Models;
using HW_AIR_S19.Models.TextProcessing;
using System.Collections;
using System.Data.Entity;

namespace HW_AIR_S19.Models.Indexing
{
    public class Index
    {
        public static AIR_S19Entities1 db = new AIR_S19Entities1();

        public static void BuildEnglishIndex()
        {
            List<EQUESTION> Questions = db.EQUESTIONs.Where(Q => Q.Indexed == 0).ToList<EQUESTION>() ;
            int NumberOfQuestions = db.EQUESTIONs.ToList().Count;

            foreach (EQUESTION Question in Questions)
            {
                var QuestionText = Question.VALUE + " " + Question.ANSWER;

                IDictionary<string, Int32> QeustionTerms = QuestionText.EnglishTokenize();

                foreach (string key in QeustionTerms.Keys)
                {
                    // TF : Term Frequency
                    var Frequency = QeustionTerms[key];

                    ETERM Term = db.ETERMs.Where( T => T.VALUE == key).FirstOrDefault<ETERM>();

                    if (Term != null)
                    {
                        Term.IDF = (Convert.ToInt32(Term.IDF) + 1).ToString();
                        db.Entry(Term).State = EntityState.Modified;
                    }
                    else
                    {
                        Term = new ETERM();

                        Term.ID = Guid.NewGuid();
                        Term.VALUE = key;
                        Term.IDF = Frequency.ToString();
                        db.ETERMs.Add(Term);
                    }

                    EQUESTIONTERM QuestionTerm = db.EQUESTIONTERMs.Where(QT => QT.TERMID == Term.ID && QT.QUESTIONID == Question.ID).FirstOrDefault<EQUESTIONTERM>();
                    if (QuestionTerm == null) { 
                        QuestionTerm = new EQUESTIONTERM();
                        QuestionTerm.ID = Guid.NewGuid();
                        QuestionTerm.QUESTIONID = Question.ID;
                        QuestionTerm.TERMID = Term.ID;
                        QuestionTerm.TF = Frequency.ToString();
                        db.EQUESTIONTERMs.Add(QuestionTerm);
                    }
                }

                Question.Indexed = 1;
                db.Entry(Question).State = EntityState.Modified;
                db.SaveChanges();

                List<EQUESTIONTERM> QuestionTerms = db.EQUESTIONTERMs.Where(QT => QT.QUESTIONID == Question.ID).ToList<EQUESTIONTERM>();

                foreach (EQUESTIONTERM QuestionTerm in QuestionTerms)
                {
                    QuestionTerm.WEIGHT = (Math.Round(Convert.ToDouble(QuestionTerm.TF) * Math.Log(NumberOfQuestions / Convert.ToDouble(QuestionTerm.ETERM.IDF)), 2)).ToString();
                    db.Entry(QuestionTerm).State = EntityState.Modified;
                }
                db.SaveChanges();

            }
        }
        public static void BuildArabicIndex()
        {
            List<AQUESTION> Questions = db.AQUESTIONs.Where(Q => Q.Indexed == 0).ToList<AQUESTION>();
            int NumberOfQuestions = db.AQUESTIONs.ToList().Count;

            foreach (AQUESTION Question in Questions)
            {
                var QuestionText = Question.VALUE + " " + Question.ANSWER;

                IDictionary<string, Int32> QeustionTerms = QuestionText.ArabicTokenize();

                foreach (string key in QeustionTerms.Keys)
                {
                    // TF : Term Frequency
                    var Frequency = QeustionTerms[key];

                    ATERM Term = db.ATERMs.Where(T => T.VALUE == key).FirstOrDefault<ATERM>();

                    if (Term != null)
                    {
                        Term.IDF = (Convert.ToInt32(Term.IDF) + 1).ToString();
                        db.Entry(Term).State = EntityState.Modified;
                    }
                    else
                    {
                        Term = new ATERM();

                        Term.ID = Guid.NewGuid();
                        Term.VALUE = key;
                        Term.IDF = Frequency.ToString();
                        db.ATERMs.Add(Term);
                    }

                    AQUESTIONTERM QuestionTerm = db.AQUESTIONTERMs.Where(QT => QT.TERMID == Term.ID && QT.QUESTIONID == Question.ID).FirstOrDefault<AQUESTIONTERM>();
                    if (QuestionTerm == null)
                    {
                        QuestionTerm = new AQUESTIONTERM();
                        QuestionTerm.ID = Guid.NewGuid();
                        QuestionTerm.QUESTIONID = Question.ID;
                        QuestionTerm.TERMID = Term.ID;
                        QuestionTerm.TF = Frequency.ToString();
                        db.AQUESTIONTERMs.Add(QuestionTerm);
                    }
                }

                Question.Indexed = 1;
                db.Entry(Question).State = EntityState.Modified;
                db.SaveChanges();

                List<AQUESTIONTERM> QuestionTerms = db.AQUESTIONTERMs.Where(QT => QT.QUESTIONID == Question.ID).ToList<AQUESTIONTERM>();

                foreach (AQUESTIONTERM QuestionTerm in QuestionTerms)
                {
                    QuestionTerm.WEIGHT = (Math.Round(Convert.ToDouble(QuestionTerm.TF) * Math.Log(NumberOfQuestions / Convert.ToDouble(QuestionTerm.ATERM.IDF)), 2)).ToString();
                    db.Entry(QuestionTerm).State = EntityState.Modified;
                }
                db.SaveChanges();
            }
        }
    }
}