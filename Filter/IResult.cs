using System;
using System.Windows.Input;

namespace Filter.Filters
{

    public interface IResult
    {
        public ICommand UitvoerenCommand { get; }

        public IFilter Filter { get; set; }

        public KeuzeModel Model { get; set; }

        Action<IResult> Actie { get; set; }

        public Icon Icon { get; set; }
    }
}