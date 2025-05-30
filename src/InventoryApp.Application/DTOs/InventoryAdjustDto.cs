using System;

namespace InventoryApp.Application.DTOs
{
    /// <summary>
    /// Povecanje/smanjenje kolicine proizvoda
    /// </summary>
    /// 
    public class InventoryAdjustDto
    {
        public int ProductId { get; set; }
        public int Delta { get; set; } // +/-
        public string Location { get; set; }
    }
}
