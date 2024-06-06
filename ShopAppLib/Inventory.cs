// Liam Kandel
using ShopApp.Models;

namespace ShopApp
{
    internal class Inventory
    {
        private List<Item>? items = new List<Item>();
        public Inventory() { }
        private object lockObject = new object();
        public void Create(Item item)
        {
            if (items == null)
                return;

            lock (lockObject)
            {
                int newId;
                if (items.Any()) // If there are any items in the list
                {
                    // The new item ID is the max ID value found in each item in the items list plus 1
                    newId = items.Select(x => x.Id).Max() + 1;
                }
                else
                {
                    // If first item in the list, make its ID 1
                    newId = 1;
                }

                item.Id = newId;
                items.Add(item);
            }
        }
        public bool Read(int id)
        {

            if (items == null)
            { return false; }
            // Search through the items in the list until an item's ID matches the index
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null)
            { return false; }
            else { return true; }

        }
        public bool Update(int id, Item itemUpdated)
        {
            if (items == null)
                return false;

            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) return false;

            itemUpdated.Id = item.Id;

            int index = items.IndexOf(item);
            items[index] = itemUpdated;

            return true;
        }
        public bool Delete(int id)
        {
            if (items == null)
                return false;
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) { return false; }
            items.Remove(item);
            return true;
        }
        public void Print()
        {
            if (items == null)
            {
                Console.WriteLine("Inventory is empty.");
            }
            else
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item.ToString());
                }

            }
        }

        public Item getItem(int id)
        {
            if (items == null) { return new Item(); }
            var item = items.FirstOrDefault(i => i.Id == id);
            if (item == null) { return new Item(); }

            return item;
        }
    }
}

