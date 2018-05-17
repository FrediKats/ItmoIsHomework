USE ReviewYourselfProjectdb
GO
DROP TABLE IF EXISTS Solution
GO
CREATE TABLE Solution
(
    SolutionID      UNIQUEIDENTIFIER    NOT NULL,
    AuthorID        UNIQUEIDENTIFIER    NOT NULL,
    TaskID          UNIQUEIDENTIFIER    NOT NULL,
    TextData        NVARCHAR(MAX)       NULL,
    Posted          DATETIMEOFFSET      NOT NULL,
    Resolved        BINARY              NOT NULL,

    CONSTRAINT PK_SOLUTION              PRIMARY KEY (SolutionID),
    CONSTRAINT FK_SOLUTION_AUTHOR_ID    FOREIGN KEY (AuthorID) REFERENCES ResourceUser(UserID),
    CONSTRAINT FK_SOLUTION_TASK_ID      FOREIGN KEY (TaskID) REFERENCES ResourceTask(TaskID),
    CONSTRAINT CH_SOLUTION_POSTED       CHECK (Posted > '2018-05-12 00:00:00')
)