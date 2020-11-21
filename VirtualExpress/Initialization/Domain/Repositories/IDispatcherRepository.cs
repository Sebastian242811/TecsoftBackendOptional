﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Initialization.Domain.Model;

namespace VirtualExpress.Initialization.Domain.Repositories
{
    public interface IDispatcherRepository
    {
        Task<IEnumerable<Dispatcher>> ListAsync();
        Task AddAsync(Dispatcher Dispatcher);
        Task<Dispatcher> GetDispatcherByUsernameAndPassword(string Username, string Password);
        Task<Dispatcher> FindById(int id);
        void Update(Dispatcher Dispatcher);
        void Remove(Dispatcher Dispatcher);
        
    }
}
