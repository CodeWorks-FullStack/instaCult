namespace InstaCult.Models;

public class CultMember : RepoItem<int>
{
  public int CultId { get; set; }
  public string AccountId { get; set; }
}
