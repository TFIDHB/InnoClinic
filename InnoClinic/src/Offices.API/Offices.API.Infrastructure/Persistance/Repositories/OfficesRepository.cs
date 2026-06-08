using Application.Interfaces;
using Domain.Entities;
using InnoClinic.Shared.Repositories;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace Infrastructure.Persistance.Repositories
{
    public class OfficesRepository(OfficesDbContext context) : IOfficesRepository
    {
        private readonly IMongoCollection<Office> _collection = context.Offices;

        public async Task<Office?> GetByIdAsync(Guid id, CancellationToken ct = default)
            => await _collection.Find(x => x.Id == id).FirstOrDefaultAsync(ct);

        public async Task<IEnumerable<Office>> GetAllAsync(CancellationToken ct = default)
            => await _collection.Find(_ => true).ToListAsync(ct);

        public async Task CreateAsync(Office office, CancellationToken ct = default)
            => await _collection.InsertOneAsync(office, null, ct);

        public async Task UpdateAsync(Office office, CancellationToken ct = default)
            => await _collection.ReplaceOneAsync(x => x.Id == office.Id, office, cancellationToken: ct);

        public async Task DeleteAsync(Guid id, CancellationToken ct = default)
            => await _collection.DeleteOneAsync(x => x.Id == id, ct);

        public Task<bool> AnyAsync(Expression<Func<Office, bool>> predicate, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
