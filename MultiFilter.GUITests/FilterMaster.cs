using Filter.Filter_Calculator;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data.Gender_filter;
using GUITests.Data.Postal_codes;
using MultiFilter.Core;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Data.ActionFilters;
using MultiFilter.GUITests.Data.Companies;
using MultiFilter.GUITests.Data.LogicalFilters;
using MultiFilter.GUITests.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace MultiFilter.GUITests
{
    public class MyFilterFactory<T> : FilterFactory<T>
    {
        public MyFilterFactory(ObservableCollection<T> friends) : base(friends)
        {
            Filters = new ObservableCollection<IFilter>();

            //Filter setup
            Filters.Add(new MultipleChoiceFilter<Friend, Company>(new CompanyFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, PostalCode>(new PostalCodeFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, Gender>(new GenderFilterSettings()));
            Filters.Add(new ActionFilter(new MessagesFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, double>(new WeightFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, int>(new AgeFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, DateTime>(new DateOfBirthSetting()));
            Filters.Add(new LogicalFilter<Friend, DateTime?>(new DateOfDeathSetting()));
            Filters.Add(new LogicalFilter<Friend, string>(new CompanySetting()));
            Filters.Add(new BooleanFilter<Friend>(new Data.BooleanFilter.LikesToPartyFilterSetting()));
            Filters.Add(new BooleanFilter<Friend>(new Data.BooleanFilter.NonAutonaticFilter()));
            Filters.Add(new BooleanFilter<Friend>(new Data.BooleanFilter.BestFriendFilterSetting()));

            //Filter uitvoerder initialiseren
            FilterExecutor = new FilterExecutor<T>();
            FilterExecutor.Setup(Filters.ToList());

            DataLocation = new MultiFilter.Data.DataLocation(@"f:\","bestandje.dat");
        }

        public override void SetData(List<T> friends)
        {
            FilterExecutor.SetData(friends);
            

            Filter();
        }


    }
}
