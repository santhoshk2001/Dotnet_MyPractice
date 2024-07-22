use InfiniteDB

select * from tblemployees

select * from tblDepartment

--ex 2. create a function that returns the total no of male employees
create function count_male_employees() 
returns int
as
begin
    declare @male_count int;

    select @male_count = count(*)
    from tblemployees
    where gender = 'male';

    return @male_count;
end;

-- Call the function
select dbo.count_male_employees() as Total;


--ex 3. Write a function to add 2 numbers and return the value
create function dbo.AddTwoNumbers 
(
    @num1 int, 
    @num2 int
)
returns int
as
begin
    return @num1 + @num2;
end;

-- call the function
select dbo.AddTwoNumbers(10, 20) as Result;



  --ex 4. write a function to calculate the area of rectangle
 create function dbo.CalculateRectangleArea 
(
    @length float, 
    @width float
)
returns float
as
begin
    return @length * @width;
end;

-- call the function
select dbo.CalculateRectangleArea(10.0, 20.0) as Area;



create or alter function fn_GetEmployeesbyGender(@gender varchar(8))
returns
@EmpBygender table(
Empid int, EmployeeName varchar(40),
Gender varchar(7))
as
begin
--bulk insertion
insert into @EmpBygender
select Empid,Empname,gender from tblemployees 
where Gender= @gender
--handling exception situation
if @@ROWCOUNT = 0
begin
  insert into @EmpBygender
  values(0,'No Emp found with the given gender',null)
  end
return
end
 
--to execute the above function
select * from fn_GetEmployeesbyGender('male')

