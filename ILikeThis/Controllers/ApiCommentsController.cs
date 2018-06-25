using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ILikeThis.Data;
using ILikeThis.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ILikeThis.Controllers
{
    public class MyCom
    {
        public int Id;
        public int Answer;
        public string UserID;
        public string Text;
        public int Likes;
    }

    [Produces("application/json")]
    [Route("api/ApiComments")]
    public class ApiCommentsController : Controller
    {
        private readonly CommentContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ApiCommentsController(CommentContext context, UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comments.ToListAsync());
        }

        [HttpGet]
        // GET: api/ApiComments
        public string GetComments()
        {
            
            List<MyCom> mydata2 = new List<MyCom>();
            var x = _context.Comments.ToArray();
            var y = _context.Likes.ToArray();
            string mydata = "";
            for (int i = 0; i < x.Length; i++)
            {
                MyCom myCom = new MyCom();
                myCom.Id = x[i].Id;
                myCom.UserID = x[i].UserID;
                myCom.Text = x[i].Text;
                if (x[i].Answer != null)
                {
                    myCom.Answer = x[i].Answer.Id;
                }
                else myCom.Answer = 0;


               myCom.Likes = 0;
                for (int j = 0; j < y.Length; j++)
                {
                    if(y[j].CommentID.Id == myCom.Id)
                    {
                        myCom.Likes++;
                    }
                }
                mydata2.Add(myCom);
            }
            mydata = Newtonsoft.Json.JsonConvert.SerializeObject(mydata2);
            return mydata;
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            string mydata = "";
            mydata = Newtonsoft.Json.JsonConvert.SerializeObject(_context.Likes);

            return mydata;
        }


    [HttpPost]

        public async Task<IActionResult> PostComment(string Text)
        {
            Comment comment = new Comment();

            comment.Text = Text;
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            comment.UserID = user.Email;
            if (ModelState.IsValid)
            {
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(comment);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> PostComment(int id)
        {
            Like like = new Like();
            var x = _context.Likes.ToArray();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            like.UserID = user.Email;
            like.CommentID = _context.Comments.Find(id);
            
            for (int i = 0; i < x.Length; i++)
            {
                if (x[i].CommentID== like.CommentID && x[i].UserID == like.UserID)
                {
                    return View(like);
                }
            }
            
            if (ModelState.IsValid)
            {
                _context.Add(like);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(like);
        }
    }
}