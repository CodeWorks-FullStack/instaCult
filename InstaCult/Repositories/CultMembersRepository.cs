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

  internal List<Cultist> GetCultists(int cultId)
  {
    string sql = @"
    SELECT
    cm.*,
    a.*
    FROM cultMembers cm
    JOIN accounts a ON cm.accountId = a.id
    WHERE cm.cultId = @cultId; 
    ";
    List<Cultist> cultists = _db.Query<CultMember, Cultist, Cultist>(sql, (cm, cultist) =>
    {
      cultist.CultMemberId = cm.Id;
      return cultist;
    }, new { cultId }).ToList();
    return cultists;
  }

  internal CultMember GetOneCultMember(int cultMemberId)
  {
    string sql = @"
    SELECT
    *
    FROM cultMembers
    WHERE id = @cultMemberId;
    ";
    return _db.Query<CultMember>(sql, new { cultMemberId }).FirstOrDefault();
  }

  internal bool Remove(int cultMemberId)
  {
    string sql = @"
    DELETE FROM cultMembers
    WHERE id = @cultMemberId;
    ";
    // ----------------------example: { cultMemberId: 5}
    // dapper (execute/query) runs your sql replacing matching Keys with their values
    //   DELETE FROM cultMembers
    //   WHERE id = 5;
    int rows = _db.Execute(sql, new { cultMemberId });
    return rows > 0;
  }
}
