namespace TiendaApi.Models
{
    public class ItemStoreDatabaseSettings
    {
        public string ConnectionStrings { get; set; } = null!;

        public string Database { get; set; } = null!;

        public string ItemsCollectionName { get; set; } = null!;
    }
}
