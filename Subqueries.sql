Sub Query--->

Query inside a Query
Query  (Inside Query)


output of inside query----> input to the outer query


32 queries

31-- subqueries

1 outer query

select (6th place) [column/s] [from]---> (1st place)
[wher](2nd place)
group by(3rd place)
having(4th place)
order by(5th place)


select (()()()) 
where(sub1 (sub2)(..)(sub...))

MOSTLY,JOINS ARE FASTER THAN SUBQUERIES.

1) Whenever Joins are used, query optimizer rewrites the joins efficiently.
2)Inner Joins are faster than any other joins.
-----------------------------
1)Non co-related queries are not slower, EXISTS not slower.

2)Slower when writing Corelated subqueries(they have force repeated exection.)
---------------------------------
when to use what?

requirement:
1)fetch related data----Join
2)Existence of data-----Exists(Subquery)
3)Single value data fetch----Scalar Subqueries
4)Too much nested Subqueries are difficult to read,understand and predict,
then use joins

Types of Subqueries:
1) Single Row Subquery:Return single row/value
2) Multiple Row Subquery: Return Mulitple rows
3) Multiple Column Subquery: Return Multiple columns(1 row/M rows)
4) Corelated SubQuery:Depends on the outer query
5) Non CoRelated Subquery:Independent
6) Scalar : Single Value
7) Subquery in from Clause(Derived Tables) : Subquery in the from
8) Exists: Fast check on existence(bool)
9) Not Exists Subquery: Anti-join
10)Nested SubQueries: Multiple Levels
------------------------------------------------------------

create table SimpleEmp
(
Empno int identity(1,1) primary key,
Empname varchar(20),
Sal numeric(10,2)
)

insert into SimpleEmp values('Ankit',10000),
('Sanket',20000),('Mrunal',30000),
('Nikitha',50000),('Natasha',40000)

select * from SimpleEmp




---Find the employees who have salaries greater than average salary
1) Find average salry
select avg(sal) from simpleemp (30000)
2) find the salary > 1

---Single row subquery
select * from Simpleemp
where sal>	(select avg(sal) from simpleemp)

---------------------------------------------
---List the products who are either in categoryid =1 or catgoryid=2
select * from products
where categoryid =1 or categoryid=2

select * from products
where categoryid in(1,2)
--------------------------------------------
----Multiple Row SubQuery===in, any ,all---Single column Multiple Rows

---List Products along with supplierid, and the suppliers are in 'germany'
select Supplierid from suppliers 
where country='Germany'

select * from products
where supplierid in(select Supplierid from suppliers 
where country='Germany')

select * from products 
where supplierid=11 or supplierid=12 or supplierid=13

---ANY
----find the product/s whose price > than any product in categoryid=1
1) find the price of products in categoryid=1

select unitprice from products
where categoryid=1

(Range--->4.5 to 263.5)
Any----> price greater than lowest(min)

2) compare the other products price from any category with the 1st category

select * from products 
where unitprice > any (select unitprice from products
where categoryid=1)
order by unitprice



select * from products 
where unitprice >  (select min(unitprice) from products
where categoryid=1)
order by unitprice

----find the product/s whose price > than all product in categoryid=1
> all--> max
select * from products 
where unitprice > all (select unitprice from products
where categoryid=2)
order by unitprice


select * from products 
where unitprice >  (select max(unitprice) from products
where categoryid=2)
order by unitprice


select max(unitprice) from products

 Multiple Columns---One Row

 --Find the orders with same customerid and employeeid as order id =10250
 select * from orders 
 where customerid=
 (select customerid from orders
 where orderid=10250) 
 and employeeid=
 (select employeeid from orders
 where orderid=10250)


 select * from orders
 where customerid='HANAR' and employeeid=4

-----LIST ALL THE EMPLOYEES IF AND ONLY IF THERE ARE ANY(MINIMUM ONE) EMPLOYEES 
----WHO IS/ARE FROM INDIA.

select * from employees
where exists(select * from employees where Country like 'India')


select * from employees
where exists(select 1 from employees where Country like 'USA')

-----Find the customers who have placed atleast one order

select * from customers
91

2----not placed orders even once

select distinct(customerid) from orders
89

select customerid from orders 
order by customerid

select * from customers c
where exists(select 1 from orders o
where c.customerid=o.customerid)

--stop searching for the same customerid which is atleast found once
--makes it very efficient

select customerid,count(customerid) ordercount from orders
group by customerid
having count(customerid)>2


---Listing the customer details who have placed orders >2 times


select customerid,companyname from customers c
where customerid in
(select customerid as ordercount from orders o
group by o.customerid
having count(o.customerid)=6)

----print average price per category
select categoryid,avg(unitprice) as avgprice from products
group by categoryid


select t.categoryid,avg(t.unitprice) as avgprice from 
(select categoryid, unitprice from  products) t
group by t.categoryid
----------------------------------

----customerid with ordercount
select customerid,count(orderid) orderCount from orders
group by customerid


select t.customerid,t.ordercount from
(select customerid,count(orderid) orderCount from orders
group by customerid) t
---------------------------------
select * from [order details]

select orderid,productid,unitprice,quantity,unitprice*quantity as amt,discount 
from [order details]


select orderTotal.orderid,orderTotal.totalOrderValue from
(select orderid,sum(unitprice*quantity) totalOrderValue from [order details]
group by orderid) as orderTotal

----max value of order
select max(ordertotal.totalOrderValue) from
(select sum(unitprice*quantity) totalOrderValue from [order details]
group by orderid) as orderTotal



select 168+98+174=440


----not placed the order even once
select * from customers c
where not exists(select 1 from orders o
where c.customerid=o.customerid)

select * from orders
where customerid in ('FISSA','PARIS')


select * from orders 
where customerid='ALFKI'  1,3,4,6---empid

select customerid,count(customerid) ordercount,employeeid,orderid from  orders
where customerid='ALFKI'
group by employeeid,customerid,orderid


---display count of orders employeeid wise
select count(customerid) ordercount,employeeid from  orders
group by employeeid

---display count of orders employeeid wise, and only display the employees who have brought more then 50 orders(Having is used when you wnat to filter/condition on aggregate function)
select count(customerid) ordercount,employeeid from  orders
group by employeeid
having count(customerid)>50


select count(customerid) from orders


select * from categories

select * from products

---count products/productid by cateogry(catid)
---categorywise count of products
select count(productid),categoryid from products
group by categoryid

----Count of employees deptwise
select count(empid),deptno from emp
group by deptno

---------------------------------------------------
---Co-related

---Find the products whose unitprice is greater than the average unitprice in their own category
select avg(unitprice),categoryid from products
group by categoryid
37.9791	1
23.0625	2
25.16	3
28.73	4
20.25	5
54.0066	6
32.37	7
20.6825	8

select p.productid,p.unitprice,p.categoryid from products p
where p.unitprice >(select avg(p1.unitprice)  from products p1
where p.categoryid=p1.categoryid)
order by p.categoryid

------Scalar Subquery--returns only one value
--Show each product with its average price in its category
select p2.productname,p2.categoryid,p2.unitprice,
(select avg(p.unitprice) from products p
where p2.categoryid=p.categoryid) avgprice
from products p2











