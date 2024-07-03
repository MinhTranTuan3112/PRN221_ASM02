use master;
go
if exists (select name
from sys.databases
where name = 'SalesManagementDb')
begin
  drop database SalesManagementDb;
end;
go
create database SalesManagementDb;
go
use SalesManagementDb;
go

create table Member
(
  MemberId int not null,
  Email varchar(100) not null,
  CompanyName varchar(40) not null,
  City varchar(15) not null,
  Country varchar(15) not null,
  [Password] varchar(30) not null,

  primary key (MemberId)
);

create table [Order]
(
  OrderId int not null,
  MemberId int not null,
  OrderDate datetime not null default getdate(),
  RequiredDate datetime null,
  ShippedDate datetime null,
  Freight money null,

  primary key (OrderId),
  foreign key (MemberId) references dbo.Member(MemberId)
);

create table Category
(
  CategoryId int not null,
  CategoryName varchar(100) not null,

  primary key (CategoryId)
);

create table Product
(
  ProductId int not null,
  CategoryId int not null,
  ProductName varchar(40) not null,
  [Weight] varchar(20) not null,
  UnitPrice money not null,
  UnitsInStock int not null,

  primary key (ProductId),
  foreign key (CategoryId) references dbo.Category(CategoryId)
);


create table OrderDetail
(
  OrderId int not null,
  ProductId int not null,
  UnitPrice money not null,
  Quantity int not null,
  Discount float not null,

  primary key (OrderId, ProductId),
  foreign key (OrderId) references dbo.[Order](OrderId),
  foreign key (ProductId) references dbo.Product(ProductId)
);

-- Sample data for Member table
INSERT INTO [dbo].[Member]
  ([MemberId], [Email], [CompanyName], [City], [Country], [Password])
VALUES
  (1, 'minh@gmail.com', 'My Company', 'TPHCM', 'VN', '12345'),
  (2, 'jane.smith@example.com', 'XYZ Inc.', 'London', 'United Kingdom', 'qwerty456'),
  (3, 'mike.johnson@example.com', 'Acme Industries', 'Sydney', 'Australia', 'abc123def'),
  (4, 'sarah.williams@example.com', 'Technology Solutions', 'Berlin', 'Germany', 'password789'),
  (5, 'david.brown@example.com', 'Retail Enterprises', 'Tokyo', 'Japan', 'qwerty12345');

-- Sample data for Order table
INSERT INTO [dbo].[Order]
  ([OrderId], [MemberId], [OrderDate], [RequiredDate], [ShippedDate], [Freight])
VALUES
  (1, 1, '2023-05-01 10:30:00', '2023-05-05 00:00:00', '2023-05-03 15:00:00', 50.00),
  (2, 2, '2023-05-15 14:20:00', '2023-05-20 00:00:00', '2023-05-18 11:30:00', 75.00),
  (3, 3, '2023-06-01 09:00:00', '2023-06-05 00:00:00', '2023-06-03 17:45:00', 35.00),
  (4, 4, '2023-06-10 16:40:00', '2023-06-15 00:00:00', '2023-06-12 13:20:00', 60.00),
  (5, 5, '2023-06-15 11:10:00', '2023-06-20 00:00:00', '2023-06-17 09:00:00', 45.00);

-- Sample data for Category table
INSERT INTO [dbo].[Category]
  ([CategoryId], [CategoryName])
VALUES
  (1, 'Electronics'),
  (2, 'Home Appliances'),
  (3, 'Clothing'),
  (4, 'Books'),
  (5, 'Sports Equipment');

-- Sample data for Product table
INSERT INTO [dbo].[Product]
  ([ProductId], [CategoryId], [ProductName], [Weight], [UnitPrice], [UnitsInStock])
VALUES
  (1, 1, 'Smartphone', '0.2 kg', 599.99, 100),
  (2, 2, 'Refrigerator', '50 kg', 799.99, 50),
  (3, 3, 'Jeans', '1 kg', 49.99, 200),
  (4, 4, 'Novel', '0.5 kg', 19.99, 300),
  (5, 5, 'Treadmill', '80 kg', 1499.99, 20);

-- Sample data for OrderDetail table
INSERT INTO [dbo].[OrderDetail]
  ([OrderId], [ProductId], [UnitPrice], [Quantity], [Discount])
VALUES
  (1, 1, 599.99, 2, 0.1),
  (1, 4, 19.99, 5, 0.05),
  (2, 2, 799.99, 1, 0.0),
  (3, 3, 49.99, 3, 0.2),
  (4, 5, 1499.99, 1, 0.0),
  (5, 1, 599.99, 1, 0.0),
  (5, 4, 19.99, 2, 0.1);