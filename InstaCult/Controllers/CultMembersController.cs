namespace InstaCult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CultMembersController : ControllerBase
{
  private readonly CultMembersService _cultMembersService;
  private readonly Auth0Provider _auth;

  public CultMembersController(CultMembersService cultMembersService, Auth0Provider auth)
  {
    _cultMembersService = cultMembersService;
    _auth = auth;
  }


  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Cultist>> Create([FromBody] CultMember cultMemberData)
  {
    try
    {
      Cultist userInfo = await _auth.GetUserInfoAsync<Cultist>(HttpContext);
      cultMemberData.AccountId = userInfo.Id;
      // NOTE think about what your client needs as a response, you might have to assemble that data to return it.
      // in this case we want the data of who joined, so we get the id from the service/repo, and join it in the controller to return to the client.
      int id = _cultMembersService.Create(cultMemberData);
      userInfo.CultMemberId = id;
      return Ok(userInfo);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpDelete("{id}")]
  [Authorize]
  public async Task<ActionResult<string>> Remove(int id)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      string message = _cultMembersService.Remove(id, userInfo.Id);
      return Ok(message);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
}
