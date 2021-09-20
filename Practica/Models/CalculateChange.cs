using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Practica.Models
{
    public class CalculateChange
    {
        public double itemPrice { get; set; }
        public double payment { get; set; }
        public string change { get; set; }
        public List<Denomination> listChangeDenominations { get; set; }
    }

    public class Denomination
    {
        public int quantity { get; set; }
        public string currencyDenomination { get; set; }
        public bool isRemaining { get; set; }
    }

}