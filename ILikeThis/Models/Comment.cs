using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ILikeThis.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public Comment Answer { get; set; }
        public string UserID { get; set; }
        public string Text { get; set; }
        
    }
}
