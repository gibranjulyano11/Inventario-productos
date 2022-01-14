namespace Lib.Service.Mongo.Entities
{
    public class Pagination
    {
        public int pageSize { get; set; } = 100;
        public int page { get; set; } = 1;
    }
}
