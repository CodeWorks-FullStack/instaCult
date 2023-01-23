namespace InstaCult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CultsController : ControllerBase
{
  private readonly CultsService _cultsService;
  private readonly Auth0Provider _auth;

  public CultsController(CultsService cultsService, Auth0Provider auth)
  {
    _cultsService = cultsService;
    _auth = auth;
  }

  [HttpGet]
  public ActionResult<List<Cult>> Get()
  {
    try
    {
      List<Cult> cults = _cultsService.Get();
      return Ok(cults);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

  [HttpPost]
  [Authorize]
  public async Task<ActionResult<Cult>> Create([FromBody] Cult cultData)
  {
    try
    {
      Account userInfo = await _auth.GetUserInfoAsync<Account>(HttpContext);
      cultData.LeaderId = userInfo.Id;
      Cult cult = _cultsService.Create(cultData);
      return Ok(cult);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }

}
