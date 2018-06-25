using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILikeThis.Models
{
    public class Like
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public Comment CommentID { get; set; }
    }
}
