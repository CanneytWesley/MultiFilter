
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Windows.Input;

namespace Filter.Filters
{
    public class Result : IResult
    {
        public ICommand UitvoerenCommand { get; }
        public IFilter Filter { get; set; }
        public KeuzeModel Model { get; set; }
        public Action<IResult> Actie { get; set; }
        public Icon Icon { get; set; }

        public Result(IFilter filter, Action<IResult> actie, Icon icon)
        {
            Filter = filter;
            UitvoerenCommand = new RelayCommand(Uitvoeren);
            Actie = actie;
            Icon = icon;
        }
        public Result(IFilter filter, string onderdeel, object model,  Action<IResult> actie, Icon icon) : this(filter,actie, icon)
        => Model = new KeuzeModel(onderdeel, model);

        public Result(IFilter filter, string onderdeel, Action<IResult> actie, Icon icon) : this(filter,actie, icon)
        => Model = new KeuzeModel(onderdeel);

        private void Uitvoeren()
        => Actie.Invoke(this);
        
    }

}