﻿using MultiFilter.Core.Filters.Model;
using System;
using System.Windows.Input;

namespace Filter.Filter_Results
{

    public interface IResult
    {
        public ICommand ExecuteCommand { get; }

        public IFilter Filter { get; set; }

        public MultipleChoiceModel Model { get; set; }

        Action<IResult> Action { get; set; }

        public Icon Icon { get; set; }

        public bool IsEqualTo(IResult result)
        {
            return Model?.Item == result.Model?.Item && Filter?.ShortCut == result.Filter?.ShortCut && 
                Filter?.Title == result.Filter?.Title && Filter?.Icon?.IconColor == Filter?.Icon?.IconColor;
        }
    }
}