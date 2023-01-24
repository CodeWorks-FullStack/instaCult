namespace InstaCult.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CultsController : ControllerBase
{
  private readonly CultsService _cultsService;
  private readonly Auth0Provider _auth;

  private readonly CultMembersService _cultMembersService;

  public CultsController(CultsService cultsService, Auth0Provider auth, CultMembersService cultMembersService)
  {
    _cultsService = cultsService;
    _auth = auth;
    _cultMembersService = cultMembersService;
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
  [HttpGet("{id}")]
  public ActionResult<Cult> GetOne(int id)
  {
    try
    {
      Cult cult = _cultsService.GetOne(id);
      return Ok(cult);
    }
    catch (Exception e)
    {
      return BadRequest(e.Message);
    }
  }
  // NOTE {id} makes id a parameter, /members extends the url
  [HttpGet("{id}/members")]
  // ----------------------<Cultist> we use our extened Profile model here NOT the cultMember model, the cultMember Model represents the relationship not the data of who is the cultist.
  public ActionResult<List<Cultist>> GetCultists(int id)
  {
    try
    {
      List<Cultist> cultists = _cultMembersService.GetCultists(id);
      return Ok(cultists);
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
