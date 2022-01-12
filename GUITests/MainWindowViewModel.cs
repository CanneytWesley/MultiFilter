using Filter;
using Filter.Filters;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data;
using GUITests.Data.ActieFilters;
using GUITests.Data.Certificaat;
using GUITests.Data.Gender_filter;
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
            FilterCommand = new RelayCommand<FilterResult>(Filter);

            //Filter instellen
            Filters.Add(new MultipleChoiceFilter<Friend, Company>(new CompanyFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, PostalCode>(new PostalCodeFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, Gender>(new GenderFilterSettings()));
            Filters.Add(new ActionFilter( new MessagesFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, double>(new WeightFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, int>(new AgeFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, DateTime>(new DateOfBirthSetting()));

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
            Filter(null);
        }

        private void Filter(FilterResult result)
        {
            if (result == null)
                result = new();

            FilterUitvoerder.Filteren(result.Soort, result.Resultaten); ;

            Friends.Clear();
            FilterUitvoerder.Resultaat.ForEach(p => Friends.Add(p));
        }
    }
}
