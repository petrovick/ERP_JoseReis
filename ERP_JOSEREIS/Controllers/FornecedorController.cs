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
            var fornecedores = db.Fornecedores.Include(f => f.pessoa);
            return View(fornecedores.ToList());
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
            PessoaFisica pf = db.PessoasFisicas.Find(id);
            FornecedorViewModel fornecedorVM;
            if(pf != null)
            {
                fornecedorVM = new FornecedorViewModel(fornecedor, pf);
            }
            else
            {
                PessoaJuridica pj = db.PessoasJuridicas.Find(id);
                fornecedorVM = new FornecedorViewModel(fornecedor, pj);
            }
            return View(fornecedorVM);
        }

        public ActionResult DetailsPF()
        {
            IEnumerable<PessoaFisica> fornecedor = db.PessoasFisicas;
            if(fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        public ActionResult DetailsPJ()
        {
            IEnumerable<PessoaJuridica> fornecedor = db.PessoasJuridicas;
            if(fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }

        //
        // GET: /Fornecedor/Create

        public ActionResult CreatePJ()
        {
            Fornecedor fornecedor = new Fornecedor();
            PessoaJuridica pj = new PessoaJuridica();
            var fornecedorVM = new FornecedorViewModel(fornecedor, pj);
            return View("Edit", fornecedorVM);
        }

        public ActionResult CreatePF()
        {
            Fornecedor fornecedor = new Fornecedor();
            PessoaFisica pf = new PessoaFisica();
            var fornecedorVM = new FornecedorViewModel(fornecedor, pf);
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
            ViewBag.IdFornecedor = new SelectList(db.Pessoas, "IdPessoa", "Nome", fornecedor.IdFornecedor);
            return View(fornecedor);
        }

        //
        // GET: /Fornecedor/Edit/5

        //public ActionResult Edit(int id = 0)
        //{
        //    Fornecedor fornecedor = db.Fornecedores.Find(id);
        //    PessoaFisica pf;
        //    PessoaJuridica pj;

        //    FornecedorViewModel fornecedorVM;
        //    if (fornecedor == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    try
        //    {
        //        pf = db.PessoasFisicas.Find(id);
        //        fornecedorVM = new FornecedorViewModel(fornecedor, pf);
        //        return View(fornecedorVM);
        //    }catch(Exception ex)
        //    {
        //        pj = db.PessoasJuridicas.Find(id);
        //        fornecedorVM = new FornecedorViewModel(fornecedor, pj);
        //        return View(fornecedorVM);
        //    }
        //    ViewBag.IdFornecedor = new SelectList(db.Pessoas, "IdPessoa", "Nome", fornecedor.IdFornecedor);
        //    return View(fornecedor);
        //}

        //
        // POST: /Fornecedor/Edit/5

        public ActionResult Edit(Fornecedor fornecedor, PessoaFisica pessoaFisica)
        {
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
        [ValidateAntiForgeryToken]
        public ActionResult EditPJ(Fornecedor fornecedor, PessoaJuridica pessoaJuridica)
        {
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

/*        public ActionResult Delete(int id = 0)
        {
            Fornecedor fornecedor = db.Fornecedores.Include(f => f.Pessoa).Single(f => f.IdPessoa == id);
            if (fornecedor == null)
            {
                return HttpNotFound();
            }
            return View(fornecedor);
        }
*/
        //
        // POST: /Fornecedor/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
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