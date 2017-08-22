using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using niner.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace niner.Controllers
{
    public class DashboardController : Controller
    {
        private NinerContext _context;

        public DashboardController(NinerContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("bright_ideas")]
        public IActionResult Index()
        {
            int? UserId = HttpContext.Session.GetInt32("UserId");
            if (UserId != null)
            {
                ViewBag.LoggedUser = _context.Users.SingleOrDefault(user => user.UserId == UserId);
                List<Post> AllPosts = _context.Posts
                    .Include(p => p.User)
                    .Include(p => p.Likes)
                    .ThenInclude(l => l.User)
                    .OrderByDescending(l => l.Likes.Count)
                    .ToList();
                ViewBag.AllPosts = AllPosts;
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        [Route("addpost")]
        public IActionResult AddPost(Post post)
        {
          if (ModelState.IsValid && _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")) != null)
          {
            Post newPost = new Post{
              Content = post.Content,
              UserId = (int)HttpContext.Session.GetInt32("UserId"),
              CreatedAt = DateTime.UtcNow,
              UpdatedAt = DateTime.UtcNow
            };
            _context.Posts.Add(newPost);
            _context.SaveChanges();
            return RedirectToAction("Index");
          }
          else
          {
            return View("Index", post);
          }
        }

        [HttpGet]
        [Route("users/{ThisUserId}")]
        public IActionResult Profile(int ThisUserId)
        {
            if (_context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")) != null)
            {
                Console.WriteLine(ThisUserId);
                User LoggedUser = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                ViewBag.LoggedUser = LoggedUser;
                User ThisUser = _context.Users.SingleOrDefault(u => u.UserId == ThisUserId);
                Console.WriteLine(ThisUser.Name);
                ViewBag.ThisUser = ThisUser;
                List<Like> Likes = _context.Likes.Where(l => l.UserId == ThisUserId).ToList();
                ViewBag.LikeCount = Likes.Count;
                List<Post> Posts = _context.Posts.Where(p => p.UserId == ThisUserId).ToList();
                ViewBag.PostCount = Posts.Count;
                return View("Profile");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }
        [HttpGet]
        [Route("addlike/{LoggedUserId}/{PostId}")]
        public IActionResult AddLike(int LoggedUserId, int PostId)
        {
            Console.WriteLine("madeittoaddlike");
            if (_context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")) != null)
            {
                Console.WriteLine(LoggedUserId);
                Console.WriteLine(PostId);
                Like ExistsAlready = _context.Likes.SingleOrDefault(l => l.UserId == LoggedUserId && l.PostId == PostId);
                if (ExistsAlready == null)
                {
                    Like newLike = new Like{
                        PostId = PostId,
                        UserId = LoggedUserId
                    };
                    _context.Likes.Add(newLike);
                    _context.SaveChanges();
                }
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }
        [HttpGet]
        [Route("bright_ideas/{PostId}")]
        public IActionResult LikeStatus(int PostId)
        {
            if (_context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId")) != null)
            {
                Console.WriteLine(PostId);
                User LoggedUser = _context.Users.SingleOrDefault(u => u.UserId == HttpContext.Session.GetInt32("UserId"));
                ViewBag.LoggedUser = LoggedUser;
                Post ThisPost = _context.Posts.Include(p => p.Likes).ThenInclude(l => l.User).SingleOrDefault(p => p.PostId == PostId);
                Console.WriteLine(ThisPost.Content);
                ViewBag.ThisPost = ThisPost;
                return View("ThisPost");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
          
        }
    }
}