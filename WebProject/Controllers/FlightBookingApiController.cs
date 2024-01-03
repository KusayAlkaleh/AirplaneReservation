using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebProject.Models;
using WebProject.Data;
using WebProject.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightBookingApiController : ControllerBase
    {
        private readonly DemoDbContext DemoDbContext;
        public FlightBookingApiController(DemoDbContext mvcDemoDbContext)
        {
            this.DemoDbContext = mvcDemoDbContext;
        }

        //Get data
        [HttpGet]
        public async Task<List<Flight>> GetFlights()
        {
            var allFlights = await DemoDbContext.Flights.ToListAsync();

            return allFlights;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Flight>> GetFlight(int id)
        {
            var flight = await DemoDbContext.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            return flight;
        }

        // POST: api/FlightBookingApi
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Reservation reservation)
        {
            if(reservation == null)
                return NotFound();

            DemoDbContext.Reservation.Add(reservation);
            await DemoDbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutFlight(int id, Flight flight)
        {
            if (id != flight.FlightID)
            {
                return BadRequest();
            }

            DemoDbContext.Entry(flight).State = EntityState.Modified;

            try
            {
                await DemoDbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (flight == null)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFlight(int id)
        {
            var flight = await DemoDbContext.Flights.FindAsync(id);

            if (flight == null)
            {
                return NotFound();
            }

            DemoDbContext.Flights.Remove(flight);
            await DemoDbContext.SaveChangesAsync();

            return NoContent();
        }

    }
}
