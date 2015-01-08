using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class MatchingSetViewModel
    {
      
        List<MatchingSets> _matchingSetsList = null;
        List<MappedCategoriesName> _mappedCategoryNames = null;
      

        public List<MatchingSets> MatchingSetsList
        {
            get { return _matchingSetsList; }
            set { _matchingSetsList = value; }
        }

        public List<MappedCategoriesName> MappedCategoriesName
        {
            get { return _mappedCategoryNames; }
            set { _mappedCategoryNames = value; }
        }
        public bool IsIncludeVAT { get; set; }

        public string Currency { get; set; }

        public bool IsShowPrices { get; set; }
    }
}