using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMindWpf
{
    public class Player
    {
        public string Name { get; set; }
        public int Attempts { get; set; }
        public DateTime Date { get; set; }

        public Player(string name, int attempts)
        {
            Name = name;
            Attempts = attempts;
            Date = DateTime.Now;
        }

        public Player() { }

        public override string ToString()
        {
            return Date.ToShortDateString() + Name.PadLeft(40) + Attempts.ToString().PadLeft(40);
        }
    }
}
