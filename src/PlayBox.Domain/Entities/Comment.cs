using PlayBox.Domain.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public string UserName { get; set; }
        public Guid ContentId { get; set; }
        public Content Contents { get; set; }
    }
}
