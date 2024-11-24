using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrangeTeam
{
    internal class Player
    {
        public string Name { get; set; }
        public int Power { get; set; }
        public int Position { get; set; }

        public Player(string name, int power, int position) 
        {
            Name = name;
            Power = power;
            Position = position;
        }
    }
}
