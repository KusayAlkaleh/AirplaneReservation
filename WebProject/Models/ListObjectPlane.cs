using WebProject.Models.Domain;

namespace WebProject.Models
{
    public class ListObjectPlane
    {
        public Plane plane { get; set; }
        public List<Plane> ListOfPlanes { get; set; }
    }
}
