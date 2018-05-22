DROP TABLE IF EXISTS Token
GO
CREATE TABLE Token
(
    TokenData   UNIQUEIDENTIFIER    NOT NULL,
    UserID      UNIQUEIDENTIFIER    NOT NULL,
	
    CONSTRAINT PK_TOKEN             PRIMARY KEY (UserID, TokenData),
    CONSTRAINT FK_TOKEN_USER_ID     FOREIGN KEY (UserID) REFERENCES ResourceUser(UserID)
)