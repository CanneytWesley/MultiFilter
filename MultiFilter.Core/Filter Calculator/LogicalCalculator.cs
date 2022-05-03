using System;
using System.Collections.Generic;
using System.Linq;

namespace Filter.Filter_Calculator
{
    public enum LogicalOperator
    {
        And,
        Or,
        Value,
        SmallerThan,
        GreaterThan,
        SmallerOrEqualThan,
        GreaterOrEqualThan,
        Equal,
        NotEqual,
        Star,
    }

    public class LogicalCalculator
    {
        private Dictionary<string, LogicalOperator> Operators = new Dictionary<string, LogicalOperator>(){
            { @">",LogicalOperator.GreaterThan },
            { @"<",LogicalOperator.SmallerThan },
            { @"=",LogicalOperator.Equal },
            { @"!=",LogicalOperator.NotEqual },
            { @"<>",LogicalOperator.NotEqual },
            { @">=",LogicalOperator.GreaterOrEqualThan },
            { @"<=",LogicalOperator.SmallerOrEqualThan },
            { @"&",LogicalOperator.And },
            { @"&&",LogicalOperator.And },
            { @"EN",LogicalOperator.And },
            { @"|",LogicalOperator.Or },
            { @"||",LogicalOperator.Or },
            { @"OF",LogicalOperator.Or },
        };

        public List<LogicalValue> Logic { get; set; }

        public bool IsSuccess { get; private set; }

        public LogicalCalculator()
        {
            Logic = new List<LogicalValue>();
        }

        public void Calculate(string text)
        {
            IsSuccess = true;

            if (text.Length <= 1)
            {
                IsSuccess = false;
                return;
            }

            text = text.Replace(" ", "");

            for (int i = 0; i < text.Length-1; i++)
            {
                if (text.Length >= 2 && 
                    Operators.TryGetValue(text.Substring(i, 2).ToUpper(), out LogicalOperator valDouble))
                {
                    if (i != 0)
                    {
                        bool isDoubleOK = TryParse(text.Substring(0, i), out decimal resultDouble);
                        if (text.Length != 0) Logic.Add(new LogicalValue(resultDouble));

                        if (!isDoubleOK) IsSuccess = false;
                    }

                    Logic.Add(new LogicalValue(valDouble));

                    text = text.Substring(i + 2);
                    i = -1;
                }
                else if (Operators.TryGetValue(text.Substring(i, 1).ToUpper(), out LogicalOperator valSingle))
                {
                    if (i != 0)
                    {
                        bool SingleOK = TryParse(text.Substring(0, i), out decimal resultSingle);
                        if (text.Length != 0) Logic.Add(new LogicalValue(resultSingle));

                        if (!SingleOK) IsSuccess = false;
                    }

                    Logic.Add(new LogicalValue(valSingle));

                    text = text.Substring(i+1);
                    i = -1;
                }
            }

            bool otherOK = TryParse(text, out decimal result);
            if (text.Length != 0) Logic.Add(new LogicalValue(result));
            if (!otherOK) IsSuccess = false;

            if (Logic.Count == 1 && Logic[0].Operator == LogicalOperator.Value)
                Logic.Insert(0, new LogicalValue(LogicalOperator.Equal));

            TestLogicBuild();
        }

        private bool TryParse(string text, out decimal result)
        {
            result = 0;
            var ok = decimal.TryParse(text, out decimal value);

            if (ok)
            {
                result = value;
                return true;
            }

            var date = new DateTime();
            if (!ok) ok = DateTime.TryParse(text, out date);

            if (ok)
            {
                result = Convert.ToDecimal(date.Ticks);
                return true;
            }

            return false;
        }

        private void TestLogicBuild()
        {
            if (Logic.Count == 0) return;
            if (Logic[0].Operator == LogicalOperator.And || Logic[0].Operator == LogicalOperator.Or) IsSuccess = false;
            if (Logic[Logic.Count -1].Operator == LogicalOperator.And || Logic[Logic.Count-1].Operator == LogicalOperator.Or) IsSuccess = false;
            if (!IsSuccess) return;

            int stand = 0;

            for (int i = 0; i < Logic.Count; i++)
            {
                if (stand == 0 && (Logic[i].Operator == LogicalOperator.Value ||
                    Logic[i].Operator == LogicalOperator.And ||
                    Logic[i].Operator == LogicalOperator.Or)) IsSuccess = false;
                else if (stand == 1 && Logic[i].Operator != LogicalOperator.Value)
                    IsSuccess = false;
                else if (stand == 2 && Logic[i].Operator != LogicalOperator.And && Logic[i].Operator != LogicalOperator.Or)
                    IsSuccess = false;

                stand++;
                if (stand == 3) stand = 0;
            }
        }

        
    }

    public class LogicalValue
    {
        public LogicalOperator Operator { get; set; }

        public decimal value { get; set; }

        public LogicalValue(decimal waarde)
        {
            value = waarde;
            Operator = LogicalOperator.Value;
        }

        public LogicalValue(LogicalOperator op)
        {
            Operator = op;
        }
    }
}