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
    public class VendorsController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        VendorsController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }

        // GET: api/Vendors
        public List<VendorViewModel> getVendors()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<vendor> vendorList = db.vendors.ToList();
            List<VendorViewModel> avList = vendorList.Select(x => new VendorViewModel
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
             
            return avList;
        }

        // GET: api/Vendors/5
        [ResponseType(typeof(vendor))]
        public IHttpActionResult getVendor(int id)
        {
            vendor vendor = db.vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            return Ok(vendor);
        }

        // PUT: api/Vendors/5
        [ResponseType(typeof(void))]
        public IHttpActionResult putVendor(int id, vendor vendor)
        {

            //db.Configuration.ProxyCreationEnabled = true;
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != vendor.vd_id)
            {
                return BadRequest();
            }

            db.Entry(vendor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!vendorExists(id))
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

        // POST: api/Vendors
        [ResponseType(typeof(vendor))]
        public int postVendor(vendor vendor)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            //db.vendors.Add(vendor);
            //db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = vendor.vd_id }, vendor);

            vendor vd = new vendor();
            vd = db.vendors.Where(x => x.vd_name == vendor.vd_name && x.vd_atype_id == vendor.vd_atype_id).FirstOrDefault();
            if (vd == null)
            {
                db.vendors.Add(vendor);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }

        }

        // DELETE: api/Vendors/5
        [ResponseType(typeof(vendor))]
        public IHttpActionResult deleteVendor(int id)
        {
            vendor vendor = db.vendors.Find(id);
            if (vendor == null)
            {
                return NotFound();
            }

            db.vendors.Remove(vendor);
            db.SaveChanges();

            return Ok(vendor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool vendorExists(int id)
        {
            return db.vendors.Count(e => e.vd_id == id) > 0;
        }
    }
}