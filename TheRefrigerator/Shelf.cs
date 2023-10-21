using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TheRefrigerator
{
    public class Shelf
    {//A counter to ensure an unique ID

        public static int Counter = 1;
        public int Id { get; set; }
        public int Floor { get; set; }
        public double Place { get; set; }
        public List<Item> ItemsList { get; set; }

        public Shelf(int floor,double place) { 
            Id=Counter++;
            Floor=floor;
            Place=place;
            ItemsList = new List<Item>();

        }

        public override string ToString()
        {
            string x= "Shelf "+Id+" on floor " + Floor + " " + Place + " cm, Contains: "+Environment.NewLine;
            foreach(Item item in ItemsList)
            {
                x += item.ToString();
                x += Environment.NewLine;
            }
            return x;
        }

     
        public double PlaceLeft()
        {
            double sum =0;
            foreach(Item item in ItemsList)
            {
                sum += item.Size;
            }

            return Place-sum;

        }
    }
}
