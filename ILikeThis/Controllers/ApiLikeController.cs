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
    [Produces("application/json")]
    [Route("api/ApiLike")]
    public class ApiLikeController : Controller
    {
        private readonly CommentContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public ApiLikeController(CommentContext context, UserManager<ApplicationUser> userManager,
          SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Likes.ToListAsync());
        }

        [HttpGet]
        // GET: api/ApiLike
        public async Task<IActionResult> GetLikes()
        {
            var MyLikes = _context.Likes;
            




            return View(await _context.Likes.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Post(int id)
        {
           Like like = new Like();
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                throw new ApplicationException($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }
            
            like.UserID = user.Email;
            like.CommentID = _context.Comments.Find(id);
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
