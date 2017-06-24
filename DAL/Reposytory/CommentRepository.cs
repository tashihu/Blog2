using ORM;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class CommentRepository: IRepository<ORM.Comments>
    {
        private readonly DbContext context;

        public CommentRepository(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(Comments e)
        {
            context.Set<ORM.Comments>().Add(e);
            context.SaveChanges();
        }
        public void Update(Comments e)
        {            
            var comments = context.Set<ORM.Comments>()
                            .Where(comm => comm.CommentId.Equals(e.CommentId))
                            .FirstOrDefault();
            comments = e;
            context.Entry(comments).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var comments = context.Set<ORM.Comments>()
                            .Where(comm => comm.CommentId.Equals(id))
                            .Select(comment => comment)
                            .FirstOrDefault();
            context.Set<ORM.Comments>().Remove(comments);
            context.SaveChanges();
            
        }
        public Comments Get(int id)
        {
            return context.Set<ORM.Comments>()
                .Where(comm => comm.CommentId.Equals(id))
                .Select(comment => comment)
                .FirstOrDefault();
        }
        public IEnumerable<Comments> GetAll()
        {
            return context.Set<ORM.Comments>().Select(comment => comment);
        }
    }
}
