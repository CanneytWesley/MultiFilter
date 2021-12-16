using Filter;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            DependencyProperty.Register("FilterOnderdelen", typeof(ObservableCollection<IFilter>), typeof(SCFilter), new PropertyMetadata(null));

        public SCFilter()
        {
            InitializeComponent();

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

            SetPopupState(result.Count > 0);
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
                if (filter is IInitialiseren ini) await ini.Initialiseren();
            }
        }

    }
}
