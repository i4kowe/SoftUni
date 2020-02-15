﻿namespace Andreys.Services
{
    using Andreys.Models;
    using Andreys.ViewModels.Products;
    using System.Collections.Generic;

    public interface IProductsService
    {
        int Add(ProductAddInputModel productsAddInputModel);

        IEnumerable<Product> GetAll();

        Product GetById(int id);

        void DeleteById(int id);
    }
}
