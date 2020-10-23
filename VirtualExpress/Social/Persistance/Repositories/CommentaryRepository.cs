using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VirtualExpress.General.Persistance.Context;
using VirtualExpress.General.Repositories;
using VirtualExpress.Social.Domain.Models;
using VirtualExpress.Social.Domain.Repositories;

namespace VirtualExpress.Social.Persistance.Repositories
{
    public class CommentaryRepository:BaseRepository, ICommentaryRepository
    {
        public CommentaryRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddAsync(Commentary comentary)
        {
            await _context.Comentaries.AddAsync(comentary);
        }

        public async Task<Commentary> FindById(int id)
        {
            return await _context.Comentaries.FindAsync(id);
        }

        public async Task<IEnumerable<Commentary>> ListAsync()
        {
            return await _context.Comentaries.ToListAsync();
        }

        public void Remove(Commentary comentary)
        {
            _context.Comentaries.Remove(comentary);
        }

        public void Update(Commentary comentary)
        {
            _context.Comentaries.Update(comentary);
        }
    }
}
