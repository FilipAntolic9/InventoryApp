using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Web.ViewModels
{
    public class InventoryAdjustVm
    {
        [Required]
        public int ProductId { get; set; }
        [Required, Range(-100000, 100000)]
        public int Delta { get; set; }
        [Required, MaxLength(100)]
        public string Location { get; set; }
    }
}