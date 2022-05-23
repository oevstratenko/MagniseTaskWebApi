namespace MagniseTaskBC
{
    public interface IMagniseTaskRepository
    {
        Task SaveChangesAsync();
        IQueryable<T> Set<T>() where T : class;
        IQueryable<T> NoTrack<T>() where T : class;
        void AddObject<T>(T aEntity) where T : class;
    }
}
