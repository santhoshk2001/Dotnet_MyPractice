--example
create database Healthcare on primary
(
--primary group
Name='HC',
filename ='C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Healthcare.mdf',
size = 5mb,
maxsize=unlimited,
filegrowth=1024kb
),
--secondary filegroup
--first file in secondary group
filegroup healthgrp1
(
Name='HealthCarefile1',
filename='C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\HealthCarefile1.ndf',
size=1mb,
maxsize=unlimited,
filegrowth=1024kb),
--like wise we can create many secondary groups
--for log file
filegroup Healthloggrp
(
Name='HealthLogFile',
filename='C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Healthlogfile1.ldf',
size=5mb,
maxsize=unlimited,
filegrowth=1024kb)

