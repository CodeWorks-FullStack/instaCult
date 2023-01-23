namespace InstaCult.Models;

public class Cult : RepoItem<int>
{
  public string Name { get; set; }
  public string Description { get; set; }
  // NOTE member count is incremented by the create cultMember function as a hard value
  public int? MemberCount { get; set; }
  // NOTE CalcMemberCount is a value calculated based on the many to many table whenever the information is gotten from the database
  public int? CalcMemberCount { get; set; }
  public string LeaderId { get; set; }
  public Profile Leader { get; set; }

}
