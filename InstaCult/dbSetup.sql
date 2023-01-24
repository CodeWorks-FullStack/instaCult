CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8 COMMENT '';


-- SECTION cults
CREATE TABLE cults(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  name VARCHAR(50) NOT NULL,
  description TEXT NOT NULL,
  leaderId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',

  Foreign Key (leaderId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

-- ALTER TABLE let you make changes to existing tables, ADD adds a COLUMN, MODIFY can change and existing one.accounts
-- Becareful altering tables, you can easily create 'bad' data if not careful
ALTER TABLE cults
 ADD COLUMN memberCount int NOT NULL DEFAULT 0;

INSERT INTO cults
(name, leaderId, description)
VALUES
('Children of Oslo', '634844a08c9d1ba02348913d', 'WE worship the one true wrinkle brain. No Herbert stans allowed. Xanther friends are cool though.');


-- SECTION cultMembers
-- NOTE DONT FORGET CASCADE
CREATE TABLE cultMembers(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  cultId INT NOT NULL,
  accountId VARCHAR(255) NOT NULL,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  
  Foreign Key (cultId) REFERENCES cults (id) ON DELETE CASCADE,
  Foreign Key (accountId) REFERENCES accounts (id) ON DELETE CASCADE
) default charset utf8 COMMENT '';

INSERT INTO `cultMembers`
(cultId, `accountId`)
VALUES
(1, '6216b36ebc31a249987812b1');


-- SECTION SCRATCH PAD

    SELECT
    cult.*,
    COUNT(cm.id) AS calcMemberCount,
    prof.*
    FROM cults cult
    LEFT JOIN cultMembers cm ON cult.id = cm.cultId
    JOIN accounts prof ON prof.id = cult.leaderId
    GROUP BY (cult.id);

    UPDATE cults SET
    memberCount = memberCount +1
    WHERE id = 1;


SELECT * from accounts;
