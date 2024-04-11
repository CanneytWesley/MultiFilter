using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Results;
using Filter.General_Interfaces;
using GalaSoft.MvvmLight.CommandWpf;
using MultiFilter.Core;
using MultiFilter.Core.Filters;
using MultiFilter.Core.Filters.Model;
using MultiFilter.Data;
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







        public Brush FilterBorderColor
        {
            get { return (Brush)GetValue(FilterBorderColorProperty); }
            set { SetValue(FilterBorderColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterBorderColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterBorderColorProperty =
            DependencyProperty.Register("FilterBorderColor", typeof(Brush), typeof(MLFilter), new PropertyMetadata(Brushes.SkyBlue, FilterBorderColorChanged));



        public Brush FilterIconColor
        {
            get { return (Brush)GetValue(FilterIconColorProperty); }
            set { SetValue(FilterIconColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterIconColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterIconColorProperty =
            DependencyProperty.Register("FilterIconColor", typeof(Brush), typeof(MLFilter), new PropertyMetadata(Brushes.SkyBlue, FilterIconColorChanged));




        public Brush FilterTextColor
        {
            get { return (Brush)GetValue(FilterTextColorProperty); }
            set { SetValue(FilterTextColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FilterTextColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FilterTextColorProperty =
            DependencyProperty.Register("FilterTextColor", typeof(Brush), typeof(MLFilter), new PropertyMetadata(Brushes.SlateGray, FilterTextColorChanged));




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

            Loaded += MLFilter_Loaded;

        }

        private void MLFilter_Loaded(object sender, RoutedEventArgs e)
        {
            if (FilterMaster != null)
            {
                if (FilterMaster.DataLocation == null || FilterMaster.DataLocation.NotValid())
                    BDSaveFilter.Visibility = Visibility.Collapsed;
                else
                    BDSaveFilter.Visibility = Visibility.Visible;
            }
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
                pz.FilterMaster.AddFilter -= pz.AddFilter;
                pz.FilterMaster.AddFilter += pz.AddFilter;
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


        private static void FilterBorderColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            //SolidColorBrush MyBrush = Brushes.Black;

            pz.Resources["MainColor"] = (SolidColorBrush)pz.FilterBorderColor;
        }

        private static void FilterIconColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            //SolidColorBrush MyBrush = Brushes.Black;

            pz.Resources["FilterButtonColor"] = (SolidColorBrush)pz.FilterIconColor;
        }

        private static void FilterTextColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var pz = (MLFilter)d;
            //SolidColorBrush MyBrush = Brushes.Black;

            pz.Resources["TextColor"] = (SolidColorBrush)pz.FilterTextColor;
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

        public void AddFilter(IResult result)
        {
            if (ActiveFilter == null) return;

            if (ActiveFilter != null && !ActiveFilter.Any(p => p.IsEqualTo(result)) && result != null)
            {
                ActiveFilter.Add(result);
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
            bool isLogischeFilterOther = false;
            bool isLogischeFilterString = false;
            bool isBooleanFilter = false;
            string tekst = "";

            if (result != null)
            {
                if (!string.IsNullOrWhiteSpace(result.InformationText))
                {
                    tekst = result.InformationText;
                }
                else
                {
                    var type = result.GetType();

                    if (type.GetGenericTypeDefinition() == typeof(LogicalFilter<,>))
                    {
                        var args = type.GetGenericArguments();
                        if (args.Length > 1)
                        {
                            if (args[1] == typeof(string)) isLogischeFilterString = true;
                            else isLogischeFilterOther = true;
                        }
                    }
                    else
                    {
                        isBooleanFilter = type.GetInterfaces().Any(p => p == typeof(IBooleanFilter));
                        TBLogicalFilter.Text = result.Title;
                    }
                }
            }
            else
            {
                TBLogicalFilter.Text = "";
            }


            if (isLogischeFilterOther)
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
                    "Enkele voorbeelden: " +
                    Environment.NewLine +
                    "<15000" +
                    Environment.NewLine +
                    "<3en>1";

            }
            else if (isLogischeFilterString)
            {
                TxtInformationAboutFilter.Text =
                    "Met deze filter kun je logisch filteren." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Exact filteren: Appel" +
                    Environment.NewLine +
                    "Of ongeveer: Ap*" +
                    Environment.NewLine +
                    "Of ongeveer: *Ap";

            }
            else if (isBooleanFilter)
            {
                TxtInformationAboutFilter.Text =
                    "Dit is een Ja/Nee filter." +
                    Environment.NewLine +
                    Environment.NewLine +
                    "Volgende statussen kunnen gebruikt worden:" +
                    Environment.NewLine +
                    "ja, nee, yes, no, 1, 0, true, false";
            }
            else if (!string.IsNullOrWhiteSpace(tekst))
            {
                TxtInformationAboutFilter.Text = tekst;
            }
            else
            {
                TxtInformationAboutFilter.Text = "Er zijn geen resultaten die voldoen aan uw criteria...";
            }
        }
        public void SetPopupState(bool state)
        {
            if (Popup.IsOpen == state) return;

            Popup.IsOpen = state;

            if (!state)
            {
                MouseHook.UnHook();
            }
            else
            {
                MouseHook.SetHook(this);
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
            var shortcuts = FilterMaster.Filters.Where(p => p is ILogicalFilter || p is IBooleanFilter).ToList();

            var filtergesplit = TxtFilter.Text.Split(' ').ToList();

            if (filtergesplit.Count > 1)
            {
                var filter = filtergesplit[0];

                var shortcut = shortcuts.FirstOrDefault(p => p.ShortCut.Equals(filter, StringComparison.OrdinalIgnoreCase));

                if (shortcut is ILogicalFilter lf)
                {
                    SetPopupState(false);
                    var getfilter = await lf.FilterLogical(TxtFilter.Text);
                    foreach (var ft in getfilter) ExecuteFilter(ft);

                    TxtFilter.Text = "";
                }
                else if (shortcut is IBooleanFilter bf)
                {
                    SetPopupState(false);
                    var getfilter = await bf.Filter(TxtFilter.Text);
                    foreach (var ft in getfilter) ExecuteFilter(ft);

                    TxtFilter.Text = "";
                }
            }
        }

        private async void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            await CreateFilter();
        }


        private void DetailFilter_Click(object sender, RoutedEventArgs e)
        {
            if (ChkDetailFilter.IsChecked == true)
            {
                DetailView.Visibility = Visibility.Visible;
                ButtonView.Visibility = Visibility.Collapsed;
                TitelView.Visibility = Visibility.Collapsed;
            }
            else
            {
                DetailView.Visibility = Visibility.Collapsed;
                ButtonView.Visibility = Visibility.Visible;
                TitelView.Visibility = Visibility.Visible;
            }
        }

        private void FilterSave_MouseUp(object sender, MouseButtonEventArgs e)
        {
            SetPopupState(false);

            FilterMaster.SaveFilter(ActiveFilter.ToList());
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                var filter = (IResult)((Grid)sender).Tag;

                ActiveFilter.Remove(filter);

                FilterMaster.Command.Execute(new FilterResult() { Results = ActiveFilter.ToList(), Edit = Edit });
                SetAndOrLabel();


                TxtFilter.Text = filter.Filter.ShortCut + " " + filter.Model.Item;
                TxtFilter.CaretIndex = TxtFilter.Text.Length;
            }
        }
    }
}
