SELECT TABLE_NAME
FROM INFORMATION_SCHEMA.TABLES
WHERE TABLE_TYPE='BASE TABLE'

SELECT name, definition, type_desc
FROM sys.sql_modules m
    INNER JOIN sys.objects o
    ON m.object_id=o.object_id