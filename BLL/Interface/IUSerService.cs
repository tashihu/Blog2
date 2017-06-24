using ORM;
using System.Collections.Generic;

namespace BLL.Interface
{
    public interface IUSerService : IService<Users>
    {
        Users getUserByEmail(string email);
        Users getUserByName(string name);
    }
}
