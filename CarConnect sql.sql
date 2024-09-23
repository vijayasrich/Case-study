create database CarConnect
--1.customer table
create table Customer (
CustomerID int identity(1,1) primary key,
FirstName varchar(20) not null,
LastName varchar(20) not null,
Email varchar(25) unique not null,
PhoneNumber varchar(20),
[Address] varchar(30),
Username varchar(10) unique not null,
[Password] varchar(10) not null,
RegistrationDate date not null)

-- 2. vehicle table
create table Vehicle (
VehicleID int identity(1,1) primary key,
Model varchar(10) not null,
Make varchar(10) not null,
[Year] int not null,
Color varchar(10),
RegistrationNumber varchar(20) unique not null,
[Availability] varchar(15) not null,
DailyRate decimal(10, 2) not null)

-- 3. reservation table
create table Reservation (
ReservationID int identity(1,1) primary key,
CustomerID int not null,
VehicleID int not null,
StartDate date not null,
EndDate date not null,
TotalCost decimal(10, 2) not null,
Status varchar(20) not null,
foreign key (CustomerID) references Customer(CustomerID),
foreign key (VehicleID) references Vehicle(VehicleID))

-- 4. admin table
create table [Admin] (
AdminID int identity(1,1) primary key ,
FirstName varchar(10) not null,
LastName varchar(10) not null,
Email varchar(25) unique not null,
PhoneNumber varchar(20),
Username varchar(20) not null,
[Password] varchar(25) not null,
[Role] varchar(30) not null,
JoinDate date not null)

--insert values into Customer
insert into Customer(FirstName, LastName, Email, PhoneNumber, [Address], Username, [Password], RegistrationDate)
values('a','a','aa@gmail.com','1234567891','aa,TN','aa1','aa12','2024-09-01'),
('b','b','bb@gmail.com','1234567892','aa,AP','bb1','bb12','2024-09-02'),
('c','c','cc@gmail.com','1234567893','aa,TS','cc1','cc12','2024-09-03'),
('d','d','dd@gmail.com','1234567894','aa,TN','dd1','dd12','2024-09-04'),
('e','e','ee@gmail.com','1234567895','aa,AP','ee1','ee12','2024-09-05'),
('f','f','ff@gmail.com','1234567896','aa,TS','ff1','ff12','2024-09-06'),
('g','g','gg@gmail.com','1234567897','aa,AP','gg1','gg12','2024-09-07'),
('h','h','hh@gmail.com','1234567898','aa,TN','hh1','hh12','2024-09-08')
select * from Customer
--insert values into vehicle
insert into Vehicle (Model, Make, [Year], Color, RegistrationNumber, [Availability], DailyRate)
values('accord','honda',2021,'black','abc123','yes',50.00),
('camry','toyota',2020,'white','xyz456','no',45.00),
('mustang','ford',2019,'red','mno789','yes',70.00),
('civic','honda',2022,'blue','def234','yes',55.00),
('altima','nissan',2021,'silver','ghi567','no',60.00),
('model s','tesla',2023,'white','jkl890','yes',100.00),
('sorento','kia',2020,'green','stu123','yes',65.00),
('tucson','hyundai',2021,'yellow','vwx456','yes',58.00)
select * from Vehicle
--insert values into reservation
insert into Reservation (CustomerID, VehicleID, StartDate, EndDate, TotalCost, Status)
values(1, 5, '2023-10-01', '2023-10-05', 200.00, 'confirmed'),
(2, 6, '2023-10-03', '2023-10-07', 220.00, 'pending'),
(1, 7, '2023-10-05', '2023-10-10', 250.00, 'confirmed'),
(3, 5, '2023-10-10', '2023-10-15', 180.00, 'cancelled'),
(4, 8, '2023-10-02', '2023-10-06', 260.00, 'confirmed'),
(2, 9, '2023-10-08', '2023-10-12', 240.00, 'pending'),
(5, 10, '2023-10-09', '2023-10-14', 300.00, 'confirmed'),
(6, 11, '2023-10-11', '2023-10-15', 270.00, 'pending')
select * from Reservation
--insert values into admin
insert into [Admin] (FirstName, LastName, Email, PhoneNumber, Username, [Password], [Role], JoinDate)
values('ab', 'ab', 'ab@gmail.com', '1234567891', 'abadmin', 'ab1', 'manager', '2023-09-01'),
('cd', 'cd', 'cd@gmail.com', '1234567892', 'abadmin', 'cd1', 'supervisior', '2023-09-01'),
('ef', 'ef', 'ef@gmail.com', '1234567893', 'abadmin', 'ef1', 'administrator', '2023-09-01'),
('gh', 'gh', 'gh@gmail.com', '1234567894', 'abadmin', 'gh1', 'stafff', '2023-09-01'),
('ij', 'ij', 'ij@gmail.com', '1234567895', 'abadmin', 'ij1', 'manager', '2023-09-01'),
('kl', 'kl', 'kl@gmail.com', '1234567896', 'abadmin', 'kl1', 'supervisior', '2023-09-01'),
('mn', 'mn', 'mn@gmail.com', '1234567897', 'abadmin', 'mn1', 'staff', '2023-09-01'),
('op', 'op', 'op@gmail.com', '1234567898', 'abadmin', 'op1', 'administrator', '2023-09-01')
select * from [Admin]


