﻿using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Models;
using System;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.LogicalFilters
{
    public class CompanySetting : ILogicalFilterSettings<Friend, string>
    {

        public CompanySetting()
        {
            Title = "Company index";
            Shortcut = "Co";
            Icon = new Icon(Brushes.Silver.ToString(), Icons.Gelukt);
            PropertyFromDataset = p => p.Company;
        }

        public Func<Friend, string> PropertyFromDataset { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public Icon Icon { get; set; }
        public string InformationText { get; set; }
    }
}
