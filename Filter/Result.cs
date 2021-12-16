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

        public Result(IFilter filter, Action<IResult> actie)
        {
            Filter = filter;
            UitvoerenCommand = new RelayCommand(Uitvoeren);
            Actie = actie;
        }
        public Result(IFilter filter, string onderdeel, object model,  Action<IResult> actie) : this(filter,actie)
        => Model = new KeuzeModel(onderdeel, model);

        public Result(IFilter filter, string onderdeel, Action<IResult> actie) : this(filter,actie)
        => Model = new KeuzeModel(onderdeel);

        private void Uitvoeren()
        => Actie.Invoke(this);
        
    }

}