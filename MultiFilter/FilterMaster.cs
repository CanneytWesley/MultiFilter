using Filter.Filter_Calculator;
using Filter.Filter_Results;
using GalaSoft.MvvmLight.CommandWpf;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
using MultiFilter.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MultiFilter.Core
{
    public abstract class FilterFactory<T> : FilterMaster
    {
        public ObservableCollection<T> Collection { get; }
        public FilterExecutor<T> FilterExecutor { get; set; }
        public abstract void SetData(List<T> objects);

        public event EventHandler FilterExecuted;
        public override event AsyncEventHandler Initialise;

        public bool Initialised { get; private set; }

        public FilterFactory(ObservableCollection<T> collection)
        {
            Collection = collection;
            Command = new RelayCommand<FilterResult>(async (FilterResult result) => { await Filter(result); });
        }


        public override Task Filter(FilterResult result = null)
        {
            if (!Initialised)
            {
                Initialised = true;
                Initialise?.Invoke(this, null);

                if (DataLocation != null && !DataLocation.NotValid() )
                    return Task.CompletedTask;
            }

            if (result == null)
                result = new();
            FilterExecutor.Filter(result.Edit, result.Results); ;

            Collection.Clear();

            if (FilterExecutor.Result != null)
                FilterExecutor.Result.ForEach(p => Collection.Add(p));

            FilterExecuted?.Invoke(this, EventArgs.Empty);

            return Task.CompletedTask;
        }
        
        
    }

    public abstract class FilterMaster
    {
        public delegate Task AsyncEventHandler(object sender, EventArgs e);
        public abstract event AsyncEventHandler Initialise;
        public ICommand Command { get; set; }
        ObservableCollection<IFilter> filters;
        public ObservableCollection<IFilter> Filters
        {
            get
            {
                return filters;
            }
            set
            {
                if (value != filters)
                {
                    filters = value;

                    if (value != null)
                    {
                        value.CollectionChanged += Value_CollectionChanged;
                        Value_CollectionChanged(this, null);
                    }
                }
            }
        }

        private void Value_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var shortcuts = Filters.Select(p => ((BaseFilter)p).ShortCut).ToList();
            foreach (var item in Filters)
            {
                ((BaseFilter)item).SetShortcuts(shortcuts);
                if (item is IFilterExecuteEvent fu)
                {
                    fu.ExecuteFilter -= ExecuteFilter;
                    fu.ExecuteFilter += ExecuteFilter;
                }
            }
        }

        public DataLocation DataLocation { get; set; }

        public void ExecuteFilter(IResult result)
        {
            FilterEventHandler?.Invoke(result);
        }


        internal event EventHandler TriggerFilterEvent;
        internal event FilterEventHandler FilterEventHandler;

        public void InvokeFilter()
        {
            TriggerFilterEvent?.Invoke(this,EventArgs.Empty);
        }


        public abstract Task Filter(FilterResult result = null);

        internal void SaveFilter(List<IResult> results)
        {
            if (DataLocation == null) return;
            if (DataLocation.NotValid()) return;

            FilterDataService service = new FilterDataService(DataLocation);
            service.Save(results);
        }

        public List<DataModel> ReadFilter()
        {
            if (DataLocation == null) return new List<DataModel>();
            if (DataLocation.NotValid()) return new List<DataModel>() ;

            FilterDataService service = new FilterDataService(DataLocation);
            return service.Read();
        }
    }
}
