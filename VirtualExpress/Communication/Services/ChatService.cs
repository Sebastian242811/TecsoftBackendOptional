using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.Communication.Domain.Services;
using VirtualExpress.Communication.Domain.Services.Responses;

namespace VirtualExpress.Communication.Services
{
    public class ChatService : IChatService
    {
        public readonly IChatRepository _ChatRepository;
        public async Task<ChatResponse> DeleteAsync(int id)
        {
            var existingChat = await _ChatRepository.FindById(id);
            if (existingChat == null)
                return new ChatResponse("Chat not found");
            try
            {
                _ChatRepository.Remove(existingChat);


                return new ChatResponse(existingChat);
            }
            catch (Exception e)
            {
                return new ChatResponse($"An error ocurred while deleting the Chat: {e.Message}");
            }
        }

        public async Task<ChatResponse> GetByIdAsync(int id)
        {
            var existingChat = await _ChatRepository.FindById(id);
            if (existingChat == null)
                return new ChatResponse("Chat not found");
            return new ChatResponse(existingChat);
        }

        public async Task<IEnumerable<Chat>> ListAsync()
        {
            return await _ChatRepository.ListAsync();
        }

        public async Task<ChatResponse> SaveAsync(Chat chat)
        {
            try
            {
                await _ChatRepository.AddAsync(chat);
                return new ChatResponse(chat);
            }
            catch (Exception e)
            {
                return new ChatResponse($"An error ocurred while saving the Chat: {e.Message}");
            }
        }

        public async Task<ChatResponse> UpdateAsync(int id, Chat chat)
        {
            var existingChat = await _ChatRepository.FindById(id);
            if (existingChat == null)
                return new ChatResponse("Chat not found");
            try
            {
                _ChatRepository.Update(existingChat);

                return new ChatResponse(chat);
            }
            catch (Exception e)
            {
                return new ChatResponse($"An error ocurred while updating the Chat: {e.Message}");
            }
        }
    }
}
