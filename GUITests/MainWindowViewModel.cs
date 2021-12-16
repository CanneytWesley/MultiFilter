using Filter;
using Filter.Filters;
using GUITests.Data;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace GUITests
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Lot> AlleLoten { get; set; }
        public ObservableCollection<Lot> Loten { get; set; }
        public ObservableCollection<IFilter> Filters { get; set; }
        public MainWindowViewModel()
        {
            AlleLoten = new ObservableCollection<Lot>();
            Loten = new ObservableCollection<Lot>();
            Filters = new ObservableCollection<IFilter>();

            SeedLoten.GetSeed().ForEach(p => AlleLoten.Add(p));

            Filteren(null);

            var filter = new KeuzeFilter<DBLeverancier>(new LeveranciersData(), "Leveranciers", "L");
            filter.Icon = new Icon(Brushes.Green.ToString(), Icons.Gelukt);
            filter.FilterUitvoeren += Filteren;
            Filters.Add(filter);

            var filter2 = new KeuzeFilter<DBProduct>(new ProductenData(), "Producten", "P");
            filter2.Icon = new Icon(Brushes.Orange.ToString(), Icons.Freeze);
            filter2.FilterUitvoeren += Filteren;
            Filters.Add(filter2);

            Filters.Add(new ActionFilter() { Titel = "Hello world", Action = () => { MessageBox.Show("Hello World"); },Icon = new Icon(Brushes.Blue.ToString(), Icons.Alertbericht) });
        }

        private void Filteren(IResult s)
        {
            
            var loten = AlleLoten.ToList();

            if (s?.Model?.Model is DBLeverancier l)
            {
                loten = AlleLoten.Where(p => p.Leverancier == l.Naam).ToList();
            }
            else if (s?.Model?.Model is DBProduct pr)
            {
                loten = AlleLoten.Where(p => p.Afmetingen.IndexOf(pr.Naam) != -1).ToList();
            }

            Loten.Clear();
            loten.ForEach(p => Loten.Add(p));
        }
    }
}
