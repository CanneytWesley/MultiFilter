using Filter;
using Filter.Filter_Calculator;
using Filter.Filter_Settings;
using Filter.Filters;
using Filter.Filters.Model;
using GUITests.Data;
using GUITests.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Media;

namespace GUITests.Data.Companies
{
    public class CompanyFilterSetting : IMultipleChoiceSettings<Friend, Company>
    {
        public Func<Company, string> PropertyToFilterWith { get; set; }
        public Func<Friend, string> PropertyFromDataset { get; set; }
        public string Title { get; set; }
        public string Shortcut { get; set; }
        public FilterOption FilterOptions { get; set; }
        public Icon Icon { get; set; }

        public CompanyFilterSetting()
        {
            PropertyToFilterWith =  p => p.Name;
            PropertyFromDataset = p => p.Company;
            Title = "Companies";
            Shortcut = "C";
            FilterOptions = FilterOption.IndexOf;
            Icon = new Icon(Brushes.Green.ToString(), Icons.Gelukt);
        }

        public Task<List<Company>> GetData()
        {
            return Task.FromResult(new List<Company>() 
                { 
                new Company("Bpost"),
                new Company("Philip Morris Benelux"),
                new Company("Pfizer Service Company"),
                new Company("Carrefour Belgium"),
                new Company("Bridgestone Europe"),
                new Company("Daikin Europe N.V."),
                new Company("ExxonMobil Petroleum & Chemical"),
                new Company("Zoetis Belgium"),
                new Company("Mazda Motor Logistics Europe"),
                new Company("UCB Pharma"),
                new Company("Total Petrochemicals & Refining SA/NV"),
                new Company("Mercedes-Benz Belgium Luxembourg"),
                new Company("ArcelorMittal Belgium"),
                new Company("Janssen Pharmaceutica"),
                new Company("Aperam Stainless Belgium"),
                new Company("Electrabel"),
                new Company("Avnet Europe"),
                new Company("Etablissementen Franz Colruyt"),
                new Company("BASF Antwerpen"),
                new Company("Total Belgium"),
                new Company("Proximus"),
                new Company("Audi Brussels S.A.:N.V."),
                new Company("Société Nationale des Chemins de Fer Belges"),
                new Company("Aurubis Belgium"),
                new Company("TS Europe"),
                new Company("Belfius Bank"),
                new Company("Umicore"),
                new Company("Glaxosmithkline Biologicals"),
                new Company("BNP Paribas Fortis"),
                new Company("Volvo Car Belgium"),
                new Company("KBC Bank"),
                new Company("Scabel"),
                new Company("Delhaize Le Lion/De Leeuw"),
                new Company("Toyota Motor Europe"),
                new Company("AW Europe"),
                new Company("D’Ieteren"),
                new Company("Volvo Group Belgium"),
                new Company("Pfizer Innovative Supply Point International"),
                new Company("Tech Data"),
                new Company("Cargill"),
                new Company("Luminus"),
                new Company("Mastercard Europe"),
                new Company("Gmed Healthcare"),
                new Company("Kuwait Petroleum (Belgium)"),
                new Company("Eurelec Trading"),
                new Company("Barry Callebaut Belgium NV"),

      

            });
        }
    }
}
