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
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly ICustomerRepository _customerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChatService(IChatRepository chatRepository, IUnitOfWork unitOfWork, ICustomerRepository customerRepository, ICompanyRepository companyRepository)
        {
            _chatRepository = chatRepository;
            _unitOfWork = unitOfWork;
            _customerRepository = customerRepository;
            _companyRepository = companyRepository;
        }

        public async Task<ChatResponse> DeleteAsync(int id)
        {
            var existingChat = await _chatRepository.FindById(id);
            if (existingChat == null)
                return new ChatResponse("Chat not found");
            try
            {
                _chatRepository.Remove(existingChat);
                await _unitOfWork.CompleteAsync();
                return new ChatResponse(existingChat);
            }
            catch (Exception e)
            {
                return new ChatResponse($"An error ocurred while deleting the Chat: {e.Message}");
            }
        }

        public async Task<ChatResponse> GetByIdAsync(int id)
        {
            var existingChat = await _chatRepository.FindById(id);
            if (existingChat == null)
                return new ChatResponse("Chat not found");
            return new ChatResponse(existingChat);
        }

        public async Task<IEnumerable<Chat>> ListAsync()
        {
            return await _chatRepository.ListAsync();
        }

        public async Task<IEnumerable<Chat>> ListAsyncByCompanyId(int companyId)
        {
            return await _chatRepository.ListAsyncByCompanyId(companyId);
        }

        public async Task<IEnumerable<Chat>> ListAsyncByCustomerId(int customerId)
        {
            return await _chatRepository.ListAsyncByCustomerId(customerId);
        }

        public async Task<ChatResponse> SaveAsync(Chat chat)
        {
            var existingCustomer = await _customerRepository.FindById(chat.CustomerId);
            if (existingCustomer == null)
                return new ChatResponse("Customer not found");

            var existingCompany = await _companyRepository.FindCompanyById(chat.CompanyId);
            if (existingCompany == null)
                return new ChatResponse("Company not found");

            chat.Customer = existingCustomer;
            chat.Company = existingCompany;
            try
            {
                await _chatRepository.AddAsync(chat);
                await _unitOfWork.CompleteAsync();
                return new ChatResponse(chat);
            }
            catch (Exception e)
            {
                return new ChatResponse($"An error ocurred while saving the Chat: {e.Message}");
            }
        }

        public async Task<ChatResponse> UpdateAsync(int id, Chat chat)
        {
            var existingChat = await _chatRepository.FindById(id);
            if (existingChat == null)
                return new ChatResponse("Chat not found");
            try
            {
                _chatRepository.Update(existingChat);
                await _unitOfWork.CompleteAsync();
                return new ChatResponse(chat);
            }
            catch (Exception e)
            {
                return new ChatResponse($"An error ocurred while updating the Chat: {e.Message}");
            }
        }
    }
}
