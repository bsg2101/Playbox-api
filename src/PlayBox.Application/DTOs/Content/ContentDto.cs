using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.DTOs.Content
{
    public record ContentDto
    {
        public Guid Id { get; init; }
        public string Title { get; init; }
        public string Description { get; init; }
        public string ImageUrl { get; init; }
        public ICollection<LinkDto> Links { get; init; } = new List<LinkDto>();
        public ICollection<CommentDto> Comments { get; init; } = new List<CommentDto>();
        public DateTime CreatedAt { get; init; }
    }
}
