using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.CompanyManagement.Domain.Models;
using VirtualExpress.CompanyManagement.Domain.Repositories;
using VirtualExpress.CompanyManagement.Domain.Services;
using VirtualExpress.CompanyManagement.Domain.Services.Responses;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.Initialization.Domain.Repositories;

namespace VirtualExpress.CompanyManagement.Services
{
    public class TerminalService: ITerminalService
    {
        private readonly ITerminalRepository _terminalRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICityRepository _cityRepository;
        private readonly ICompanyRepository _companyRepository;

        public TerminalService(ITerminalRepository terminalRepository, IUnitOfWork unitOfWork, ICityRepository cityRepository, ICompanyRepository companyRepository)
        {
            _terminalRepository = terminalRepository;
            _unitOfWork = unitOfWork;
            _cityRepository = cityRepository;
            _companyRepository = companyRepository;
        }

        public async Task<TerminalResponse> AssignTerminalCompanyAsync(int terminaId, int companyID)
        {
            try
            {
                await _terminalRepository.AssignTerminalCompany(terminaId, companyID);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception e)
            {
                return new TerminalResponse($"An error ocurred while assigning the terminal: {e.Message}");
            }

            return new TerminalResponse(await _terminalRepository.FindByTerminalIdAndCompanyId(terminaId, companyID));
        }

        public async Task<TerminalResponse> DeleteAsync(int id)
        {
            var existingTerminal = await _terminalRepository.FindById(id);
            if (existingTerminal == null)
                return new TerminalResponse("Terminal not found");
            try
            {
                _terminalRepository.Remove(existingTerminal);
                await _unitOfWork.CompleteAsync();

                return new TerminalResponse(existingTerminal);
            }
            catch (Exception e)
            {
                return new TerminalResponse($"An error ocurred while deleting the terminal: {e.Message}");
            }
        }

        public async Task<TerminalResponse> GetByIdAsync(int id)
        {
            var existing = await _terminalRepository.FindById(id);
            if (existing == null)
                return new TerminalResponse("Terminal not found");
            return new TerminalResponse(existing);
        }

        public async Task<IEnumerable<Terminal>> ListAsync()
        {
            return await _terminalRepository.ListAsync();
        }

        public async Task<IEnumerable<Terminal>> ListByCityOriginIdAndCityShipIdAsync(int cityOriginId, int cityShipId)
        {
            return await _terminalRepository.ListByCityOriginIdAndCityShipIdAsync(cityOriginId, cityShipId);
        }

        public async Task<IEnumerable<Terminal>> ListByCompanyByIdAsync(int id)
        {
            return await _terminalRepository.ListByCompanyByIdAsync(id);
        }

        public async Task<TerminalResponse> SaveAssync(Terminal terminal)
        {
            var existingterminals = await _terminalRepository.ListAsync();
            var existingcities = await _cityRepository.FindById(terminal.CityId);
            var existingcompany = await _companyRepository.FindCompanyById(terminal.CompanyId);
            foreach(Terminal Terminal in existingterminals)
            {
                if(Terminal.Name.Equals(terminal.Name) && Terminal.Adress.Equals(Terminal.Adress) && Terminal.CityId==terminal.CityId && Terminal.CompanyId==terminal.CompanyId)
                {
                    return new TerminalResponse("This terminal is already registered");
                }
            }
            if (existingcities == null)
            {
                return new TerminalResponse("City doesnt exist");
            }
            if (existingcompany == null)
            {
                return new TerminalResponse("Company doesnt exist");
            }
            try
            {
                await _terminalRepository.AddAsync(terminal);
                await _unitOfWork.CompleteAsync();

                return new TerminalResponse(terminal);
            }
            catch (Exception e)
            {
                return new TerminalResponse($"An error ocurred while saving the terminal: {e.Message}");
            }

        }

        public async Task<TerminalResponse> UpdateAssync(int id, Terminal terminal)
        {
            var existingTerminal = await _terminalRepository.FindById(id);
            if (existingTerminal == null)
                return new TerminalResponse("Terminal not found");
            existingTerminal.Name = terminal.Name;
            existingTerminal.Adress = terminal.Adress;
            try
            {
                _terminalRepository.Update(existingTerminal);
                await _unitOfWork.CompleteAsync();

                return new TerminalResponse(existingTerminal);
            }
            catch (Exception e)
            {
                return new TerminalResponse($"An error ocurred while deleting the terminal: {e.Message}");
            }
        }
    }
}
