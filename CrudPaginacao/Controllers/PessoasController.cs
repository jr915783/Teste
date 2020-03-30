using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CrudPaginacao.Models;
using X.PagedList;


namespace CrudPaginacao.Controllers
{
    public class PessoasController : Controller
    {
        private Base db = new Base();

        // GET: Pessoas
        public ActionResult Index(Pessoa pesquisa, int pagina = 1)
        {
            var pessoa = db.Pessoas.OrderBy(p => p.Nome)
                                          .ToPagedList(pagina, 5);

            // Verificar se é uma pesquisa
            if (!String.IsNullOrWhiteSpace(pesquisa.Nome))
            {

                pessoa = db.Pessoas.Where(p => p.Nome.Contains(pesquisa.Nome)).OrderBy(p => p.Nome)
                              .ToPagedList(pagina, 10);

            }

            return View(pessoa);
        }

        public ActionResult Ativo(Pessoa pesquisa, int pagina = 1)
        {
            var pessoa = db.Pessoas.Where(p => p.Ativo).OrderBy(p => p.Id)
                                          .ToPagedList(pagina, 5);

            // Verificar se é uma pesquisa
            if (!String.IsNullOrWhiteSpace(pesquisa.Nome))
            {

                pessoa = db.Pessoas.Where(p => p.Nome.Contains(pesquisa.Nome)).Where(p => p.Ativo).OrderBy(p => p.Nome)
                              .ToPagedList(pagina, 10);

            }

            return View(pessoa);
        }

        //Validar se a existe o cpf cadastrado no banco
        public JsonResult ValidarCpf(string Cpf, int? Id)
        {
            return Json(!db.Pessoas.Any(c => c.Cpf == Cpf && c.Id != Id), JsonRequestBehavior.AllowGet);
        }




        // GET: Pessoas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return PartialView(pessoa);
        }

        // GET: Pessoas/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nome,Cpf,Email,Telefone,DataNascimento,Cep,Rua,Bairro,Cidade,Uf,Ativo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Pessoas.Add(pessoa);
                db.SaveChanges();
                TempData["sucesso"] = "Adicionado com sucesso !";
                return RedirectToAction("Index");
            }

            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nome,Cpf,Email,Telefone,DataNascimento,Cep,Rua,Bairro,Cidade,Uf,Ativo")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pessoa).State = EntityState.Modified;
                db.SaveChanges();
                TempData["sucesso"] = "Editado com sucesso !";
                return RedirectToAction("Index");
            }
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pessoa pessoa = db.Pessoas.Find(id);
            if (pessoa == null)
            {
                return HttpNotFound();
            }
            return PartialView(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            TempData["sucesso"] = "Excluido com sucesso !";

            Pessoa pessoa = db.Pessoas.Find(id);
            db.Pessoas.Remove(pessoa);
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
