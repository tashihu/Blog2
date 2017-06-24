using ORM;
using System.Collections.Generic;

namespace BLL.Interface
{
    public interface IPostService: IService<Posts>
    {
         IEnumerable<Posts> GetUserPosts(int id,int count = 10, int offset = 0);
         IEnumerable<Posts> GetLastPosts(int count = 10, int offset = 0);
    }
}
