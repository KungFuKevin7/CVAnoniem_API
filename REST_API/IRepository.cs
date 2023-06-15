namespace REST_API
{
    public interface IRepository<T>
    {
        /// <summary>
        /// Method that handles the GET Request
        /// </summary>
        /// <returns>List of the given type</returns>
        List<T> Get();

        /// <summary>
        /// Method that handles the GET Request using an id
        /// </summary>
        /// <param name="id">id parameter to get items by id</param>
        /// <returns>List of the given type that has the reoccuring id</returns>
        List<T> GetByID(int id);

        /// <summary>
        /// Method that handles the POST Request
        /// </summary>
        /// <param name="objectsToAdd">An object of the given type, to be added
        /// to the database</param>
        void Add(T objectsToAdd);
        /// <summary>
        /// Method that handles the PUT Request
        /// </summary>
        /// <param name="updatedObject">An object of the given type, to be 
        /// updated in the database</param>
        /// <param name="id">id of the item that ought to be updated</param>
        void Update(T updatedObject, int id);

        /// <summary>
        /// Method that Handles the DELETE Request
        /// </summary>
        /// <param name="id">id of the item that ought to be removed</param>
        void Delete(int id);
    }
}
