create database task8

use task8
go

create schema barwon_health
go

create table barwon_health.Doctor(
ID int primary key identity(1,1),
Name varchar(20),
Email varchar(50),
Phone varchar(20),
Specialty varchar(50),
YOE tinyint
)

create table barwon_health.Patient(
UR_num int primary key identity(1,1),
Name varchar(20),
Address varchar(100),
Age tinyint,
Email varchar(50),
Phone varchar(20),
Medicare_Card_Num int,
Doctor_ID int references barwon_health.Doctor(ID)
)

create table barwon_health.Company(
ID int primary key identity(1,1),
Name varchar(50),
Address varchar(100),
Phone Varchar(20)
)

create table barwon_health.Drug(
ID int primary key identity(1,1),
Drug_strength varchar(20),
Trade_name varchar(50),
Company_ID int references barwon_health.Company(ID)
)

create table barwon_health.Doctor_Prescribe_Patient_Drug(
Doc_ID int references barwon_health.Doctor(ID),
Patient_ID int references barwon_health.Patient(UR_num),
Drug_ID int references barwon_health.Drug(ID),
Date datetime2,
Quantity tinyint,
primary key(Doc_ID, Patient_ID, Drug_ID)
)

-- DML Questions

-- 1. SELECT: Retrieve all columns from the Doctor table.
select *
from barwon_health.Doctor

-- 2. ORDER BY: List patients in the Patient table in ascending order of their ages.
select *
from barwon_health.Patient
order by barwon_health.Patient.Age

-- 3. OFFSET FETCH: Retrieve the first 10 patients from the Patient table, starting from the 5th record.
select *
from barwon_health.Patient
order by Patient.UR_num
offset 5 rows
fetch next 10 rows only

-- 4. SELECT TOP: Retrieve the top 5 doctors from the Doctor table.
select top(5) *
from barwon_health.Doctor

-- 5. SELECT DISTINCT: Get a list of unique address from the Patient table.
select distinct Address
from barwon_health.Patient

-- 6. WHERE: Retrieve patients from the Patient table who are aged 25.
select *
from barwon_health.Patient
where Age=25

-- 7. NULL: Retrieve patients from the Patient table whose email is not provided.
select *
from barwon_health.Patient
where Email is null

-- 8. AND: Retrieve doctors from the Doctor table who have experience greater than 5 years and specialize in 'Cardiology'.
select *
from barwon_health.Doctor
where YOE>5 and Specialty = 'Cardiology'

-- 9. IN: Retrieve doctors from the Doctor table whose speciality is either 'Dermatology' or 'Oncology'.
select *
from barwon_health.Doctor
where Specialty in('Dermatology', 'Oncology')

-- 10. BETWEEN: Retrieve patients from the Patient table whose ages are between 18 and 30.
select *
from barwon_health.Patient
where Age between 18 and 30

-- 11. LIKE: Retrieve doctors from the Doctor table whose names start with 'Dr.'.
select *
from barwon_health.Doctor
where Name like 'Dr.%'

-- 12. Column & Table Aliases: Select the name and email of doctors, aliasing them as 'DoctorName' and 'DoctorEmail'.
select Name as DoctorName, Email as DoctorEmail
from barwon_health.Doctor

-- 13. Joins: Retrieve all prescriptions with corresponding patient names.
select Doc_ID, Patient_ID, Drug_ID, Date, Quantity,Name as PatientName
from barwon_health.Doctor_Prescribe_Patient_Drug p join barwon_health.Patient pa
on p.Patient_ID = pa.UR_num

-- 14. GROUP BY: Retrieve the count of patients grouped by their cities.
--!!   BASED ON THE MOCK DATA I USED, I NEEDED TO SEARCH FOR A WAY TO TRIM THE ADDRESS TO GET THE CITY SO I SEARCHED AND FOUND THIS SUBSTRING METHOD

select ltrim(SUBSTRING(Address, CHARINDEX(',', Address) + 1, LEN(Address))) as City, COUNT(*) as Count
FROM barwon_health.Patient
group by ltrim(SUBSTRING(Address, CHARINDEX(',', Address) + 1, LEN(Address)))


