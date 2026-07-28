using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using InventoryApi.Models;
using InventoryApi.Areas.Identity.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace InventoryApi.Controllers
{
    [Authorize]
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public class ProductSearchParameters
        {
            public string? Title { get; set; }
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts([FromQuery] string? search)
        {
            string? vendorIdClaim = User.FindFirstValue("VendorId");

            int? vendorId = int.TryParse(vendorIdClaim, out var id) ? id : 0;

            var filteredProducts = _context.Products.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string cleanSearch = search.Trim().ToLower();
                filteredProducts = filteredProducts.Where(p => p.Category.Name.ToLower().Contains(cleanSearch) ||
                                                                p.Title.ToLower().Contains(cleanSearch));
            }

            if (!User.IsInRole("Admin"))
            {
                filteredProducts = filteredProducts
                    .Where(p => p.VendorId == vendorId);
            }
            else
            {
                filteredProducts = filteredProducts
                    .Include(p => p.Vendor);
            }

            return await filteredProducts.Include(p => p.Category).ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            // var product = await _context.Products.FindAsync(id);
            var product = await _context.Products.Include(p => p.Shipments).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, Product productdto)
        {

            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Title = string.IsNullOrEmpty(productdto.Title) ? null : productdto.Title;
            product.Sku = string.IsNullOrEmpty(productdto.Sku) ? null : productdto.Sku;
            product.CategoryId = productdto.CategoryId.HasValue ? productdto.CategoryId : null;
            product.Price = productdto.Price;
            product.Quantity = productdto.Quantity;
            product.Description = string.IsNullOrEmpty(productdto.Description) ? null : productdto.Description;
            product.Notes = string.IsNullOrEmpty(productdto.Notes) ? null : productdto.Notes;
            product.IntakeDate = productdto.IntakeDate;
            product.LocationId = productdto.LocationId.HasValue ? productdto.LocationId : null;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPut("assign-shipments/{id}")]
        public async Task<IActionResult> AssignShipments(int id, [FromBody] List<Shipment> shipments)
        {
            var product = await _context.Products.Include(p => p.Shipments).FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            var incomingIds = shipments.Select(c => c.Id).ToList();
            var toRemove = product.Shipments.Where(c => !incomingIds.Contains(c.Id)).ToList();
            _context.Shipments.RemoveRange(toRemove);

            foreach (var shipment in shipments)
            {
                var child = product.Shipments.FirstOrDefault(c => c.Id == shipment.Id && shipment.Id != 0);
                if (child == null)
                {
                    product.Shipments.Add(new Shipment
                    {
                        TrackingCode = shipment.TrackingCode,
                        Address = shipment.Address,
                        City = shipment.City,
                        State = shipment.State,
                        Zip = shipment.Zip,
                    });
                }
                else
                {
                    child.TrackingCode = shipment.TrackingCode;
                    child.Address = shipment.Address;
                    child.City = shipment.City;
                    child.State = shipment.State;
                    child.Zip = shipment.Zip;
                }
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            string? vendorIdClaim = User.FindFirstValue("VendorId");
            int? vendorId = int.TryParse(vendorIdClaim, out var id) ? id : 0;

            product.VendorId = vendorId;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProduct), new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
