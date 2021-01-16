using System;
using SolarCoffee.Data.Models;
using System.Linq;
using SolarCoffee.Web.ViewModels;
using System.Collections.Generic;

namespace SolarCoffee.Web.Serialization
{
    /// <summary>
    /// Handles mapping Order data models to and from related View Models
    /// </summary>
    public static class OrderMapper
    {
        /// <summary>
        /// Maps an InvoiceModel view model to a SalesOrder data model
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static SalesOrder SerializeInvoiceToOrder (InvoiceModel invoice)
        {
            var salesOrderItems = invoice.LineItems
                .Select(item => new SalesOrderItem
                {
                    Id = item.Id,
                    Quantity = item.Quantity,
                    Product = ProductMapper.SerializeProductModel(item.Product)
                }).ToList();

            return new SalesOrder
            {
                SalesOrderItems = salesOrderItems,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow
            };
        }
        /// <summary>
        /// maps a collection of salesorders to Order
        /// </summary>
        /// <param name="orders"></param>
        /// <returns></returns>
        public static List<OrderModel> SerializeOrdersToViewModels(IEnumerable<SalesOrder> orders)
        {
            return orders.Select(order => new OrderModel
            {
                Id = order.Id,
                CreatedOn = order.CreatedOn,
                UpdatedOn = order.UpdatedOn,
                SalesOrderItems = SerializeSalesOrderItems(order.SalesOrderItems),
                Customer = CustomerMapper.SerializeCustomer(order.Customer),
                IsPaid = order.IsPaid
            }).ToList();
        }
        /// <summary>
        /// Maps a collection of SalesOrderItems (data) to SalesOrderItemsModels (viewModels)
        /// </summary>
        /// <param name="orderItems"></param>
        /// <returns></returns>
        private static List<SalesOrderItemModel> SerializeSalesOrderItems(IEnumerable<SalesOrderItem> orderItems)
        {
            return orderItems.Select(item => new SalesOrderItemModel {
                Id = item.Id,
                Quantity = item.Quantity,
                Product = ProductMapper.SerializeProductModel(item.Product)
            }).ToList();
        }
    }
}
