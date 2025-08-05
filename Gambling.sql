CREATE DATABASE Gambling;
USE Gambling;

Drop database Gambling; 

CREATE TABLE Client(
    ClientID int IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Name nvarchar(50) NOT NULL,
    Surname nvarchar(50) NOT NULL,
    ClientBalance decimal(18,2) NOT NULL
);

CREATE TABLE TransactionType(
    TransactionTypeID smallint IDENTITY(1,1) PRIMARY KEY NOT NULL, 
    TransactionTypeName nvarchar(50) NOT NULL
);

CREATE TABLE Transactions(
    TransactionID bigint IDENTITY(1,1) PRIMARY KEY NOT NULL,
    Amount decimal(18,2) NOT NULL,
    TransactionTypeID smallint NOT NULL,
    ClientID int NOT NULL,
    Comment nvarchar(100) NULL,
    FOREIGN KEY (ClientID) REFERENCES Client(ClientID),
    FOREIGN KEY (TransactionTypeID) REFERENCES TransactionType(TransactionTypeID)
);


INSERT INTO TransactionType (TransactionTypeName) 
VALUES ('Debit'),
       ('Credit');


INSERT INTO Client (Name, Surname, ClientBalance) 
VALUES ('Peter', 'Parker',1000.00),     
       ('Tony', 'Stark', 800000.00),     
       ('Bruce', 'Banner', 254111.00);  


INSERT INTO Transactions (Amount, TransactionTypeID, ClientID, Comment) 
VALUES (1000.00, 1, 1, 'Winnings'),      
       (-500.00, 2, 3, 'Losing'),         
       (-9000.00, 2, 2, 'Losing');
	  

