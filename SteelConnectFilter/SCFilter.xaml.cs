using Filter;
using Filter.Filters;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SteelConnectFilter
{
    /// <summary>
    /// Interaction logic for SCFilter.xaml
    /// </summary>
    public partial class SCFilter : UserControl
    {
        public ObservableCollection<IFilter> FilterOnderdelen
        {
            get { return (ObservableCollection<IFilter>)GetValue(FilterOnderdelenProperty); }
            set { SetValue(FilterOnderdelenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterOnderdelen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterOnderdelenProperty =
            DependencyProperty.Register("FilterOnderdelen", typeof(ObservableCollection<IFilter>), typeof(SCFilter), new PropertyMetadata(null, CollectionChangedCallBack));



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(SCFilter), new PropertyMetadata(null));

        public List<IResult> ActieveFilter { get; set; }

        public Soort Soort { get; set; }

        public ICommand FilterGekliktCommand { get; set; }
        public ICommand SetShortCutCommand { get; set; }

        public SCFilter()
        {
            InitializeComponent();
            SetEnOfInformatie(Soort.En);
            ActieveFilter = new List<IResult>();
            FilterGekliktCommand = new RelayCommand(() => { SetPopupState(false); });
            SetShortCutCommand = new RelayCommand<string>(SetShortCut);

        }

        private static void CollectionChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (SCFilter)d;
            if (pz.FilterOnderdelen != null && e.NewValue != e.OldValue)
            {
                pz.FilterOnderdelen.CollectionChanged += FilterOnderdelen_CollectionChanged;
                FilterOnderdelen_CollectionChanged(pz, null);
            }
        }


        private void ButtonToonActieveFilters_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LstResultaten.Items.Clear();
            ActieveFilter.ForEach(p => LstResultaten.Items.Add(p));
        }

        private void ButtonEnOf_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Soort == Soort.En)
                SetEnOfInformatie(Soort.Of);
            else if (Soort == Soort.Of)
                SetEnOfInformatie(Soort.En);

            Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter, Soort = Soort });
        }


        private void FilterReset_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ActieveFilter.Clear(); 
            LblActieveFilterCount.Content = ActieveFilter.Count;
            Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter, Soort = Soort });
        }
        private void SetEnOfInformatie(Soort soort)
        {
            if (soort == Soort.En)
            {
                Soort = Soort.En;
                PathEnOf.Data = Geometry.Parse(new Icons().En);
                GridEnOf.ToolTip = "Elke filter wordt als een geheel getoond.";
            }
            else if (soort == Soort.Of)
            {
                Soort = Soort.Of;
                PathEnOf.Data = Geometry.Parse(new Icons().Of);
                GridEnOf.ToolTip = "Elke filter wordt appart uitgezocht en getoond.";
            }
        }

        private static void FilterOnderdelen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var pz = (SCFilter)sender;
            foreach (var item in pz.FilterOnderdelen)
            {
                if (item is IFilterUitvoerenEvent fu)
                {
                    fu.FilterUitvoeren -= pz.FilterUitvoeren;
                    fu.FilterUitvoeren += pz.FilterUitvoeren;
                }
            }
        }

        private void FilterUitvoeren(IResult resultaat)
        {
            if (ActieveFilter == null) return;

            if (ActieveFilter != null && !ActieveFilter.Contains(resultaat) && resultaat != null)
                ActieveFilter.Add(resultaat);

            LblActieveFilterCount.Content = ActieveFilter.Count;

            Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter, Soort = Soort });
        }

        private void SetShortCut(string obj)
        {
            TxtFilter.Text = obj + " ";
            TxtFilter.CaretIndex = TxtFilter.Text.Length;
        }

        private async void FilterTekst_TextChanged(object sender, TextChangedEventArgs e)
        {
            await OnderdelenInitialiseren();

            var taken = new List<Task<List<IResult>>>();
            foreach (var filter in FilterOnderdelen)
            {
                taken.Add(filter.Filteren(TxtFilter.Text));
            }

            await Task.WhenAll(taken);

            var result = taken.SelectMany(p => p.Result).ToList();

            LstResultaten.Items.Clear();
            result.ForEach(p => LstResultaten.Items.Add(p));

            SetPopupState(result.Count > 0 && TxtFilter.Text.Length > 0);
        }

        public void SetPopupState(bool state)
        {
            Popup.IsOpen = state;

            if (!state) MouseHook.UnHook();
            else MouseHook.SetHook(this);
        }

        private async Task OnderdelenInitialiseren()
        {
            foreach (var filter in FilterOnderdelen)
            {
                if (filter is IInitialiseren ini && !ini.IsGeinitialiseerd)
                {
                    await ini.Initialiseren();
                }
            }
        }

        private void TxtFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FilterTekst_TextChanged(this, null);
        }

    }
}
