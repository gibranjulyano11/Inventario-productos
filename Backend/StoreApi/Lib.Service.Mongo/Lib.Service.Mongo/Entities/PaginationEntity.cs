using System.Collections.Generic;

namespace Lib.Service.Mongo.Entities
{
    public class PaginationEntity<TDocument> : Pagination
    {
        public int totalPages { get; set; }
        public int totalRows { get; set; }
        public IEnumerable<TDocument> data { get; set; }
    }
}
