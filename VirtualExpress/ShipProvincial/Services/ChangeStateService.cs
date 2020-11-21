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
        private readonly IPackageService _packageRepository;
        private readonly IChangeStateRepository _changeStateRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeStateService(IUnitOfWork unitOfWork, IChangeStateRepository changeStateRepository, IPackageService packageRepository)
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

        public async Task<ChangeStateResponse> GetByPackageIdAndInitStateAndEndState(int packageID, int initState, int endState)
        {
            var existingChange = await _changeStateRepository.GetByPackageIdAndInitStateAndEndState(packageID, initState, endState);
            if (existingChange == null)
                return new ChangeStateResponse("Change not found");
            return new ChangeStateResponse(existingChange);
        }

        public async Task<IEnumerable<ChangeState>> ListAsync()
        {
            return await _changeStateRepository.ListAsync();
        }

        public async Task<IEnumerable<ChangeState>> ListAsyncbypackageid(int id)
        {
            return await _changeStateRepository.ListAsyncbypackageid(id);
        }

        public async Task<ChangeStateResponse> SaveAsync(ChangeState ChangeState)
        {

            try
            {
                await _changeStateRepository.AddAsync(ChangeState);
                await _unitOfWork.CompleteAsync();
                await _packageRepository.UpdateStateAsync(ChangeState.PackageId,(int)ChangeState.FinalState);
                return new ChangeStateResponse(ChangeState);
            }
            catch (Exception e)
            {
                return new ChangeStateResponse($"An error ocurred while saving the ChangeState: {e.Message}");
            }
        }
    }
}
