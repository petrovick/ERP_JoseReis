using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_JOSEREIS.Models;
using ERP_JOSEREIS.ViewModels;

namespace ERP_JOSEREIS.Controllers
{
    public class ClienteController : Controller
    {
        private ERPContext db = new ERPContext();

        //
        // GET: /Cliente/

        public ActionResult Index()
        {
            var clientes = db.Clientes.Include(c => c.Pessoa);
            return View(clientes.ToList());
        }

        //
        // GET: /Cliente/Details/5

        public ActionResult Details(int id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            PessoaFisica pf = db.PessoasFisicas.Find(id);
            ClienteViewModel clienteVM;
            if (pf != null)
                clienteVM = new ClienteViewModel(cliente, pf);
            else
            {
                PessoaJuridica pj = db.PessoasJuridicas.Find(id);
                clienteVM = new ClienteViewModel(cliente, pj);
            }
            return View(clienteVM);
        }

        public ActionResult DetailsPF()
        {
            IEnumerable<PessoaFisica> cliente = db.PessoasFisicas;
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        public ActionResult DetailsPJ()
        {
            IEnumerable<PessoaJuridica> cliente = db.PessoasJuridicas;
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        //
        // GET: /Cliente/Create

        public ActionResult CreatePJ()
        {
            //ViewBag.IdCliente = new SelectList(db.PessoasJuridicas, "IdPessoa", "Nome");
            //return View();
            Cliente cliente = new Cliente();
            PessoaJuridica pj = new PessoaJuridica();
            var clienteVM = new ClienteViewModel(cliente, pj);
            return View("Edit", clienteVM);
        }

        public ActionResult CreatePF()
        {
            Cliente cliente = new Cliente();
            PessoaFisica pf = new PessoaFisica();
            var clienteVM = new ClienteViewModel(cliente, pf);
            return View("Edit", clienteVM);

        }

        //
        // POST: /Cliente/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                db.Clientes.Add(cliente);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdCliente = new SelectList(db.Pessoas, "IdPessoa", "Nome", cliente.IdCliente);
            return View(cliente);
        }

        //
        // GET: /Cliente/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Cliente cliente = db.Clientes.Find(id);
            PessoaFisica pf;
            PessoaJuridica pj;
            
            ClienteViewModel clienteVM;
            if (cliente == null)
            {
                return HttpNotFound();
            }
            try
            {
                pf = db.PessoasFisicas.Find(id);
                clienteVM = new ClienteViewModel(cliente, pf);
                return View(clienteVM);
            }
            catch (Exception e)
            {
                pj = db.PessoasJuridicas.Find(id);
                clienteVM = new ClienteViewModel(cliente, pj);
                return View(clienteVM);
            } 
            
        }

        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]  
        public ActionResult Edit(Cliente cliente, PessoaFisica pessoaFisica)
        {
            if (ModelState.IsValid)//Se nao tem nenhum erro na hora de salvar
            {
                if (pessoaFisica.IdPessoa != 0)
                {
                    db.Entry(cliente).State = EntityState.Modified;
                    db.Entry(pessoaFisica).State = EntityState.Modified; //É como um merge no java.
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Clientes.Add(cliente);
                    db.PessoasFisicas.Add(pessoaFisica);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ClienteViewModel clienteVM = new ClienteViewModel(cliente, pessoaFisica);
            return View("Edit", clienteVM);
        }



        //
        // POST: /Cliente/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPJ(Cliente cliente, PessoaJuridica pessoaJuridica)
        {
            if (ModelState.IsValid)//Se nao tem nenhum erro na hora de salvar
            {
                if (pessoaJuridica.IdPessoa != 0)
                {
                    db.Entry(cliente).State = EntityState.Modified;
                    db.Entry(pessoaJuridica).State = EntityState.Modified; //É como um merge no java.
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Clientes.Add(cliente);
                    db.PessoasJuridicas.Add(pessoaJuridica);
                    db.SaveChanges();
                }
            }
            ClienteViewModel clienteVM = new ClienteViewModel(cliente, pessoaJuridica);
            return View("Edit", clienteVM);
        }


        //
        // GET: /Cliente/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Cliente cliente = db.Clientes.Include(c => c.Pessoa).Single(c => c.IdCliente == id);
            if (cliente == null)
            {
                return HttpNotFound();
            }
            return View(cliente);
        }

        
        //
        // POST: /Cliente/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Clientes.Find(id);
            db.Clientes.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}