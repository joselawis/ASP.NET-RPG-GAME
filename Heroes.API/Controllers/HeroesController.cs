using Heroes.Application.Commands.CreateHero;
using Heroes.Application.Commands.GainExperience;
using Heroes.Application.Queries.GetAllHeroes;
using Heroes.Application.Queries.GetHero;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Heroes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HeroesController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = new GetAllHeroesQuery();
            var result = await _sender.Send(query, cancellationToken);

            if (!result.IsSuccess)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetHeroQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            if (!result.IsSuccess)
            {
                return NotFound(result.Error);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateHeroRequest request, CancellationToken cancellationToken)
        {
            var command = new CreateHeroCommand(request.Name, request.Class);
            var result = await _sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return CreatedAtAction(
                nameof(GetById),
                new { id = result.Value!.Id },
                result.Value);
        }

        [HttpPost("{id:guid}/experience")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GainExperience(
            Guid id,
            [FromBody] GainExperienceRequest request,
            CancellationToken cancellationToken)
        {
            var command = new GainExperienceCommand(id, request.ExperienceAmount);
            var result = await _sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Error);
            }

            return Ok(new { Message = "Experience gained successfully." });
        }
    }
}