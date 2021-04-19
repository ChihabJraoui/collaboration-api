using Application.Repositories;
using AutoMapper;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class MethodesRepository : IMethodesRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public MethodesRepository(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<bool> UniqueName(string name, CancellationToken cancellationToken)
        {
            return await _context.Workspaces
                .AllAsync(n => n.Name != name);
        }

    }
}
