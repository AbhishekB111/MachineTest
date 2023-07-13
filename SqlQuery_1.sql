create database MachineTest

Create table Products
(
ProductId int identity(1,1) primary Key,
ProductName nvarchar(50) null,
CategoryId int null
)

Create table Categories
(
CategoryId int identity(1,1) primary Key,
CategoryName nvarchar(50) null
)