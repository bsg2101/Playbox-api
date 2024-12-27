using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.DTOs.Content
{
    public record UpdateContentDto
    {
        public string Title { get; init; } = string.Empty;
        public string Description { get; init; } = string.Empty;
        public string ImageUrl { get; init; } = string.Empty;
        public ICollection<UpdateLinkDto> Links { get; init; } = new List<UpdateLinkDto>();
    }
}
