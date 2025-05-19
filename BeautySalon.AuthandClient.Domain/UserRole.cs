namespace BeautySalon.AuthandClient.Domain
{
    public class UserRole : Enumeration
    {
        public static readonly UserRole Client = new (1, nameof(Client));
        public static readonly UserRole Employee  = new (2, nameof(Employee));

        protected UserRole(int id, string name) : base(id, name)
        {
        }
    }
}
