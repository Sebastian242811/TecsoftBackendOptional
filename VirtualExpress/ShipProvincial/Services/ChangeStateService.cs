using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Models;
using VirtualExpress.ShipProvincial.Domain.Repositories;
using VirtualExpress.ShipProvincial.Domain.Services;
using VirtualExpress.ShipProvincial.Domain.Services.Responses;

namespace VirtualExpress.ShipProvincial.Services
{
    public class ChangeStateService : IChangeStateService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IChangeStateRepository _changeStateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeStateService(IUnitOfWork unitOfWork, IChangeStateRepository changeStateRepository, IPackageRepository packageRepository)
        {
            this._unitOfWork = unitOfWork;
            _changeStateRepository = changeStateRepository;
            _packageRepository = packageRepository;
        }

        public async Task<ChangeStateResponse> FindChangeStateById(int id)
        {
            var existingChangeState = await _changeStateRepository.FindById(id);
            if (existingChangeState == null)
                return new ChangeStateResponse("ChangeState not found");
            return new ChangeStateResponse(existingChangeState);
        }

        public async Task<IEnumerable<ChangeState>> ListAsync()
        {
            return await _changeStateRepository.ListAsync();
        }

        public async Task<ChangeStateResponse> SaveAsync(ChangeState ChangeState)
        {

            try
            {
                await _changeStateRepository.AddAsync(ChangeState);
                await _unitOfWork.CompleteAsync();
                return new ChangeStateResponse(ChangeState);
            }
            catch (Exception e)
            {
                return new ChangeStateResponse($"An error ocurred while saving the ChangeState: {e.Message}");
            }
        }
    }
}
