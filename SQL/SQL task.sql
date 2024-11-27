Create database Sqltask;
use Sqltask;

create table Project(
	Project_name varchar(20) primary key,
    Employee_Id numeric);

insert into Project values('Banking',101),('Insurence',102),('Government',102),('Amazon',103),
('Flipcart',104),('Meshow',103);


create table Employee(
	Employee_name varchar(20),
    Employee_Id  numeric(10) primary key,
    Department_Id numeric(10) ,
    Employee_salary decimal(10,2),
    Date_of_joining date );
    

    create table Department(
Depatment_name varchar(20),
Department_Id numeric(10) primary key);   
alter table Department add column Department_location varchar(30);
update Department set Department_location='New york' where Department_Id=1;
update Department set Department_location='Chennai' where Department_Id=2;

insert into Department value('Finance',3,'Pondycherry');
select * from Department;
    /* Inserting the data into the table */    
    insert into Employee values('Saravanan',107,3,25000.00,'2023-11-21',102),
		('Sanker',102,1,25000.00,'2024-11-20'),('Subu',103,2,26000.00,'2024-11-21'),
        ('Praveen',104,1,25000.00,'2023-11-21');
    update Employee set Employee_salary= 70000 where Employee_Id=104;
	alter table Employee add column Manager_Id numeric;
    update Employee set Manager_Id = 104 where Employee_Id = 101;
      update Employee set Manager_Id = 103 where Employee_Id = 102;
        update Employee set Manager_Id = 102 where Employee_Id = 103;
          update Employee set Manager_Id = 101 where Employee_Id = 104;
    
                  /* Basic Queries */
                  
/* 1.Write a query to display all rows and columns from the employees table */

Select * from Employee;

/* Retrieve only the name and salary of all employees from the employees table */

select  Employee_name , Employee_salary  from Employee;

/* Write a query to find all employees whose salary is greater than 50,000. */

Select Employee_Id, Employee_name, Employee_salary from Employee  where Employee_salary > 50000;

/* List all employees who joined the company in the year 2020. */

select * from Employee where year(Date_of_joining)=2024;

/* Retrieve the details of employees whose names start with the letter 'A'. */

select * from Employee where Employee_name like 'P%';

				/* Aggregate Functions */
                
/* Write a query to calculate the average salary of all employees. */

       Select Avg(Employee_salary)  from Employee;   
       
 /* Find the total number of employees in the company. */    
 
 select count(Employee_Id) from Employee;
 
 /* Write a query to find the highest salary in the employees table.*/
 
 select * from Employee where Employee_salary = (select max(Employee_salary) from Employee);
 
 /*Calculate the total salary paid by the company for all employees.*/
 
 select sum(Employee_salary) from Employee;
 
 /* Find the count of employees in each department. */
 
 select Department_Id, count(*) as Employee_count from Employee group by Department_Id;
                
                         /*Joins*/
                         
 /* Write a query to retrieve employee names along with their
  department names (using employees and departments tables).*/
   
   Select Employee.Employee_name,Department.Depatment_name From Employee join Department
   on Employee.Department_Id = Department.Department_Id;
   
/* List all employees who have a manager (self-join on employees table).*/

select Employee1.Employee_name,Employee2.Employee_name as Manager_name  from Employee Employee1 
join Employee Employee2 on Employee1.Employee_Id=Employee2.Manager_Id ;

/* Find the names of employees who are working on multiple
 projects (using employees and projects tables). */
 
select Employee.Employee_name,Project.Project_name from Employee join Project
on Employee.Employee_Id=Project.Employee_Id;

/*Find the names of employees who are working on multiple projects 
(using employees and projects tables).*/

select Employee.Employee_name from Employee join Project on Employee.Employee_Id=Project.Employee_Id
 group by Employee.Employee_name having count(Project.Employee_Id)>1;

/*Retrieve the names of employees who do not belong to any department.*/     

