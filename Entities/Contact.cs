using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EfConcurrencyTest.Entities
{
    public class Contact
    {
        public Contact(string number, ContactType contactType)
        {
            Number = number;
            ContactType = contactType;
        }

        public Contact(int contactId, string number, ContactType contactType)
        {
            ContactId = contactId;
            Number = number;
            ContactType = contactType;
        }

        public int ContactId { get; private set; }
        public string Number { get; private set; }
        public ContactType ContactType { get; private set; }

        public void ChangeNumber(string newNumber)
        {
            Number = newNumber;
        }
    }

    public enum ContactType
    {
        Phone,
        Cellphone,
    }
}
