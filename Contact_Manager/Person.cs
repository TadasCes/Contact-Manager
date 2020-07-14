using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Contact_Manager
{
    class Person
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        public long PhoneNumber { get; set; }
        public List<long> PhoneNumbers = new List<long>();

        public string Address { get; set; }

        public Person(string firstName, string lastName, List<long> phoneNumbers)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumbers = phoneNumbers;
        }

        public Person(string firstName, string lastName, List<long> phoneNumbers, string address)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.PhoneNumbers = phoneNumbers;
            this.Address = address;
        }

        public void AddNewNumber(long phoneNumber)
        {
            this.PhoneNumbers.Add(phoneNumber);
        }

        public void ListAllNumbersOfAPerson()
        {
            foreach (long number in PhoneNumbers)
            {
                Console.WriteLine(number);
            }
        }
    }
}
