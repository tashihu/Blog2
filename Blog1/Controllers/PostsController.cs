using BLL.Interface;
using Blog1.Models;
using ORM;
using System.Linq;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class PostsController : Controller
    {
        private readonly IService<Posts> postService;
        private readonly IService<Comments> commentService;
        private readonly IService<Users> userService;

        public PostsController(IService<Posts> service, IService<Comments> commentService, IService<Users> userService)
        {
            this.postService = service;
            this.commentService = commentService;
            this.userService = userService;
        }

        // GET: Post
        public ActionResult Index()
        {        
            return View();
        }
        [HttpGet]
        public PartialViewResult list(int count=10,int offset=0)
        {
            return PartialView(postService
                                    .GetAll()
                                    .Skip(count*offset)
                                    .Take(count)
                                    .OrderBy(post=>post.PostId)
                                    .Select(post => new PostViewModel()
            {
                Text = post.Text,
                Id = post.PostId,
                UserName = userService.Get(post.UserId).Email             

            }
                ));
        }

        [HttpGet]
        public ViewResult Autor(string id,int count = 10, int offset = 0)
        {
            var user = userService.Get(_user => _user.Name.Equals(id)).FirstOrDefault();
            if (user != null)
            {
                ViewBag.name = user.Name;
                return View(postService
                                 .Get(post=>post.UserId.Equals(user.UserId))
                                 .Skip(offset*count)
                                 .Take(count)
                                 .OrderBy(post => post.PostId)
                        .Select(post => new PostViewModel()
                        {
                            Text = post.Text,
                            Id = post.PostId,
                            UserName = userService.Get(post.UserId).Email
                        }
                    ));
            }
            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var postentity = postService.Get(id);
            var username = userService.Get(postentity.UserId).Name;
            var post = new PostViewModel()
            {
                Text = postentity.Text,
                Id = postentity.PostId,
                UserId = postentity.UserId,
                UserName = username
            };            
            return View(post);
        }
        [HttpGet]
        public ViewResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "user")]
        public ActionResult Create(PostNewModel e)
        {            // User.Identity.Name
            try
            {
                var post = new Posts()
                {
                    Text = e.Text,
                    UserId = userService.Get(user => user.Email.Equals(User.Identity.Name))
                                        .FirstOrDefault()
                                        .UserId
                };
                postService.Create(post);
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            if (!User.Identity.IsAuthenticated) return RedirectToAction("Login", "Account");
            var post = postService.Get(id);
            var autor = userService.Get(post.UserId);
            if (User.Identity.Name.Equals(autor.Email) || User.IsInRole("admin") )
            {
                var comments = commentService.GetAll().Where(comm => comm.PostId == id);
                foreach (var item in comments.ToList()) commentService.Delete(item.CommentId);
                postService.Delete(id);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var postEntity = postService.Get(id);
            var postModel = new Models.PostNewModel() { Id = postEntity.PostId, Text =postEntity.Text };
            return View(postModel);
        }
        [HttpPost]
        public ActionResult Edit(Models.PostNewModel collection)
        {
            var post = postService.Get(collection.Id);
            post.Text = collection.Text;
            postService.Update(post);
            return RedirectToAction("Index", "Home");
        }

    }
}