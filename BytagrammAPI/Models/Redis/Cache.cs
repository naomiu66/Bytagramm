namespace BytagrammAPI.Models.Redis
{
    public class Cache<T> where T : class, ICachable
    {
        public string Key { get; set; }
        public T Payload { get; set; }
    }
}
