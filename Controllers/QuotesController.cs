using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewQuotesApi.Data;
using NewQuotesApi.Models;

namespace NewQuotesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class QuotesController : ControllerBase
    {
        private QuotesDbContext _quotesDbContext;

        public QuotesController(QuotesDbContext quotesDbContext)
        {
            _quotesDbContext = quotesDbContext;
        }
        // GET: api/Quotes
        [HttpGet]
        [ResponseCache (Duration = 60, Location = ResponseCacheLocation.Client)]
        [AllowAnonymous]
        public IActionResult Get(string sort)
        {
            IQueryable<Quote> quotes;
            switch(sort)
                {
                case "desc":
                    quotes = _quotesDbContext.Quotes.OrderByDescending(q => q.CreatedAt);
                    break;
                case "asc":
                    quotes = _quotesDbContext.Quotes.OrderBy(q => q.CreatedAt);
                    break;
                default:
                    quotes = _quotesDbContext.Quotes;
                    break;
            }
            // return _quotesDbContext.Quotes;
             return Ok(quotes); //other status code can be returned: return NotFound(_quotesDbContext.Quotes)
        }
        [HttpGet ("[action]")]
        public IActionResult PagingQuote (int? pageNumber, int? pageSize )
        {
            var quotes = _quotesDbContext.Quotes;
            var currentPageNumber = pageNumber ?? 1;
            var currentPageSize = pageSize ?? 5;

            return Ok(quotes.Skip((currentPageNumber - 1) * currentPageSize).Take(currentPageSize));
            //    var quotes = _quotesDbContext.Quotes;
            //    return Ok(quotes.Skip((pageNumber-1)*pageSize).Take(pageSize));

        }

        [HttpGet]
        [Route("[action]")]
        public IActionResult SearchQuote(string type)
        {
            var quotes = _quotesDbContext.Quotes.Where(q => q.Type.StartsWith(type));
            return Ok(quotes);
        }


        //To get the records of a particular user.
        [HttpGet("[action]")]
        public IActionResult MyQuote()
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            var quotes = _quotesDbContext.Quotes.Where(q=>q.UserId == userId);
            return Ok(quotes);
        }


        // GET: api/Quotes/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var quote = _quotesDbContext.Quotes.Find(id);
            if (quote == null)
            {
                return NotFound("No record found...");
            }
            return Ok(quote);
        }


        // POST: api/Quotes
        [HttpPost]
        public IActionResult Post([FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            quote.UserId = userId;
            _quotesDbContext.Quotes.Add(quote);
            _quotesDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Quotes/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Quote quote)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var entity = _quotesDbContext.Quotes.Find(id);
            if (entity == null) //Incase a user wants to update against an Id that doesn't exist; the error needs to be handled 
            {
                return NotFound("No record found against this id....");
            }

            if (userId != entity.UserId)
            {
                return BadRequest("Sorry you can't update this record"); //this restrict a user from modifying a post that is not his
            }
            else
            {
                entity.Title = quote.Title;
                entity.Author = quote.Author;
                entity.Description = quote.Description;
                entity.Type = quote.Type;
                entity.CreatedAt = quote.CreatedAt;
                _quotesDbContext.SaveChanges();
                return Ok("record updated succesfully......");
            }
        
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var quote = _quotesDbContext.Quotes.Find(id);
            if(quote == null)
            {
                return NotFound("no record found against this id");
            }
            if (userId != quote.UserId)
            {
                return BadRequest("Sorry you can't delete this record");
            }
            else
            {
                 _quotesDbContext.Quotes.Remove(quote);
                 _quotesDbContext.SaveChanges();
                 return Ok("Code deleted");
            }
            
        }
    }
}
