using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class ListWithSeat
    {
        public Seat Seat { get; set; }
        public List<Seat> SeatList { get; set; }
    }
}
