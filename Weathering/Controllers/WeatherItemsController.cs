using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Weathering.Models;

namespace Weathering.Controllers
{
    [Route("api/WeatherItems")]
    [ApiController]
    public class WeatherItemsController : ControllerBase
    {
        private readonly WeatherContext _context;

        public WeatherItemsController(WeatherContext context)
        {
            _context = context;
        }

        // GET: api/WeatherItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WeatherItem>>> GetWeatherItems()
        {
            return await _context.WeatherItems.ToListAsync();
        }

        // GET: api/WeatherItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WeatherItem>> GetWeatherItem(long id)
        {
            var weatherItem = await _context.WeatherItems.FindAsync(id);

            if (weatherItem == null)
            {
                return NotFound();
            }

            return weatherItem;
        }

        // PUT: api/WeatherItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeatherItem(long id, WeatherItem weatherItem)
        {
            if (id != weatherItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(weatherItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeatherItemExists(id))
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

        // POST: api/WeatherItems
        [HttpPost]
        public async Task<ActionResult<WeatherItem>> PostTodoItem(WeatherItem weatherItem)
        {
            _context.WeatherItems.Add(weatherItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetWeatherItem), new { id = weatherItem.Id }, weatherItem);
        }


        // DELETE: api/WeatherItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeatherItem(long id)
        {
            var weatherItem = await _context.WeatherItems.FindAsync(id);
            if (weatherItem == null)
            {
                return NotFound();
            }

            _context.WeatherItems.Remove(weatherItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeatherItemExists(long id)
        {
            return _context.WeatherItems.Any(e => e.Id == id);
        }
    }
}
