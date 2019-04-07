using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Web_API_Editora;

namespace Web_API_Editora.Controllers
{
    public class livro_autorController : ApiController
    {
        private EditoraEntities db = new EditoraEntities();

        // GET: api/livro_autor
        public IQueryable<livro_autor> Getlivro_autor()
        {
            return db.livro_autor;
        }

        // GET: api/livro_autor/5
        [ResponseType(typeof(livro_autor))]
        public IHttpActionResult Getlivro_autor(int id)
        {
            livro_autor livro_autor = db.livro_autor.Find(id);
            if (livro_autor == null)
            {
                return NotFound();
            }

            return Ok(livro_autor);
        }

        // PUT: api/livro_autor/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putlivro_autor(int id, livro_autor livro_autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livro_autor.Id)
            {
                return BadRequest();
            }

            db.Entry(livro_autor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!livro_autorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/livro_autor
        [ResponseType(typeof(livro_autor))]
        public IHttpActionResult Postlivro_autor(livro_autor livro_autor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.livro_autor.Add(livro_autor);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (livro_autorExists(livro_autor.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = livro_autor.Id }, livro_autor);
        }

        // DELETE: api/livro_autor/5
        [ResponseType(typeof(livro_autor))]
        public IHttpActionResult Deletelivro_autor(int id)
        {
            livro_autor livro_autor = db.livro_autor.Find(id);
            if (livro_autor == null)
            {
                return NotFound();
            }

            db.livro_autor.Remove(livro_autor);
            db.SaveChanges();

            return Ok(livro_autor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool livro_autorExists(int id)
        {
            return db.livro_autor.Count(e => e.Id == id) > 0;
        }
    }
}