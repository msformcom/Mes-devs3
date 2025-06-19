using FirstMVCApp.Models;
using FirstMVCApp.Services.DALEmployes;

namespace FirstMVCApp.Services
{
    public class EmployeServiceFromDB : IEmployeService
    {
        private readonly EmployeDbContext context;

        public EmployeServiceFromDB(EmployeDbContext context)
        {
            this.context = context;
        }

        public Task<Models.Employe> AddEmployeAsync(Models.Employe e)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Employe>> AugmenterEmployesAsync(EmployeSearchModel search, decimal taux)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employe> DeleteEmployeAsync(string matricule)
        {
            throw new NotImplementedException();
        }

        public Task<Models.Employe> GetEmployeAsync(string matricule)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Models.Employe>> GetEmployesAsync(EmployeSearchModel search)
        {
            var daos = context.Employes;

            var pocos = context.Employes.Select(dao => new Employe()
            {
                Matricule = dao.Matricule,
                Nom = dao.Nom,
                Actif = dao.Actif,
                DateEntree = dao.DateEntree,
                Prenom = dao.Prenom,
                Salaire = dao.Salaire,
            });
            // Quelle est la requète envoyée
            // Aucune

            // Au moment de l'utilisation la requète sera envoyé
            //foreach(var e in pocos)
            //{

            //}
            return Task.FromResult((IEnumerable<Employe>)pocos);
            

        }
    }
}
