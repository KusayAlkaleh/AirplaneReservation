using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebProject.Data;
using WebProject.Models;

namespace WebProject.Controllers
{
    public class SeatController : Controller
    {
        private readonly DemoDbContext DemoDbContext;
        public SeatController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var allPlanes = await DemoDbContext.Planes.ToListAsync();

            SeatPlaneModel model = new SeatPlaneModel
            {
                Seats = await DemoDbContext.Seats.ToListAsync(),
                Planes = await DemoDbContext.Planes.ToListAsync()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SeatPlaneModel model) 
        {
            if(model.Seat != null)
            {
                // Checking the capacity of plane
                var getInformationPlane = await DemoDbContext.Planes.FindAsync(model.Seat.PlaneId);
                var counOfSeats = await DemoDbContext.Seats.CountAsync(x => x.PlaneId == model.Seat.PlaneId);

                if(getInformationPlane.Capacity == counOfSeats)
                {
                    ViewBag.alertMsg = "Capacity is full!";
                    ViewBag.capacityState = true;
                }
                else
                {
                    // updating seats of plane
                    getInformationPlane.ReservedSeats++;
                    getInformationPlane.AvailableSeats--;
                    DemoDbContext.Planes.Update(getInformationPlane);
                    await DemoDbContext.Seats.AddAsync(model.Seat);
                    await DemoDbContext.SaveChangesAsync();
                }

                SeatPlaneModel newModel = new SeatPlaneModel
                {
                    Seats = await DemoDbContext.Seats.ToListAsync(),
                    Planes = await DemoDbContext.Planes.ToListAsync(),
                };

                return View(newModel);
            }
            return RedirectToAction("Index", "Admin");
        }

        public async Task<IActionResult> ViewSeats(int planeId)
        {
            var seatsOfPlane = await DemoDbContext.Seats.Where(x => x.PlaneId == planeId).ToListAsync();
            ListWithSeat model = new ListWithSeat
            {
                SeatList = seatsOfPlane
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int SeatId)
        {
            var newDeger = SeatId;
            return View("ViewSeats");
        }

        public async Task<IActionResult> Delete(int seatId)
        {
            var removedSeat = await DemoDbContext.Seats.FindAsync(seatId);

            if(removedSeat != null)
            {
                //Get plane info for updating it ReservedSeats & AvailableSeats seats
                var editingPlane = await DemoDbContext.Planes.FirstOrDefaultAsync(x => x.PlaneID == removedSeat.PlaneId);
                editingPlane.ReservedSeats--;
                editingPlane.AvailableSeats++;
                DemoDbContext.Planes.Update(editingPlane);

                //Removing seat from database
                DemoDbContext.Seats.Remove(removedSeat);
                await DemoDbContext.SaveChangesAsync();

                return RedirectToAction("ViewSeats", new {planeId = removedSeat.PlaneId });
            }
            
            return RedirectToAction("Index", "Admin");
        }
    }
}
