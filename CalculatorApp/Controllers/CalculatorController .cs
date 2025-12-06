using CalculatorApp.Services;
using Microsoft.AspNetCore.Mvc;

namespace CalculatorApp.Controllers
{
    public class CalculatorController : Controller
    {
        private readonly ICalculatorService _service;

        public CalculatorController(ICalculatorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate([FromForm] double a, [FromForm] double b, [FromForm] string operation)
        {
            try
            {
                double result = operation?.ToLower() switch
                {
                    "add" => _service.Add(a, b),
                    "sub" => _service.Subtract(a, b),
                    "mul" => _service.Multiply(a, b),
                    "div" => _service.Divide(a, b),
                    _ => throw new ArgumentException("Invalid operation")
                };

                return Json(new { success = true, result });
            }
            catch (DivideByZeroException ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "An error occurred: " + ex.Message });
            }
        }
    }
}
