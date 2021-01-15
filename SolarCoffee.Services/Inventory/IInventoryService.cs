using System;
using System.Collections.Generic;
using SolarCoffee.Data.Models;
using SolarCoffee.Services.Product;

namespace SolarCoffee.Services.Inventory
{
    public interface IInventoryService
    {
        public List<Data.Models.ProductInventory> GetCurrentInventory();
        public ServiceResponse<Data.Models.ProductInventory> UpdateUnitsAvailable(int id, int adjustment);
        public ProductInventory GetByProductId(int productId);
        //public void CreateSnapShot(ProductInventory inventory);
        public List<ProductInventorySnapshot> GetSnapshotHistory();
    }
}
