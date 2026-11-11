using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyWallet
{
     public class Expense
    {
        public int eid { get; set; }
        public int uid { get; set; }
        public string reason { get; set; } = "";
        public DateTime date { get; set; }
        public decimal amount { get; set; }
     }
}
