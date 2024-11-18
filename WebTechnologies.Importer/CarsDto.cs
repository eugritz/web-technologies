using System.Text.Json.Serialization;
using WebTechnologies.Core;

namespace WebTechnologies.Importer
{
    internal class CarsDto
    {
        [JsonPropertyName("cars")]
        public List<Car>? Cars { get; set; }
    }
}
