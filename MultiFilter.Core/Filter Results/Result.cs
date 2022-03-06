using GalaSoft.MvvmLight.CommandWpf;
using MultiFilter.Core.Filters.Model;
using System;
using System.Windows.Input;

namespace Filter.Filter_Results
{
    public abstract class Result : IResult
    {
        public ICommand ExecuteCommand { get; }
        public IFilter Filter { get; set; }
        public MultipleChoiceModel Model { get; set; }
        public Action<IResult> Action { get; set; }
        public Icon Icon { get; set; }

        public Result(IFilter filter, Action<IResult> actie, Icon icon)
        {
            Filter = filter;
            ExecuteCommand = new RelayCommand(Uitvoeren);
            Action = actie;
            Icon = icon;
        }


        private void Uitvoeren()
        => Action.Invoke(this);
        
    }

}