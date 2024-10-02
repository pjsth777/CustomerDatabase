

-- Create Database
CREATE DATABASE CustomerDatabase;
USE CustomerDatabase;


-- Create Customer Table
CREATE TABLE Customer (
	CustomerID INT PRIMARY KEY IDENTITY(1,1),
	Name NVARCHAR(100),
	Email NVARCHAR(100),
	Phone NVARCHAR(15)
);


-- Create Location Table
CREATE TABLE Location (
	LocationID INT PRIMARY KEY IDENTITY(1,1),
	CustomerID INT FOREIGN KEY REFERENCES Customer(CustomerID),
	Address NVARCHAR(255)
);


-- Insert Dummy Data
INSERT INTO Customer (Name, Email, Phone) VALUES
('Ram Bahadur', 'ram.bahadur@example.com', '9801234567'),
('Sita Kumari', 'sita.kumari@example.com', '9802345678'),
('Gopal Shrestha', 'gopal.shrestha@example.com', '9803456789'),
('Laxmi Adhikari', 'laxmi.adhikari@example.com', '9804567890'),
('Bikram Gurung', 'bikram.gurung@example.com', '9805678901'),
('Sarita Lama', 'sarita.lama@example.com', '9806789012'),
('Kishor Khadka', 'kishor.khadka@example.com', '9807890123'),
('Puja Raut', 'puja.raut@example.com', '9808901234'),
('Rajesh Thapa', 'rajesh.thapa@example.com', '9809012345'),
('Anita Sharma', 'anita.sharma@example.com', '9810123456'),
('Suman Rai', 'suman.rai@example.com', '9811234567'),
('Manju Karki', 'manju.karki@example.com', '9812345678'),
('Kiran Basnet', 'kiran.basnet@example.com', '9813456789'),
('Mina Magar', 'mina.magar@example.com', '9814567890'),
('Arjun Poudel', 'arjun.poudel@example.com', '9815678901'),
('Nisha Joshi', 'nisha.joshi@example.com', '9816789012'),
('Deepak Maharjan', 'deepak.maharjan@example.com', '9817890123'),
('Sangita Tamang', 'sangita.tamang@example.com', '9818901234'),
('Prakash Sapkota', 'prakash.sapkota@example.com', '9819012345'),
('Rita KC', 'rita.kc@example.com', '9820123456');


INSERT INTO Location (CustomerID, Address) VALUES
(1, 'Kalanki, Kathmandu'),
(2, 'Baneshwor, Kathmandu'),
(3, 'Kumaripati, Lalitpur'),
(4, 'Pulchowk, Lalitpur'),
(5, 'Chabahil, Kathmandu'),
(6, 'Birauta, Pokhara'),
(7, 'Mahendranagar, Kanchanpur'),
(8, 'Dharan, Sunsari'),
(9, 'Itahari, Sunsari'),
(10, 'Butwal, Rupandehi'),
(11, 'Narayanghat, Chitwan'),
(12, 'Boudha, Kathmandu'),
(13, 'Koteshwor, Kathmandu'),
(14, 'Satdobato, Lalitpur'),
(15, 'Bhaktapur Durbar Square, Bhaktapur'),
(16, 'Dhulikhel, Kavre'),
(17, 'Suryabinayak, Bhaktapur'),
(18, 'Lakeside, Pokhara'),
(19, 'Biratnagar, Morang'),
(20, 'Dhangadhi, Kailali'),
(1, 'Swayambhu, Kathmandu'),
(2, 'New Road, Kathmandu'),
(3, 'Patan Durbar Square, Lalitpur'),
(4, 'Jhamsikhel, Lalitpur'),
(5, 'Kalimati, Kathmandu');




