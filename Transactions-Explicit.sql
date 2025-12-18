Transaction Control:
It ensures that a group of SQL Statements executes as  a "ONE LOGICAL UNIT OF WORK".ie. All succeed or All Fail.

A transaction can be one statement/multiple statements and they follow
ACID properties.

Transaction Control Statments:
Explicit Tansactions:
1) Begin Transaction:Starts the transaction
2)Commit Transaction: Saves Transaction
3) Rollback Transaction:Undo Changes
4) Save Transaction: Creates a Savepoint from which u can commit  or rollback


Begin transaction
insert into Customers (CustomerID,CompanyName)
values ('TEST1','TEST')
update Customers
set CompanyName='Newname'
where Customerid='TEST1'
Commit Transaction


Begin transaction
insert into Customers (CustomerID,CompanyName)
values ('TEST2','Test2 Data')
Commit Transaction


Begin transaction
delete from Customers 
where Customerid='TEST2'
Rollback transaction

select * from Customers
where Customerid='TEST2'

begin try
Begin transaction
delete from Customers 
where Customerid='TEST2'
commit transaction
end try
begin catch
rollback transaction
print 'tran failed'
end catch

begin transaction
insert into Customers (CustomerID,CompanyName)
values ('TEST2','Test2 Data')
save transaction s1
insert into Customers (CustomerID,CompanyName)
values ('TEST3','Test3')
rollback transaction s1
commit transaction

//test2 customerid will be save,
 //test3 customerid will be rollback


 select * from Customers
 where customerid='TEST3'

 //default commit/auto commit

 insert into Customers (CustomerID,CompanyName)
values ('TEST3','Test3')

-----------------------------------
Isolation Levels: