namespace WebTechnologies.Core
{
    public class Dealer
    {
        public int Id { get; private set; }

        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Area { get; set; }
        public double Rating { get; set; }
        public List<Car> Cars { get; set; } = new List<Car>();

        public Dealer(string name,
            string city,
            string address,
            string area,
            double rating)
        {
            Name = name;
            City = city;
            Address = address;
            Area = area;
            Rating = rating;
        }
    }
}
