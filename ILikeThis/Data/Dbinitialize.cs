using ILikeThis.Data;
using ILikeThis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspDotNetCore.Data
{
    public class Dbinitialize
    {
        public static void Initialize(CommentContext context)
        {
            context.Database.EnsureCreated();

            if (context.Comments.Any())
            {
                return;
            }
            var comments = new Comment[]
           {
                new Comment {UserID="first@gmail.com",Text="Перший коментар"}
           };
            foreach (var item in comments)
            {
                context.Comments.Add(item);
            }

            context.SaveChanges();
        }
    }
}
