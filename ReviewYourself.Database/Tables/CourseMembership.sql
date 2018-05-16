DROP TABLE IF EXISTS CourseMembership
GO
CREATE TABLE CourseMembership
(
    UserID          UNIQUEIDENTIFIER    NOT NULL,
    CourseID        UNIQUEIDENTIFIER    NOT NULL,
    Permission      INT                 NOT NULL,

    CONSTRAINT PK_COURSE_MEMBERSHIP                 PRIMARY KEY (UserID, CourseID),
    CONSTRAINT FK_COURSE_MEMBERSHIP_USER_ID         FOREIGN KEY (UserID) REFERENCES ResourceUser(UserID),
    CONSTRAINT FK_COURSE_MEMBERSHIP_COURSE_ID       FOREIGN KEY (CourseID) REFERENCES Course(CourseID),
    CONSTRAINT CH_COURSE_MEMBERSHIP_PERMISSION      CHECK (Permission BETWEEN 0 and 1)  --only 2? maybe we should use bool
)