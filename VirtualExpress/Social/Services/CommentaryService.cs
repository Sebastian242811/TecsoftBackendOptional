using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Social.Domain.Repositories;
using VirtualExpress.Social.Domain.Services;
using VirtualExpress.Social.Domain.Services.Responses;

namespace VirtualExpress.Social.Services
{
    public class CommentaryService:ICommentaryService
    {
        private readonly ICommentaryRepository _CommentaryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CommentaryService(ICommentaryRepository CommentaryRepository, IUnitOfWork unitOfWork)
        {
            _CommentaryRepository = CommentaryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommentaryResponse> DeleteAsync(int id)
        {
            var existing = await _CommentaryRepository.FindById(id);
            if (existing == null)
                return new CommentaryResponse("Commentary not found");

            try
            {
                _CommentaryRepository.Remove(existing);
                await _unitOfWork.CompleteAsync();

                return new CommentaryResponse(existing);
            }
            catch (Exception e)
            {
                return new CommentaryResponse($"An error ocurred while deleting the Commentary: {e.Message}");
            }
        }

        public async Task<CommentaryResponse> GetById(int id)
        {
            var existing = await _CommentaryRepository.FindById(id);
            if (existing == null)
                return new CommentaryResponse("Commentary not found");
            return new CommentaryResponse(existing);
        }

        public async Task<IEnumerable<Commentary>> ListAsync()
        {
            return await _CommentaryRepository.ListAsync();
        }

        public async Task<CommentaryResponse> SaveAsync(Commentary Commentary)
        {
            try
            {
                await _CommentaryRepository.AddAsync(Commentary);
                await _unitOfWork.CompleteAsync();

                return new CommentaryResponse(Commentary);
            }
            catch (Exception e)
            {
                return new CommentaryResponse($"An error ocurred while saving the Commentary: {e.Message}");
            }
        }

        public async Task<CommentaryResponse> UpdateAsync(int id, Commentary Commentary)
        {
            var existing = await _CommentaryRepository.FindById(id);
            if (existing == null)
                return new CommentaryResponse("Commentary not found");
            existing.Opinion = Commentary.Opinion;
            existing.Valoration = Commentary.Valoration;
            try
            {
                _CommentaryRepository.Update(existing);
                await _unitOfWork.CompleteAsync();

                return new CommentaryResponse(existing);
            }
            catch (Exception e)
            {
                return new CommentaryResponse($"An error ocurred while updating the Commentary: {e.Message}");
            }
        }
    }
}
