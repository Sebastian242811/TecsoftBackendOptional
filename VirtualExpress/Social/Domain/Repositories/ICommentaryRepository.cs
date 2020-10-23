using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Social.Domain.Models;

namespace VirtualExpress.Social.Domain.Repositories
{
    public interface ICommentaryRepository
    {
        Task<IEnumerable<Commentary>> ListAsync();
        Task AddAsync(Commentary Comentary);
        Task<Commentary> FindById(int id);
        void Update(Commentary Comentary);
        void Remove(Commentary Comentary);
    }
}
