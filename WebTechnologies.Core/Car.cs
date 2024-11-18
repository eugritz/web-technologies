using System.Text.Json.Serialization;

namespace WebTechnologies.Core
{
    public class Car
    {
        public int Id { get; private set; }

        [JsonPropertyName("firm")]
        public string Firm { get; set; }

        [JsonPropertyName("model")]
        public string Model { get; set; }

        [JsonPropertyName("year")]
        public int Year { get; set; }

        [JsonPropertyName("power")]
        public int Power { get; set; }

        [JsonPropertyName("color")]
        public string Color { get; set; }

        [JsonPropertyName("price")]
        public double Price { get; set; }

        private List<Dealer> Dealers { get; set; } = new List<Dealer>();

        public Car(string firm,
            string model,
            int year,
            int power,
            string color,
            double price)
        {
            Firm = firm;
            Model = model;
            Year = year;
            Power = power;
            Color = color;
            Price = price;
        }
    }
}
