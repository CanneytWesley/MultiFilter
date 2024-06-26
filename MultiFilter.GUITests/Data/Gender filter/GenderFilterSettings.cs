﻿using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests;
using MultiFilter.GUITests.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.Gender_filter
{
    public class GenderFilterSettings : IMultipleChoiceSettings<Friend, Gender>
    {
        public Func<Gender, string> PropertyToFilterWith { get; set; } = p => p.ToString();
        public Func<Friend, string> PropertyFromDataset { get; set; } = p => p.Sex.ToString();
        public string Title { get; set; }
        = "Gender";
        public string Shortcut { get; set; }
        = "G";
        public Icon Icon { get; set; }
        = new Icon(Brushes.AntiqueWhite.ToString(), Icons.Bericht);
        public string InformationText { get; set; }

        public Task<List<Gender>> GetData()
        {
            return Task.FromResult(
                new List<Gender>() {
                    Gender.Female,
                    Gender.Men,
                    Gender.Other,
                });
        }
    }
}
