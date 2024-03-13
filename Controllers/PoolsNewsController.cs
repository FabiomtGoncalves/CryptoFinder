using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CryptoExchange;

namespace CryptoExchange.Controllers
{
    [Authorize]
    public class PoolsNewsController : Controller
    {
        private CryptoExchangeEntities2 db = new CryptoExchangeEntities2();

        // GET: PoolsNews
        public ActionResult Index()
        {
            return View(db.PoolsNews.ToList());
        }

        // GET: PoolsNews/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolsNew poolsNew = db.PoolsNews.Find(id);
            if (poolsNew == null)
            {
                return HttpNotFound();
            }
            return View(poolsNew);
        }

        // GET: PoolsNews/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PoolsNews/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,email,pool_name,staked_amount")] PoolsNew poolsNew)
        {
            if (ModelState.IsValid)
            {
                db.PoolsNews.Add(poolsNew);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(poolsNew);
        }

        // GET: PoolsNews/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolsNew poolsNew = db.PoolsNews.Find(id);
            if (poolsNew == null)
            {
                return HttpNotFound();
            }
            return View(poolsNew);
        }

        // POST: PoolsNews/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,email,pool_name,staked_amount")] PoolsNew poolsNew)
        {
            if (ModelState.IsValid)
            {
                db.Entry(poolsNew).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(poolsNew);
        }

        // GET: PoolsNews/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PoolsNew poolsNew = db.PoolsNews.Find(id);
            if (poolsNew == null)
            {
                return HttpNotFound();
            }
            return View(poolsNew);
        }

        // POST: PoolsNews/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PoolsNew poolsNew = db.PoolsNews.Find(id);
            db.PoolsNews.Remove(poolsNew);
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
