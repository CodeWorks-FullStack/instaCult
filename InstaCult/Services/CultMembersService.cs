namespace InstaCult.Services;

public class CultMembersService
{
  private readonly CultMembersRepository _repo;
  private readonly CultsService _cultService;

  public CultMembersService(CultMembersRepository repo, CultsService cultService)
  {
    _repo = repo;
    _cultService = cultService;
  }

  internal int Create(CultMember cultMemberData)
  {
    int id = _repo.Create(cultMemberData);
    // NOTE next section is for hard coded memberCount
    Cult cult = _cultService.GetOne(cultMemberData.CultId);
    cult.MemberCount++;
    _cultService.Update(cult.Id, cult);
    // ---- this can be skipped if calculating count
    return id;
  }
}
