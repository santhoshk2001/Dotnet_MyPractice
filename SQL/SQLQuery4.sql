use InfiniteDB

select * from tblemployees

alter table tblemployees add Remarks varchar(30)

--Transactions
-- 1.
begin transaction
select * from tblemployees where Empname='Bindu'
update tblemployees set remarks='Active'
where Empname='Bindu'
select * from tblemployees where Empname='Bindu'

commit

-- 2.
begin tran
insert into tblemployees values(160,'Vinay','male',6500,2,1234356,'Chennai',null) -- this has to happen
save tran t1 
save tran t1
select * from tblemployees
delete from tblemployees where Empid=150
select * from tblemployees where Empid=150
save tran t2
update tblDepartment set budget=50000 where DeptNo=2
select * from tblDepartment
rollback tran t2
--rollback tran t1
--rollback
commit tran t2 -- or rollback
commit tran t1 -- only one commit is allowed


select * from tblDepartment
select * from tblemployees




--procedure with exception handling, transactions, tsql and few dml operations
create table Products
(ProductId int primary key,
Productname varchar(30) not null,
Price int,
QuantityAvailable int)

--let us populate data into products
insert into Products values(101,'Laptops',55000,100),
(102,'Desktops',35000,50),(103,'Tablets',60000,45),(104,'SmartPhones',45000,25)


--product sales table
create table ProductSales
(SaleId int primary key,
ProductId int references Products(ProductId),
QuantitySold int)

select * from Products

select * from ProductSales

select max(saleid) from ProductSales

create or alter proc Sales
@pid int, @qty_to_sell int
as 
begin
--first we need to check if there is enough stock available to sell
declare @stockavailable int
select @stockavailable=QuantityAvailable from Products where ProductId=@pid
--we need to throw an error if the stock is less than the qty to sell
if(@stockavailable<@qty_to_sell)
begin
raiserror('Not Enough Stock on hand to sell',16,1)
end

else
begin --we shall start a transaction
begin tran
--we need to update the qty available in products table and also insert ine sales row in productsales table
update Products
set QuantityAvailable=(QuantityAvailable-@qty_to_sell)
where ProductId=@pid

--now let us insert one row into productsales
--inorder not to have pk violation, we should not hard code an data for sale id we can write a logic that autogenerates the saleid is less than the qty to sell

declare @maxsaleid int
select @maxsaleid=case
when max(saleId) is null then 0
else max(saleId)
end
from ProductSales 
--we will increment the @maxsaleid by 1, to avoid pk violation
--set @maxsaleid = @maxsaleid+1

insert into ProductSales values(@maxsaleid,@pid,@qty_to_sell)
--@@Error, a global variable will have 0 if no errors
if(@@ERROR <> 0)
begin
rollback transaction
print 'something went wrong...try later, rolling back'
end

else
begin
commit transaction 
print 'Transaction Successfull'
end
end
end

--let us execute the above proc
Sales 102,20


set implicit_transactions off 

insert into tblDepartment values(21,'New Dept',null,'New Loc2')
select * from tblDepartment

update tblDepartment set loc='Cochin' where DeptNo=21
rollback

--Triggers
--1.
create trigger trgEmpIns
on tblemployees
for insert
as
 begin
  select * from inserted
  select * from tblemployees
end

insert into tblemployees values(300,'Tanmayee','Female',6700,1,998800,'Bengaluru',null)

--2.
create or alter trigger trgUpdEmp
on tblemployees
for update
as
begin
select * from deleted
select remarks from inserted
declare @status varchar(20)
select @status=remarks from inserted
if(@status='active')
 begin
 print 'Employee is active'
 end
end

update tblemployees set remarks ='active' where empid=160

--3.
create trigger trg_Nochanges
on tbldepartment
for insert,update,delete
as
begin
 print 'cannot manipulate table'
 rollback
 end

 select * from tblDepartment
 insert into tblDepartment values(100,'aa',567,'l1')

 update tblDepartment set loc='Cochin' where deptno=6

 drop trigger trg_nochanges




 create table Employee_Audit(Msg varchar(max))
 
create or alter trigger trgAuditInsert
on tblemployees
for insert
as
begin
declare @id int
select @id=empid from inserted
 
insert into Employee_Audit
values('New Employee with Empid '+ ' '+ cast(@id as varchar(5)) + ' is added at '
+ cast(getdate() as nvarchar(20)))
end
 
insert into tblemployees values(200,'Banurekha','Female',6500,4,007,'chennai',null)
 
select * from employee_audit


create table Employee_delete(Msg varchar(max))
create or alter trigger trgAuditdelete
on products
for delete
as
begin
declare @id int
select @id=ProductId from deleted
insert into Employee_delete
values('Employee with Empid '+ ' '+ cast(@id as varchar(5)) + ' is deleted at '
+ cast(getdate() as nvarchar(20)))
end 


delete from products where ProductId=101

select * from employee_delete
