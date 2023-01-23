namespace InstaCult.Services;

public class CultsService
{
  private readonly CultsRepository _repo;

  public CultsService(CultsRepository repo)
  {
    _repo = repo;
  }

  internal Cult Create(Cult cultData)
  {
    Cult cult = _repo.Create(cultData);
    return cult;
  }

  internal List<Cult> Get()
  {
    List<Cult> cults = _repo.Get();
    return cults;
  }

  internal Cult GetOne(int id)
  {
    Cult cult = _repo.GetOne(id);
    if (cult == null)
    {
      throw new Exception($"no cult at id:{id}");
    }
    return cult;
  }

  internal Cult Update(int id, Cult update)
  {
    Cult original = GetOne(id);
    update.Name = update.Name ?? original.Name;
    update.Description = update.Description ?? original.Description;
    update.MemberCount = update.MemberCount ?? original.MemberCount;
    _repo.Update(update);
    return update;
  }
}
