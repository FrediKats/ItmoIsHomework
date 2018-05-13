DROP TABLE IF EXISTS ResourceUser
GO
CREATE TABLE ResourceUser
(
    UserID          UNIQUEIDENTIFIER    NOT NULL,
    UserLogin       NVARCHAR(32)        NOT NULL,
    Email           NVARCHAR(64)        NOT NULL,
    UserPassword    NVARCHAR(64)        NOT NULL,
    FirstName       NVARCHAR(32)        NOT NULL,
    LastName        NVARCHAR(32)        NOT NULL,
    Bio             NVARCHAR(512)       NULL,   --or TEXT

    CONSTRAINT PK_RESOURCE_USER                 PRIMARY KEY (UserID),
    CONSTRAINT UN_RESOURCE_USER_USER_LOGIN      UNIQUE (UserLogin),
    CONSTRAINT CH_RESOURCE_USER_EMAIL           CHECK (Email LIKE '%@%.%'),
    CONSTRAINT CH_RESOURCE_USER_PASSWORD        CHECK (LEN(UserPassword) > 5),
    CONSTRAINT CH_RESOURCE_USER_FIRST_NAME      CHECK (LEN(FirstName) > 0),
    CONSTRAINT CH_RESOURCE_USER_LAST_NAME       CHECK (LEN(LastName) > 0),
)