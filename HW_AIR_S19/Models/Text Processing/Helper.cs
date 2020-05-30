using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HW_AIR_S19.Models.TextProcessing
{
	public static class Helper
	{
		private static HashSet<String> EnglishStopWords = new HashSet<String>(new String[]{
			"a", "about", "above", "above", "across", "after", "afterwards", "again", "against", 
			"all", "almost", "alone", "along", "already", "also","although","always","am","among", 
			"amongst", "amoungst", "amount",  "an", "and", "another", "any","anyhow","anyone",
			"anything","anyway", "anywhere", "are", "around", "as",  "at", "back","be","became", 
			"because","become","becomes", "becoming", "been", "before", "beforehand", "behind", 
			"being", "below", "beside", "besides", "between", "beyond", "bill", "both", "bottom",
			"but", "by", "call", "can", "cannot", "cant", "co", "con", "could", "couldnt", "cry", 
			"de", "describe", "detail", "do", "done", "down", "due", "during", "each", "eg", "eight", 
			"either", "eleven","else", "elsewhere", "empty", "enough", "etc", "even", "ever", 
			"every", "everyone", "everything", "everywhere", "except", "few", "fifteen", "fify", 
			"fill", "find", "fire", "first", "five", "for", "former", "formerly", "forty", "found", 
			"four", "from", "front", "full", "further", "get", "give", "go", "had", "has", "hasnt", 
			"have", "he", "hence", "her", "here", "hereafter", "hereby", "herein", "hereupon", "hers", 
			"herself", "him", "himself", "his", "how", "however", "hundred", "ie", "if", "in", "inc", 
			"indeed", "interest", "into", "is", "it", "its", "itself", "keep", "last", "latter", 
			"latterly", "least", "less", "ltd", "made", "many", "may", "me", "meanwhile", "might", 
			"mill", "mine", "more", "moreover", "most", "mostly", "move", "much", "must", "my", 
			"myself", "name", "namely", "neither", "never", "nevertheless", "next", "nine", "no", 
			"nobody", "none", "noone", "nor", "not", "nothing", "now", "nowhere", "of", "off", 
			"often", "on", "once", "one", "only", "onto", "or", "other", "others", "otherwise", "our", 
			"ours", "ourselves", "out", "over", "own","part", "per", "perhaps", "please", "put", 
			"rather", "re", "same", "see", "seem", "seemed", "seeming", "seems", "serious", "several",
			"she", "should", "show", "side", "since", "sincere", "six", "sixty", "so", "some", 
			"somehow", "someone", "something", "sometime", "sometimes", "somewhere", "still", "such",
			"system", "take", "ten", "than", "that", "the", "their", "them", "themselves", "then", 
			"thence", "there", "thereafter", "thereby", "therefore", "therein", "thereupon", "these", 
			"they", "thickv", "thin", "third", "this", "those", "though", "three", "through", 
			"throughout", "thru", "thus", "to", "together", "too", "top", "toward", "towards", 
			"twelve", "twenty", "two", "un", "under", "until", "up", "upon", "us", "very", "via",
			"was", "we", "well", "were", "what", "whatever", "when", "whence", "whenever", "where",
			"whereafter", "whereas", "whereby", "wherein", "whereupon", "wherever", "whether",
			"which", "while", "whither", "who", "whoever", "whole", "whom", "whose", "why", "will",
			"with", "within", "without", "would", "yet", "you", "your", "yours", "yourself", 
			"yourselves", "the" ,"i"
		});

        private static HashSet<String> ArabicStopWords = new HashSet<String>(new String[]{
             "في","على","إلى",  "من","عن",  "هناك","أو",  "ثم","نحن",  "أنا","أنتم",  "أنت","أنتما",  "إياه",
             "أنتن",  "إياها","هو",  "إياهما","هي",  "إياهم","هما",  "إياهن","هم",  "إياك","هن",  "إياكما",
             "تلك",  "إياكم","ذا",  "إياكن","ذاك",  "إياي","ذلك",  "إيانا","ذان",  "أولاء","ذانك",  "أولئك",
             "ذه",  "أولالك","ذين",  "ذي","ذينك",  "هؤلاء","هذان",  "هاتان","هذه",  "هانه","هذي",  "هاتي",
            "هذين",  "هاتين","هنا",  "هذا","من",  "هنالك","ما",  "التي","أين",  "الذي","أي",  "اللذين",
            "أيان",  "الذين","حيثما",  "اللذان","كيفما",  "اللاتي","متى",  "اللتان","هماكم",  "اللتيا","كيف",  "اللتيا",
            "ماذا",  "اللواتي","هلم",  "كأي","قلما",  "كأين","إن",  "إليك","لا",  "إليكم","لات",  "إليكما","أن",  "إليكن",
            "كأن",  "عليك","لعل",  "ها","لكن",  "هاك","أي",  "ليت","أيا",  "أجل","بل",  "إذما","بلا",  "إذن","حتى",  "إذ",
            "سوف",  "ألا","عل",  "إلى","في",  "أم","كلا",  "أما","هلا",  "كي","وا",  "لم","إذ",  "لن","إلا",  "لو","على",  "لولا",
            "عن",  "لوما","قد",  "هل","عدا",  "لما","بعض",  "مذ","سوى",  "منذ","غير",  "حاشا","كل",  "خلا","ذات",  "لعمر",
            "عندما",  "مثل","كلما",  "مع","قبل",  "نحو","خلف",  "حيث","أمام",  "كلتا","تحت",  "سيما","يمين",  "أصلاً","أصبح",  "بين",
            "كان",  "صار","ليس",  "ظل","انفك",  "عاد","مادام",  "برح","مازال",  "مافتئ","فوق",  "و"});

        private static PorterStemmer PorterStemmer = new PorterStemmer();

        private static ISRIStemmer ISRIStemmer = new ISRIStemmer();

        public static IDictionary<string,Int32> EnglishTokenize(this String data)
		{
            var StemmedTerms = new List<string>();
            var FinalTerms = new Dictionary<string, Int32>();

            StringBuilder term = new StringBuilder();

            for (int i = 0, index = 0; i < data.Length; i++)
            {
               char c = data[i];

               switch (c)
               {
                   case '<':
                       i = skipHtmlTag(ref data, i);
                       continue;
                   case '>':
                   case '\r':
                   case '\n':
                   case '\t':
                   case '.':
                   case ',':
                       c = ' ';
                       break;
               }

               if (Char.IsLetter(c))
               {
                   if (Char.IsUpper(c))
                       term.Append(Char.ToLower(c));
                   else
                       term.Append(c);
               }
               else if (Char.IsSeparator(c) == false)
                   term.Append(c);

               if (Char.IsSeparator(c) || i == data.Length - 1)
               {
                   // new term
                   var t = term.ToString();
                   term.Clear();

                   if (String.IsNullOrEmpty(t))
                       continue;

                   // filter out stop words
                   if (EnglishStopWords.Contains(t))
                       continue;

                   var Stem = PorterStemmer.StemWord(t);

                   StemmedTerms.Add(Stem);
               }

            }

            foreach(string Term in StemmedTerms)
            {
               var Frequency = StemmedTerms.FindAll(delegate (string t) { return t == Term; }).Count ;
               if (FinalTerms.ContainsKey(Term) == false)
                   FinalTerms.Add(Term, Frequency);
            }

			return FinalTerms;
		}

        public static IDictionary<string, Int32> ArabicTokenize(this String data)
        {
            var StemmedTerms = new List<string>();
            var FinalTerms = new Dictionary<string, Int32>();

            StringBuilder term = new StringBuilder();

            for (int i = 0, index = 0; i < data.Length; i++)
            {
                char c = data[i];

                switch (c)
                {
                    case '<':
                        i = skipHtmlTag(ref data, i);
                        continue;
                    case '>':
                    case '\r':
                    case '\n':
                    case '\t':
                    case '.':
                    case ',':
                        c = ' ';
                        break;
                }

                if (Char.IsLetter(c))
                {
                    if (Char.IsUpper(c))
                        term.Append(Char.ToLower(c));
                    else
                        term.Append(c);
                }
                else if (Char.IsSeparator(c) == false)
                    term.Append(c);

                if (Char.IsSeparator(c) || i == data.Length - 1)
                {
                    // new term
                    var t = term.ToString();
                    term.Clear();

                    if (String.IsNullOrEmpty(t))
                        continue;

                    // filter out stop words
                    if (ArabicStopWords.Contains(t))
                        continue;

                    var Stem = ISRIStemmer.StemWord(t);

                    StemmedTerms.Add(Stem);
                }

            }

            foreach (string Term in StemmedTerms)
            {
                var Frequency = StemmedTerms.FindAll(delegate (string t) { return t == Term; }).Count;
                if (FinalTerms.ContainsKey(Term) == false)
                    FinalTerms.Add(Term, Frequency);
            }

            return FinalTerms;
        }


        private static int skipHtmlTag(ref String data, int startAt)
		{
			for (int i = startAt; i < data.Length; i++)
			{
				switch (data[i])
				{
					case '<':
					case '/':
						continue;
					case '>':
						return i;
				}

				if (Char.IsLetter(data[i]) == false)
					return startAt;
			}

			return startAt;
		}
	}
}
