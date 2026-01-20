using Microsoft.AspNetCore.Mvc;
using Server.Services;
using Server.Dtos;

[ApiController]
[Route("api")]
public class InvestmentController : ControllerBase
{
    private readonly IInvestmentService _service;

    public InvestmentController(IInvestmentService service)
    {
        _service = service;
    }

    [HttpGet("options")]
   
    public async Task<ActionResult<IEnumerable<InvestmentOptionDto>>> GetOptions()
    {
       
        var options = await _service.GetAvailableOptions();

     
        return Ok(options);
    }

    [HttpGet("state")]
    public async Task<ActionResult<UserStateDto>> GetState([FromQuery] string username)
    {
        if (string.IsNullOrWhiteSpace(username))
            return BadRequest("Username is required");

        var state = await _service.GetUserState(username);
        return Ok(state);
    }

    [HttpPost("invest")]
    public async Task<IActionResult> Invest([FromBody] InvestRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.OptionId))
            return BadRequest("Missing required parameters");

        var (success, error) = await _service.TryInvest(request.Username, request.OptionId);

        return success ? Ok() : BadRequest(new { error });
    }
}

public record InvestRequest(string Username, string OptionId);