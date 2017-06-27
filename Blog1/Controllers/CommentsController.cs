using BLL.Interface;
using ORM;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IService<Users> userService;
        private readonly IService<Comments> commentService;

        public CommentsController(IService<Users> service, IService<Comments> commentService)
        {
            this.userService = service;
            this.commentService = commentService;
        }

        public PartialViewResult List(int id)
        {
            int postid = id;
            var comments = commentService.Get(where: comm => comm.PostId.Equals(postid)).Select(comm => new Blog1.Models.CommentViewModel()
            {
                Id = comm.CommentId,
                Text = comm.commentText,
                User = userService.Get(comm.UserId).Email
            });
            return PartialView(comments);
        }

        public PartialViewResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                int postid = Convert.ToInt32(RouteData.Values["id"]);
                if (User.Identity.IsAuthenticated)
                {                   
                    int userid = userService.Get(where: user => user.Email.Equals(User.Identity.Name))                                    
                                    .FirstOrDefault().UserId;
                    commentService.Create(
                        new Comments() { PostId = postid,
                                            UserId = userid,
                                            commentText = collection["text"].ToString()
                                    });                    
                }
                return RedirectToAction("Details", "Posts", new { id = postid });

            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        [CommentAutor]
        public ActionResult Delete(int id)
        {            
            var comment = commentService.Get(id);
                commentService.Delete(id);
            return RedirectToAction("Details", "Posts", new { id = comment.PostId });
        }
        [HttpGet]
        [CommentAutor]
        public ActionResult Edit(int id)
        {
            var commEntity = commentService.Get(id);
            var commModel = new Models.NewCommentModel()
                    {   Id= commEntity.CommentId,
                        PostId = commEntity.PostId,
                        UserId = commEntity.UserId,
                        Text = commEntity.commentText
                    };
            return View(commModel);
        }
        [HttpPost]
        [CommentAutor]
        public ActionResult Edit(Models.NewCommentModel collection)
        {
            var comm = commentService.Get(collection.Id);
            comm.commentText = collection.Text;
            commentService.Update(comm);
            return RedirectToAction("Details", "Posts", new { id = comm.PostId });
        }

    }
}
