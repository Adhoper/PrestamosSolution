using Prestamos.Web.Models;

namespace Prestamos.Web.Interfaces
{
    public interface IPrestamoApiService
    {
        Task<PrestamoViewModel> CalcularPrestamoAsync(PrestamoViewModel model);
    }
}
