using System.Threading.Tasks;
using InventoryApp.Application.DTOs;

namespace InventoryApp.Application.Services
{
    public interface IInventoryService
    {
        /// <summary>
        /// Promjena kolicine zaliha +/- 
        /// </summary>
        /// 
        Task AdjustAsync(InventoryAdjustDto dto);
    }
}
