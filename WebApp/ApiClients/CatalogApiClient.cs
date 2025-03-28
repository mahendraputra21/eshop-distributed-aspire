﻿using Catalog.Models;

namespace WebApp.ApiClients;

public class CatalogApiClient(HttpClient httpClient)
{
    public async Task<List<Product>> GetProductsAsync()
    {
        var response = await httpClient.GetFromJsonAsync<List<Product>>($"/products");
        return response!;
    }

    public async Task<Product> GetProductByIdAsync(int id)
    {
        var response = await httpClient.GetFromJsonAsync<Product>($"/products/{id}");
        return response!;
    }
}
