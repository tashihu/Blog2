using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IRepository<Posts> PostRepository;

        public PostService(IRepository<Posts> repository)
        {
            this.PostRepository = repository;
        }

        public void Create(Posts e)
        {
            PostRepository.Create(e);
        }
        public Posts Get(int id)
        {
            return PostRepository.Get(id);
        }
        public IEnumerable<Posts> GetAll()
        {
            return PostRepository.GetAll().Select(post => post);
        }
        public IEnumerable<Posts> GetUserPosts(int id,int count=10,int offset=0)
        {
            return PostRepository.GetAll().Where(post=>post.UserId == id).Select(post => post);
        }
        public IEnumerable<Posts> GetLastPosts(int count = 10, int offset = 0)
        {
            return PostRepository.GetAll().Skip(offset).Take(count).Select(post => post);
        }
        public void Update(Posts e)
        {
            PostRepository.Update(e);
        }
        public void Delete(int id)
        {
            PostRepository.Delete(id);
        }
    }
}
