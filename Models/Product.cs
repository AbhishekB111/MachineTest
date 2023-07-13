using System.ComponentModel.DataAnnotations;

namespace MachineTest.Models
{
	public class Product
	{
		[Key]
		public int ProductId { get; set; }


		[Display(Name = "Product Name")]
		public string ProductName { get; set; }

		public int CategoryId { get; set; }
	}
}
