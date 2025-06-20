namespace FirstMVCApp.Services
{
    public class EmployeServiceException : Exception
    {
        public EmployeServiceException() : base("Erreur non précisée")
        {
            
        }
        public EmployeServiceException(string message) : base(message)
        {
            
        }

    }
}
