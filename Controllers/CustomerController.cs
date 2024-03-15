using ManyToManyCodeFirst.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace ManyToManyCodeFirst.Controllers;
//using Microsoft.AspNetCore.Mvc;
[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    //using ManyToManyCodeFirst.Models;  
    private readonly OrderContext _context;
    public CustomersController(OrderContext context)
    {
        _context = context;
    }


    // GET: api/Customers
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers()
    {
        if (_context.Customers == null)
        {
            return NotFound();
        }
        //using Microsoft.EntityFrameworkCore;
        return await _context.Customers.ToListAsync();
    }


    // GET: api/Customers/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer(int id)
    {
        if (_context.Customers == null)
        {
            return NotFound();
        }
       
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }
        return customer;
    }
   
    // PUT: api/Customers/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCustomer(int id, Customer customer)
    {
        if (id != customer.CustomerId)
        {
            return BadRequest();
        }


        _context.Customers.Update(customer);
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CustomerExists(id))
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


    // POST: api/Customers
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
    {
        if (_context.Customers == null)
        {
            return Problem("Entity set 'OrderContext.Customers'  is null.");
        }
       
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();


        return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
    }


    // DELETE: api/Customers/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCustomer(int id)
    {
        if (_context.Customers == null)
        {
            return NotFound();
        }
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
        {
            return NotFound();
        }


        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();


        return NoContent();
    }


    private bool CustomerExists(int id)
    {
        return (_context.Customers?.Any(e => e.CustomerId == id)).GetValueOrDefault();
    }
}
