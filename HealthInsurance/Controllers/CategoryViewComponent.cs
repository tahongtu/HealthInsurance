using HealthInsurance.Data;
using Microsoft.AspNetCore.Mvc;

namespace HealthInsurance.Controllers
{
    [ViewComponent(Name = "Category")]
    public class CategoryViewComponent: ViewComponent
    {
        private readonly HealthInsuranceContext _context;

        public CategoryViewComponent(HealthInsuranceContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var category = _context.Category.ToList();
            return View("Category", category);
        }
    }
}
