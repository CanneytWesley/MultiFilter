﻿using Filter.Filters;
using System;
using System.Collections.Generic;

namespace MultiFilter
{
    public class FilterResultaat
    {
        private List<IResult> resultaten;

        public List<IResult> Resultaten
        {
            get
            {
                return resultaten;
            }
            set
            {
                if (value != null)
                    resultaten = value;
            }
        }

        public Soort Soort { get; set; }

        public FilterResultaat()
        {
            Resultaten = new List<IResult>();
        }



    }
}