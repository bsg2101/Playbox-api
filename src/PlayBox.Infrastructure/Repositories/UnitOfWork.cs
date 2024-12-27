using PlayBox.Domain.Entities;
using PlayBox.Domain.Interfaces;
using PlayBox.Infrastructure.Data;
using PlayBox.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        private IGenericRepository<Content> _contentRepository;
        private IGenericRepository<Comment> _commentRepository;
        private IGenericRepository<Link> _linkRepository;
        private IGenericRepository<User> _userRepository;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Content> Contents => _contentRepository ??= new GenericRepository<Content>(_context);
        public IGenericRepository<Comment> Comments => _commentRepository ??= new GenericRepository<Comment>(_context);
        public IGenericRepository<Link> Links => _linkRepository ??= new GenericRepository<Link>(_context);
        public IGenericRepository<User> Users => _userRepository ??= new GenericRepository<User>(_context);

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
