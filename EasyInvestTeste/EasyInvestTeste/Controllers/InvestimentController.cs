using EasyInvestTeste.Services.Implementation;
using Microsoft.AspNetCore.Mvc;

namespace EasyInvestTeste.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvestimentController : Controller
    {
        [HttpGet]
        public ActionResult Get()
        {
            var _service = new InvestimentServiceImpl();

            var list = _service.Find(); 

            return Json(list);
        }
    }
}