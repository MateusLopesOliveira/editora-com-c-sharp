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
    public class LivrosController : ApiController
    {
        private EditoraEntities db = new EditoraEntities();

        // GET: api/Livros
        public IQueryable<Livros> GetLivros()
        {
            return db.Livros;
        }

        // GET: api/Livros/5
        [ResponseType(typeof(Livros))]
        public IHttpActionResult GetLivros(int id)
        {
            Livros livros = db.Livros.Find(id);
            if (livros == null)
            {
                return NotFound();
            }

            return Ok(livros);
        }

        // PUT: api/Livros/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLivros(int id, Livros livros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != livros.Id)
            {
                return BadRequest();
            }

            db.Entry(livros).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LivrosExists(id))
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

        // POST: api/Livros
        [ResponseType(typeof(Livros))]
        public IHttpActionResult PostLivros(Livros livros)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Livros.Add(livros);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LivrosExists(livros.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = livros.Id }, livros);
        }

        // DELETE: api/Livros/5
        [ResponseType(typeof(Livros))]
        public IHttpActionResult DeleteLivros(int id)
        {
            Livros livros = db.Livros.Find(id);
            if (livros == null)
            {
                return NotFound();
            }

            db.Livros.Remove(livros);
            db.SaveChanges();

            return Ok(livros);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LivrosExists(int id)
        {
            return db.Livros.Count(e => e.Id == id) > 0;
        }
    }
}