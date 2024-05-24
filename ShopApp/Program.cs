// Liam Kandel
using ShopApp.Models;

namespace ShopApp
// I apologize in advance for how ugly this file is, theres a ton of repeated code and the indentation is really bad

{
    internal class Program
    {
        public static Inventory inventory = new Inventory();
        public static Shop shop = new Shop(inventory);

        static void Main(string[] args)
        {


            while (true) // Main program loop
            {
                MainMenu(); // Prints welcome message 
                string? choice = Console.ReadLine();


                while (choice != "1" && choice != "2" && choice != "3")
                {
                    Console.WriteLine("Invalid choice, try again");
                    Console.Write(">");
                    choice = Console.ReadLine();
                }

                if (choice == "1") // All inventory functionality here
                {
                    InventoryMenu(); // Print inventory menu

                    Console.Write(">");
                    choice = Console.ReadLine();


                    int.TryParse(choice, out int value);
                    switch (value)
                    {
                        case 1: // Create item in inventory 
                            Console.WriteLine("Enter a name for the item:");
                            string? name = Console.ReadLine() ?? "";

                            Console.WriteLine($"Enter a description for {name}");
                            string? description = Console.ReadLine() ?? "";

                            double price;
                            do
                            {
                                Console.WriteLine($"Enter a price for {name}:");

                            } while (!double.TryParse(Console.ReadLine(), out price));


                            int stock;
                            do
                            {
                                Console.WriteLine($"Enter current stock for {name}:");

                            } while (!int.TryParse(Console.ReadLine(), out stock));

                            inventory.Create(new Item { Name = name, Description = description, Price = price, Stock = stock });
                            Console.WriteLine($"{name} Successfully added");

                            Console.ReadKey(); // Wait for user input
                            Console.Clear(); // Clear the console

                            break;
                        case 2: // Read item in inventory
                            Console.WriteLine("Enter an item ID to read");
                            Console.Write(">");

                            string? id = Console.ReadLine() ?? "";
                            int intId;

                            while (!int.TryParse(id, out intId))
                            {
                                Console.WriteLine("Invalid input. Try again");
                                Console.Write(">");
                                id = Console.ReadLine();
                            }

                            Console.Clear();
                            Console.WriteLine("ID\tName\t\tPrice($)\tStock");

                            if (!inventory.Read(intId))
                            {
                                Console.WriteLine("Read unsuccessful");
                                Console.ReadKey();
                                break;
                            }

                            Console.WriteLine(inventory.getItem(intId).ToString() + inventory.getItem(intId).Description);
                            break;

                        case 3: // Update item in inventory
                            {
                                Console.WriteLine("Enter an item ID to update");
                                Console.Write(">");

                                id = Console.ReadLine();
                                while (!int.TryParse(id, out intId))
                                {
                                    Console.WriteLine("Invalid input. Try again");
                                    Console.Write(">");
                                    id = Console.ReadLine();
                                }

                                Console.Clear();
                                Console.WriteLine("ID\tName\t\tPrice($)\tStock");

                                Console.WriteLine(inventory.getItem(intId).ToString() + inventory.getItem(intId).Description);

                                if (!inventory.Read(intId))
                                {
                                    Console.WriteLine("Read unsuccessful");
                                    Console.ReadKey();
                                    break;
                                }

                                Console.WriteLine("Enter a name for the item:");
                                name = Console.ReadLine() ?? "";
                                Console.WriteLine($"Enter a description for {name}");
                                description = Console.ReadLine() ?? "";

                                do
                                {
                                    Console.WriteLine($"Enter a price for {name}:");

                                } while (!double.TryParse(Console.ReadLine(), out price));


                                do
                                {
                                    Console.WriteLine($"Enter current stock for {name}:");

                                } while (!int.TryParse(Console.ReadLine(), out stock));

                                inventory.Update(intId, new Item { Name = name, Description = description, Price = price, Stock = stock });
                            }
                            break;

                        case 4: // Delete an item
                            Console.WriteLine("Enter an item ID to delete");
                            Console.Write(">");

                            id = Console.ReadLine();
                            while (!int.TryParse(id, out intId))
                            {
                                Console.WriteLine("Invalid input. Try again");
                                Console.Write(">");
                                id = Console.ReadLine();
                            }

                            Console.Clear();
                            Console.WriteLine("ID\tName\t\tPrice($)\tStock");

                            if (!inventory.Read(intId))
                            {
                                Console.WriteLine("Delete unsuccessful");
                                Console.ReadKey();
                                break;
                            }

                            Console.WriteLine(inventory.getItem(intId).ToString() + inventory.getItem(intId).Description);

                            Console.WriteLine("Delete? y/N");
                            string? shouldDelete = Console.ReadLine() ?? "n";
                            if (shouldDelete?.ToLower() != "y" || !inventory.Delete(intId))
                            {
                                Console.WriteLine("Delete unsucessful");
                                break;
                            }

                            Console.WriteLine("Delete successful");
                            Console.ReadKey();
                            break;
                        case 5:
                            Console.Clear();
                            Console.WriteLine("ID\tName\t\tPrice($)\tStock");
                            inventory.Print();
                            Console.ReadKey();
                            break;
                        case 6:
                            break;
                    }

                }

                else if (choice == "2")
                {
                    ShopMenu();

                    Console.Write(">");
                    choice = Console.ReadLine();

                    if (int.TryParse(choice, out int value))
                    {
                        switch (value)
                        {
                            case 1: // Add to cart
                                Console.WriteLine("Enter item ID to add to cart");
                                Console.Write(">");

                                int id;
                                while (!int.TryParse(Console.ReadLine(), out id))
                                    Console.WriteLine("Invalid input");

                                if (!inventory.Read(id))
                                {
                                    Console.WriteLine("Item not found");
                                    break;
                                }

                                if (!shop.AddToCart(id))
                                {
                                    Console.WriteLine("Item not added to cart");
                                    break;
                                }

                                Console.WriteLine("Item added to cart!");
                                break;
                            case 2:

                                Console.WriteLine("Enter item ID to remove from cart");
                                Console.Write(">");

                                while (!int.TryParse(Console.ReadLine(), out id))
                                    Console.WriteLine("Invalid input");

                                if (!inventory.Read(id))
                                {
                                    Console.WriteLine("Item not found");
                                    break;
                                }

                                if (!shop.RemoveFromCart(id))
                                {
                                    Console.WriteLine("Item not removed from cart");
                                    break;
                                }

                                Console.WriteLine("Item removed from cart!");
                                break;
                            case 3:
                                shop.CheckOut();
                                break;

                        }
                    }
                }
                else if (choice == "3")
                    break;

            }
        }

        static void InventoryMenu()
        {
            Console.WriteLine("1. Create item");
            Console.WriteLine("2. Read item");
            Console.WriteLine("3. Update item");
            Console.WriteLine("4. Delete item");
            Console.WriteLine("5. View Inventory");
            Console.WriteLine("6. Back to Menu");
        }
        static void MainMenu()
        {
            Console.WriteLine("Welcome! Please select an option:");
            Console.WriteLine("1. Manage Inventory");
            Console.WriteLine("2. Shop");
            Console.Write("> ");
        }
        static void ShopMenu()
        {
            Console.WriteLine("1. Add to Cart");
            Console.WriteLine("2. Remove from cart");
            Console.WriteLine("3. Check out");
            Console.WriteLine("4. Back to Menu");
        }
    }
}

