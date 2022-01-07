using Filter;
using Filter.Filters;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data;
using GUITests.Data.ActieFilters;
using GUITests.Data.Certificaat;
using GUITests.Data.Kwaliteiten;
using GUITests.Data.LogischeFilters;
using MultiFilter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GUITests
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Lot> Loten { get; set; }
        public ObservableCollection<IFilter> Filters { get; set; }
        public ICommand FilterenCommand { get; set; }
        public FilterBerekenen<Lot> FilterUitvoerder { get; set; }


        public MainWindowViewModel()
        {
            Loten = new ObservableCollection<Lot>();
            Filters = new ObservableCollection<IFilter>();
            FilterenCommand = new RelayCommand<FilterResultaat>(Filteren);

            //Filter instellen
            Filters.Add(new KeuzeFilter<Lot, DBLeverancier>(new LeveranciersFilterInstelling()));
            Filters.Add(new KeuzeFilter<Lot, DBProduct>(new ProductenFilterInstelling()));
            Filters.Add(new KeuzeFilter<Lot, DBKwaliteit>(new KwaliteitFilterInstelling()));
            Filters.Add(new KeuzeFilter<Lot, DBCertificaat>(new CertificaatFilterInstelling()));
            Filters.Add(new ActieFilter( new MessagesFilterInstelling()));
            Filters.Add(new LogischeFilter<Lot, double>(new BreedteFilterInstelling()));

            //Filter uitvoerder initialiseren
            FilterUitvoerder = new FilterBerekenen<Lot>();
            FilterUitvoerder.Instellen(Filters.ToList());
        }

        internal async Task LoadData()
        {
            await Task.Run(() => {
                FilterUitvoerder.SetData(SeedLoten.GetSeed());
            });

            //Filteren zodat data getoond wordt.
            Filteren(null);
        }

        private void Filteren(FilterResultaat result)
        {
            if (result == null)
                result = new();

            FilterUitvoerder.Filteren(result.Soort, result.Resultaten); ;

            Loten.Clear();
            FilterUitvoerder.Resultaat.ForEach(p => Loten.Add(p));
        }
    }
}
