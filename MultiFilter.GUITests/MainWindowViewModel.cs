using Filter.Filter_Calculator;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data.Gender_filter;
using GUITests.Data.Postal_codes;
using MultiFilter;
using MultiFilter.Core;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
using MultiFilter.GUITests.Data.ActionFilters;
using MultiFilter.GUITests.Data.Companies;
using MultiFilter.GUITests.Data.LogicalFilters;
using MultiFilter.GUITests.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiFilter.GUITests
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Friend> Friends { get; set; }
        
        public ICommand InvokeFilterCommand { get; set; }
        

        public MyFilterFactory<Friend> FilterMaster { get; set; }


        public MainWindowViewModel()
        {
            Friends = new ObservableCollection<Friend>();
            FilterMaster = new MyFilterFactory<Friend>(Friends);
            InvokeFilterCommand = new RelayCommand(InvokeFilter);
        }

        private void InvokeFilter()
        {
            FilterMaster.InvokeFilter();
        }

        internal async Task LoadData()
        {
            //await Task.Run(() =>
            //{
                FilterMaster.SetData(SeedFriends.GetSeed());
            //});

            
        }

    }
}
