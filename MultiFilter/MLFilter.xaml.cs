using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Results;
using Filter.General_Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using MultiFilter.Core;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
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

        public FilterMaster FilterMaster
        {
            get { return (FilterMaster)GetValue(FilterMasterProperty); }
            set { SetValue(FilterMasterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterMaster.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterMasterProperty =
            DependencyProperty.Register("FilterMaster", typeof(FilterMaster), typeof(MLFilter), new PropertyMetadata(null, FilterMasterChanged));


        public int TextBoxWidth
        {
            get { return (int)GetValue(TextBoxWidthProperty); }
            set { SetValue(TextBoxWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TextBoxWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextBoxWidthProperty =
            DependencyProperty.Register("TextBoxWidth", typeof(int), typeof(MLFilter), new PropertyMetadata(150, TekstBoxWidthChanged));




        public ObservableCollection<IResult> ActiveFilter { get; set; }

        public Edit Edit { get; set; }

        public ICommand FilterClickCommand { get; set; }
        public ICommand MouseOverCommand { get; set; }
        public ICommand SetShortCutCommand { get; set; }

        public MLFilter()
        {
            InitializeComponent();
            SetEnOfInformation(Edit.And);
            ActiveFilter = new ObservableCollection<IResult>();
            FilterOverzicht.ItemsSource = ActiveFilter;
            FilterClickCommand = new RelayCommand(() => { SetPopupState(false); });
            MouseOverCommand = new RelayCommand<string>((string s) => { 
                TBHuidigeFilter.Text = s;
                if (s.Length > 0) TBHuidigeFilter.Visibility = Visibility.Visible;
                else TBHuidigeFilter.Visibility = Visibility.Collapsed;
            });
            SetShortCutCommand = new RelayCommand<string>(SetShortCut);
            SetAndOrLabel();

        }


        private static void FilterMasterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            if (pz.FilterMaster != null)
            {
                pz.FilterMaster.TriggerFilterEvent -= pz.TriggerFilter;
                pz.FilterMaster.TriggerFilterEvent += pz.TriggerFilter;
                pz.FilterMaster.FilterEventHandler -= pz.ExecuteFilter;
                pz.FilterMaster.FilterEventHandler += pz.ExecuteFilter;
            }
        }

        private void TriggerFilter(object sender, EventArgs e)
        {
            FilterMaster.Command.Execute(new FilterResult() { Results = ActiveFilter.ToList(), Edit = Edit });
        }

        private static void TekstBoxWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            pz.BorderTxtFilter.Width = pz.TextBoxWidth;
            pz.Popup.Width = pz.TextBoxWidth;
        }


        private void ButtonToonActieveFilters_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LstResults.Items.Clear();
            ActiveFilter.ToList().ForEach(p => LstResults.Items.Add(p));
        }

        private void ButtonEnOf_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Edit == Edit.And)
                SetEnOfInformation(Edit.Or);
            else if (Edit == Edit.Or)
                SetEnOfInformation(Edit.And);

            FilterMaster.Command.Execute(new FilterResult() { Results = ActiveFilter.ToList(), Edit = Edit }); 
            SetAndOrLabel();
        }


        private void FilterReset_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ActiveFilter.Clear();

            FilterMaster.Command.Execute(new FilterResult() { Results = ActiveFilter.ToList(), Edit = Edit }); 
            SetAndOrLabel();
        }
        private void SetEnOfInformation(Edit soort)
        {
            if (soort == Edit.And)
            {
                Edit = Edit.And; 
                LblEnofOf.Content = "EN";
                LblEnofOf.ToolTip = "Gegevens moeten voldoen aan alle voorwaarden.";
            }
            else if (soort == Edit.Or)
            {
                Edit = Edit.Or;
                LblEnofOf.Content = "OF";
                LblEnofOf.ToolTip = "Gegevens moeten voldoen aan één van de voorwaarden.";
            }
        }

        private static void Filters_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            var pz = (MLFilter)sender;
            var shortcuts = pz.FilterMaster.Filters.Select(p => ((BaseFilter)p).ShortCut).ToList();
            foreach (var item in pz.FilterMaster.Filters)
            {
                ((BaseFilter)item).SetShortcuts(shortcuts);
                if (item is IFilterExecuteEvent fu)
                {
                    fu.ExecuteFilter -= pz.ExecuteFilter;
                    fu.ExecuteFilter += pz.ExecuteFilter;
                }
            }
        }

        public void ExecuteFilter(IResult result)
        {
            if (ActiveFilter == null) return;

            if (ActiveFilter != null && !ActiveFilter.Any(p => p.IsEqualTo(result) ) && result != null)
            {
                ActiveFilter.Add(result);

                FilterMaster.Command.Execute(new FilterResult() { Results = ActiveFilter.ToList(), Edit = Edit });
                SetAndOrLabel();
            }
        }

        private void SetAndOrLabel()
        {
            if (ActiveFilter == null)
            {
                BorderLblEnOf.Visibility = Visibility.Collapsed;
                return;
            }

            if (ActiveFilter.Count >= 2) BorderLblEnOf.Visibility = Visibility.Visible;
            else BorderLblEnOf.Visibility = Visibility.Collapsed;
        }

        private void SetShortCut(string obj)
        {
            TxtFilter.Text = obj + " ";
            TxtFilter.CaretIndex = TxtFilter.Text.Length;
        }

        private async void FilterText_TextChanged(object sender, TextChangedEventArgs e)
        {
            await Initialise();

            var tasks = new List<Task<List<IResult>>>();
            foreach (var filter in FilterMaster.Filters)
            {
                tasks.Add(filter.Filter(TxtFilter.Text));
            }

            await Task.WhenAll(tasks);

            var result = tasks.SelectMany(p => p.Result).ToList();

            LstResults.Items.Clear();
            TxtInformationAboutFilter.Visibility = Visibility.Visible;

            if (TxtFilter.Text.Length > 0)
            {
                result.ForEach(p => LstResults.Items.Add(p));

                if (result.Count == 0) TxtInformationAboutFilter.Visibility = Visibility.Visible;
                else TxtInformationAboutFilter.Visibility = Visibility.Collapsed;

                SetPopupState(true);
                CheckShortcut();
            }
        }

        private void CheckShortcut()
        {
            
            var result = FilterMaster.Filters.FirstOrDefault(p => ((BaseFilter)p).HasThisShortCut(TxtFilter.Text));
            bool result2 = false;

            if (result != null)
            {
                var type = result.GetType();
                result2 = type.GetInterfaces().Any(p => p == typeof(ILogicalFilter));
                TBLogicalFilter.Text = result.Title;
            }
            else
            {
                TBLogicalFilter.Text = "";
            }

            if (result != null && result2)
            {
                TxtInformationAboutFilter.Text =
                    "Met deze filter kun je logisch filteren." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Verschillende logische operatoren kunnen gebruikt worden:" +
                    Environment.NewLine +
                    "en & of | < <= > >= = !=" +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Enkele voorbeelden: "+
                    Environment.NewLine +
                    "<15000" + 
                    Environment.NewLine +
                    "<3en>1";

            }
            else
            {
                TxtInformationAboutFilter.Text = "Er zijn geen resultaten die voldoen aan uw criteria...";
            }
        }
        private Brush TextBoxBorderColor;
        public void SetPopupState(bool state)
        {
            if (TextBoxBorderColor == null) TextBoxBorderColor = BorderTxtFilter.BorderBrush;
            Popup.IsOpen = state;

            if (!state)
            {
                MouseHook.UnHook();
                BorderTxtFilter.BorderBrush = TextBoxBorderColor;
            }
            else
            {
                MouseHook.SetHook(this);
                BorderTxtFilter.BorderBrush = Brushes.SkyBlue;
            }
        }

        private async Task Initialise()
        {
            foreach (var filter in FilterMaster.Filters)
            {
                if (filter is IInitialise ini && !ini.IsInitialised)
                {
                    await ini.Initialise();
                }
            }
        }

        private void TxtFilter_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FilterText_TextChanged(this, null);

            SetPopupState(true);
        }

        private void DeleteResult_Click(object sender, MouseButtonEventArgs e)
        {
            Label l = (Label)sender;

            IResult result = (IResult) l.Tag;

            ActiveFilter.Remove(result);

            FilterMaster.Command.Execute(new FilterResult() { Results = ActiveFilter.ToList(), Edit = Edit }); 
            SetAndOrLabel();


            TxtFilter.Text = "";
        }

        private async void TxtFilter_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                await CreateFilter();
            }
        }

        private async Task CreateFilter()
        {
            var shortcuts = FilterMaster.Filters.Where(p => p is ILogicalFilter).Select(p => p as ILogicalFilter).ToList();

            var filtergesplit = TxtFilter.Text.Split(' ').ToList();

            if (filtergesplit.Count > 1)
            {
                var filter = filtergesplit[0];

                var shortcut = shortcuts.FirstOrDefault(p => p.ShortCut.IndexOf(filter, StringComparison.OrdinalIgnoreCase) == 0);

                if (shortcut != null)
                {
                    var getfilter = await shortcut.FilterLogical(TxtFilter.Text);
                    foreach (var ft in getfilter) ExecuteFilter(ft);

                    TxtFilter.Text = "";
                    SetPopupState(false);
                }
            }
        }

        private async void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            await CreateFilter();
        }
    }
}
