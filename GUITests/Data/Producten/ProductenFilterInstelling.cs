using Filter;
using Filter.Filters;
using GUITests.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests
{
    public class ProductenFilterInstelling : IKeuzeFilterInstellingen<Lot,DBProduct>
    {
        public Func<DBProduct, string> Property { get; set; }
        = p => p.Naam;
        public Func<DBProduct, string> PropertyOmMeeTeFilteren { get; set; }
        public Func<Lot, string> PropertyUitDataGrid { get; set; }
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public ProductenFilterInstelling()
        {
            PropertyOmMeeTeFilteren = p => p.Naam;
            PropertyUitDataGrid = p => p.Product;
            Titel = "Producten";
            Shortcut = "P";
            FilterOpties = FilterOptie.IndexOf;
            Icon = new Icon(Brushes.Orange.ToString(), Icons.Freeze); 
        }

        public Task<List<DBProduct>> GetData()
        {
            return Task.FromResult(new List<DBProduct>() 
            { 
                new DBProduct("PL10"),
                new DBProduct("PL20"),
                new DBProduct("PL30"),
                new DBProduct("HEA400"),
                new DBProduct("PL40") 
            });
        }
    }
}
