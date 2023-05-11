namespace REST_API
{
    public interface IRepository<T>
    {
        string Get();
        void Add(T[] objectsToAdd);
        void Update(T updatedObject, int id);
        void Delete(int id);
    }
}
