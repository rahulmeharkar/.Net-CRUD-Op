using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.Model;
using DataAccessLayer.Repo;
using DataAccessLayer.Interface;

namespace ApiLayer.Controllers
{
    public class CategaryController : ApiController
    {
        ICategory categaryRepo = null;
        public CategaryController()
        {
            categaryRepo = new CategaryRepo();
        }
        [HttpPost]
        [Route("api/addcategary")]
        public IHttpActionResult AddCategary(CategaryModel model)
        {
            CategaryModel cat = new CategaryModel();
            cat.album_categary = model.album_categary;
            int i = categaryRepo.addCategory(cat);
            if(i >= 1)
            {
                return Ok("Succefully Added");
            }
            else
            {
                return BadRequest("Not a valid Detail/Request");
            }
        }
        [HttpGet]
        [Route("api/getcategorylist")]
        public IHttpActionResult GetCategaryList()
        {
            try
            {
                List<CategaryModel> types = new List<CategaryModel>();
                types = categaryRepo.getAllCategory();
                return Ok(types);
            }
            catch
            {
                return NotFound();
            }
        }
        [HttpDelete]
        [Route("api/deletecategary")]
        public IHttpActionResult DeleteCategary(int album_id)
        {
           int i = categaryRepo.deleteCategory(album_id);
           if (i >= 1)
           {
              return Ok("Succesfully Deleted");
           }
           else
           {
             return BadRequest();
           }
        }
        [HttpPut]
        [Route("api/updatecategary")]
        public IHttpActionResult UpdateCategary(CategaryModel model)
        {
            CategaryModel cat = new CategaryModel();
            cat.album_categary = model.album_categary;
            cat.album_id = model.album_id;
            int i = categaryRepo.editCategory(cat);
            if(i >= 1)
            {
                return Ok("Updated Succefully");
            }
            else
            {
                return BadRequest("Not a valid Detail/Request");
            }
        }
    }
}
