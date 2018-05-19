USE ReviewYourselfProjectdb
GO
DROP TABLE IF EXISTS Criteria
GO
CREATE TABLE Criteria
(
    CriteriaID              UNIQUEIDENTIFIER    NOT NULL,
    TaskID                  UNIQUEIDENTIFIER    NOT NULL,
    Title                   NVARCHAR(64)        NOT NULL,
    CriteriaDescription     NVARCHAR(512)       NULL,
    MaxPoint                INT                 NOT NULL,

    CONSTRAINT PK_CRITERIA                          PRIMARY KEY (CriteriaID),
    CONSTRAINT FK_CRITERIA_TASK_ID                  FOREIGN KEY (TaskID) REFERENCES ResourceTask(TaskID),
    CONSTRAINT CH_CRITERIA_TITLE                    CHECK (LEN(Title) > 5),
    CONSTRAINT CH_CRITERIA_CRITERIA_DESCRIPTION     CHECK (LEN(CriteriaDescription) > 30),
    CONSTRAINT CH_CRITERIA_MAX_POINT                CHECK (MaxPoint > 0)
)