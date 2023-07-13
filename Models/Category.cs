using System;
using System.ComponentModel.DataAnnotations;

namespace MachineTest.Models
{
	public class Category
	{
		[Key]
		public int CategoryId { get; set; }

		[Required]
		public String CategoryName { get; set; }
	}
}
