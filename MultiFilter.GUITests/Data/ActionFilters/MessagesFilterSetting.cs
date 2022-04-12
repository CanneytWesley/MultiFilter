using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using MultiFilter.Core.Filters.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace MultiFilter.GUITests.Data.ActionFilters
{
    public class MessagesFilterSetting : IActionFilterSettings
    {
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public Icon Icon { get; set; }

        public MessagesFilterSetting()
        {
            Title = "Acties met berichten, deze berichten worden dan getoond.";
            Shortcut = "A";
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
