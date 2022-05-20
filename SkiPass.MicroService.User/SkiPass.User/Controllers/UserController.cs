﻿using Microsoft.AspNetCore.Mvc;
using SkiPass.User.Data.Models;
using SkiPass.User.DataAccess.MongoRepository;

namespace SkiPass.User.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public IActionResult GetAll()
        {
            using (var repo = new MongoRepository<UserModel>())
            {
                var data = repo.GetAll();
                if (data == null)
                    return NotFound();
                return Ok(data);
            }
        }
        public IActionResult Get(UserModel model)
        {
            using (var repo = new MongoRepository<UserModel>())
            {
                var data = repo.Get(x => x.Id.Equals(model.Id));
                if (data == null) return NotFound();
                return Ok(data);
            }
        }
        public IActionResult Add(UserModel model)
        {
            using (var repo = new MongoRepository<UserModel>())
            {
                try
                {
                    if (model == null) return BadRequest();
                    repo.Add(model);
                    return Ok($"{model}, added.");
                }
                catch (System.Exception)
                {
                    return StatusCode(500);
                }
            }
        }
        public IActionResult Update(UserModel model)
        {
            using (var repo = new MongoRepository<UserModel>())
            {
                try
                {
                    if (model == null) return BadRequest();
                    repo.Update(model);
                    return Ok($"{model}, başarılı ile güncellendi");
                }
                catch (System.Exception)
                {
                    return StatusCode(500);
                }
            }
        }
        public IActionResult Delete(UserModel model)
        {
            using (var repo = new MongoRepository<UserModel>())
            {
                try
                {
                    if (model == null) return BadRequest();
                    repo.Delete(x => x.Id.Equals(model.Id));
                    return Ok($"{model}, deleted.");
                }
                catch (System.Exception)
                {
                    return StatusCode(500);
                }
            }
        }
        public IActionResult Count()
        {
            return Ok();
        }
    }
}
