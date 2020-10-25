using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Social.Domain.Services.Responses;

namespace VirtualExpress.Social.Domain.Services
{
    public interface ICommentaryService
    {
        Task<IEnumerable<Commentary>> ListAsync();
        Task<CommentaryResponse> SaveAsync(Commentary comentary);
        Task<CommentaryResponse> GetById(int id);
        Task<CommentaryResponse> UpdateAsync(int id, Commentary commentary);
        Task<CommentaryResponse> DeleteAsync(int id);
    }
}
