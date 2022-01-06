DROP DATABASE JobBoardDemo;
CREATE DATABASE IF NOT EXISTS JobBoardDemo;
USE JobBoardDemo;
CREATE TABLE IF NOT EXISTS Job(
  ProductId INT AUTO_INCREMENT primary key,
  Title nvarchar(100) NOT NULL ,
  SubTitle nvarchar(100),
  MinPay double,
  MaxPay double,
  Description nvarchar (1000)
);
CREATE TABLE IF NOT EXISTS Applicant(
  Id INT AUTO_INCREMENT primary key,
  Name nvarchar (100),
  WageExpectation double
);
CREATE TABLE IF NOT EXISTS JobApplicant(
    Id INT AUTO_INCREMENT primary key,
    Applicant Int NOT NULL ,
    Job Int NOT NULL,
    FOREIGN KEY (Applicant) REFERENCES Applicant(Id),
    FOREIGN KEY (Job) REFERENCES Job(ProductId)
);
