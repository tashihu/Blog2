using BLL.Interface;
using BLL.Services;
using Blog1.Models;
using ORM;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Blog1.Controllers
{
    public class PostsController : Controller
    {
        private readonly IService<Posts> postService;
        private readonly IService<Comments> commentService;
        private readonly IService<Users> userService;
        private readonly SearchService searchService;

        public PostsController(IService<Posts> service, IService<Comments> commentService, IService<Users> userService)
        {
            this.postService = service;
            this.commentService = commentService;
            this.userService = userService;
            this.searchService = new SearchService(postService);
        }
        public PartialViewResult Search()
        {
            return PartialView("_search");
        }
        [HttpPost]
        public ActionResult Search2(string SearchString = null)
        {
            var results = searchService.Search(SearchString).Select(post => new PostViewModel()
            {
                Text = post.Text,
                Id = post.PostId,
                UserName = userService.Get(post.UserId).Email

            });
            if (results.Count() > 0)
            {
                return View("Search",results);
            }
            else return View("SearchNotFound");
        }
        // GET: Post
        public ActionResult Index()
        {        
            return View();
        }
        
        [HttpGet]        
        public PartialViewResult List(int count=10,int offset=0)
        {
            return PartialView(postService
                                    .Get()
                                    .Skip(count*offset)
                                    .Take(count)
                                    .OrderBy(post=>post.CreateDate)
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
                                 .OrderBy(post => post.CreateDate)
                        .Select(post => new PostViewModel()
                        {
                            Text = post.Text,
                            Id = post.PostId,
                            UserName = userService.Get(post.UserId).Email
                        }
                    ));
            }
            return View("NotFoundAutor");
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
                UserName = username,
                CreateDate = postentity.CreateDate
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
            var user = userService.Get(u => u.Email.Equals(User.Identity.Name))
                                    .FirstOrDefault();
            var post = new Posts()
            {
                Text = e.Text,
                UserId = user.UserId,
                CreateDate = DateTime.Now
            };
            postService.Create(post);
            return RedirectToAction("Autor", new { id =  user.Name});
        }
        [HttpGet]
        [PostAutor]
        public ActionResult Delete(int id)
        {
            var comments = commentService.Get(where: comm => comm.PostId == id);
            foreach (var item in comments.ToList()) commentService.Delete(item.CommentId);
            var autor = userService.Get(postService.Get(id).UserId).Name;
            postService.Delete(id);
            return RedirectToAction("Autor", new { id = autor });
        }
        [HttpGet]
        [PostAutor]
        public ActionResult Edit(int id)
        {
            var postEntity = postService.Get(id);
            var postModel = new Models.PostNewModel() { Id = postEntity.PostId, Text =postEntity.Text };
            return View(postModel);
        }
        [HttpPost]
        [PostAutor]
        public ActionResult Edit(Models.PostNewModel collection)
        {
            var post = postService.Get(collection.Id);
            post.Text = collection.Text;
            postService.Update(post);
            return RedirectToAction("Details", "Posts", new { id = post.PostId });
        }

    }
}