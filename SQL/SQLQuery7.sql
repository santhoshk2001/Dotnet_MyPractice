select * from tblDepartment

SELECT name
FROM sys.triggers
WHERE parent_id = OBJECT_ID('tbldepartment');


CREATE TRIGGER trg_SimpleTrigger
ON tbldepartment
AFTER INSERT
AS
BEGIN
    -- Simple example logic
    IF EXISTS (SELECT 1 FROM inserted WHERE deptno < 0)
    BEGIN
        RAISERROR ('Deptno cannot be negative.', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;
