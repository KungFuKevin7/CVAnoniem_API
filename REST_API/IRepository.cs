namespace REST_API
{
    public interface IRepository<T>
    {
        List<T> Get();
        T GetByID(int id);
        void Add(T objectsToAdd);
        void Update(T updatedObject, int id);
        void Delete(int id);
    }
}
