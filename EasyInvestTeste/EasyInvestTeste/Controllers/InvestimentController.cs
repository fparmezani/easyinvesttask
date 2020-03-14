using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyInvestTeste.Controllers
{
    public class InvestimentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}