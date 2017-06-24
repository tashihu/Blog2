using BLL.Interface;
using Blog1.Models;
using ORM;
using System.Linq;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class PostsController : Controller
    {
        private readonly IPostService postService;
        private readonly IService<Comments> commentService;
        private readonly IUSerService userService;

        public PostsController(IPostService service, IService<Comments> commentService, IUSerService userService)
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
            return PartialView(postService.GetLastPosts(count,offset*count).Select(post => new PostViewModel()
            {
                Text = post.Text,
                Id = post.PostId,
                UserName = userService.Get(post.UserId).Email
                

            }
                ));
        }

        [HttpGet]
        public ViewResult Autor(int id,int count = 10, int offset = 0)
        {
            ViewBag.name = userService.Get(id).Name;
            return View(postService.GetUserPosts(id,count, offset * count)
                    .Select(post => new PostViewModel()
            {
                Text = post.Text,
                Id = post.PostId,
                UserName = userService.Get(post.UserId).Email
            }
                ));
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
        {            
            try
            {
                var post = new Posts()
                {
                    Text = e.Text,
                    UserId = userService.getUserByName(User.Identity.Name).UserId
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
            if (User.Identity.Name.Equals("admin@mail.ru"))
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