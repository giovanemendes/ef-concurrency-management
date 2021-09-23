using EfConcurrencyTest.Database;
using EfConcurrencyTest.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EfConcurrencyTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Intializing program..");

            TestDettachedEntityAsAggregate();
        }

        public static void Seed()
        {
            var context = new Context();
            var person = new Person("Giovane", 31, "12345");
            context.People.Add(person);
            context.SaveChanges();
        }

        public static void TestDettachedEntity()
        {
            try
            {
                var person = new Person(1, "Giovane", 31, "12345");

                var context = new Context();
                context.Attach(person);

                person.AlterName("Giovane Mendes Costa");

                var contextTwo = new Context();
                var personDb = contextTwo.People.Find(1);
                personDb.AlterName("Giovane Mendes");
                contextTwo.SaveChanges();

                context.SaveChanges();

                Console.WriteLine($"Name changed: { person.Name }");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Error \n Wasn't possible change the name \n");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        public static void TestDettachedEntityAsAggregate()
        {
            try
            {
                var contact = new Contact(2, "00022326565", ContactType.Cellphone);
                var person = new Person(1, "Giovane", 31, "12345", contact);

                var context = new Context();
                context.Attach(person);

                person.Contact.ChangeNumber("99987456");

                var contextTwo = new Context();
                var personDb = contextTwo.People.Include(x => x.Contact).FirstOrDefault();
                personDb.Contact.ChangeNumber("6645448848");
                contextTwo.SaveChanges();

                context.SaveChanges();

                Console.WriteLine($"Name changed: { person.Name }");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Error \n Wasn't possible change the name \n");

                ShowEntitieDiff(ex);
            }

            Console.ReadLine();
        }

        public static void TestTrackedEntity()
        {
            try
            {
                var context = new Context();
                var personDb = context.People.Find(1);
                personDb.AlterName("Giovane Mendes");

                var contextTwo = new Context();
                var personDbTwo = contextTwo.People.Find(1);
                personDbTwo.AlterName("Giovane Mendes Costa");
                contextTwo.SaveChanges();

                context.SaveChanges();

                Console.WriteLine($"Name changed: { personDbTwo.Name }");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Error \n Wasn't possible change the name \n");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        public static void TestAlterPropertyNonCheckedTrackedEntity()
        {
            try
            {
                var contextTwo = new Context();
                var personDb = contextTwo.People.Find(1);
                personDb.AlterName("Giovane");

                var contextThree = new Context();
                var personDbTwo = contextThree.People.Find(1);
                personDbTwo.AlterAge(32);
                personDbTwo.AlterDocument("321");
                contextThree.SaveChanges();

                contextTwo.SaveChanges();

                Console.WriteLine($"Name changed: { personDbTwo.Name }");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Error \n Wasn't possible change the name \n");
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }

        public static void ShowEntitieDiff(DbUpdateConcurrencyException ex)
        {
            foreach (var entry in ex.Entries)
            {
                var currentValues = entry.CurrentValues;
                var originalValues = entry.OriginalValues;
                var databaseValues = entry.GetDatabaseValues();

                foreach (var property in currentValues.Properties)
                {
                    if (!originalValues[property].Equals(currentValues[property]))
                    {
                        Console.WriteLine($"The property [{ property.PropertyInfo.Name }] was changed since last time.");
                        Console.WriteLine($"Expected [{ originalValues[property] }] but founded [{ currentValues[property] }]");
                    }
                }
            }
        }
    }
}
