using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TheRefrigerator;

namespace TheRefrigerator
{
    public class Refrigerator
    {
        //A counter to ensure an unique ID
        public static int Counter = 1;
        public int Id { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public int NumOfShells { get; set; }
        public List<Shelf> ShelvesList { get; set; }
        public static List<Refrigerator> All = new List<Refrigerator>();

        public Refrigerator(string model, string color, int numOfShells)
        {
            Id = Counter++;
            Model = model;
            Color = color;
            NumOfShells = numOfShells;
            ShelvesList = new List<Shelf>();
            All.Add(this);


        }

        public override string ToString()
        {
            string x = "";
            x += Color + " Refrigerator, model " + Model + " with " + NumOfShells + " shelves: " + Environment.NewLine;
            foreach (Shelf sh in ShelvesList)
            {
                x += sh.ToString();
                x += "  ";

            }
            return x;
        }

        public double PlaceLeft()
        {
            double sum = 0;
            foreach (Shelf sh in ShelvesList)
            {
                sum += sh.PlaceLeft();
            }
            return sum;
        }

        public void AddItem()
        {
            Console.WriteLine("Now we are going to add an item to the refrigerator!" + Environment.NewLine
                + " Please enter details: Name, Type, Cosher, ExpiryDate And Size (In cm)");

            string Name = Console.ReadLine();
            int shelfNum = 0;


            eType etype = 0;
            eCosher eCosher = 0;
            StringToEnumParser(ref etype, ref eCosher);

            DateTime date = DateTime.Parse(Console.ReadLine());
            double Size = double.Parse(Console.ReadLine());
            Item NewItem = null;
            foreach (Shelf sh in ShelvesList)
            {
                if (sh.PlaceLeft() >= Size)
                {
                    shelfNum = sh.Id;
                    NewItem = new Item(Name, shelfNum, etype, eCosher, date, Size);
                    break;
                }

            }
            if (shelfNum == 0)
            {
                Console.WriteLine("ERROR:Not enough place!");
                return;
            }


            //Console.WriteLine("----------- " + NewItem);

            Shelf chosenShelf = ShelvesList.Find(x => x.Id == shelfNum);
            chosenShelf.ItemsList.Add(NewItem);
            Console.WriteLine(chosenShelf);


        }

        public static void StringToEnumParser(ref eType etype, ref eCosher ecosher)
        {
            bool IsValidType = false;
            string type;
            do
            {
                Console.WriteLine("You can only choose food  or drink!");
                type = Console.ReadLine();
                type.ToLower();
                IsValidType = Enum.TryParse(type, out eType hlp);
            }

            while (!IsValidType);

            etype = (eType)Enum.Parse(typeof(eType), type);


            bool IsValidCosher = false;
            string cosher;
            do
            {
                Console.WriteLine("You can only choose milk, meat or parve!");
                cosher = Console.ReadLine();
                cosher.ToLower();
                IsValidCosher = Enum.TryParse(cosher, out eCosher hlp);
            }

            while (!IsValidCosher);

            ecosher = (eCosher)Enum.Parse(typeof(eCosher), cosher);


        }

        public Item TakeItemOut(int Id)
        {
            foreach (Shelf item in ShelvesList)
            {
                Item toDelete = item.ItemsList.Find(x => x.Id == Id);
                if (toDelete != null)
                {
                    item.ItemsList.Remove(toDelete);
                    return toDelete;

                }

            }
            Console.WriteLine("Item not found!");
            return null;

        }

        public void DeleteExpired()
        {
            List<Item> ToDelete, AllDeletedItems = new List<Item>();

            int i = 0;
            foreach (Shelf item in ShelvesList)
            {
                ToDelete = item.ItemsList.Where(x => x.ExpiryDate < DateTime.Now).ToList();
                AllDeletedItems.Add(ToDelete[0]);
                item.ItemsList.Remove(ToDelete[0]);
            }
            Console.WriteLine("All deleted items");
            AllDeletedItems.ForEach(item => Console.WriteLine(item));
            Console.WriteLine("Delete Expired Done!");
        }

        public Item WhatShouldIEat(eType type, eCosher cosher)
        {

            foreach (Shelf item in ShelvesList)
            {
                List<Item> ToEat = item.ItemsList.Where(x => x.Cosher == cosher && x.Type == type && x.ExpiryDate >= DateTime.Now).ToList();

                if (ToEat.Count != 0)
                    return ToEat[0];

            }
            Console.WriteLine("There is no match item!");
            return null;
        }



        public List<Item> SortByExpiryDate()
        {

            List<Item> allItems = new List<Item>();
            foreach (Shelf item in ShelvesList)
            {
                foreach (Item item2 in item.ItemsList)
                {
                    allItems.Add(item2);
                }
            }

            List<Item> sortItems = allItems.OrderBy(item => item.ExpiryDate).ToList();



            return sortItems;
        }




        public List<Shelf> SortShelfByPlaceLeft()
        {

            List<Shelf> sortShelves = ShelvesList.OrderByDescending(item => item.PlaceLeft()).ToList();
            return sortShelves;

        }

        public static List<Refrigerator> SortFrigeByPlaceLeft()
        {
            List<Refrigerator> sortFridges = All.OrderByDescending(item => item.PlaceLeft()).ToList();
            return sortFridges;


        }

        public void Shopping()
        {
            double sum = 0;
            if (CheckIfCanShopping(this.PlaceLeft()))
                return;


            else
            {
                this.DeleteExpired();

                if (CheckIfCanShopping(this.PlaceLeft()))
                    return;

                List<int> milksId = new List<int>(),
                    meatsId = new List<int>(),
                    parvesId = new List<int>();


                foreach (Shelf shelf in ShelvesList)
                {

                    foreach (Item item in shelf.ItemsList)
                    {
                        if (item.Cosher == eCosher.milk && item.ExpiryDate.AddDays(-3) <= DateTime.Now)
                        {
                            
                            milksId.Add(item.Id);
                            sum += item.Size;
                        }

                        if (item.Cosher == eCosher.meat && item.ExpiryDate.AddDays(-7) <= DateTime.Now)
                        {
                            meatsId.Add(item.Id);
                            sum += item.Size;
                        }

                        if (item.Cosher == eCosher.parve && item.ExpiryDate.AddDays(-1) <= DateTime.Now)
                        {
                            parvesId.Add(item.Id);
                            sum += item.Size;
                        }

                    }


                }
                if (CheckIfCanShopping(sum))
                    return;

                DeleteForShopping(milksId);

                if (CheckIfCanShopping(sum))
                    return;

                DeleteForShopping(meatsId);

                if (CheckIfCanShopping(sum))
                    return;

                DeleteForShopping(meatsId);
                return;





            }

        }

        public bool CheckIfCanShopping(double sum)
        {
            if (sum >= 20)
                Console.WriteLine("You can go shopping!");

            return sum >= 20;
        }

        public void DeleteForShopping(List<int> ItemsId)
        {
            int length;
           
            foreach (Shelf shelf in ShelvesList)
            {
                length = shelf.ItemsList.Count;
                for (int i = 0; i < length; i++)
                {

                    if (i<ItemsId.Count && shelf.ItemsList.Count>0&& ItemsId.Contains(shelf.ItemsList[i].Id))
                    {
                        Console.WriteLine("Deleted: " + shelf.ItemsList[i].Name);
                        shelf.ItemsList.Remove(shelf.ItemsList[i]);
                        i--;

                    }
                }


            }
        }






    }
}
