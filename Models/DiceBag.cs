using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonBuilder.Models
{
    public sealed class DiceBag
    {
        private static DiceBag instance;
        private DiceBag() {}
        public static DiceBag getDice()
        {
            if (instance == null)
                instance = new DiceBag();
            return instance;
        }

        public System.Random rand = new System.Random();

        public int roll(int sides) { return rand.Next(1, sides); }

        public IEnumerable<int> roll(int sides, int dice)
        {
            List<int> retVal = new List<int>();
            while (dice-- > 0)
            {
                retVal.Add(roll(sides));
            }
            return retVal;
        }

        public int d4() { return rand.Next(4) + 1; }
        public int d6() { return rand.Next(6) + 1; }
        public int d8() { return rand.Next(8) + 1; }
        public int d10() { return rand.Next(10) + 1; }
        public int d12() { return rand.Next(12) + 1; }
        public int d20() { return rand.Next(20) + 1; }
        public int d100() { return rand.Next(100) + 1; }

    }
}
