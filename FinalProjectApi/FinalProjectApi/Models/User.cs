using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProjectApi.Models
{
    public class User : BaseItem
    {
		public int EmployeeCardNumber { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Password { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public float CardNumber { get; set; }
		public string Token { get; set; }
		public decimal Balance { get; set; }
	}
}
