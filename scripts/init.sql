CREATE DATABASE DiscountDb;

\c DiscountDb

CREATE TABLE Coupon (
	Id SERIAL PRIMARY KEY,
	ProductName VARCHAR(24) NOT NULL,
	Description TEXT,
	Amount INT
);

INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Notebook', 'Material', 5);
INSERT INTO Coupon (ProductName, Description, Amount) VALUES ('Pen', 'Material 2', 8);