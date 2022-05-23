using MagniseTaskDAC;
using Microsoft.EntityFrameworkCore;

namespace MagniseTaskBC
{
    public class MagniseTaskRepository : IMagniseTaskRepository
    {
        private readonly MagniseTaskContext _context;
        public MagniseTaskRepository(MagniseTaskContext context) => _context = context;

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public IQueryable<T> Set<T>() where T : class
        {
            return _context.Set<T>();
        }

        public IQueryable<T> NoTrack<T>() where T : class
        {
            return _context.Set<T>().AsNoTracking();
        }

        public void AddObject<T>(T aEntity) where T : class
        {
            _context.Set<T>().Add(aEntity);
        }
    }
}
