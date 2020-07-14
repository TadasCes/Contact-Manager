using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Contact_Manager
{
    class ContactManager
    {
        public static List<Person> PhoneBook;

        public static string workingDirectory = Environment.CurrentDirectory;
        public static string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;
        public static string filePath = @"" + projectDirectory + "\\Contacts.txt";

        public static void InitPhoneBook()
        {
            if (File.Exists(filePath) && new FileInfo(filePath).Length != 0)
            {
                PhoneBook = ReadContactsFromFile();
            }
            else
            {
                PhoneBook = new List<Person>();
            }
        }

        public static void AddNewContact()
        {
            Console.WriteLine("First name: ");
            string firstName = Console.ReadLine();

            Console.WriteLine("last name: ");
            string lastName = Console.ReadLine();

            List<long> phoneNumbers = new List<long>();
            Console.WriteLine("Phone number: ");
            while (true)
            {
                long phoneNumber = long.Parse(Console.ReadLine());
                if (IsNumberFree(phoneNumber))
                {
                    phoneNumbers.Add(phoneNumber);
                    break;
                }
                else
                {
                    Console.WriteLine("Number already in use. Please try again.");
                }
            }

            Console.WriteLine("Address (optional - leave blank): ");
            string address = Console.ReadLine();


            Person person;
            if (string.IsNullOrEmpty(address))
            {
                person = new Person(firstName, lastName, phoneNumbers);
            }
            else
            {
                person = new Person(firstName, lastName, phoneNumbers, address);
            }

            PhoneBook.Add(person);
            UpdateFile();
        }

        public static void AddNewNumber()
        {
            Console.WriteLine("Choose a contact you want to add new number to. Write a number ");
            Person personToUpdate = GetPersonToUpdate();

            Console.WriteLine("Write new number: ");
            long numberInput = long.Parse(Console.ReadLine());

            if (IsNumberFree(numberInput))
            {
                personToUpdate.PhoneNumbers.Add(numberInput);
                Console.WriteLine("Number added successfully");
            }
            else Console.WriteLine("Number already in use.Please try again.");

            UpdateFile();
        }

        public static void ListAllContacts()
        {
            foreach (Person contact in PhoneBook)
            {
                Console.WriteLine("First name: " + contact.FirstName);
                Console.WriteLine("Last name: " + contact.LastName);
                Console.WriteLine("Phone numbers: ");
                contact.PhoneNumbers.ForEach(number =>
                    Console.WriteLine("\t {0}. {1}", contact.PhoneNumbers.IndexOf(number) + 1, number.ToString())
                    );
                if (!string.IsNullOrEmpty(contact.Address))
                    Console.WriteLine("Address: " + contact.Address);

                Console.WriteLine();
            }
        }

        public static bool IsNumberFree(long number)
        {
            foreach (Person person in PhoneBook)
            {
                foreach (long phoneNumber in person.PhoneNumbers)
                {
                    if (number == phoneNumber) return false;
                }
            }
            return true;
        }

        public static void UpdateContact()
        {
            Console.WriteLine("Choose a contact you want to update. Write a number ");

            Person personToUdpate = GetPersonToUpdate();

            Console.WriteLine("What would you like to update?");
            Console.WriteLine("1. First name");
            Console.WriteLine("2. Last name");
            Console.WriteLine("3. Remove a number");

            int input = int.Parse(Console.ReadLine());

            switch (input)
            {
                case 1:
                    ChangeFirstName(personToUdpate);
                    break;
                case 2:
                    ChangeLastName(personToUdpate);
                    break;
                case 3:
                    if (personToUdpate.PhoneNumbers.Count == 1)
                    {
                        Console.WriteLine("You can delete number if contact has only one. You can only delete contact");
                        break;
                    }
                    else
                    {
                        RemoveANumber(personToUdpate);
                    }
                    break;
                default:
                    Console.WriteLine("Invalid input. Choose an option from menu");
                    break;
            }
        }

        public static void ChangeFirstName(Person person)
        {
            Console.WriteLine("Write new first name: ");
            string newName = Console.ReadLine();
            person.FirstName = newName;
            UpdateFile();
        }

        public static void ChangeLastName(Person person)
        {
            Console.WriteLine("Write new last name: ");
            string newName = Console.ReadLine();
            person.LastName = newName;
            UpdateFile();
        }

        public static void RemoveANumber(Person person)
        {
            Console.WriteLine("Choose a which number to remove");

            foreach (long number in person.PhoneNumbers)
            {
                Console.WriteLine(person.PhoneNumbers.IndexOf(number) + ". " + number);
            }

            int index = int.Parse(Console.ReadLine());
            person.PhoneNumbers.RemoveAt(index);
        }

        public static void DeleteContact()
        {
            Console.WriteLine("Choose a which contact to delete");
            Person person = GetPersonToUpdate();
            PhoneBook.Remove(person);
            Console.WriteLine("Contact removed!");
        }

        public static Person GetPersonToUpdate()
        {
            while (true)
            {
                try
                {
                    foreach (Person person in PhoneBook)
                    {
                        Console.WriteLine(PhoneBook.IndexOf(person) + ". " + person.FirstName.Trim() + " " + person.LastName.Trim());
                    }
                    int index = int.Parse(Console.ReadLine());

                    return PhoneBook[index];
                }
                catch (Exception)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please choose a number from a list");
                }
            }
        }

        private static void UpdateFile()
        {
            string fullText = "";
            foreach (Person contact in PhoneBook)
            {
                string numbers = "";
                foreach (long number in contact.PhoneNumbers)
                {
                    numbers += number.ToString() + "\n";
                }
                string addToFullText = String.Format($"First name: {contact.FirstName} \n" +
                    $"Last name: {contact.LastName} \n" +
                    $"Phone numbers: \n{numbers}");

                if (!String.IsNullOrEmpty(contact.Address))
                {
                    addToFullText += "Address: " + contact.Address + "\n";
                }
                addToFullText += "\n";

                fullText += addToFullText;
            }
            File.WriteAllText(
                filePath,
                fullText);
        }

        private static List<Person> ReadContactsFromFile()
        {
            List<Person> PhoneBook = new List<Person>();
            string result = File.ReadAllText(filePath);
            string[] contacts = result.Split("\n\n", StringSplitOptions.RemoveEmptyEntries);


            foreach (string contact in contacts)
            {
                string[] contactInfo = contact.Split("\n");
                string[] firstName = contactInfo[0].Split(": ");
                string[] lastName = contactInfo[1].Split(": ");
                string[] address = new string[2];
                List<long> phoneNumbers = new List<long>();

                for (int i = 3; i < contactInfo.Length; i++)
                {
                    string currentLine = contactInfo[i].Trim();
                    if (!(currentLine[0] == char.Parse("A") || currentLine[0].Equals("\n")))
                    {
                        long number = long.Parse(currentLine);
                        phoneNumbers.Add(number);
                    }
                    else
                    {
                        address = contactInfo[i].Split(": ");
                    }
                }

                Person newPerson;
                if (!string.IsNullOrEmpty(address[1]))
                {
                    newPerson = new Person(
                        firstName[1],
                        lastName[1],
                        phoneNumbers,
                        address[1]
                        );
                    PhoneBook.Add(newPerson);
                }
                else
                {
                    newPerson = new Person(
                        firstName[1],
                        lastName[1],
                        phoneNumbers
                    );
                    PhoneBook.Add(newPerson);
                }
            }
            return PhoneBook;
        }
    }
}
