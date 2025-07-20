CREATE DATABASE MVC_NIIE_BookManagement
GO

USE MVC_NIIE_BookManagement;
GO
-- Create Author table
CREATE TABLE Author (
    AuthorId INT PRIMARY KEY IDENTITY(1,1),
    Name VARCHAR(100) NOT NULL,
    Bio VARCHAR(500)
);


-- Create Book table
CREATE TABLE Book (
    BookId INT PRIMARY KEY IDENTITY(1,1),
    Title VARCHAR(200) NOT NULL,
    Description VARCHAR(1000),
    CoverImagePath VARCHAR(255),
    AuthorId INT NOT NULL,
    FOREIGN KEY (AuthorId) REFERENCES Author(AuthorId)
);


SET IDENTITY_INSERT Author ON
GO
-- Insert 5 real authors (your teammates)
INSERT INTO Author (AuthorId, Name, Bio) VALUES
(1, 'Jane Austen', 'English novelist known for her six major novels.'),
(2, 'George Orwell', 'English novelist, essayist, journalist and critic.'),
(3, 'J.K. Rowling', 'British author, best known for the Harry Potter series.'),
(4, 'Mark Twain', 'American writer, humorist, entrepreneur, publisher, and lecturer.'),
(5, 'Haruki Murakami', 'Japanese writer known for his blend of pop culture and magical realism.');
SET IDENTITY_INSERT Author OFF
GO


SET IDENTITY_INSERT Book ON
GO
-- Insert 5 books
INSERT INTO Book (BookId, Title, Description, CoverImagePath, AuthorId) VALUES
(1, 'Pride and Prejudice', 'A romantic novel of manners.', '/images/covers/pride.jpg', 1),
(2, '1984', 'A dystopian social science fiction novel.', '/images/covers/1984.jpg', 2),
(3, 'Harry Potter and the Sorcerer''s Stone', 'The first novel in the Harry Potter series.', '/images/covers/hp1.jpg', 3),
(4, 'Adventures of Huckleberry Finn', 'A novel about the adventures of a young boy and a runaway slave.', '/images/covers/huckfinn.jpg', 4),
(5, 'Norwegian Wood', 'A nostalgic story of loss and sexuality.', '/images/covers/norwegianwood.jpg', 5);
SET IDENTITY_INSERT Book OFF
GO

