using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TelephoneAPI.Models;

namespace TelephoneAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TelephoneAPIController : ControllerBase
    {
        private readonly TelephoneContext _context;
        private LinkGenerator _linkGenerator;

        public TelephoneAPIController(TelephoneContext context, LinkGenerator linkGenerator)
        {
            _context = context;
            _linkGenerator = linkGenerator;
        }

        // GET: api/TelephoneAPI
        [HttpGet(Name = nameof(GetTelephones))]
        public async Task<ActionResult<IEnumerable<Telephone>>> GetTelephones()
        {
            return await _context.Telephones.ToListAsync();
        }

        // GET: api/TelephoneAPI/5
        [HttpGet("{id}", Name = nameof(GetTelephone))]
        public async Task<ActionResult<Telephone>> GetTelephone(long id)
        {
            var telephone = await _context.Telephones.FindAsync(id);

            if (telephone == null)
            {
                return NotFound();
            }

            return CreateLinks(telephone);
        }

        // PUT: api/TelephoneAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}", Name=nameof(PutTelephone))]
        public async Task<IActionResult> PutTelephone(long id, Telephone telephone)
        {
            if (id != telephone.Id)
            {
                return BadRequest();
            }

            _context.Entry(telephone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TelephoneExists(id))
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

        // POST: api/TelephoneAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Telephone>> PostTelephone(Telephone telephone)
        {
            _context.Telephones.Add(telephone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTelephone", new { id = telephone.Id }, telephone);
        }

        // DELETE: api/TelephoneAPI/5
        [HttpDelete("{id}", Name=nameof(DeleteTelephone))]
        public async Task<IActionResult> DeleteTelephone(long id)
        {
            var telephone = await _context.Telephones.FindAsync(id);
            if (telephone == null)
            {
                return NotFound();
            }

            _context.Telephones.Remove(telephone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TelephoneExists(long id)
        {
            return _context.Telephones.Any(e => e.Id == id);
        }

        private Telephone CreateLinks(Telephone telephone) {
            telephone.Links.Add(
                new Link(telephone.Id, 
                _linkGenerator.GetUriByAction(HttpContext, nameof(GetTelephone), values: new {telephone.Id}),
                "self",
                "GET")
            );
            
            telephone.Links.Add(
            new Link(telephone.Id, 
                _linkGenerator.GetUriByAction(HttpContext, nameof(PutTelephone), values: new {telephone.Id}),
                "update_telephone",
                "PUT")
            );
            telephone.Links.Add(
            new Link(telephone.Id, 
                _linkGenerator.GetUriByAction(HttpContext, nameof(DeleteTelephone), values: new {telephone.Id}),
                "delete_telephone",
                "DELETE")
            );

            return telephone;
        }

    }
}
