namespace InstaCult.Repositories;

public class CultsRepository
{
  private readonly IDbConnection _db;

  public CultsRepository(IDbConnection db)
  {
    _db = db;
  }

  internal Cult Create(Cult cultData)
  {
    string sql = @"
    INSERT INTO cults
    (name, description, leaderId)
    VALUES
    (@name, @description, @leaderId);
    SELECT LAST_INSERT_ID();
    ";
    int id = _db.ExecuteScalar<int>(sql, cultData);
    cultData.Id = id;
    return cultData;
  }

  internal List<Cult> Get()
  {
    // NOTE regular get all
    // string sql = @"
    // SELECT
    // cult.*,
    // prof.*
    // FROM cults cult
    // LEFT JOIN cultMembers cm ON cult.id = cm.cultId
    // JOIN accounts prof ON prof.id = cult.leaderId;
    // ";
    // NOTE get all with calculated count of members (left join/ group by/ count)
    string sql = @"
    SELECT
      cult.*,
      COUNT(cm.id) AS calcMemberCount,
      prof.*
    FROM cults cult
      LEFT JOIN cultMembers cm ON cult.id = cm.cultId
      JOIN accounts prof ON prof.id = cult.leaderId
    GROUP BY (cult.id);";
    List<Cult> cults = _db.Query<Cult, Profile, Cult>(sql, (cult, prof) =>
    {
      cult.Leader = prof;
      return cult;
    }).ToList();
    return cults;
  }

  internal Cult GetOne(int cultId)
  {
    // NOTE regular get one
    // string sql = @"
    // SELECT 
    // cult.*,
    // prof.* 
    // FROM cults cult
    // JOIN accounts prof ON prof.id = cult.leaderId
    // WHERE cult.id = @cultId;
    // ";
    // NOTE get all with calculated count of members (left join/ group by/ count)
    string sql = @"
    SELECT
      cult.*,
      COUNT(cm.id) AS calcMemberCount,
      prof.*
    FROM cults cult
      LEFT JOIN cultMembers cm ON cult.id = cm.cultId
      JOIN accounts prof ON prof.id = cult.leaderId
    WHERE cult.id = @cultId
    GROUP BY (cult.id);
    ";
    Cult cult = _db.Query<Cult, Profile, Cult>(sql, (cult, prof) =>
    {
      cult.Leader = prof;
      return cult;

    }, new { cultId }).FirstOrDefault();
    return cult;
  }

  internal bool Update(Cult update)
  {
    string sql = @"
    UPDATE cults SET
    name = @name,
    description = @description,
    memberCount = @memberCount
    WHERE id = @id;
    ";
    int rows = _db.Execute(sql, update);
    return rows > 0;
  }
}
