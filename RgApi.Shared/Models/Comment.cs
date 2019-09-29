using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RgApi.Shared.Models
{
    public class Comment
    {
        public int CommentId            { get; set; }
        public string Content           { get; set; }
        public DateTime TimeSent        { get; set; }
        public Product Product          { get; set; }

        public virtual AppUser User     { get; set; }
    }
}
