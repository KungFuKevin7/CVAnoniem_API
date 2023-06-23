namespace REST_API
{
    public interface IRepository<T>
    {
        List<T> Get();
        List<T> GetByID(int id);
        int Add(T objectsToAdd);
        void Update(T updatedObject, int id);
        void Delete(int id);
    }
}
