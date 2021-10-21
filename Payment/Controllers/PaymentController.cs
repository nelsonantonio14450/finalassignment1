using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Payment.Data;
using Payment.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Linq;



namespace Payment.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)] //ini buat authorize
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly ApiDBContext _context;

        public PaymentController(ApiDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            var items = await _context.Items.ToListAsync();
            return Ok(items);
        }

        //get by paid at
        [HttpGet("GetDate/{date1}, {date2}")] //specific date
        public async Task<IActionResult> GetByDateMovie(DateTime date1, DateTime date2)
        {
            var item = await _context.Items.Where(x => x.paidAt >= date1 && x.paidAt <= date2).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("MoreDate/{date1}")] //specific date
        public async Task<IActionResult> GetByDateMore(DateTime date1)
        {
            var item = await _context.Items.Where(x => x.paidAt >= date1).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("LessDate/{date1}")] //specific date
        public async Task<IActionResult> GetByDateLess(DateTime date1)
        {
            var item = await _context.Items.Where(x => x.paidAt <= date1).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }


        //amount
        [HttpGet("GetByAmount/{price1}, {price2}")] //specific price
        public async Task<IActionResult> GetByAmount(int price1, int price2)
        {
            var item = await _context.Items.Where(x => x.Amount >= price1 && x.Amount <= price2).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }



        //expired date
        [HttpGet("MoreExpDate/{date1}")] //specific date
        public async Task<IActionResult> GetByExpDateMore(DateTime date1)
        {
            var item = await _context.Items.Where(x => x.expirationDate >= date1).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("LessExpDate/{date1}")] //specific date
        public async Task<IActionResult> GetByExpDateLess(DateTime date1)
        {
            var item = await _context.Items.Where(x => x.expirationDate <= date1).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("GetExpDate/{date1}, {date2}")] //specific date
        public async Task<IActionResult> GetByExpDate(DateTime date1, DateTime date2)
        {
            var item = await _context.Items.Where(x => x.expirationDate >= date1 && x.expirationDate <= date2).ToListAsync();

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpGet("{id}")] //search by id
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _context.Items.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (item == null)
                return NotFound();

            return Ok(item);
        }


        //insert, update, delete
        [HttpPost] //insert
        public async Task<IActionResult> CreateItem(PaymentDetails data)
        {
            if (ModelState.IsValid)
            {
                await _context.Items.AddAsync(data);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItems", new { data.paymentDetailId }, data);
            }
            return new JsonResult("tidak bisa membayar") { StatusCode = 500 };
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateItem(int id, PaymentDetails item)
        {
            if (id != item.paymentDetailId)
                return BadRequest();

            var existItem = await _context.Items.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (existItem == null)
                return NotFound();

            existItem.cardOwnerName = item.cardOwnerName;
            existItem.cardNumber = item.cardNumber;
            existItem.Amount = item.Amount;
            existItem.paidAt = existItem.paidAt;
            existItem.expirationDate = item.expirationDate;
            existItem.securityCode = item.securityCode;

            await _context.SaveChangesAsync();

            return new JsonResult("Data Updated") { StatusCode = 201 };
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItem(int id)
        {
            var existItem = await _context.Items.FirstOrDefaultAsync(x => x.paymentDetailId == id);

            if (existItem == null)
                return NotFound();

            _context.Items.Remove(existItem);
            await _context.SaveChangesAsync();

            return new JsonResult("Data Deleted") { StatusCode = 201 };
        }
    }
}