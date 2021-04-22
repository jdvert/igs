using IgsMarket.Api.Model;

namespace IgsMarket.Api
{
    public class SeedData
    {
        public static Product[] Products = new[]
        {
            new Product(id: 1, name: "Lavender heart", price: 9.25),
            new Product(id: 2, name: "Personalised cufflinks", price: 45),
            new Product(id: 3, name: "Kids T-shirt", price: 19.95)
        };
    }
}
