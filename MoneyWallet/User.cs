using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyWallet
{
   public class User
   {
        public int uid { get; set; }
     public string username { get; set; } = "";
     public string email { get; set; } = "";
     public string passwordHash { get; set; } = "";
     public string passwordSalt { get; set; } = "";
   }
} 
