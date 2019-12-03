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
using AssetManagementAngularProject.Models;

namespace AssetManagementAngularProject.Controllers
{
    public class PurchaseEditController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        // GET: api/PurchaseEdit
        public IQueryable<Purchase_order> GetPurchase_order()
        {
            return db.Purchase_order;
        }

        public PurchaseEditController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        //// GET: api/PurchaseEdit/5
        //[ResponseType(typeof(Purchase_order))]
        //public IHttpActionResult GetPurchase_order(int id)
        //{
        //    Purchase_order purchase_order = db.Purchase_order.Find(id);
        //    if (purchase_order == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(purchase_order);
        //}


       //GET: api/PurchaseEdit/5
        [ResponseType(typeof(Purchase_order))]
        public PurchaseViewModel GetPurchase_order(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;

            Purchase_order pOrder = db.Purchase_order.Where(x => x.pd_id == id).FirstOrDefault();
            PurchaseViewModel pvModel = new PurchaseViewModel();

            pvModel.pd_id = pOrder.pd_id;
            pvModel.pd_order_no = pOrder.pd_order_no;
            pvModel.pd_ad_id = pOrder.pd_ad_id;
            pvModel.pd_ad_name = pOrder.Asset_def.ad_name;
            pvModel.pd_dateStr = pOrder.pd_dateStr;
            pvModel.pd_ad_id = pOrder.pd_ad_id;
            pvModel.pd_ddateStr = pOrder.pd_ddateStr;
            pvModel.pd_qty =Convert.ToInt32( pOrder.pd_qty);
            pvModel.pd_status = pOrder.pd_status;
            pvModel.pd_type_id = pOrder.pd_type_id;
            pvModel.pd_type_name = pOrder.Asset_type.at_name;
            pvModel.pd_vendor_id = pOrder.pd_vendor_id;
            pvModel.pd_vendor_name = pOrder.vendor.vd_name;

            return pvModel;

        }

        // PUT: api/PurchaseEdit/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase_order(int id, Purchase_order purchase_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != purchase_order.pd_id)
            {
                return BadRequest();
            }

            db.Entry(purchase_order).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Purchase_orderExists(id))
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

        // POST: api/PurchaseEdit
        [ResponseType(typeof(Purchase_order))]
        public IHttpActionResult PostPurchase_order(Purchase_order purchase_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Purchase_order.Add(purchase_order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase_order.pd_id }, purchase_order);
        }

        // DELETE: api/PurchaseEdit/5
        [ResponseType(typeof(Purchase_order))]
        public IHttpActionResult DeletePurchase_order(int id)
        {
            Purchase_order purchase_order = db.Purchase_order.Find(id);
            if (purchase_order == null)
            {
                return NotFound();
            }

            db.Purchase_order.Remove(purchase_order);
            db.SaveChanges();

            return Ok(purchase_order);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Purchase_orderExists(int id)
        {
            return db.Purchase_order.Count(e => e.pd_id == id) > 0;
        }
    }
}