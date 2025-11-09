using Combat.Application.Commands.InitiateCombat;
using Combat.Application.Queries.GetCombat;
using Combat.Application.Queries.GetHeroCombatHistory;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Combat.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CombatController(ISender sender) : ControllerBase
    {
        private readonly ISender _sender = sender;

        [HttpPost("initiate")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InitiateCombat([FromBody] InitiateCombatRequest request, CancellationToken cancellationToken)
        {
            var command = new InitiateCombatCommand(request.HeroId, request.EnemyId);
            var result = await _sender.Send(command, cancellationToken);

            if (!result.IsSuccess)
                return BadRequest(result.Error);

            return CreatedAtAction(
                nameof(GetCombat),
                new { id = result.Value!.Id },
                result.Value);
        }

        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCombat(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCombatQuery(id);
            var result = await _sender.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return NotFound(result.Error);

            return Ok(result.Value);
        }

        [HttpGet("hero/{heroId:guid}/history")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetHeroCombatHistory(Guid heroId, CancellationToken cancellationToken)
        {
            var query = new GetHeroCombatHistoryQuery(heroId);
            var result = await _sender.Send(query, cancellationToken);

            if (!result.IsSuccess)
                return StatusCode(500, result.Error);

            return Ok(result.Value);
        }
    }
}