using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiFilter.GUITests.Models
{
    public class PostalCode
    {
        public PostalCode(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
    }
}
