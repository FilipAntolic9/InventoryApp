using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Web.ViewModels
{
    public class SupplierEditVm
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
