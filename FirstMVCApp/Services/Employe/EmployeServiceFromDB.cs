using AutoMapper;
using FirstMVCApp.Models;
using FirstMVCApp.Services.DALEmployes;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace FirstMVCApp.Services
{
    public class EmployeServiceFromDB : IEmployeService
    {
        private readonly EmployeDbContext context;
        private readonly IMapper mapper;

        public EmployeServiceFromDB(EmployeDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public string NewMatricule()
        {
            return (context.Employes
                    .Select(c => Convert.ToInt32(c.Matricule))
                    .Max() + 1).ToString().PadLeft(3, '0');
        }

        public async Task<Models.Employe> AddEmployeAsync(Models.Employe e)
        {
            // Création et remplissage d'un EmployeDAO à partir des données du Employe
            var employeDAO=mapper.Map<EmployeDAO>(e);
            employeDAO.DerniereModif = DateTime.Now;


            employeDAO.Confidentiel = "Toto";
            employeDAO.Matricule = NewMatricule();

            var entitesDansLeContext = context.ChangeTracker.Entries().ToList();

            // Ajout de l'employé DAO dans le context (RAM)
            context.Employes.Add(employeDAO);
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();


            // Génération et envoit de la requete INSERT
            await context.SaveChangesAsync();

            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            e.Matricule = employeDAO.Matricule;
            // Remappage du DAO potentiellement modifié par l'instruction insert
            return await GetEmployeAsync(e.Matricule);

        }

        public Task<IEnumerable<Models.Employe>> AugmenterEmployesAsync(EmployeSearchModel search, decimal taux)
        {
            throw new NotImplementedException();
        }

        public async Task<Models.Employe> DeleteEmployeAsync(string matricule)
        {
            var entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            // L'entité est chargee dans le tracker
            // SELECT 
            var employeASupprimer=context.Employes.FirstOrDefault(c=>c.Matricule==matricule);

            if (employeASupprimer == null)
            {
                throw new EmployeServiceException("Employe à supprimé non trouvé");
            }
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            context.Employes.Remove(employeASupprimer);
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            await context.SaveChangesAsync();
            // Il n'y a plus l'entité dans le ChangeTracker
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            return await GetEmployeAsync(matricule);
        }

        public Task<Models.Employe> GetEmployeAsync(string matricule)
        {
            var employeDAO = context.Employes.FirstOrDefault(c => c.Matricule == matricule);
            if (employeDAO == null) {
                throw new EmployeServiceException();
            }
            var employeModel = mapper.Map<Employe>(employeDAO);
            return Task.FromResult(employeModel );
        }

        public Task<IEnumerable<Models.Employe>> GetEmployesAsync(EmployeSearchModel search)
        {
            // SELECT * FROM tbl_Employes
            IQueryable<EmployeDAO> query = context.Employes;
            var requete = query.ToQueryString();

            if (search.Texte != null)
            {
                // IQueryable => Ajoute une clause Where a query
                // SELECT * FROM tbl_Employes WHERE ...
                query = query.Where(c => c.Name.Contains(search.Texte)
                || c.Prenom.Contains(search.Texte)
                || c.Matricule==search.Texte
                );
            }
            requete = query.ToQueryString();
            if (search.Anciennete != null)
            {
                // IQueryable => Ajoute une clause Where a query
                // SELECT * FROM tbl_Employes WHERE ...
                query = query.Where(c => c.DateEntree.Year < DateTime.Now.Year - search.Anciennete);
            }
            requete = query.ToQueryString();


            // Quelle est la requète envoyée
            // Aucune

            // Au moment de l'utilisation la requète sera envoyé
            //foreach(var e in pocos)
            //{

            //}
            return Task.FromResult(mapper.Map<IEnumerable<Employe>>(query));
            

        }

        public async Task<Employe> UpdateEmployeAsync(Employe e)
        {
            context.ChangeTracker.Clear();
            var employeDAO = context.Employes.FirstOrDefault(c => c.Matricule == e.Matricule);
            var entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            if (employeDAO == null)
            {
                throw new EmployeServiceException();
            }

            mapper.Map(e, employeDAO);
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            // "Manipulation" de la requete UPDATE pour éviter qu'elle envoit la nouvelle valeur
            // Pour la date creation
            context.Entry(employeDAO).Property(c => c.DateEntree).IsModified = false;
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw new EmployeServiceException("Mise à jour fail");
            }
            entitesDansLeContext = context.ChangeTracker.Entries().ToList();
            return await GetEmployeAsync(e.Matricule);
            
        }
    }
}
