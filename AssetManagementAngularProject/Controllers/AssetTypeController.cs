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
    public class AssetTypeController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        // GET: api/AssetType
        public IQueryable<Asset_type> getAssetType()
        {
            return db.Asset_type;
        }

        // GET: api/AssetType/5
        [ResponseType(typeof(Asset_type))]
        public IHttpActionResult getAssetType(int id)
        {
            Asset_type asset_type = db.Asset_type.Find(id);
            if (asset_type == null)
            {
                return NotFound();
            }

            return Ok(asset_type);
        }

        // PUT: api/AssetType/5
        [ResponseType(typeof(void))]
        public IHttpActionResult putAssetType(int id, Asset_type asset_type)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != asset_type.at_id)
            {
                return BadRequest();
            }

            db.Entry(asset_type).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asset_typeExists(id))
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

        // POST: api/AssetType
        [ResponseType(typeof(Asset_type))]
        public IHttpActionResult postAssetType(Asset_type asset_type)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            db.Asset_type.Add(asset_type);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asset_type.at_id }, asset_type);
        }

        // DELETE: api/AssetType/5
        [ResponseType(typeof(Asset_type))]
        public IHttpActionResult deleteAssetType(int id)
        {
            Asset_type asset_type = db.Asset_type.Find(id);
            if (asset_type == null)
            {
                return NotFound();
            }

            db.Asset_type.Remove(asset_type);
            db.SaveChanges();

            return Ok(asset_type);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asset_typeExists(int id)
        {
            return db.Asset_type.Count(e => e.at_id == id) > 0;
        }
    }
}