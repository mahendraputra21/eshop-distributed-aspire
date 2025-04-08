using MassTransit;
using ServiceDefaults.Messaging.Events;

namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext, IBus bus)
{
    public async Task<IEnumerable<Product>> GetProductAsync()
    {
        return await dbContext.Products.ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int id)
    {
        return await dbContext.Products.FindAsync(id);
    }

    public async Task CreateProductAsync(Product product)
    {
        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateProductAsync(Product updateProduct, Product inputProduct)
    {
        // if price has changed, raise ProductPriceChanged integration event
        if (updateProduct.Price != inputProduct.Price) 
        {
            // publish Product price changed integration event for update basket prices
            var integrationEvent = new ProductPriceChangedIntegrationEvent
            {
                ProductId = updateProduct.Id, // Id only comes from db entity
                Name = inputProduct.Name,
                Description = inputProduct.Description,
                Price = inputProduct.Price, // set updated product price
                ImageUrl = inputProduct.ImageUrl
            };
            await bus.Publish(integrationEvent);
        }

        // update product with new values
        updateProduct.Name = inputProduct.Name;
        updateProduct.Description = inputProduct.Description;
        updateProduct.ImageUrl = inputProduct.ImageUrl;
        updateProduct.Price = inputProduct.Price;

        dbContext.Products.Update(updateProduct);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteProductAsync(Product deletedProduct)
    {
        dbContext.Products.Remove(deletedProduct);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Product>> SearchProductsAsync(string query)
    {
        return await dbContext.Products
                .Where(p => p.Name.Contains(query))
                .ToListAsync();
    }
}
