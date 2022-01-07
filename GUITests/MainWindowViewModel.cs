using Filter;
using Filter.Filters;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data;
using GUITests.Data.ActieFilters;
using GUITests.Data.Certificaat;
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
        public ObservableCollection<Friend> Friends { get; set; }
        public ObservableCollection<IFilter> Filters { get; set; }
        public ICommand FilterCommand { get; set; }
        public FilterBerekenen<Friend> FilterUitvoerder { get; set; }


        public MainWindowViewModel()
        {
            Friends = new ObservableCollection<Friend>();
            Filters = new ObservableCollection<IFilter>();
            FilterCommand = new RelayCommand<FilterResultaat>(Filteren);

            //Filter instellen
            Filters.Add(new KeuzeFilter<Friend, Company>(new CompanyFilterSetting()));
            Filters.Add(new KeuzeFilter<Friend, PostalCode>(new PostalCodeFilterSetting()));
            Filters.Add(new ActieFilter( new MessagesFilterSetting()));
            Filters.Add(new LogischeFilter<Friend, double>(new WeightFilterSetting()));
            Filters.Add(new LogischeFilter<Friend, int>(new AgeFilterSetting()));

            //Filter uitvoerder initialiseren
            FilterUitvoerder = new FilterBerekenen<Friend>();
            FilterUitvoerder.Instellen(Filters.ToList());
        }

        internal async Task LoadData()
        {
            await Task.Run(() => {
                FilterUitvoerder.SetData(SeedFriends.GetSeed());
            });

            //Filteren zodat data getoond wordt.
            Filteren(null);
        }

        private void Filteren(FilterResultaat result)
        {
            if (result == null)
                result = new();

            FilterUitvoerder.Filteren(result.Soort, result.Resultaten); ;

            Friends.Clear();
            FilterUitvoerder.Resultaat.ForEach(p => Friends.Add(p));
        }
    }
}
