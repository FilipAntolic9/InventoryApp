using System;
using System.ComponentModel.DataAnnotations;

namespace InventoryApp.Web.ViewModels
{
    public class CategoryEditVm
    {
        public Guid Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; }
    }
}
