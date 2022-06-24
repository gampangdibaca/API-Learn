using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class BaseController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;

        public BaseController(Repository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = repository.Get();
            if (result.Count() <= 0)
            {
                return StatusCode(204, new { status = HttpStatusCode.NoContent, message = "There is no data." });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Get Success.", data = result });
        }

        [HttpGet("{key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key) ;
            if(result == null)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = $"There is no data with key: {key}" });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Get Success.", data = result }); ;
        }

        [HttpPost]
        public ActionResult Insert(Entity entity)
        {
            var result = repository.Insert(entity);

            if (result <= 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Insert Gagal" });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Insert Berhasil" });
        }

        [HttpPut]
        public ActionResult Update(Entity entity, Key key)
        {
            var result = repository.Update(entity, key);
            if (result <= 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Update Gagal" });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Update Berhasil" });
        }

        [HttpDelete("{key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result <= 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Delete Gagal" });
            }
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Delete Berhasil" });
        }
    }

    
}
