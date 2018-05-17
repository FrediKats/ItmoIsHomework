USE ReviewYourselfProjectdb
GO
DROP TABLE IF EXISTS ResourceTask
GO
CREATE TABLE ResourceTask
(
    TaskID              UNIQUEIDENTIFIER    NOT NULL,
    CourseID            UNIQUEIDENTIFIER    NOT NULL,
    Title               NVARCHAR(64)        NOT NULL,
    TaskDescription     NVARCHAR(MAX)       NOT NULL, --or text
    Posted              DATETIMEOFFSET      NOT NULL,

    CONSTRAINT PK_RESOURCE_TASK                     PRIMARY KEY (TaskID),
    CONSTRAINT FK_RESOURCE_TASK_COURSE_ID           FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
    CONSTRAINT CH_RESOURCE_TASK_TITLE               CHECK (LEN(Title) > 5),
    CONSTRAINT CH_RESOURCE_TASK_TASK_DESCRIPTION    CHECK (LEN(TaskDescription) > 50),
    CONSTRAINT CH_RESOURCE_TASK_POSTED              CHECK (Posted > '2018-05-12 00:00:00')
)