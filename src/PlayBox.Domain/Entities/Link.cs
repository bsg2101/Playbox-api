using PlayBox.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Domain.Entities
{
    public class Link : BaseEntity
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public Guid ContentId { get; set; }
        public Content Content { get; set; }
    }
}

