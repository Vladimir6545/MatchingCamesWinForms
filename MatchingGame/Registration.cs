using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingGame
{
    [Serializable]
   public class Registration
    {
      public  string Login { get; set; }
      public string Parol { get; set; }
      public Registration(string login, string parol)
        {
            this.Login = login;
            this.Parol = parol;
        }
        public Registration()
        {}

    }
}
