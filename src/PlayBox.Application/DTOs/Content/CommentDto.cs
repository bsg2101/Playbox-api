﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.DTOs.Content
{
    public record CommentDto
    {
        public Guid Id { get; init; }
        public string Content { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public DateTime CreatedAt { get; init; }
    }
}
