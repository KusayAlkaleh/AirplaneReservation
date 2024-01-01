using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebProject.Models.Domain
{
    public class Reservation
    {
        [Key]
        public int ReservationsID { get; set; }

        [ForeignKey("AppUser")]
        public string AppUserId { get; set; }

        [ForeignKey("Flight")]
        public int FightID { get; set; }

        [ForeignKey("SeatID")]
        public int SeatID { get; set; }

        public DateTime ReservationDate { get; set; }

        public bool PaymentStatus { get; set; }



        // Navigation property
        public virtual ApplicationUser AppUser { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Seat Seat { get; set; }
    }
}
