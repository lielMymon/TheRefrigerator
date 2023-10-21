using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheRefrigerator
{
    public enum eType{food, drink }
    public enum eCosher { meat, milk,parve }


    public class Item
    {//A counter to ensure an unique ID
        public static int Counter = 1;
        public int Id { get; set; }
        public string Name { get; set; }
        public int ShelfNum { get; set; }

        public eType Type { get; set; }
        public eCosher Cosher { get; set; }

        public DateTime ExpiryDate { get; set; }
        public double Size { get; set; }

        public Item(string Name,int Shelfnum, eType type, eCosher cosher, DateTime date, double size)
        {
            Id = Counter++;
            this.Name = Name;
            this.ShelfNum = Shelfnum;
            this.Type = type;
            this.Cosher = cosher;
            this.ExpiryDate = date;
            Size = size;
        }

        public override string ToString()
        {
            return Type + " " + Name + " " + Cosher + ", on shelf " + ShelfNum + ", " + Size + " cm, better use before " + ExpiryDate + " Serial number: " + Id;
        }

    }
}
