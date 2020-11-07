using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.Communication.Domain.Services;
using VirtualExpress.Communication.Domain.Services.Responses;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Communication.Services
{
    public class MessageService : IMessageService
    {
        public readonly IChatRepository _chatRepository;
        public readonly ICustomerServiceEmployeeRepository _customerServiceEmployeeRepository;
        public readonly ICustomerRepository _customerRepository;
        public readonly IMessageRepository _MessageRepository;
        public async Task<MessageResponse> DeleteAsync(int id)
        {
            var existingMessage = await _MessageRepository.FindById(id);
            if (existingMessage == null)
                return new MessageResponse("Message not found");
            try
            {
                _MessageRepository.Remove(existingMessage);
                

                return new MessageResponse(existingMessage);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error ocurred while deleting the Message: {e.Message}");
            }
        }

        public async Task<MessageResponse> GetByIdAsync(int id)
        {
            var existingMessage = await _MessageRepository.FindById(id);
            if (existingMessage == null)
                return new MessageResponse("Message not found");
            return new MessageResponse(existingMessage);
        }

        public async Task<IEnumerable<Message>> ListAsync()
        {
            return await _MessageRepository.ListAsync();
        }

        public async Task<IEnumerable<Message>> ListByChatByIdAsync(int id)
        {
            return await _MessageRepository.ListByChatByIdAsync(id);
        }

        public async Task<MessageResponse> SaveAsync(Message message)
        {
            var existingchats = await _chatRepository.FindById(message.ChatId);
            var existingCustomer = await _customerRepository.FindById(message.CustomerId);
            var existingCustomerServEmployee = await _customerServiceEmployeeRepository.FindById(message.CustomerServiceEmployeeId);
            if (existingchats == null)
            {
                return new MessageResponse("Chat doesnt exist");
            }
            if (existingCustomer == null)
            {
                return new MessageResponse("Customer doesnt exist");
            }
            if (existingCustomerServEmployee == null)
            {
                return new MessageResponse("Customer Service Employee doesnt exist");
            }
            try
            {
                await _MessageRepository.AddAsync(message);
                return new MessageResponse(message);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error ocurred while saving the Message: {e.Message}");
            }
        }

        public async Task<MessageResponse> UpdateAsync(int id, Message message)
        {
            var existingMessage = await _MessageRepository.FindById(id);
            if (existingMessage == null)
                return new MessageResponse("Message not found");
            existingMessage.ChatId = message.ChatId;
            existingMessage.CustomerServiceEmployeeId = message.CustomerServiceEmployeeId;
            existingMessage.CustomerId = message.CustomerId;
            try
            {
                _MessageRepository.Update(existingMessage);

                return new MessageResponse(message);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error ocurred while updating the Message: {e.Message}");
            }
        }
    }
}
