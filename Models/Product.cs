namespace TiendaGuau.Models
{
    public class Product
    {
        //[Key]//! Así se declara con data annotations. En TareasContext esta especificado con FLUENT API
        public int ProductId { get; set; }

        //[ForeignKey("ClientId")]
        public int ClientId { get; set; }

        //[Required]
        public string NameProduct { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        //*public virtual ICollection<Client> Client {get; set;}
        public virtual Client Client { get; set; }
    }
}
