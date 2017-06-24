using DAL.Interfaces;
using ORM;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DAL
{
    public class PostReposytory : IRepository<Posts> 
    {
        private readonly DbContext context;

        public PostReposytory(DbContext uow)
        {
            this.context = uow;
        }

        public void Create(Posts e)
        {
            e.PostId = 0;
            context.Set<ORM.Posts>().Add(e);
            context.SaveChanges();
        }
        public void Update(Posts e)
        {
            var posts = context.Set<ORM.Posts>()
                                    .Where(post => post.PostId.Equals(e.PostId))
                                    .FirstOrDefault();
            posts.Text = e.Text;
            context.Entry(posts).State = EntityState.Modified;
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            var posts = context.Set<ORM.Posts>()
                                .Where(post => post.PostId.Equals(id))
                                .FirstOrDefault();

            context.Set<ORM.Posts>().Remove(posts);         

            context.SaveChanges();
        }
        public Posts Get(int id)
        {
            return context.Set<ORM.Posts>()
                            .Where(post => post.PostId.Equals(id))
                            .Select(post => post)
                            .ToList()
                            .FirstOrDefault();
        }
        public IEnumerable<Posts> GetAll()
        {
            return context.Set<ORM.Posts>().Select(post => post);
        }
       
    }
}
