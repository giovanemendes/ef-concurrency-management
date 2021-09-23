using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfConcurrencyTest.Entities
{
    public class Person
    {
        public Person(string name, int age, string document)
        {
            Name = name;
            Age = age;
            Document = document;
        }

        public Person(int personId, string name, int age, string document)
        {
            PersonId = personId;
            Name = name;
            Age = age;
            Document = document;
        }

        private Person()
        {
        }

        public Person(int personId, string name, int age, string document, Contact contact)
        {
            PersonId = personId;
            Name = name;
            Age = age;
            Document = document;
            Contact = contact;
        }

        public int PersonId { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Document { get; private set; }
        public Contact Contact { get; set; }

        public void AlterDocument(string document)
        {
            Document = document;
        }

        public void AlterName(string newName)
        {
            Name = newName;
        }

        public void AlterAge(int age)
        {
            Age = age;
        }

    }
}
