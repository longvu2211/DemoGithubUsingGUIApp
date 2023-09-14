using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using WebAPIDemo.Dto;
using WebAPIDemo.IRepositories;
using WebAPIDemo.Models;

namespace WebAPIDemo.Controllers
{
    [ApiController]
    [Route("api")]
    public class ArtTattooStyleController : ControllerBase
    {
        // Binding Source Parameter Inference
        // [FromBody], [FromForm], [FromHeader], [FromQuery], [FromRoute], [FromServices]
        private readonly IArtTattooStyleRepo _artTattooStyleRepo;
        private readonly IMapper _mapper;

        public ArtTattooStyleController(IArtTattooStyleRepo artTattooStyleRepo, IMapper mapper)
        {
            this._artTattooStyleRepo = artTattooStyleRepo;
            this._mapper = mapper;
        }

        [HttpGet("tattoo-styles")]
        [ProducesResponseType(200, Type = typeof(ICollection<ArtTattooStyle>))]
        public async Task<ActionResult<ICollection<ArtTattooStyle>>> GetArtTattooStyles()
        {
            var artTattooStyles = await _artTattooStyleRepo.GetArtTattooStyles();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var artTattooStylesMap = _mapper.Map<ICollection<ArtTattooStyleDto>>(artTattooStyles);
            return Ok(artTattooStylesMap);
        }

        [HttpGet("tattoo-styles/{id}")]
        [ProducesResponseType(200, Type = typeof(ArtTattooStyle))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ArtTattooStyle>> GetArtTattooStyle(int id)
        {
            if (!await _artTattooStyleRepo.HasArtTattooStyle(id)) return NotFound();
            var artTattooStyle = await _artTattooStyleRepo.GetArtTattooStyle(id);
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var artTattooStyleMap = _mapper.Map<ArtTattooStyleDto>(artTattooStyle);
            return Ok(artTattooStyleMap);
        }

        [HttpPost("tattoo-style")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ArtTattooStyle>> CreateArtTattooStyle([FromBody] ArtTattooStyleDto artTattooStyleCreate,
            [FromQuery] string serviceId)
        {
            if (artTattooStyleCreate == null) return BadRequest(ModelState);
            var duplicate = _artTattooStyleRepo.GetArtTattooStyles()
                .Result.FirstOrDefault(a => a.TattooStyleId == artTattooStyleCreate.TattooStyleId);
            if (duplicate != null)
            {
                ModelState.AddModelError("", "Tattoo Style Id already exists!");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var artTattooStyle = _mapper.Map<ArtTattooStyle>(artTattooStyleCreate);
            artTattooStyle.ServiceId = serviceId;
            if (!await _artTattooStyleRepo.CreateArtTattooStyle(artTattooStyle))
            {
                ModelState.AddModelError("", "Something went wrong saving the record!");
                return StatusCode(500, ModelState);
            }
            return Ok("Create successfully!");
        }

        [HttpGet("tattoo-styles/name")]
        [ProducesResponseType(200, Type = typeof(ArtTattooStyle))]
        [ProducesResponseType(400)]
        public async Task<ActionResult<ICollection<ArtTattooStyle>>> SearchArtTattooStylesByName([FromQuery] string name)
        {
            var artTattooStyles = await _artTattooStyleRepo.SearchArtTattooStylesByName(name);
            if (!artTattooStyles.Any()) return NotFound();
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var artTattooStylesMap = _mapper.Map<ICollection<ArtTattooStyleDto>>(artTattooStyles);
            return Ok(artTattooStylesMap);
        }
    }
}
