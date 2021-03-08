using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DataAccessLayer.Model;
using DataAccessLayer.Repo;
using DataAccessLayer.Interface;
using System.Web;

namespace ApiLayer.Controllers
{
    public class PhotoController : ApiController
    {
        IPhoto photorepo = null;
        public PhotoController()
        {
            photorepo = new PhotoRepo();
        }
        [HttpDelete]
        [Route("api/removephoto")]
        public IHttpActionResult RemovePhoto(int photo_id)
        {
            int i = photorepo.DeletePhoto(photo_id);
            if(i >= 1)
            {
                return Ok("Succesfully Removed");
            }
            else
            {
                return BadRequest("Refresh Site");
            }
        }
        [HttpPut]
        [Route("api/updatephoto")]
        public IHttpActionResult UpdatePhotoDetail(PhotoModel model)
        {
            PhotoModel photo = new PhotoModel();
            photo.useralbumphoto_id = model.useralbumphoto_id;
            photo.useralbumphoto_name = model.useralbumphoto_name;
            photo.useralbumphoto_prise = model.useralbumphoto_prise;
            photo.useralbumphoto_artist = model.useralbumphoto_artist;
            int i = photorepo.EditPhoto(photo);
            if(i >= 1)
            {
                return Ok("Succesfully Updated");
            }
            else
            {
                return BadRequest("Bad Request");
            }
        }

        [HttpGet]
        [Route("api/allalbumphoto")]
        public IHttpActionResult ListAllPhoto()
        {
            List<PhotoModel> photos = new List<PhotoModel>();
            photos = photorepo.AllPhoto();
            return Ok(photos);
        }
    }
}
