create database InfiniteDB

use infinitedb
--creating tables 
drop table tbldepartment

create table tblDepartment
(  DeptNo int primary key,        --column level constraint
   DeptName varchar(15) not null,
   Budget float 
)

-- to retrieve the data from the table department
select * from tblDepartment

--create another table called employees
create table tblemployees(
Empid int primary key,
Empname varchar(40) not null,
Gender char(7),
Salary float,
DeptNo int references tbldepartment(DeptNo),  --foreign key 
)

--alter the table to add a column without data
alter table tblemployees
add Phoneno varchar(10) unique   -- constraint while adding a column after table creation

-- now let us add some data to the 2 tables
insert into tblDepartment values(1,'IT',2000000)

--to insert multiple rows/tuples with single insert into then we give as below
insert into tblDepartment values(2,'HR',null),
(5,'Testing',1000000),(3,'Admin',3000000),(4,'Operations',null)

--inserting data into table with only selected columns
insert into tblDepartment(DeptNo,DeptName) values(6,'Accounts')

select * from tblDepartment

--add a column to a table with data
alter table tbldepartment
add Loc varchar(20) 

--we need to update the column Loc that was added after the table had data in it
update tblDepartment set loc = 'Pune' where deptno=6

--adding a constraint to a column of a table
alter table tbldepartment
add constraint con_uniq_loc unique (loc)

--let us add a check constraint on salary column of tblemployee table
alter table tblemployees
add constraint chksal check(Salary >=6000)

-- now let us add some data to the employee table
insert into tblemployees values(103, 'Anitha Gayathri', 'Female',6100,2, 2222211),
(102,'Adikeshava','Male',6200,1,3333221),(105,'Bindu','Female',6200,2,432125),
(101,'Abishek Lomte', 'Male',6000,3,'2345677')

select * from tblemployees

--adding a city column to the employees table with a default constraint
alter table tblemployees
add City varchar(30)

alter table tblemployees
add constraint city_def default 'Bangalore' for City

--inserting the default value for city
insert into tblemployees(Empid,Empname,Salary,DeptNo,phoneno) 
values(106,'Aman Ullah', 6300,4,73522380)

--inserting other than default value for the city column

insert into tblemployees(Empid,Empname,Salary,DeptNo,phoneno,city) 
values(107,'Ayesha', 6300,4,768522380,'Hyderabad')
