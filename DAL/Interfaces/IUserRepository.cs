namespace DAL.Interfaces
{
    public interface IUserRepository:IRepository<ORM.Users>
    {
        ORM.Users GetUserByEmail(string email);
        ORM.Users GetUserByName(string name);
    }
}
