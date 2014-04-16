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
    public class FornecedorController : Controller
    {
        private ERPContext db = new ERPContext();

        //
        // GET: /Fornecedor/

        public ActionResult Index()
        {
            return View(db.Fornecedores.Include(f => f.pessoa).ToList());
        }

        //
        // GET: /Fornecedor/Details/5

        public ActionResult Details(int id = 0)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            try
            {
                PessoaFisica pf = db.PessoasFisicas.Find(id);
                return View(new FornecedorViewModel(fornecedor, pf));
            }
            catch(Exception ex)
            {
                PessoaJuridica pj = db.PessoasJuridicas.Find(id);
                return View(new FornecedorViewModel(fornecedor, pj));
            }
        }

        //
        // GET: /Fornecedor/Create

        public ActionResult CreatePF()
        {
            Fornecedor fornecedor = new Fornecedor();
            PessoaFisica pf = new PessoaFisica();
            FornecedorViewModel fornecedorVM = new FornecedorViewModel(fornecedor, pf);
            return View("Edit", fornecedorVM);
        }

        public ActionResult CreatePJ()
        {
            Fornecedor fornecedor = new Fornecedor();
            PessoaJuridica pj = new PessoaJuridica();
            FornecedorViewModel fornecedorVM = new FornecedorViewModel(fornecedor, pj);
            return View("Edit", fornecedorVM);
        }

        //
        // POST: /Fornecedor/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
       
                db.Fornecedores.Add(fornecedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        //
        // GET: /Fornecedor/Edit/5

        public ActionResult Edit(int id = 0)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            
            try
            {
                var pessoa = db.PessoasFisicas.Find(id);
                return View(new FornecedorViewModel(fornecedor, pessoa));
            }
            catch (Exception ex)
            {
                var pessoa = db.PessoasJuridicas.Find(id);
                return View(new FornecedorViewModel(fornecedor, pessoa));
            }
        }

        //
        // POST: /Fornecedor/Edit/5
        [HttpPost]
        public ActionResult Edit(Fornecedor fornecedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(fornecedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(fornecedor);
        }

        [HttpPost]
        public ActionResult EditPF(Fornecedor fornecedor, PessoaFisica pessoaFisica)
        {
            pessoaFisica.DataCadastro = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (pessoaFisica.IdPessoa != 0)
                {
                    db.Entry(fornecedor).State = EntityState.Modified;
                    db.Entry(pessoaFisica).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Fornecedores.Add(fornecedor);
                    db.PessoasFisicas.Add(pessoaFisica);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            FornecedorViewModel fornecedorVM = new FornecedorViewModel(fornecedor, pessoaFisica);
            return View("Edit", fornecedorVM);
        }

        [HttpPost]
        public ActionResult EditPJ(Fornecedor fornecedor, PessoaJuridica pessoaJuridica)
        {
            pessoaJuridica.DataCadastro = DateTime.Now;

            if (ModelState.IsValid)
            {
                if (pessoaJuridica.IdPessoa != 0)
                {
                    db.Entry(fornecedor).State = EntityState.Modified;
                    db.Entry(pessoaJuridica).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    db.Fornecedores.Add(fornecedor);
                    db.PessoasJuridicas.Add(pessoaJuridica);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            FornecedorViewModel fornecedorVM = new FornecedorViewModel(fornecedor, pessoaJuridica);
            return View("Edit", fornecedorVM);
        }

        //
        // GET: /Fornecedor/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);

            if (fornecedor == null)
            {
                return HttpNotFound();
            }


            try
            {
                PessoaFisica pf = db.PessoasFisicas.Find(id);
                return View(new FornecedorViewModel(fornecedor, pf));

            }catch(Exception ex)
            {
                PessoaJuridica pj = db.PessoasJuridicas.Find(id);
                return View(new FornecedorViewModel(fornecedor, pj));
            }
        }

        //
        // POST: /Fornecedor/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Fornecedor fornecedor = db.Fornecedores.Find(id);
            db.Fornecedores.Remove(fornecedor);
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