using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebAPIDemo.Dto;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api")]
    public class ArtTattooServiceController : ControllerBase
    {
        private readonly IArtTattooServiceRepo _artTattooServiceRepo;
        private readonly IMapper _mapper;

        public ArtTattooServiceController(IArtTattooServiceRepo artTattooServiceRepo, IMapper mapper)
        {
            this._artTattooServiceRepo = artTattooServiceRepo;
            this._mapper = mapper;
        }

        [HttpGet("tattoo-services")]
        [ProducesResponseType(200, Type = typeof(ICollection<ArtTattooService>))]
        public async Task<ActionResult<ICollection<ArtTattooService>>> GetArtTattooServices()
        {
            var artTattooServices = await _artTattooServiceRepo.GetArtTattooServices();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var artTattooServicesMap = _mapper.Map<ICollection<ArtTattooServiceDto>>(artTattooServices);
            return Ok(artTattooServicesMap);
        }

        [HttpPost("tattoo-service")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult> CreateArtTattooService([FromBody] ArtTattooServiceDto artTattooServiceCreate)
        {
            if (artTattooServiceCreate == null) return BadRequest(ModelState);
            // check duplicate
            var duplicate = _artTattooServiceRepo.GetArtTattooServices()
                .Result.FirstOrDefault(a => a.ServiceId.Equals(artTattooServiceCreate.ServiceId));
            if (duplicate != null)
            {
                ModelState.AddModelError("", "Service Id already exists!");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid) return BadRequest(ModelState);
            var artTattooService = _mapper.Map<ArtTattooService>(artTattooServiceCreate);
            if (!await _artTattooServiceRepo.CreateArtTattooService(artTattooService))
            {
                ModelState.AddModelError("", "Something went wrong saving the art tattoo service!"); 
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully!");
        }
    }
}
