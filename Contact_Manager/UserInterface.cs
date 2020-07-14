using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace Contact_Manager
{
    class UserInterface
    {
        public static void Start()
        {
            ContactManager.InitPhoneBook();
            Console.WriteLine("Hello.");
            Menu();

        }

        public static void Menu()
        {
            Console.WriteLine();
            Console.WriteLine("What would you like to do? (Write a number)");

            Console.WriteLine("===========================================");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("1. Add new contact");
                Console.WriteLine("2. Show full list of contacts");
                Console.WriteLine("3. Add additional number for an existing contact");
                Console.WriteLine("4. Update contact information");
                Console.WriteLine("5. Delete a contact");
                Console.WriteLine("6. Exit");

                try
                {
                    int input = int.Parse(Console.ReadLine());

                    switch (input)
                    {
                        case 1:
                            Console.Clear();
                            ContactManager.AddNewContact();
                            Menu();
                            break;
                        case 2:
                            Console.Clear();
                            ContactManager.ListAllContacts();
                            Menu();
                            break;
                        case 3:
                            Console.Clear();
                            ContactManager.AddNewNumber();
                            Menu();
                            break;
                        case 4:
                            Console.Clear();
                            ContactManager.UpdateContact();
                            Menu();
                            break;
                        case 5:
                            Console.Clear();
                            ContactManager.DeleteContact();
                            Menu();
                            break;
                        case 6:
                            Environment.Exit(0);
                            break;
                        default:
                            Console.WriteLine("Invalid input. Choose an option from menu");
                            break;
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Choose an option from menu");
                }
            }



        }

    }
}
