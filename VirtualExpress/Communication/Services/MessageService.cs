using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Communication.Domain.Models;
using VirtualExpress.Communication.Domain.Repositories;
using VirtualExpress.Communication.Domain.Services;
using VirtualExpress.Communication.Domain.Services.Responses;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.Communication.Services
{
    public class MessageService : IMessageService
    {
        private readonly IChatRepository _chatRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly IMessageRepository _MessageRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IChatRepository chatRepository, ICompanyRepository companyRepository, ICustomerRepository customerRepository, IMessageRepository messageRepository, IUnitOfWork unitOfWork)
        {
            _chatRepository = chatRepository;
            _companyRepository = companyRepository;
            _customerRepository = customerRepository;
            _MessageRepository = messageRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<MessageResponse> DeleteAsync(int id)
        {
            var existingMessage = await _MessageRepository.FindById(id);

            if (existingMessage == null)
                return new MessageResponse("Message not found");

            try
            {
                _MessageRepository.Remove(existingMessage);
                await _unitOfWork.CompleteAsync();
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
            if (existingchats == null)
            {
                return new MessageResponse("Chat doesnt exist");
            }

            if(message.CustomerId == 0)
            {
                var existingCompany = await _companyRepository.FindCompanyById((int)message.CompanyId);
                if(existingCompany == null)
                {
                    return new MessageResponse("Company not found");
                }
                message.CustomerId = null;
                message.Company = existingCompany;
            }

            if (message.CompanyId == 0)
            {
                var existingCustomer = await _customerRepository.FindById((int)message.CustomerId);
                if (existingCustomer == null)
                {
                    return new MessageResponse("Customer not found");
                }
                message.CompanyId = null;
                message.Customer = existingCustomer;
            }
            message.Chat = existingchats;
            try
            {
                await _MessageRepository.AddAsync(message);
                await _unitOfWork.CompleteAsync();
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
            existingMessage.CompanyId = message.CompanyId;
            existingMessage.CustomerId = message.CustomerId;
            try
            {
                _MessageRepository.Update(existingMessage);
                await _unitOfWork.CompleteAsync();
                return new MessageResponse(message);
            }
            catch (Exception e)
            {
                return new MessageResponse($"An error ocurred while updating the Message: {e.Message}");
            }
        }
    }
}
