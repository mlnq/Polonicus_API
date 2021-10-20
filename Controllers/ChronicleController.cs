using Microsoft.AspNetCore.Mvc;
using Polonicus_API.Models;
using Polonicus_API.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Polonicus_API.Controllers
{
    [Route("api/outpost/{outpostId}/chronicle")]
    [ApiController]
    public class ChronicleController : ControllerBase
    {
        private readonly IChronicleService chronicleService;

        public ChronicleController(IChronicleService _chronicleService)
        {
            chronicleService = _chronicleService;
        }

        [HttpPost]
        public ActionResult Post([FromRoute] int outpostId,[FromBody] CreateChronicleDto dto)
        {
            var newChronicleId = chronicleService.Create(outpostId,dto);

            return Created($"/api/{outpostId}/chronicle/{newChronicleId}", null);
        }

        [HttpGet]
        public ActionResult Get([FromRoute] int outpostId)
        {
            var chronicles = chronicleService.GetAll(outpostId);

            return Ok(chronicles);
        }

        [HttpGet("{chronicleId}")]
        public ActionResult Get([FromRoute] int outpostId, [FromRoute] int chronicleId)
        {
            var chronicle = chronicleService.GetById(outpostId,chronicleId);

            return Ok(chronicle);
        }

        [HttpDelete("{chronicleId}")]
        public ActionResult Delete([FromRoute] int outpostId, [FromRoute] int chronicleId)
        {
            chronicleService.Remove(outpostId,chronicleId);

            return NoContent();
        }


        /// @TODO
        [HttpPut("{chronicleId}")]
        public ActionResult Put([FromRoute] int outpostId, [FromRoute] int chronicleId,[FromBody] ChronicleDto dto)
        {
            chronicleService.Update(outpostId, chronicleId,dto);
            return Ok();
        }

    }
}
