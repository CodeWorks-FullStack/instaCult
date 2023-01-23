namespace InstaCult.Models;

public class Profile : RepoItem<string>
{
  public string Name { get; set; }
  public string Picture { get; set; }
}

// NOTE having the account extend the profile keeps the email private
public class Account : Profile
{
  public string Email { get; set; }
}

// NOTE when we extend out to the CULTIST model we use profile to keep the email hidden
public class Cultist : Profile
{
  public int CultMemberId { get; set; }
}
// TODO write out extended model for many to many relationship
