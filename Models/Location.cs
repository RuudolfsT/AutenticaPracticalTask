namespace AutenticaPracticalTask.Models
{
    public class Location
    {
        public virtual string Name { get; set; }
        public virtual double Latitude { get; set; }
        public virtual double Longitude { get; set; }
        public virtual string Description { get; set; }
    }
}
