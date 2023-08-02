using Microsoft.EntityFrameworkCore;
using TiendaGuau.Models;

namespace TiendaGuau.Services
{
    public class ProductService : IProductService
    {
        TiendaGuauContext context;

        public ProductService(TiendaGuauContext dbcontext)
        {
            context = dbcontext;
        }


        public IEnumerable<Product> Get()
        {
            return context.Product.Include(p=>p.Client);
        }
        public async Task<Product> Details(int? id)
        {

            var product = await context.Product
                .Include(p=>p.Client)
                .FirstOrDefaultAsync(m => m.ProductId == id);

            return product;

        }

        public async Task Save(Product product)
        {
            context.Add(product);

            await context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
            /*var ActualProduct = context.Product.Find(id);

            if (ActualProduct != null)
            {

                ActualProduct.NameProduct = product.NameProduct;
                ActualProduct.ClientId = product.ClientId;
                ActualProduct.Description = product.Description;
                ActualProduct.Price = product.Price;

                await context.SaveChangesAsync();

            }*/

            context.Update(product); //metodo EF.
            await context.SaveChangesAsync();
        }

        public async Task Delete(Product product)
        {
            context.Remove(product);

            await context.SaveChangesAsync();
        }

    }

    public interface IProductService
    {
        IEnumerable<Product> Get();

        Task<Product> Details(int? id);

        Task Save(Product product);

        Task Update(Product product);

        Task Delete(Product product);
    }
}
