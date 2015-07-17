-- Create tables in SQL server
use Supermarkets
go

create table Vendors (
Id int primary key identity,
Name nvarchar(100) not null
)
go

create table Measures (
Id int primary key identity,
Name nvarchar(50) not null
)

create table Categories (
Id int primary key identity,
Name nvarchar(100) not null,
ParentCategoryId int null,
constraint FK_Id_ParentCategoryId foreign key(ParentCategoryId) references  Categories(Id)
)
go

create table Products (
Id int primary key identity,
VendorId int not null,
Name nvarchar(100) not null,
CategoryId int not null,
MeasureId int not null,
Price money not null,
constraint FK_VendorId_Vendors foreign key(VendorId) references Vendors(Id),
constraint FK_MeasureId_Measures foreign key(MeasureId) references Measures(Id),
constraint FK_CategoryId_Categories_Id foreign key (CategoryId) references Categories(Id)
)
go

create table Shops (
Id int primary key identity,
Name nvarchar(100) not null
)
go

create table Sales (
Id int primary key identity,
ProductId int not null,
ShopId int not null,
Quantity int not null,
UnitPrice money not null,
constraint FK_ProductId_Products_Id foreign key(ProductId) references Products(Id),
constraint FK_ShopId_Shops_Id foreign key (ShopId) references Shops (Id)
)
go

create table Expenses (
Id int primary key identity,
VendorId int not null,
Ammount money not null,
[Date] date not null,
constraint FK_VendorId_Vendors_Id foreign key (VendorId) references Vendors(Id)
)
go