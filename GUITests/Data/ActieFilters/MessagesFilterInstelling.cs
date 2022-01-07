using Filter;
using Filter.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace GUITests.Data.ActieFilters
{
    public class MessagesFilterInstelling : IActieFilterInstellingen
    {
        public string Titel { get; set; }
        public string Shortcut { get; set; }
        public FilterOptie FilterOpties { get; set; }
        public Icon Icon { get; set; }

        public MessagesFilterInstelling()
        {
            Titel = "Acties met berichten, deze berichten worden dan getoond.";
            Shortcut = "A";
            FilterOpties = FilterOptie.Exact;
            Icon = new Icon(Brushes.Blue.ToString(), Icons.Alertbericht);
    }

        public Task<List<FilterAction>> GetData()
        {
            return Task.FromResult(new List<FilterAction>() { 
                new FilterAction("Hello world",() => { MessageBox.Show("Hello World"); }),
                new FilterAction("Async delayed message",async () => { await Task.Run(() =>{ Thread.Sleep(1000); MessageBox.Show("Hello Async World");  }); }),
            });
        }
    }
}
