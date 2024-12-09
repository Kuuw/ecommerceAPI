﻿using Entities.DTO;
using Entities.Models;

namespace DAL.Abstract
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        public void AddStockEntry(int ProductId, int Stock);
        public void UpdateStock(int ProductId, int Stock);
        public List<Product>? GetPaged(int page, int pageSize, ProductFilter productFilter);
        public ProductStock GetStock(int ProductId);
        public int GetFilteredCount(ProductFilter productFilter);
    }
}
