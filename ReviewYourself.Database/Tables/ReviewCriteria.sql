DROP TABLE IF EXISTS ReviewCriteria
GO
CREATE TABLE ReviewCriteria
(
    ReviewID                UNIQUEIDENTIFIER    NOT NULL,
    CriteriaID              UNIQUEIDENTIFIER    NOT NULL,
    Rating                  INT                 NOT NULL,
    CriteriaDescription     NVARCHAR(512)       NULL,

    CONSTRAINT PK_REVIEW_CRITERIA                   PRIMARY KEY (ReviewID, CriteriaID),
    CONSTRAINT FK_REVIEW_CRITERIA_REVIEW_ID         FOREIGN KEY (ReviewID) REFERENCES Review(ReviewID),
    CONSTRAINT FK_REVIEW_CRITERIA_CRITERIA_ID       FOREIGN KEY (CriteriaID) REFERENCES Criteria(CriteriaID),
    CONSTRAINT CH_REVIEW_CRITERIA_RATING            CHECK (Rating >= 0)  --CHECK (Rating BETWEEN 0 AND (SELECT SUM(MaxPoint) FROM Criteria WHERE ReviewCriteria.CriteriaID = Criteria.CriteriaID)),
)