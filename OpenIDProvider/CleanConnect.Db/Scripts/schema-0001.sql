/**
* Create the database schema
 */

CREATE TABLE IF NOT EXISTS CleanUser (
  Id           BIGSERIAL PRIMARY KEY,
  FamilyName     VARCHAR(50)  NOT NULL,
  OtherNames    VARCHAR(255) NULL,
  Email        VARCHAR(150) NOT NULL UNIQUE,
  EmailVerified BOOLEAN DEFAULT FALSE,
  CreatedDate  TIMESTAMP    NOT NULL,
  ModifiedDate TIMESTAMP    NOT NULL
);

CREATE TABLE IF NOT EXISTS Credential (
  Id             BIGSERIAL PRIMARY KEY,
  UserId         BIGINT      NOT NULL REFERENCES CleanUser,
  Username       VARCHAR(50) NOT NULL UNIQUE,
  HashedPassword VARCHAR(1000) NOT NULL,
  Salt           VARCHAR(1000) NOT NULL,
  Fails          INT          NOT NULL DEFAULT 0
  CreatedDate    TIMESTAMP   NOT NULL,
  ModifiedDate   TIMESTAMP   NOT NULL
);

CREATE TABLE IF NOT EXISTS UserSession (
  Id           UUID PRIMARY KEY,
  UserId       BIGINT NOT NULL REFERENCES CleanUser,
  RefreshToken UUID NOT NULL,
  CreatedDate  TIMESTAMP NOT NULL,
  ModifiedDate TIMESTAMP NOT NULL
);

CREATE TABLE IF NOT EXISTS Client (
  Id        UUID PRIMARY KEY,
  Name      VARCHAR(50) NOT NULL,
  Secret    VARCHAR(40) NOT NULL   
);

CREATE TABLE IF NOT EXISTS ClientUrls (
  Id       BIGSERIAL PRIMARY KEY,
  ClientId UUID NOT NULL REFERENCES Client,
  Url      VARCHAR(250) NOT NULL
)






