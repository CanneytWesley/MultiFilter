using Filter.Filter_Calculator;
using Filter.Filter_Results;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
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
        public FilterExecutor<T> FilterExecutor { get; set; }
        public abstract void SetData(List<T> objects);
    }

    public abstract class FilterMaster
    {
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


        public abstract void Filter(FilterResult result);
    }
}
