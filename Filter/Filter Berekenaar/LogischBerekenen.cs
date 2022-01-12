using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Berekenaar
{
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

    public class LogischBerekenen
    {
        private Dictionary<string, LogischeOperator> Operators = new Dictionary<string, LogischeOperator>(){
            { @">",LogischeOperator.GroterDan },
            { @"<",LogischeOperator.KleinerDan },
            { @"=",LogischeOperator.GelijkAan },
            { @"!=",LogischeOperator.NietGelijkAan },
            { @"<>",LogischeOperator.NietGelijkAan },
            { @">=",LogischeOperator.GroterOfGelijkAan },
            { @"<=",LogischeOperator.KleinerOfGelijkAan },
            { @"&",LogischeOperator.En },
            { @"&&",LogischeOperator.En },
            { @"EN",LogischeOperator.En },
            { @"|",LogischeOperator.Of },
            { @"||",LogischeOperator.Of },
            { @"OF",LogischeOperator.Of },
        };

        public List<LogischeWaarde> Logica { get; set; }

        public bool IsSuccessVol { get; private set; }

        public LogischBerekenen()
        {
            Logica = new List<LogischeWaarde>();
        }

        public void BerekenLogica(string tekst)
        {
            IsSuccessVol = true;

            if (tekst.Length <= 1)
            {
                IsSuccessVol = false;
                return;
            }

            tekst = tekst.Replace(" ", "");

            for (int i = 0; i < tekst.Length-1; i++)
            {
                if (tekst.Length >= 2 && 
                    Operators.TryGetValue(tekst.Substring(i, 2).ToUpper(), out LogischeOperator valDubbel))
                {
                    if (i != 0)
                    {
                        bool DubbelOK = TryParse(tekst.Substring(0, i), out decimal resultDubbel);
                        if (tekst.Length != 0) Logica.Add(new LogischeWaarde(resultDubbel));

                        if (!DubbelOK) IsSuccessVol = false;
                    }

                    Logica.Add(new LogischeWaarde(valDubbel));

                    tekst = tekst.Substring(i + 2);
                    i = -1;
                }
                else if (Operators.TryGetValue(tekst.Substring(i, 1).ToUpper(), out LogischeOperator valEnkel))
                {
                    if (i != 0)
                    {
                        bool EnkelOK = TryParse(tekst.Substring(0, i), out decimal resultEnkel);
                        if (tekst.Length != 0) Logica.Add(new LogischeWaarde(resultEnkel));

                        if (!EnkelOK) IsSuccessVol = false;
                    }

                    Logica.Add(new LogischeWaarde(valEnkel));

                    tekst = tekst.Substring(i+1);
                    i = -1;
                }
            }

            bool overigeOK = TryParse(tekst, out decimal result);
            if (tekst.Length != 0) Logica.Add(new LogischeWaarde(result));
            if (!overigeOK) IsSuccessVol = false;

            TestLogischeOpbouwing();
        }

        private bool TryParse(string tekst, out decimal result)
        {
            result = 0;
            var ok = decimal.TryParse(tekst, out decimal waarde);

            if (ok)
            {
                result = waarde;
                return true;
            }

            var date = new DateTime();
            if (!ok) ok = DateTime.TryParse(tekst, out date);

            if (ok)
            {
                result = Convert.ToDecimal(date.Ticks);
                return true;
            }

            return false;
        }

        private void TestLogischeOpbouwing()
        {
            if (Logica.Count == 0) return;
            if (Logica[0].Operator == LogischeOperator.En || Logica[0].Operator == LogischeOperator.Of) IsSuccessVol = false;
            if (Logica[Logica.Count -1].Operator == LogischeOperator.En || Logica[Logica.Count-1].Operator == LogischeOperator.Of) IsSuccessVol = false;
            if (!IsSuccessVol) return;

            int stand = 0;

            for (int i = 0; i < Logica.Count; i++)
            {
                if (stand == 0 && (Logica[i].Operator == LogischeOperator.Waarde ||
                    Logica[i].Operator == LogischeOperator.En ||
                    Logica[i].Operator == LogischeOperator.Of)) IsSuccessVol = false;
                else if (stand == 1 && Logica[i].Operator != LogischeOperator.Waarde)
                    IsSuccessVol = false;
                else if (stand == 2 && Logica[i].Operator != LogischeOperator.En && Logica[i].Operator != LogischeOperator.Of)
                    IsSuccessVol = false;

                stand++;
                if (stand == 3) stand = 0;
            }
        }

        
    }

    public class LogischeWaarde
    {
        public LogischeOperator Operator { get; set; }

        public decimal Waarde { get; set; }

        public LogischeWaarde(decimal waarde)
        {
            Waarde = waarde;
            Operator = LogischeOperator.Waarde;
        }

        public LogischeWaarde(LogischeOperator op)
        {
            Operator = op;
        }
    }
}