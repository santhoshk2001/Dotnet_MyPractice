use InfiniteDB

SELECT d.deptname, SUM(e.salary) AS total_salary
FROM tblemployees e
JOIN tbldepartment d ON e.deptno = d.deptno
GROUP BY ROLLUP(d.deptname);


SELECT 
    COALESCE(d.deptname, 'General') AS Department,
    COALESCE(e.gender, 'All genders') AS Gender,
    SUM(e.salary) AS TotalSalary
FROM tblemployees e
LEFT JOIN tblDepartment d ON e.deptno = d.deptno
GROUP BY ROLLUP(d.deptname, e.gender);


select * from tblemployees

select * from tblDepartment