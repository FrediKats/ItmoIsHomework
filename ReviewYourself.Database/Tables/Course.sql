USE ReviewYourselfProjectdb
GO
DROP TABLE IF EXISTS Course
GO
CREATE TABLE Course
(
    CourseID            UNIQUEIDENTIFIER    NOT NULL,
    Title               NVARCHAR(64)        NOT NULL,
    CourseDescription   NVARCHAR(512)       NULL,   --or text
    MentorID            UNIQUEIDENTIFIER    NOT NULL,
	
    CONSTRAINT PK_COURSE                        PRIMARY KEY (CourseID),
    CONSTRAINT FK_COURSE_MENTOR_ID              FOREIGN KEY (MentorID) REFERENCES ResourceUser(UserID),
    CONSTRAINT CH_COURSE_TITLE                  CHECK (LEN(Title) > 5),
    CONSTRAINT CH_COURSE_COURSE_DESCRIPTION     CHECK (LEN(CourseDescription) > 50)
)