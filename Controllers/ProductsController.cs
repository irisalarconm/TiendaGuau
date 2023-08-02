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
    public class ProductsController : Controller
    {
        private readonly TiendaGuauContext _context;
        IProductService productService;
        ProductValidator productValidator;
        IClientService clientService;
        

        public ProductsController(TiendaGuauContext context, IProductService service, ProductValidator validator, IClientService serviceClient)
        {
            _context = context;
            productService = service;
            clientService = serviceClient;
            productValidator = validator;
            
        }

        // GET: Products
        public IActionResult Index()
        {
            try
            {
                var getProducts = productService.Get();
                return View(getProducts);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View();
            }

            
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var details = await productService.Details(id);
            
   
            if (details == null)
            {
                return NotFound();
            }
            
            return View(details);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewBag.ClientId = new SelectList(_context.Client, "ClientId", "NameClient");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ClientId,NameProduct,Description,Price")] Product product)
        {
            ViewBag.ClientId = new SelectList(_context.Client, "ClientId", "NameClient");

            var validation = productValidator.Validate(product);

            if (!validation.IsValid)
            {
                foreach (var e in validation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                return View(product);
            }

            try
            {
                await productService.Save(product);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View(product);
            }
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            try
            {
                var product = await _context.Product.FindAsync(id);
                ViewBag.ClientList = new SelectList(_context.Client, "ClientId", "NameClient");
                
                if (product == null)
                {
                    return NotFound();
                }

                return View(product);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View();
            }
            
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("ProductId,ClientId,NameProduct,Description,Price")] Product product)
        {
            var validation = productValidator.Validate(product);

            if (!validation.IsValid)
            {
                foreach (var e in validation.Errors)
                {
                    ModelState.AddModelError(e.PropertyName, e.ErrorMessage);
                }
                return View(product);
            }

            try
            {
                await productService.Update(product);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View(product);
            }
           
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Product == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Client)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Product product)
        {
            if (_context.Product == null)
            {
                return Problem("Entity set 'TiendaGuauContext.Product'  is null.");
            }
            try
            {
                await productService.Delete(product);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Error in the creation: " + ex.Message);
                return View(product);
            }
           
        }
    }
}
