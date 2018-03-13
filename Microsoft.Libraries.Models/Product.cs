using System;

namespace Microsoft.Libraries.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string SupplierAddress { get; set; }
        public int UnitPrice { get; set; }
        public int UnitsInStock { get; set; }
        public bool IsActive { get; set; }
        public string Remarks { get; set; }

        public override string ToString()
        {
            return string.Format(@"{0}, {1}, {2}, {3}, {4}, {5}, {6}",
                this.ProductId, this.ProductName, this.SupplierAddress, this.UnitPrice,
                this.UnitsInStock, this.IsActive, this.Remarks);
        }
    }
}
