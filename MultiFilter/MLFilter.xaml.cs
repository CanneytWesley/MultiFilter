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

namespace MultiFilter
{
    /// <summary>
    /// Interaction logic for MultiFilter.xaml
    /// </summary>
    public partial class MLFilter : UserControl
    {
        public ObservableCollection<IFilter> FilterOnderdelen
        {
            get { return (ObservableCollection<IFilter>)GetValue(FilterOnderdelenProperty); }
            set { SetValue(FilterOnderdelenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterOnderdelen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterOnderdelenProperty =
            DependencyProperty.Register("FilterOnderdelen", typeof(ObservableCollection<IFilter>), typeof(MLFilter), new PropertyMetadata(null, CollectionChangedCallBack));



        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(MLFilter), new PropertyMetadata(null));



        public int TextBoxWidth
        {
            get { return (int)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register("TextBoxWidth", typeof(int), typeof(MLFilter), new PropertyMetadata(150, TekstBoxWidthChanged));




        public ObservableCollection<IResult> ActieveFilter { get; set; }

        public Soort Soort { get; set; }

        public ICommand FilterGekliktCommand { get; set; }
        public ICommand SetShortCutCommand { get; set; }

        public MLFilter()
        {
            InitializeComponent();
            SetEnOfInformatie(Soort.En);
            ActieveFilter = new ObservableCollection<IResult>();
            FilterOverzicht.ItemsSource = ActieveFilter;
            FilterGekliktCommand = new RelayCommand(() => { SetPopupState(false); });
            SetShortCutCommand = new RelayCommand<string>(SetShortCut);
            SetEnOfLabel();

        }

        private static void CollectionChangedCallBack(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            if (pz.FilterOnderdelen != null && e.NewValue != e.OldValue)
            {
                pz.FilterOnderdelen.CollectionChanged += FilterOnderdelen_CollectionChanged;
                FilterOnderdelen_CollectionChanged(pz, null);
            }
        }
        private static void TekstBoxWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            pz.TxtFilter.Width = pz.TextBoxWidth;
        }


        private void ButtonToonActieveFilters_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LstResultaten.Items.Clear();
            ActieveFilter.ToList().ForEach(p => LstResultaten.Items.Add(p));
        }

        private void ButtonEnOf_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Soort == Soort.En)
                SetEnOfInformatie(Soort.Of);
            else if (Soort == Soort.Of)
                SetEnOfInformatie(Soort.En);

            Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter.ToList(), Soort = Soort }); 
            SetEnOfLabel();
        }


        private void FilterReset_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ActieveFilter.Clear(); 

            LblActieveFilterCount.Content = ActieveFilter.Count;
            Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter.ToList(), Soort = Soort }); 
            SetEnOfLabel();
        }
        private void SetEnOfInformatie(Soort soort)
        {
            if (soort == Soort.En)
            {
                Soort = Soort.En; 
                LblEnofOf.Content = "EN";
                PathEnOf.Data = Geometry.Parse(new Icons().En);
                GridEnOf.ToolTip = "Gegevens moeten voldoen aan alle voorwaarden (en)";
            }
            else if (soort == Soort.Of)
            {
                Soort = Soort.Of;
                LblEnofOf.Content = "OF";
                PathEnOf.Data = Geometry.Parse(new Icons().Of);
                GridEnOf.ToolTip = "Gegevens moeten voldoen aan één van de voorwaarden (of)";
            }
        }

        private static void FilterOnderdelen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var pz = (MLFilter)sender;
            foreach (var item in pz.FilterOnderdelen)
            {
                if (item is IFilterUitvoerenEvent fu)
                {
                    fu.FilterUitvoeren -= pz.FilterUitvoeren;
                    fu.FilterUitvoeren += pz.FilterUitvoeren;
                }
            }
        }

        public void FilterUitvoeren(IResult resultaat)
        {
            if (ActieveFilter == null) return;

            if (ActieveFilter != null && !ActieveFilter.Any(p => p.IsGelijkAan(resultaat) ) && resultaat != null)
            {
                ActieveFilter.Add(resultaat);

                LblActieveFilterCount.Content = ActieveFilter.Count;

                Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter.ToList(), Soort = Soort });
                SetEnOfLabel();
            }
        }

        private void SetEnOfLabel()
        {
            if (ActieveFilter == null)
            {
                BorderLblEnOf.Visibility = Visibility.Collapsed;
                return;
            }

            if (ActieveFilter.Count >= 2) BorderLblEnOf.Visibility = Visibility.Visible;
            else BorderLblEnOf.Visibility = Visibility.Collapsed;
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
            TxtGeenResultaten.Visibility = Visibility.Visible;

            if (TxtFilter.Text.Length > 0)
            {
                result.ForEach(p => LstResultaten.Items.Add(p));

                if (result.Count == 0) TxtGeenResultaten.Visibility = Visibility.Visible;
                else TxtGeenResultaten.Visibility = Visibility.Collapsed;
            }



            //SetPopupState(result.Count > 0 && TxtFilter.Text.Length > 0);
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

            SetPopupState(true);
        }

        private void ResultVerwijderen_Click(object sender, MouseButtonEventArgs e)
        {
            Label l = (Label)sender;

            IResult result = (IResult) l.Tag;

            ActieveFilter.Remove(result);

            LblActieveFilterCount.Content = ActieveFilter.Count;
            Command.Execute(new FilterResultaat() { Resultaten = ActieveFilter.ToList(), Soort = Soort }); 
            SetEnOfLabel();


            TxtFilter.Text = "";
        }

        private async void TxtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var shortcuts = FilterOnderdelen.Where(p => p is ILogischeFilter).Select(p => p as ILogischeFilter).ToList();

                var filtergesplit = TxtFilter.Text.Split(' ').ToList() ;

                if (filtergesplit.Count > 1)
                {
                    var filter = filtergesplit[0];

                    var shortcut = shortcuts.FirstOrDefault(p => p.ShortCut.IndexOf(filter, StringComparison.OrdinalIgnoreCase) == 0);

                    if (shortcut != null)
                    {
                        var getfilter = await shortcut.LogischFilteren(TxtFilter.Text);
                        foreach (var ft in getfilter) FilterUitvoeren(ft);

                    }
                }
            }
        }
    }
}
