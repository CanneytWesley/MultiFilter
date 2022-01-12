﻿using Filter.Filter_Calculator;
using Filter.Filters;
using Filter.Filters.Model;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data.ActionFilters;
using GUITests.Data.Companies;
using GUITests.Data.Gender_filter;
using GUITests.Data.LogicalFilters;
using GUITests.Data.Postal_codes;
using GUITests.Models;
using MultiFilter;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GUITests
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Friend> Friends { get; set; }
        public ObservableCollection<IFilter> Filters { get; set; }
        public ICommand FilterCommand { get; set; }
        public FilterExecutor<Friend> FilterExecutor { get; set; }


        public MainWindowViewModel()
        {
            Friends = new ObservableCollection<Friend>();
            Filters = new ObservableCollection<IFilter>();
            FilterCommand = new RelayCommand<FilterResult>(Filter);

            //Filter setup
            Filters.Add(new MultipleChoiceFilter<Friend, Company>(new CompanyFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, PostalCode>(new PostalCodeFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, Gender>(new GenderFilterSettings()));
            Filters.Add(new ActionFilter( new MessagesFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, double>(new WeightFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, int>(new AgeFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, DateTime>(new DateOfBirthSetting()));

            //Filter uitvoerder initialiseren
            FilterExecutor = new FilterExecutor<Friend>();
            FilterExecutor.Setup(Filters.ToList());
        }

        internal async Task LoadData()
        {
            await Task.Run(() => {
                FilterExecutor.SetData(SeedFriends.GetSeed());
            });

            //Filteren zodat data getoond wordt.
            Filter(null);
        }

        private void Filter(FilterResult result)
        {
            if (result == null)
                result = new();

            FilterExecutor.Filter(result.Edit, result.Results); ;

            Friends.Clear();
            FilterExecutor.Result.ForEach(p => Friends.Add(p));
        }
    }
}
