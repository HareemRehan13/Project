using Eproject_NCS.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Eproject_NCS.Controllers
{
	public class HomeController : Controller
	{
		private readonly NexusContext db;
		public HomeController(NexusContext _db)
		{
			db = _db;
		}


		[Authorize(Roles = "User")]
		public IActionResult Index()
		{
			return View();
		}
		[Authorize]
		public IActionResult Privacy()
		{
			return View();
		}

		
	}
}
