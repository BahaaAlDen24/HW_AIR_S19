using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_AIR_S19.Models.MatchingModels
{
    interface ISearch
    {
        List<EQUESTION> EnglishSearch(string Query);
        List<AQUESTION> ArabicSearch(string Query);
    }
}
