using Filter.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Drawing;

namespace Filter
{
    /// <summary>
    /// Er zijn verschillende soorten implementaties:
    /// KeuzeFilter => Een lijst van gegevens waar je een keuze uitmaakt om dan te filteren
    /// ActieFilter => Als je één van de toegevoegde acties uitvoert wordt de gekoppelde actie (methode) uitgevoerd.
    /// LogischeFilter => Hier worden logische operators gebruikt. Bijvoorbeeld bij afmetingen of nummers >1000 & <5000
    /// </summary>
    public interface IFilter
    {
        public Task<List<IResult>> Filteren(string uitvoeren);

        public string Titel { get; }
        string ShortCut { get; set; }

        Icon Icon { get; set; }
    }
}