-- 15. HAVING: Retrieve cities with more than 3 patients.
select ltrim(substring(Address, charindex(',', Address) + 1, len(Address))) as City, count(*) as count
from barwon_health.Patient
group by ltrim(substring(Address, charindex(',', Address) + 1, len(Address)))
having count(*) > 3

-- 16. GROUPING SETS: Retrieve counts of patients grouped by cities and ages.
select ltrim(substring(Address, CHARINDEX(',', Address) +1, len(Address))) as City, Age, COUNT(*) as count
from barwon_health.Patient
group by GROUPING sets(
(ltrim(substring(Address, CHARINDEX(',', Address) +1, len(Address)))),
(Age)
)

-- 17. CUBE: Retrieve counts of patients considering all possible combinations of city and age.
select ltrim(substring(Address, CHARINDEX(',', Address) +1, len(Address))) as City, Age, COUNT(*) as count
from barwon_health.Patient
group by cube (
(ltrim(substring(Address, CHARINDEX(',', Address) +1, len(Address)))),
Age)

-- 18. ROLLUP: Retrieve counts of patients rolled up by city.
select ltrim(substring(Address, CHARINDEX(',', Address) +1, len(Address))) as City, COUNT(*) as count
from barwon_health.Patient
group by rollup (ltrim(substring(Address, CHARINDEX(',', Address) +1, len(Address))))

-- 19. EXISTS: Retrieve patients who have at least one prescription.
select Name as patient
from barwon_health.Patient pa
where exists (
select *
from barwon_health.Doctor_Prescribe_Patient_Drug pr
where pa.UR_num = pr.Patient_ID
)

-- 20. UNION: Retrieve a combined list of doctors and patients.
select Name as AllNames
from barwon_health.Patient
union 
select Name
from barwon_health.Doctor

-- 21. Common Table Expression (CTE): Retrieve patients along with their doctors using a CTE.
with patient_Doc as (
select ptnt.Name as patient_Name, doc.Name as His_doctor
from barwon_health.Patient ptnt join barwon_health.Doctor doc
on ptnt.Doctor_ID = doc.ID
)

select *
from patient_Doc

-- 22. INSERT: Insert a new doctor into the Doctor table.
insert into barwon_health.Doctor(Name) values('Hameed')

-- 23. INSERT Multiple Rows: Insert multiple patients into the Patient table.
insert into barwon_health.Patient(Name,Age,Doctor_ID) values ('Adel',29,14) , ('Ahmed Khaled',21,5)

-- 24.UPDATE: Update the phone number of a doctor.
update  barwon_health.Doctor
set Phone = 01285691710
where ID = 26

-- 25. UPDATE JOIN: Update the city of patients who have a prescription from a specific doctor.
update barwon_health.Patient
set Address = RTRIM(substring(Address, 0, LEN(Address) - CHARINDEX(',',Address) + 1)) + ' Cairo'
from barwon_health.Patient pa
join barwon_health.Doctor_Prescribe_Patient_Drug pd
on pa.UR_num = pd.Patient_ID
join barwon_health.Doctor doc
on doc.ID = pd.Doc_ID
where doc.ID = 2

-- 26. DELETE: Delete a patient from the Patient table.
delete barwon_health.Patient
where UR_num = 27

-- 27. Transaction: Insert a new doctor and a patient, ensuring both operations succeed or fail together.
begin transaction;

insert into barwon_health.Doctor(Name) values ('Emad')

insert into barwon_health.Patient(Name) values ('Patrick')
commit;
rollback;
go

-- 28. View: Create a view that combines patient and doctor information for easy access.
create view Info as
select pa.Name as patientName, UR_num, pa.Phone as patientPhone, doc.Name as DoctorName, doc.ID as DoctorID, doc.Phone as DoctorPhone
from barwon_health.Patient pa, barwon_health.Doctor doc
go

select *
from Info

-- 29. Index: Create an index on the 'phone' column of the Patient table to improve search performance.
create index patient_phone
on barwon_health.Patient(Phone);

-- 30. Backup: Perform a backup of the entire database to ensure data safety.
BACKUP DATABASE task8
TO DISK = 'E:\.Net Eraasoft\SQL Server\SQL\MSSQL16.MSSQLSERVER\MSSQL\Backup\barwon_health.bak'
WITH FORMAT, INIT, NAME = 'Full Backup of barwon_health';
