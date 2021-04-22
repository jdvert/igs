using System;
using System.Text.Json.Serialization;

namespace IgsMarket.Api.Model
{
    public class Product
    {
        private string _name;
        private double _price;

        public int Id { get; init; }
        public string Name
        {
            get => _name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name value is required.");
                }

                _name = value;
            }
        }

        [JsonIgnore]
        public double Price
        {
            get => _price;
            private set
            {
                if (value <= 0)
                {
                    throw new ArgumentException("Price must be greater than zero.");
                }

                _price = value;
            }
        }

        [JsonPropertyName("price")]
        public string FormattedPrice
        {
            get => Price.ToString("N");
            private set
            {
                if (!double.TryParse(value, out var doubleValue))
                {
                    throw new ArgumentException("Formatted string value is not a valid number.");
                }

                Price = doubleValue;
            }
        }

        internal Product(int id, string name, double price)
        {
            Id = id;
            Name = name;
            Price = price;
        }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void SetPrice(double price)
        {
            Price = price;
        }
    }
}
