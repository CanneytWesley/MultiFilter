using Filter.Filter_Calculator;
using Filter.Filter_Results;
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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiFilter.GUITests
{
    public class MyFilterMaster<T> : FilterFactory<T>
    {
        private readonly ObservableCollection<T> Friends;

        public MyFilterMaster(ObservableCollection<T> friends)
        {
            this.Friends = friends;
            Filters = new ObservableCollection<IFilter>();

            //Filter setup
            Filters.Add(new MultipleChoiceFilter<Friend, Company>(new CompanyFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, PostalCode>(new PostalCodeFilterSetting()));
            Filters.Add(new MultipleChoiceFilter<Friend, Gender>(new GenderFilterSettings()));
            Filters.Add(new ActionFilter(new MessagesFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, double>(new WeightFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, int>(new AgeFilterSetting()));
            Filters.Add(new LogicalFilter<Friend, DateTime>(new DateOfBirthSetting()));
            Filters.Add(new LogicalFilter<Friend, string>(new CompanySetting()));

            Command = new RelayCommand<FilterResult>(Filter);

            //Filter uitvoerder initialiseren
            FilterExecutor = new FilterExecutor<T>();
            FilterExecutor.Setup(Filters.ToList());
        }

        public override void Filter(FilterResult result)
        {
            if (result == null)
                    result = new();
                    FilterExecutor.Filter(result.Edit, result.Results); ;

            Friends.Clear();
            FilterExecutor.Result.ForEach(p => Friends.Add(p));
        }

        public override void SetData(List<T> friends)
        {
            FilterExecutor.SetData(friends);

            Filter(null);
        }


    }
}
