using BLL.Interface;
using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Linq;

namespace BLL.Services
{
    public class CommentService : IService<Comments>
    {
        private readonly IRepository<Comments> commentRepository;

        public CommentService(IRepository<Comments> repository)
        {
            this.commentRepository = repository;
        }

        public void Create(Comments e)
        {
            commentRepository.Create(e);
        }

        public void Delete(int id)
        {
            commentRepository.Delete(id);
        }

        public Comments Get(int id)
        {
            return commentRepository.Get(id);
        }
        public IEnumerable<Comments> GetAll()
        {
            return commentRepository.GetAll().Select(comm => comm);
        }

        public void Update(Comments e)
        {
            commentRepository.Update(e);
        }
    }
}
