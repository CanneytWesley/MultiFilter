using Filter;
using Filter.Filters;
using GalaSoft.MvvmLight.CommandWpf;
using GUITests.Data;
using GUITests.Data.Certificaat;
using GUITests.Data.Kwaliteiten;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace GUITests
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Lot> AlleLoten { get; set; }
        public ObservableCollection<Lot> Loten { get; set; }
        public ObservableCollection<IFilter> Filters { get; set; }

        public ICommand FilterenCommand { get; set; }
        public MainWindowViewModel()
        {
            AlleLoten = new ObservableCollection<Lot>();
            Loten = new ObservableCollection<Lot>();
            Filters = new ObservableCollection<IFilter>();
            FilterenCommand = new RelayCommand<FilterResultaat>(Filteren);

            SeedLoten.GetSeed().ForEach(p => AlleLoten.Add(p));

            Filteren(null);

            var filter = new KeuzeFilter<DBLeverancier>(new LeveranciersData(), "Leveranciers", "L");
            filter.Icon = new Icon(Brushes.Green.ToString(), Icons.Gelukt);
 
            Filters.Add(filter);

            var filter2 = new KeuzeFilter<DBProduct>(new ProductenData(), "Producten", "P");
            filter2.Icon = new Icon(Brushes.Orange.ToString(), Icons.Freeze);
            Filters.Add(filter2);

            var filter3 = new KeuzeFilter<DBKwaliteit>(new KwaliteitsData(), "Kwaliteiten", "K");
            filter3.Icon = new Icon(Brushes.Purple.ToString(), Icons.Mislukt);
            Filters.Add(filter3);

            var filter4 = new KeuzeFilter<DBCertificaat>(new CertificaatData(), "Certificaten", "C");
            filter4.Icon = new Icon(Brushes.Pink.ToString(), Icons.Bericht);
            Filters.Add(filter4);

            Filters.Add(new ActieFilter() { Titel = "Hello world", Action = () => { MessageBox.Show("Hello World"); },Icon = new Icon(Brushes.Blue.ToString(), Icons.Alertbericht) });
        }

        private void Filteren(FilterResultaat result)
        {
            if (result == null)
                result = new();

            var berekenaar = new FilterBerekenen<Lot>(AlleLoten.ToList(), result.Soort);

            foreach (var s in result.Resultaten)
            {
                if (s?.Model?.Model is DBLeverancier l)
                {
                    berekenaar.Add(AlleLoten.ToList().Where(p => p.Leverancier == l.Naam));
                }
                else if (s?.Model?.Model is DBProduct pr)
                {
                    berekenaar.Add(AlleLoten.Where(p => p.Afmetingen.IndexOf(pr.Naam) != -1));
                }
                else if (s?.Model.Model is DBKwaliteit kw)
                {
                    berekenaar.Add(AlleLoten.Where(p => p.Kwaliteit.IndexOf(kw.Naam, StringComparison.OrdinalIgnoreCase) != -1));
                }
                else if (s?.Model.Model is DBCertificaat ce)
                {
                    berekenaar.Add(AlleLoten.Where(p => p.Certificaat.IndexOf(ce.Naam, StringComparison.OrdinalIgnoreCase) != -1));
                }
            }

            Loten.Clear();
            berekenaar.Resultaat.ForEach(p => Loten.Add(p));
        }
    }
}
