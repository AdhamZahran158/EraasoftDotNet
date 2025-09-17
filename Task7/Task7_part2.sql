create schema task7_part2
go

-- 1. Create a table named "Employees" with columns for ID (integer), Name (varchar), and Salary (decimal).
create table task7_part2.Employees(
ID int,
Name varchar(20),
Salary decimal
)

-- 2. Add a new column named "Department" to the "Employees" table with data type varchar(50).
alter table task7_part2.Employees
add Department varchar(50)	

-- 3. Remove the "Salary" column from the "Employees" table.
alter table task7_part2.Employees
drop column Salary

-- 4. Rename the "Department" column in the "Employees" table to "DeptName".
exec sp_rename 'task7_part2.Employees.Department','DeptName','column'

-- 5. Create a new table called "Projects" with columns for ProjectID (integer) and ProjectName (varchar).
create table task7_part2.Projects(
ProjectID int,
ProjectName varchar(20))

-- 6. Add a primary key constraint to the "Employees" table for the "ID" column.
alter table task7_part2.Employees
alter column ID int not null

alter table task7_part2.Employees
add constraint PK_Employees primary key (ID)

-- 7. Add a unique constraint to the "Name" column in the "Employees" table.
alter table task7_part2.Employees
add constraint uq_Employees Unique(Name)

-- 8. Create a table named "Customers" with columns for CustomerID (integer), FirstName (varchar), LastName (varchar), and Email (varchar), and Status (varchar).
create table task7_part2.Customers(
CustomerID int,
FirstName varchar(20),
LastName varchar(20),
Email varchar(50),
Status varchar(20)
)

-- 9. Add a unique constraint to the combination of "FirstName" and "LastName" columns in the "Customers" table.
alter table task7_part2.Customers
add constraint uq_Customers unique(FirstName,LastName)

-- 10. Create a table named "Orders" with columns for OrderID (integer), CustomerID (integer), OrderDate (datetime), and TotalAmount (decimal).
create table task7_part2.Orders(
OrderID int,
CustomerID int,
OrderDate datetime2,
TotalAmount decimal
)

-- 11. Add a check constraint to the "TotalAmount" column in the "Orders" table to ensure that it is greater than zero.
alter table task7_part2.Orders
add constraint check_orders check (TotalAmount > 0) 
go

-- 12. Create a schema named "Sales" and move the "Orders" table into this schema.
create schema Sales
go

alter schema Sales
transfer task7_part2.Orders

-- 13. Rename the "Orders" table to "SalesOrders."
exec sp_rename 'Sales.Orders','SalesOrders'