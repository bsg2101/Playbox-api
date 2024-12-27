using PlayBox.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Domain.Entities
{
    public class Content : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Link> Links { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
