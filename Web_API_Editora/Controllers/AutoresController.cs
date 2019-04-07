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
    public class AutoresController : ApiController
    {
        private EditoraEntities db = new EditoraEntities();

        // GET: api/Autores
        public IQueryable<Autores> GetAutores()
        {
            return db.Autores;
        }

        // GET: api/Autores/5
        [ResponseType(typeof(Autores))]
        public IHttpActionResult GetAutores(int id)
        {
            Autores autores = db.Autores.Find(id);
            if (autores == null)
            {
                return NotFound();
            }

            return Ok(autores);
        }

        // PUT: api/Autores/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAutores(int id, Autores autores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != autores.Id)
            {
                return BadRequest();
            }

            db.Entry(autores).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutoresExists(id))
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

        // POST: api/Autores
        [ResponseType(typeof(Autores))]
        public IHttpActionResult PostAutores(Autores autores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Autores.Add(autores);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (AutoresExists(autores.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = autores.Id }, autores);
        }

        // DELETE: api/Autores/5
        [ResponseType(typeof(Autores))]
        public IHttpActionResult DeleteAutores(int id)
        {
            Autores autores = db.Autores.Find(id);
            if (autores == null)
            {
                return NotFound();
            }

            db.Autores.Remove(autores);
            db.SaveChanges();

            return Ok(autores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AutoresExists(int id)
        {
            return db.Autores.Count(e => e.Id == id) > 0;
        }
    }
}