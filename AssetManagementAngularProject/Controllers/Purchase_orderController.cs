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
    public class Purchase_orderController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        // GET: api/Purchase_order
        //public IQueryable<Purchase_order> GetPurchase_order()
        //{
        //    return db.Purchase_order;
        //}

        public Purchase_orderController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/PurchaseOrder
        public List<PurchaseViewModel> GetPurchase_order()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Purchase_order> purchaseList = db.Purchase_order.ToList();
            List<PurchaseViewModel> avList = purchaseList.Select(x => new PurchaseViewModel
            {
                pd_id = x.pd_id,
                pd_order_no = x.pd_order_no,
                pd_ad_name = x.Asset_def.ad_name,
                pd_type_name= x.Asset_type.at_name,
                pd_qty = Convert.ToInt32(x.pd_qty),
                pd_vendor_name = x.vendor.vd_name,
                pd_dateStr = x.pd_dateStr,
                pd_ddateStr = x.pd_ddateStr,
                pd_status=x.pd_status
            }).ToList();

            return avList;
        }

        public List<Asset_type> GetAsset_Types(string na)
        {
            //db.Configuration.ProxyCreationEnabled = true;
           Asset_def ad = db.Asset_def.Where(x => x.ad_name == na).FirstOrDefault();
            List<Asset_type> atList = new List<Asset_type>();
            if (ad != null)
            {
                 atList = db.Asset_type.Where(x => x.at_id == ad.ad_type_id).ToList();
            }
            
            return atList;
          //  return db.Asset_type.Where(x => x.at_id == ad.ad_type_id).ToList();
        }

        public List<VendorViewModel> GetPurchase_order(int id)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<vendor>vd = db.vendors.Where(x => x.vd_atype_id == id).ToList();
            List<VendorViewModel> vList = vd.Select(x => new VendorViewModel
            {
                vd_id = x.vd_id,
                vd_name = x.vd_name,
                vd_type = x.vd_type,
                vd_atype_id = x.vd_atype_id,
                vd_atype_name = x.Asset_type.at_name,
                vd_addr = x.vd_addr,
                vd_from = x.vd_fromStr,
                vd_to = x.vd_toStr
            }).ToList();
            return vList;
        }

        // PUT: api/Purchase_order/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPurchase_order(int id, Purchase_order purchase_order)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

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

        // POST: api/Purchase_order
        [ResponseType(typeof(Purchase_order))]
        public IHttpActionResult PostPurchase_order(Purchase_order purchase_order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            purchase_order.pd_date = DateTime.Now;
            db.Purchase_order.Add(purchase_order);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = purchase_order.pd_id }, purchase_order);
        }

        // DELETE: api/Purchase_order/5
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