create database InfiniteDB

use InfiniteDB
 
-- Drop table if it exists
IF OBJECT_ID('tblDepartment', 'U') IS NOT NULL
DROP TABLE tblDepartment;
-- Create tblDepartment table
CREATE TABLE tblDepartment (
DeptNo INT PRIMARY KEY,
DeptName VARCHAR(15) NOT NULL,
Budget FLOAT,
Loc NVARCHAR(20)
);
-- Insert data into tblDepartment
INSERT INTO tblDepartment (DeptNo, DeptName, Budget, Loc) VALUES
(1, 'IT', 2000000, NULL),
(2, 'HR', NULL, NULL),
(5, 'Testing', 1000000, NULL),
(3, 'Admin', 3000000, NULL),
(4, 'Operations', NULL, NULL),
(6, 'Accounts', NULL, 'Pune');
-- Verify data in tblDepartment
SELECT * FROM tblDepartment;
 
-- Drop table if it exists
IF OBJECT_ID('tblemployees', 'U') IS NOT NULL
DROP TABLE tblemployees;
-- Create tblemployees table
CREATE TABLE tblemployees (
Empid INT PRIMARY KEY,
Empname VARCHAR(40) NOT NULL,
Gender CHAR(7),
Salary FLOAT,
DeptNo INT REFERENCES tbldepartment(DeptNo),
Phoneno VARCHAR(10) UNIQUE
);
-- Alter table to add City column with default constraint
ALTER TABLE tblemployees
ADD City VARCHAR(30) DEFAULT 'Bangalore';
-- Insert data into tblemployees
INSERT INTO tblemployees (Empid, Empname, Gender, Salary, DeptNo, Phoneno)
VALUES
(103, 'Anitha Gayathri', 'Female', 6100, 2, '2222211'),
(102, 'Adikeshava', 'Male', 6200, 1, '3333221'),
(105, 'Bindu', 'Female', 6200, 2, '432125'),
(101, 'Abishek Lomte', 'Male', 6000, 3, '2345677'),
(106, 'Aman Ullah', NULL, 6300, 4, '73522380'),
(107, 'Ayesha', NULL, 6300, 4, '768522380');
-- Verify data in tblemployees
SELECT * FROM tblemployees;
 
CREATE PROCEDURE UpdateEmployeeSalary
    @empid INT
AS
BEGIN
    -- Declare variables to store employee details
    DECLARE @empName VARCHAR(40);
    DECLARE @empSalary FLOAT;
    -- Retrieve employee details
    SELECT @empName = Empname, @empSalary = Salary
    FROM tblemployees
    WHERE Empid = @empid;
    -- Check if the salary is less than 6300 and update if necessary
    IF @empSalary < 6300
    BEGIN
        UPDATE tblemployees
        SET Salary = Salary + 100
        WHERE Empid = @empid;
        -- Retrieve the updated salary
        SELECT @empSalary = Salary
        FROM tblemployees
        WHERE Empid = @empid;
    END
    -- Display the updated details
    SELECT @empName AS EmployeeName, @empSalary AS UpdatedSalary;
END;

EXEC UpdateEmployeeSalary @empid = 103;
 
SELECT Empid, Empname, Salary
FROM tblemployees
WHERE Empid = 103;


use AssignmentDB

select * from EMP;

select * from DEPT;

CREATE PROCEDURE GetEmployeeManagerAndSalary
    @empno INT,
    @mgr_id INT OUTPUT,
    @sal DECIMAL(7, 2) OUTPUT
AS
BEGIN
    -- Retrieve the manager ID and salary of the employee
    SELECT @mgr_id = MGR_ID, @sal = SAL
    FROM EMP
    WHERE EMPNO = @empno;

    -- Output the results
    SELECT @mgr_id AS ManagerID, @sal AS Salary;
END;
GO

DECLARE @mgr_id INT;
DECLARE @sal DECIMAL(7, 2);

EXEC GetEmployeeManagerAndSalary @empno = 7369, @mgr_id = @mgr_id OUTPUT, @sal = @sal OUTPUT;

SELECT @mgr_id AS ManagerID, @sal AS Salary;


--ex3
CREATE PROCEDURE GetDepartmentStats
    @deptno INT,
    @avg_sal DECIMAL(7, 2) OUTPUT,
    @emp_count INT OUTPUT
AS
BEGIN
    -- Retrieve the average salary and count of employees in the specified department
    SELECT @avg_sal = AVG(SAL), @emp_count = COUNT(*)
    FROM EMP
    WHERE DEPTNO = @deptno;

    -- Output the results
    SELECT @avg_sal AS AverageSalary, @emp_count AS EmployeeCount;
END;
GO

DECLARE @avg_sal DECIMAL(7, 2);
DECLARE @emp_count INT;

EXEC GetDepartmentStats @deptno = 10, @avg_sal = @avg_sal OUTPUT, @emp_count = @emp_count OUTPUT;

SELECT @avg_sal AS AverageSalary, @emp_count AS EmployeeCount;








