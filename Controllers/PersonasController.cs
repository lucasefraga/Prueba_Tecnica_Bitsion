using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Prueba_Tecnica_Bitsion.Models;

namespace Prueba_Tecnica_Bitsion.Controllers
{
    public class PersonasController : Controller
    {
        private Prueba_Tecnica_BitsionEntities db = new Prueba_Tecnica_BitsionEntities();

        // GET: Personas
        public ActionResult Index()
        {
            var persona = db.Persona.Include(p => p.EstadoPersona).Include(p => p.Genero1);
            return View(persona.ToList());
        }

        // GET: Personas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // GET: Personas/Create
        public ActionResult Create()
        {
            ViewBag.Estado = new SelectList(db.EstadoPersona, "Id", "Nombre");
            ViewBag.Genero = new SelectList(db.Genero, "Id", "Nombre");
            return View();
        }

        // POST: Personas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombres,Apellidos,Genero,Estado,Manejar,Lentes,Diabetes,Otras_Enfermedades")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Persona.Add(persona);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Estado = new SelectList(db.EstadoPersona, "Id", "Nombre", persona.Estado);
            ViewBag.Genero = new SelectList(db.Genero, "Id", "Nombre", persona.Genero);
            return View(persona);
        }

        // GET: Personas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            ViewBag.Estado = new SelectList(db.EstadoPersona, "Id", "Nombre", persona.Estado);
            ViewBag.Genero = new SelectList(db.Genero, "Id", "Nombre", persona.Genero);
            return View(persona);
        }

        // POST: Personas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombres,Apellidos,Genero,Estado,Manejar,Lentes,Diabetes,Otras_Enfermedades")] Persona persona)
        {
            if (ModelState.IsValid)
            {
                db.Entry(persona).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Estado = new SelectList(db.EstadoPersona, "Id", "Nombre", persona.Estado);
            ViewBag.Genero = new SelectList(db.Genero, "Id", "Nombre", persona.Genero);
            return View(persona);
        }

        // GET: Personas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Persona persona = db.Persona.Find(id);
            if (persona == null)
            {
                return HttpNotFound();
            }
            return View(persona);
        }

        // POST: Personas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Persona persona = db.Persona.Find(id);
            db.Persona.Remove(persona);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
