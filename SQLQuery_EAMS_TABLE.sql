CREATE DATABASE EAMS
CREATE TABLE EMP_TABLE
(
	Emp_Id int Primary Key,
	Emp_Name varchar(30) Not Null,
	Emp_Dept varchar(30) Not Null,
	Emp_Salary bigint,
	Emp_Current_Role varchar(30)Not Null,
	Emp_New_Role varchar(30) Not Null,
	Emp_Appraisal_Date Date
)

insert into EMP_TABLE(Emp_Id,Emp_Name,Emp_Dept,Emp_Salary,Emp_Current_Role,Emp_New_Role,Emp_Appraisal_Date)
values(1,'Amar Jagdale','HR',2200.0,'Intern','Junior Associate','2021-9-8')

Select* from EMP_TABLE
