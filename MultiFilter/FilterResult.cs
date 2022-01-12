using Filter.Filters;
using System;
using System.Collections.Generic;

namespace MultiFilter
{
    public class FilterResult
    {
        private List<IResult> results;

        public List<IResult> Results
        {
            get
            {
                return results;
            }
            set
            {
                if (value != null)
                    results = value;
            }
        }

        public Edit Edit { get; set; }

        public FilterResult()
        {
            Results = new List<IResult>();
        }



    }
}