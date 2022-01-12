using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUITests.Models
{
    public class PostalCode
    {
        public PostalCode(string code)
        {
            this.Code = code;
        }

        public string Code { get; set; }
    }
}
