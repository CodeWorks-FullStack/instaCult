namespace InstaCult.Repositories;

public class CultMembersRepository
{
  private readonly IDbConnection _db;

  public CultMembersRepository(IDbConnection db)
  {
    _db = db;
  }

  internal int Create(CultMember cultMemberData)
  {
    // NOTE hard coded member count MySQL only solution
    // string sql = @"
    // UPDATE cults SET
    // memberCount = memberCount +1
    // WHERE id = @cultId;

    // INSERT INTO cultMembers
    // (cultId, accountId)
    // VALUES
    // (@cultId, @accountId);
    // SELECT LAST_INSERT_ID();
    // ";
    string sql = @"
    INSERT INTO cultMembers
    (cultId, accountId)
    VALUES
    (@cultId, @accountId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, cultMemberData);
    return id;
  }
}
