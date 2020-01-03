using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Totems.Models;
using Totems.Services;

namespace Totems.Controllers
{
    [Route("[controller]")] //Access the API with the totems route
    [ApiController]
    public class TotemsController : ControllerBase
    {
        private JsonFileTotemService service { get; } //get access to our simple database
        //Retrieve all Totems to a list 
        private List<Totem> TotemList { 
            get { return service.GetTotems().ToList(); }
            
            }
        //Constructor injecting the database object
        public TotemsController(JsonFileTotemService _service)
        {

            service = _service;
            
        }

        //Default retrieves all Totems
        [HttpGet]
        public ActionResult<Totem> Get()
        {
            return Ok(TotemList);
        }

        [HttpGet] //Get a totem by specifying an Id or origion
        [Route("totem/{item}")]
        public ActionResult<Totem> Get(string item)
        {

            var totem = TotemList.Find(totem => totem.Id.Equals(item, StringComparison.InvariantCultureIgnoreCase) || totem.Origin.Equals(item, StringComparison.InvariantCultureIgnoreCase));

            if (totem == null)
            {
                return NotFound("Sorry the totem you are looking for does not exists. Check if the spelling is correct");
            }
            else
            {
                return Ok(totem);
            }
        }
        [HttpGet]
        [Route("all/{name}")] //Get a totem by specifying an Id
        public ActionResult<Totem> GetAll(string name)
        {
            var totems = TotemList.FindAll(p => p.Animal.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (totems.Count<=0)
            {
                return NotFound("No totems match your search!");
            }
            else
            {
                return Ok(totems);
            }
        }

        
        [HttpPost]
        [Route("Add")]
        public ActionResult<Totem> Create(
            )
        {
            string id = Request.Form["id"];
            string name = Request.Form["name"];
            string origin = Request.Form["origin"];
            string description = Request.Form["description"];
            string image = Request.Form["image"];
            string body = Request.Form["body"];

            var totem = new Totem(id, name,origin, description, image, body.Split(@"`"));
            if (service.AddTotem(totem))
            {
                return Ok("A new totem added \n " + totem);
            }

           return Conflict("The totem you tried to add already exist");
           
        }
       [HttpPost("update")]
       public ActionResult<Totem> Update(
           )
        {
            string id = Request.Form["id"];
            string key = Request.Form["key"];
            string value = Request.Form["value"];

            if (service.UpdateTotem(id, key, value))
            {
                return Ok("Update Successful");
            }
            else
            {
                return Conflict("No totem with id " + id);
            }
            
        }
        [HttpGet("remove/{id}")]
        public ActionResult Remove(string id)
        {
            if (service.DeleteTotem(id))
            {
                return Ok("Totem removed");
            }
            else
            {
                return NotFound("Totem not found!");
            }
        }

    }
}