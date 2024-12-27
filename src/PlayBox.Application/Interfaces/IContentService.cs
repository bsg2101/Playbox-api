using PlayBox.Application.Common.Models;
using PlayBox.Application.DTOs.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.Interfaces
{
    public interface IContentService
    {
        Task<ServiceResponse<ContentDto>> GetByIdAsync(Guid id);
        Task<ServiceResponse<List<ContentDto>>> GetAllAsync();
        Task<ServiceResponse<ContentDto>> CreateAsync(CreateContentDto createContentDto);
        Task<ServiceResponse<ContentDto>> UpdateAsync(Guid id, UpdateContentDto updateContentDto);
        Task<ServiceResponse<bool>> DeleteAsync(Guid id);
    }
}
