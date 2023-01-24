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

  internal List<Cultist> GetCultists(int cultId)
  {
    List<Cultist> cultists = _repo.GetCultists(cultId);
    return cultists;
  }

  internal string Remove(int cultMemberId, string userId)
  {
    CultMember cultMember = _repo.GetOneCultMember(cultMemberId);
    if (cultMember == null) throw new Exception($"no cult member at id {cultMemberId}");

    Cult cult = _cultService.GetOne(cultMember.CultId);
    if (userId != cult.LeaderId) throw new Exception($"Where do you think you're going?");

    bool result = _repo.Remove(cultMemberId);
    if (result == false) throw new Exception($"not member at id: {cultMemberId}");

    return "Someone has left the cult, they can't be far.";
  }
}
