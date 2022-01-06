CREATE DATABASE IF NOT EXISTS JobBoardDemo;
USE JobBoardDemo;
CREATE TABLE IF NOT EXISTS Job(
    Id INT primary key,
    Title nvarchar(100) NOT NULL ,
    SubTitle nvarchar(100),
    MinPay int,
    MaxPay int,
    description nvarchar (1000)
    );

CREATE TABLE IF NOT EXISTS Applicant(
    Id INT primary key,
    Name nvarchar (100)
    );

CREATE TABLE IF NOT EXISTS JobApplicant(
   Id INT primary key,
   Applicant Int NOT NULL ,
   Job Int NOT NULL,
   FOREIGN KEY (Applicant) REFERENCES Applicant(Id),
   FOREIGN KEY (Job) REFERENCES Job(Id)

    );
