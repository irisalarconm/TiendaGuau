using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TiendaGuau.Models;
using TiendaGuau.Services;
using TiendaGuau.Validators;

namespace TiendaGuau.Controllers
{
    //[Route("[controller]")]
    public class ClientsController : Controller
    {
        private readonly TiendaGuauContext _context;
        IClientService clientService;
        ClientValidator clientValidator;

        public ClientsController(TiendaGuauContext context, IClientService service, ClientValidator validator)
        {
            _context = context;
            clientService = service;
            clientValidator = validator;
        }

        public IActionResult Index()
        {
            try
            {
                var getClients = clientService.Get();
                return View(getClients);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View();
            }


        }

        // GET: Clients/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }
            try
            {
                var details = await clientService.Details(id);
                return View(details);
            }
            catch(Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View();
            }
            
        }
            

        // GET: Clients/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClientId,NameClient,LastnameClient,DNIClient,AdressClient,Phone, status")]Client client)
        {
            var validation = clientValidator.Validate(client);

            if (!validation.IsValid)
            {
               foreach(var e in validation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                return View(client);
            }

            try
            {
                await clientService.Save(client);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View(client);
            }

            
        }
        

        // GET: Clients/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }

            try
            {
                var client = await _context.Client.FindAsync(id);
                return View(client);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View();
            }

           
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ClientId,NameClient,LastnameClient,DNIClient,AdressClient,Phone,status")] Client client)
        {
            var validation = clientValidator.Validate(client);

            if (!validation.IsValid)
            {
                foreach (var e in validation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                return View(client);
            }

            try
            {
                await clientService.Update(client);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View(client);
            }

            
            //return Ok();

        }

        // GET: Clients/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Client == null)
            {
                return NotFound();
            }
            var client = await _context.Client
                .FirstOrDefaultAsync(m => m.ClientId == id);

            if (client == null)
            {
                return NotFound();
            }

            return View(client);

        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Client client)
        {
            try
            {
                await clientService.Delete(client);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View(client);
            }

            
            
        }

    }
}
