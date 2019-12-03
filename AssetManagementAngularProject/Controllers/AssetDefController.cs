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
    public class AssetDefController : ApiController
    {
        private AssetMVCEntities db = new AssetMVCEntities();

        public AssetDefController()
        {
            db.Configuration.ProxyCreationEnabled = false;
        }
        // GET: api/AssetDef
        public List<AssetViewModel> getAssetDef()
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_def> assetList = db.Asset_def.ToList();
            List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
            {
                ad_id = Convert.ToInt32(x.ad_id),
                ad_name = x.ad_name,
                ad_type_name = x.Asset_type.at_name,
                ad_type_id=x.ad_type_id,
                ad_class = x.ad_class
            }).ToList();
           
            return avList;
        }

        public List<AssetViewModel> getAssetDef(string name)
        {
            db.Configuration.ProxyCreationEnabled = true;
            List<Asset_def> assetList = db.Asset_def.Where(x => x.ad_name.StartsWith(name)).ToList();
            List<AssetViewModel> avList = assetList.Select(x => new AssetViewModel
            {
                ad_id = Convert.ToInt32(x.ad_id),
                ad_name = x.ad_name,
                ad_type_name = x.Asset_type.at_name,
                ad_type_id = x.ad_type_id,
                ad_class = x.ad_class
            }).ToList();

            return avList;
            
        }

        // GET: api/AssetDef/5
        [ResponseType(typeof(Asset_def))]
        public IHttpActionResult getAssetDef(int id)
        {
            Asset_def asset_def = db.Asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            return Ok(asset_def);
        }

        // PUT: api/AssetDef/5
        [ResponseType(typeof(void))]
        public IHttpActionResult putAssetDef(int id, Asset_def asset_def)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            if (id != asset_def.ad_id)
            {
                return BadRequest();
            }

            db.Entry(asset_def).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asset_defExists(id))
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

        // POST: api/AssetDef
        [ResponseType(typeof(Asset_def))]
        public int postAssetDef(Asset_def asset_def)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            Asset_def asset = new Asset_def();
            asset = db.Asset_def.Where(x => x.ad_name == asset_def.ad_name && x.ad_type_id == asset_def.ad_type_id).FirstOrDefault();
            if (asset == null)
            {
                db.Asset_def.Add(asset_def);
                db.SaveChanges();
                return 1;
            }
            else
            {
                return 0;
            }
        }

        // DELETE: api/AssetDef/5
        [ResponseType(typeof(Asset_def))]
        public IHttpActionResult deleteAssetDef(int id)
        {
            Asset_def asset_def = db.Asset_def.Find(id);
            if (asset_def == null)
            {
                return NotFound();
            }

            db.Asset_def.Remove(asset_def);
            db.SaveChanges();

            return Ok(asset_def);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asset_defExists(int id)
        {
            return db.Asset_def.Count(e => e.ad_id == id) > 0;
        }
    }
}