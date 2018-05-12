DROP TABLE IF EXISTS ResourceUser
GO
CREATE TABLE ResourceUser
(
    UserID          UNIQUEIDENTIFIER    NOT NULL,
    UserLogin       NVARCHAR(32)        NOT NULL,   --seems that 'login' & 'password' are keywords
    Email           NVARCHAR(64)        NOT NULL,
    UserPassword    NVARCHAR(64)        NOT NULL,
    FirstName       NVARCHAR(32)        NOT NULL,
    Lastname        NVARCHAR(32)        NOT NULL,
    Bio             NVARCHAR(512)       NULL,   --or TEXT

    CONSTRAINT PK_RESOURCE_USER                 PRIMARY KEY (UserID),
    CONSTRAINT CH_RESOURCE_USER_PASSWORD        CHECK (LEN(UserPassword) > 5),
    CONSTRAINT CH_RESOURCE_USER_FIRST_NAME      CHECK (LEN(FirstName) > 0),
    CONSTRAINT CH_RESOURCE_USER_LAST_NAME       CHECK (LEN(LastName) > 0)
)