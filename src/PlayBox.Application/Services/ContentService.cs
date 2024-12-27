using AutoMapper;
using PlayBox.Application.Common.Models;
using PlayBox.Application.DTOs.Content;
using PlayBox.Application.Interfaces;
using PlayBox.Domain.Entities;
using PlayBox.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Application.Services
{
    public class ContentService : IContentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ContentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ContentDto>> GetByIdAsync(Guid id)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(id);
            if (content == null)
                return new ServiceResponse<ContentDto> { Success = false, Message = "Content not found" };

            var contentDto = _mapper.Map<ContentDto>(content);
            return new ServiceResponse<ContentDto> { Data = contentDto };
        }

        public async Task<ServiceResponse<List<ContentDto>>> GetAllAsync()
        {
            var contents = await _unitOfWork.Contents.GetAllAsync();
            var contentsDto = _mapper.Map<List<ContentDto>>(contents);
            return new ServiceResponse<List<ContentDto>> { Data = contentsDto };
        }

        public async Task<ServiceResponse<ContentDto>> CreateAsync(CreateContentDto createContentDto)
        {
            var content = _mapper.Map<Content>(createContentDto);
            await _unitOfWork.Contents.AddAsync(content);
            await _unitOfWork.CompleteAsync();

            var contentDto = _mapper.Map<ContentDto>(content);
            return new ServiceResponse<ContentDto> { Data = contentDto };
        }

        // Eksik olan metodların implementasyonları
        public async Task<ServiceResponse<ContentDto>> UpdateAsync(Guid id, UpdateContentDto updateContentDto)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(id);
            if (content == null)
                return new ServiceResponse<ContentDto> { Success = false, Message = "Content not found" };

            _mapper.Map(updateContentDto, content);
            await _unitOfWork.Contents.UpdateAsync(content);
            await _unitOfWork.CompleteAsync();

            var contentDto = _mapper.Map<ContentDto>(content);
            return new ServiceResponse<ContentDto> { Data = contentDto };
        }

        public async Task<ServiceResponse<bool>> DeleteAsync(Guid id)
        {
            var content = await _unitOfWork.Contents.GetByIdAsync(id);
            if (content == null)
                return new ServiceResponse<bool> { Success = false, Message = "Content not found" };

            await _unitOfWork.Contents.DeleteAsync(id);
            await _unitOfWork.CompleteAsync();

            return new ServiceResponse<bool> { Data = true, Message = "Content deleted successfully" };
        }
    }
}
