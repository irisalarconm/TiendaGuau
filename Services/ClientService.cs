using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TiendaGuau.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TiendaGuau.Services
{
    public class ClientService : IClientService
    {
        TiendaGuauContext context;

        public ClientService(TiendaGuauContext dbcontext)
        {
            context = dbcontext;
        }

        public IEnumerable<Client> Get()
        {
            return context.Client;
        }

        public async Task<Client> Details(int? id)
        {
       
            var client = await context.Client
                .FirstOrDefaultAsync(m => m.ClientId == id);

            return client;

        }

        public async Task Save(Client client)
        {

            context.Add(client);

            await context.SaveChangesAsync(true);

        }

        public async Task Update(Client client)
        {
            context.Update(client); //metodo EF.
            await context.SaveChangesAsync();
            
        }

        public async Task Delete(Client client)
        {
            
            context.Remove(client);

                await context.SaveChangesAsync();
        }

    }

    public interface IClientService
    {
        IEnumerable<Client> Get();

        Task<Client> Details(int? id);
        Task Save(Client client);
        Task Update(Client client);

        Task Delete(Client client);
    }
}
