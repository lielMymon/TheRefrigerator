using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;

namespace TheRefrigerator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //הגדרת מקרר עם 2 מדפים כל מדף מכיל פריטים
            DateTime d = new DateTime(2024, 02, 02);
            DateTime d2 = new DateTime(2002, 02, 02);
            DateTime d3 = new DateTime(2023, 10, 20);
            DateTime d4 = new DateTime(2011, 04, 03);
            Item x1 = new Item("cheese1", 3, eType.food, eCosher.milk, d, 21.7);
            Item x2 = new Item("cheese2", 3, eType.food, eCosher.milk, d2, 4.3);
            Shelf y = new Shelf(2, 25);
            y.ItemsList.Add(x1);
            y.ItemsList.Add(x2);


            Item x3 = new Item("cheese3", 3, eType.food, eCosher.milk, d3, 10);
            Item x4 = new Item("cheese4", 3, eType.food, eCosher.milk, d4, 2.5);
            Shelf y2 = new Shelf(4, 25);
            y2.ItemsList.Add(x3);
            y2.ItemsList.Add(x4);

            Refrigerator r = new Refrigerator("Samsung", "gray", 2);
            r.ShelvesList.Add(y);
            r.ShelvesList.Add(y2);

            Shelf y3 = new Shelf(4, 15);
            Shelf y4=new Shelf(5, 10);

            Refrigerator r2 = new Refrigerator("LG", "White", 3);
            r2.ShelvesList.Add(y);
            r2.ShelvesList.Add(y3);
            r2.ShelvesList.Add(y4);

            Console.WriteLine("Press 1: Print all the items on the refrigerator and all its contents.\r\nPress 2: Print how much space is left in the refrigerator\r\nPress 3: Add an item to the refrigerator .\r\nPress 4: Remove an item from the refrigerator.\r\nPress 5: Clean the refrigerator and print all the checked items.\r\nPress 6: What do I want to eat? \r\nPress 7: Print all the products sorted by their expiration date.\r\nPress 8: Print all the shelves arranged according to the free space left on them.\r\nPress 9: Print all the refrigerators arranged according to the free space left in them.\r\nPress 10: Prepare the refrigerator for shopping\r\nPress 100: system shutdown.");
            int UserChoose;
            
            
            do
            {
                Console.WriteLine("CHOOSE COMMAND NUMBER!");
                UserChoose = int.Parse(Console.ReadLine());
                switch (UserChoose)
                {
                    case 1:
                        {
                            Console.WriteLine(r.ToString());
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine(r.PlaceLeft());
                            break;
                        }
                    case 3:
                        {

                            r.AddItem();
                            break;

                        }
                    case 4:
                        {
                            Console.WriteLine("Enter the item Id!");
                            int ItemId = int.Parse(Console.ReadLine());
                            Console.WriteLine(r.TakeItemOut(ItemId));
                            break;
                        }
                    case 5:
                        {
                            r.DeleteExpired();
                            Console.WriteLine(r.ToString());
                            break;

                        }
                    case 6:
                        {
                            Console.WriteLine("WHat should I eat?  Enter type anf cosher!");
                            eCosher cosher = 0;
                            eType type = 0;
                            Refrigerator.StringToEnumParser(ref type, ref cosher);
                            
                            if(r.WhatShouldIEat(type, cosher)!=null)
                                Console.WriteLine(r.WhatShouldIEat(type, cosher));
                            break;
                        }
                    case 7:
                        {
                            (r.SortByExpiryDate()).ForEach(item => Console.WriteLine(item));
                            break;

                        }

                    case 8:
                        {

                            (r.SortShelfByPlaceLeft()).ForEach(item => Console.WriteLine(item));
                            break;

                        }
                    case 9:
                        {
                            Refrigerator.SortFrigeByPlaceLeft().ForEach(item => Console.WriteLine(item));
                            break;


                        }
                    case 10:
                        {
                            r.Shopping();
                            break;
                        }
                    case 100:
                        {
                            Console.WriteLine("The system is turned OFF!");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("ERROR:no Command found!");
                            break;
                        }

                }
            }
            while (UserChoose != 100);







        }
    }
}
