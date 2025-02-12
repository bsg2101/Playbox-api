﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.DTOs.Content
{
    public record CreateLinkDto
    {
        public string Title { get; init; } = string.Empty;
        public string Url { get; init; } = string.Empty;
    }
}
