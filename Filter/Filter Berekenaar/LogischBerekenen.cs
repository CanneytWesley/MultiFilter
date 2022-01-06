using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Filter.Filter_Berekenaar
{
    public class LogischBerekenen
    {
        public List<LogischeWaarde> Logica { get; set; }

        public LogischBerekenen()
        {
            Logica = new List<LogischeWaarde>();
        }

        public void BerekenLogica(string tekst)
        { 
            
        }
    }

    public class LogischeWaarde
    {
        public LogischeOperator Operator { get; set; }

        public string Waarde { get; set; }
    }

    public enum LogischeOperator
    { 
        En,
        Of,
        Waarde,
        KleinerDan,
        GroterDan,
        KleinerOfGelijkAan,
        GroterOfGelijkAan,
        GelijkAan,
        NietGelijkAan,
    }
}
