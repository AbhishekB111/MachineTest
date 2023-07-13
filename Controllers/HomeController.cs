using MachineTest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using System.Linq;
using X.PagedList;

namespace MachineTest.Controllers
{
	public class HomeController : Controller
	{
		private readonly AppDbContext _Db;

		public HomeController(AppDbContext Db)
		{
			_Db = Db;
		}
		public IActionResult Index()
		{
			return View();
		}

		#region "Product"
		public IActionResult Products()
		{
			var ProductList = _Db.Products;

			return View(ProductList);
		}

		[HttpGet]
		public IActionResult AddProductView(Product product)
		{

			ViewBag.CategoryList = _Db.Categories;

			if (product.ProductId > 0)
			{
				return View(product);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(Product product)
		{
			if (product.ProductId <= 0)
			{
				_Db.Products.Add(product);
				await _Db.SaveChangesAsync();
				return RedirectToAction("Products");
			}
			else
			{
				_Db.Entry(product).State = EntityState.Modified;
				await _Db.SaveChangesAsync();
				return RedirectToAction("Products");
			}
		}

		public async Task<IActionResult> DeleteProduct(int ProductId)
		{
			var Product = await _Db.Products.FindAsync(ProductId);
			_Db.Products.Remove(Product);
			await _Db.SaveChangesAsync();
			return RedirectToAction("Products");
		}
		#endregion

		#region "Category"

		[HttpGet]
		public IActionResult Categories()
		{
			var categoryList = _Db.Categories;
			return View(categoryList);
		}

		[HttpGet]
		public IActionResult AddCategoryView(Category category)
		{
			if (category != null)
			{
				return View(category);
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AddCategory(Category category)
		{
			if (category.CategoryId <= 0)
			{
				_Db.Categories.Add(category);
				await _Db.SaveChangesAsync();
			}
			else
			{
				_Db.Entry(category).State = EntityState.Modified;
				await _Db.SaveChangesAsync();
			}
			return RedirectToAction("Categories");
		}

		public async Task<IActionResult> DeleteCategory(int CategoryId)
		{
			var category = await _Db.Categories.FindAsync(CategoryId);
			if (category != null)
			{
				_Db.Categories.Remove(category);
				await _Db.SaveChangesAsync();
			}
			return RedirectToAction("Categories");
		}
		#endregion

		#region "ProductList"

		public IActionResult ProductList(int? PageSize, int? PageNo)
		{
			BindPagesize();
			BindPageNo();
			var ProductList = from p in _Db.Products
							  join c in _Db.Categories
							  on p.CategoryId equals c.CategoryId
							  into pc
							  from c in pc.DefaultIfEmpty()

							  select new ProductList
							  {
								  ProductId = p.ProductId,
								  CategoryId = p.CategoryId,
								  ProductName = p.ProductName,
								  CategoryName = c == null ? "" : c.CategoryName
							  };
			if (PageSize > 0 && PageNo > 0)
			{
				var list = ProductList.Where(model => model.ProductId > (PageSize * PageNo) - PageSize && model.ProductId <= (PageSize * PageNo)).ToList();
				return View(list);
			}
			return View(ProductList.ToList().ToPagedList(PageNo ?? 1, PageSize ?? 5));
		}

		private void BindPagesize()
		{
			var PageSize = new List<Int32>();
			for (int i = 1; i <= 10; i++)
			{
				PageSize.Add(i * 5);
			}
			ViewBag.PageSize = PageSize;

		}
		private void BindPageNo()
		{
			var PageNo = new List<Int32>();
			for (int i = 1; i <= 10; i++)
			{
				PageNo.Add(i);
			}
			ViewBag.PageNo = PageNo;
		}
		#endregion
	}
}
