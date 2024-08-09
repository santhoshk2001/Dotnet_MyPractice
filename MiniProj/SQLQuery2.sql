-- Create the database
CREATE DATABASE RailwayReservationSystem;
GO
USE RailwayReservationSystem;
GO

-- Create Users Table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
);

select * from Users

-- Create Admins Table
CREATE TABLE Admins (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(50) NOT NULL
);

select * from Admins

-- Create Trains Table (with DateOfTravel and TrainStatus)
CREATE TABLE Trains (
    Tno INT NOT NULL,
    Tname NVARCHAR(100) NOT NULL,
    FromStation NVARCHAR(100) NOT NULL,
    DestStation NVARCHAR(100) NOT NULL,
    ClassOfTravel NVARCHAR(50) NOT NULL,
    Price DECIMAL(10, 2) NOT NULL,
    SeatsAvailable INT NOT NULL,
    TrainStatus NVARCHAR(20) NOT NULL CHECK (TrainStatus IN ('Active', 'Inactive')),
    DateOfTravel DATE NOT NULL,
    PRIMARY KEY (Tno, ClassOfTravel, DateOfTravel)
);

select * from Trains

-- Create Bookings Table
CREATE TABLE Bookings (
    BookingId INT IDENTITY(1,1) PRIMARY KEY,
    Tno INT NOT NULL,
    UserId INT NOT NULL,
    ClassOfTravel NVARCHAR(50) NOT NULL,
    NumberOfSeats INT NOT NULL,
    BookingDate DATETIME NOT NULL,
    DateOfTravel DATE NOT NULL,
    TotalPrice DECIMAL(10, 2) NOT NULL,
    BookingStatus NVARCHAR(20) NOT NULL DEFAULT 'Active',
    FOREIGN KEY (Tno, ClassOfTravel, DateOfTravel) REFERENCES Trains(Tno, ClassOfTravel, DateOfTravel),
    FOREIGN KEY (UserId) REFERENCES Users(UserId)
);

select * from Bookings

-- Add constraint to ensure only active trains can be booked
ALTER TABLE Bookings
ADD CONSTRAINT CHK_ActiveTrain CHECK (
    NOT EXISTS (
        SELECT 1 FROM Trains t
        WHERE t.Tno = Bookings.Tno
        AND t.ClassOfTravel = Bookings.ClassOfTravel
        AND t.DateOfTravel = Bookings.DateOfTravel
        AND t.TrainStatus = 'Inactive'
    )
);

-- Create Cancellations Table
CREATE TABLE Cancellations (
    CancellationId INT IDENTITY(1,1) PRIMARY KEY,
    BookingId INT NOT NULL,
    NumberOfSeatsCancelled INT NOT NULL,
    CancellationDate DATETIME NOT NULL,
    FOREIGN KEY (BookingId) REFERENCES Bookings(BookingId)
);

select * from Cancellations

-- Create Refunds Table
CREATE TABLE Refunds (
    RefundId INT IDENTITY(1,1) PRIMARY KEY,
    CancellationId INT NOT NULL,
    RefundAmount DECIMAL(10, 2) NOT NULL,
    RefundDate DATETIME NOT NULL,
    FOREIGN KEY (CancellationId) REFERENCES Cancellations(CancellationId)
);

-- Create WaitingList Table
CREATE TABLE WaitingList (
    WaitingId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    Tno INT NOT NULL,
    ClassOfTravel NVARCHAR(50) NOT NULL,
    NumberOfSeats INT NOT NULL,
    DateOfTravel DATE NOT NULL,
    RequestDate DATETIME NOT NULL,
    FOREIGN KEY (UserId) REFERENCES Users(UserId),
    FOREIGN KEY (Tno, ClassOfTravel, DateOfTravel) REFERENCES Trains(Tno, ClassOfTravel, DateOfTravel)
);