using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace testApp.models
{
    public abstract class AbstractTransaction
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }

        public abstract string Type { get; }
    }
}
