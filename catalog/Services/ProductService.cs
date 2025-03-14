namespace Catalog.Services;

public class ProductService(ProductDbContext dbContext)
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
}
