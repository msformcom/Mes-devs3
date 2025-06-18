
using FirstMVCApp.Models;

namespace FirstMVCApp.Services
{
    public interface IEmployeService
    {
        Task<IEnumerable<Employe>> GetEmployesAsync(EmployeSearchModel search);
        Task<Employe> GetEmployeAsync(string matricule);

        Task<Employe> DeleteEmployeAsync(string matricule);

        Task<IEnumerable<Employe>> AugmenterEmployesAsync(EmployeSearchModel search,decimal taux);
    }
}
