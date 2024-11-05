using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store4.Core.Entities.Orders
{
	public class Address
	{
        public Address()
        {
            
        }
        public Address(int fName, string lName, string city, string street, string country)
		{
			FName = fName;
			LName = lName;
			City = city;
			Street = street;
			this.country = country;
		}

		public int FName { get; set; }
        public string LName { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string country { get; set; }
    }
}
