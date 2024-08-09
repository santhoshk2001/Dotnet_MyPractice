CREATE DATABASE RailwayDB;

USE RailwayDB;


-- Create Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);


select * from Users

-- Create Admins Table
CREATE TABLE Admins (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(50) NOT NULL
);


select * from Admins

-- Create Trains Table
CREATE TABLE Trains (
    Tno INT NOT NULL,
    ClassOfTravel NVARCHAR(50) NOT NULL,
    Tname NVARCHAR(100) NOT NULL,
    FromStation NVARCHAR(100) NOT NULL,
    DestStation NVARCHAR(100) NOT NULL,
    DateOfTravel DATE NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    SeatsAvailable INT NOT NULL,
    TrainStatus NVARCHAR(20) NOT NULL,
    PRIMARY KEY (Tno, ClassOfTravel, DateOfTravel)
);


select * from Trains

-- Create Bookings Table
CREATE TABLE Bookings (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    Tno INT NOT NULL,
    ClassOfTravel NVARCHAR(50) NOT NULL,
    UserId INT NOT NULL,
    NumberOfSeats INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    DateOfTravel DATE NOT NULL,
    FOREIGN KEY (Tno, ClassOfTravel, DateOfTravel) REFERENCES Trains(Tno, ClassOfTravel, DateOfTravel),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);


select * from Bookings

-- Create Cancellations Table
CREATE TABLE Cancellations (
    CancellationId INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL,
    NumberOfSeatsCancelled INT NOT NULL,
    CancellationDate DATETIME NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
);


select * from Users
select * from Admins
select * from Trains
select * from Bookings
select * from Cancellations