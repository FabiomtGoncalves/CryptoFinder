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
    public class PoolsController : Controller
    {
        private CryptoExchangeEntities2 db = new CryptoExchangeEntities2();

        // GET: Pools
        public ActionResult Index()
        {
            var pools = db.Pools.Include(p => p.User);
            return View(pools.ToList());
        }

        // GET: Pools/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pool pool = db.Pools.Find(id);
            if (pool == null)
            {
                return HttpNotFound();
            }
            return View(pool);
        }

        // GET: Pools/Create
        public ActionResult Create()
        {
            ViewBag.userid = new SelectList(db.Users, "id", "username");
            return View();
        }

        // POST: Pools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,userid,pool_name,staked_amount")] Pool pool)
        {
            if (ModelState.IsValid)
            {
                db.Pools.Add(pool);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userid = new SelectList(db.Users, "id", "username", pool.userid);
            return View(pool);
        }

        // GET: Pools/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pool pool = db.Pools.Find(id);
            if (pool == null)
            {
                return HttpNotFound();
            }
            ViewBag.userid = new SelectList(db.Users, "id", "username", pool.userid);
            return View(pool);
        }

        // POST: Pools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,userid,pool_name,staked_amount")] Pool pool)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pool).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userid = new SelectList(db.Users, "id", "username", pool.userid);
            return View(pool);
        }

        // GET: Pools/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pool pool = db.Pools.Find(id);
            if (pool == null)
            {
                return HttpNotFound();
            }
            return View(pool);
        }

        // POST: Pools/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pool pool = db.Pools.Find(id);
            db.Pools.Remove(pool);
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
