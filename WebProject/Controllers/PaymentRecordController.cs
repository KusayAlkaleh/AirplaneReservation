using Microsoft.AspNetCore.Mvc;
using WebProject.Data;
using WebProject.Models.Domain;

namespace WebProject.Controllers
{
    public class PaymentRecordController : Controller
    {
        private readonly DemoDbContext DemoDbContext;
        public PaymentRecordController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }
        public IActionResult Index()
        {
            List<PaymentRecord> objCategoryList = DemoDbContext.PaymentRecords.ToList();
            return View(objCategoryList);
        }
    }
}
