using PlayBox.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBox.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Content> Contents { get; }
        IGenericRepository<Comment> Comments { get; }
        IGenericRepository<Link> Links { get; }
        IGenericRepository<User> Users { get; }
        Task<int> CompleteAsync();
    }
}