select Employee_name from Employee where Department_Id is null;

                          /*Subqueries

 Write a query to find the employees with the second-highest salary. */
 
 select max(Employee_salary) as Second_highest_salary
 from Employee where Employee_salary < (select max(Employee_salary) from Employee);
          
/*Retrieve the names of employees whose salary is above the department average salary.*/

select Employee.Employee_name from Employee join Department on
 Employee.Department_Id=Department.Department_Id where Employee.Employee_salary 
 >( select avg(E2.Employee_salary) from Employee E2 where E2.department_Id=Employee.Department_Id);

/*  Find employees who earn more than the average salary of the entire company.*/

select Employee_name,Employee_salary from Employee where
 Employee_salary >(select avg(Employee_salary) from Employee);
 
 /*Write a query to find the department with the highest number of employees.*/
 
 SELECT Depatment_name
FROM Department
WHERE Department_Id = (
    SELECT Department_Id
    FROM Employee
    GROUP BY Department_Id
    ORDER BY COUNT(Employee_Id) DESC
    LIMIT 1
);

/*List all employees who work in a department located in 'New York'.*/

SELECT Employee_name, Employee_Id FROM Employee WHERE Department_Id = (
    SELECT Department_Id FROM Department
    WHERE Department_location = 'New York'
);
                                     /*Set Operators*/

/* Write a query to find employees who work in either the 'HR' or 'Finance' department.*/

select Employee_name, Employee_Id
from Employee
join Department ON Employee.Department_Id = Department.Department_Id
where Department.Depatment_name = 'ES'
union
select Employee_name, Employee_Id
from Employee
join Department ON Employee.Department_Id = Department.Department_Id
where Department.Depatment_name = 'Finance';



/* Retrieve the names of employees who are working on both Project A and Project B.*/

select * from Employee
join Project ON Employee.Employee_Id = Project.Employee_Id
where Project.Project_name = 'Insurence' union
select * from Employee
join Project ON Employee.Employee_Id = Project.Employee_Id
where Project.Project_name = 'Flipcart';

select * from Project;

/* Find employees who are not assigned to any project*/

Select E.Employee_name From Employee E Where E.Employee_Id not in (Select P.Employee_Id From Project P);


/*  Write a query to get all unique job titles across all departments.*/

select distinct D.Depatment_name from Department D;

/*  Combine two tables (employees and former_employees) and remove duplicates.*/

 create table Former_Employee(
	Employee_name varchar(20),
    Employee_Id  numeric(10) primary key,
    Department_Id numeric(10) ,
    Employee_salary decimal(10,2),
    Date_of_joining date,
    Manager_Id numeric);
    drop table Former_Employee;
    
  insert into Former_Employee values('Saran',105,null,25000.00,'2023-11-21',102),
		('Sanker',102,1,25000.00,'2024-11-20',null),('Subu',103,2,26000.00,'2024-11-21',null),
        ('Praveen',104,1,25000.00,'2023-11-21',null);
        
SELECT * FROM Employee UNION SELECT * FROM Former_Employee;

                      /* DML and DDL */

/* Write a query to add a new employee to the employees table.*/

insert into Employee values ('John Doe', 108, 3, 50000.00, '2024-11-25', 104);

/* Update the salary of all employees in the 'IT' department by 10%.*/

update Employee set Employee_salary = Employee_salary * 1.10 where Department_Id = (
    select Department_Id from Department where Depatment_name = 'ES');
    
/* Delete all employees who have not worked for more than 5 years.*/

delete from Employee where Date_of_joining > current_date - interval 5 year;

/* Create a new table departments_backup with the same structure as the departments table.*/

create table Department_backup as select * from Department where 1=0;
select * from Department_backup;

/*. Drop the temporary_data table from the database.*/

drop table Departments_backup;

									/*. Constraints*/

/*. Add a primary key to the employees table. */

/*. Write a query to create a foreign key between employees and departments tables.*/

/*. Add a unique constraint to the email column in the employees table.*/

/*. Write a query to check all constraints applied on the employees table.*/

/*.Remove the NOT NULL constraint from the phone_number column in the employees table.*/
 
 

