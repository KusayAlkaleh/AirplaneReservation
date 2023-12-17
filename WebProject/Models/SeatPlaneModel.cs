using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class SeatPlaneModel
    {
        public Seat Seat { get; set; }
        public List<Plane> Planes { get; set; }
        public List<Seat> Seats { get; set; }
    }
}
