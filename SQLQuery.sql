CREATE DATABASE ListaStudentow;

CREATE TABLE ListaStudentow.dbo.wykladowcy
(
[Pesel] BIGINT PRIMARY KEY,
[Imie] VARCHAR(200),
[Wiek] INT,
[Kierunek] VARCHAR(200)
);

INSERT INTO ListaStudentow.dbo.wykladowcy([Imie], [Wiek], [Pesel], [Kierunek]) VALUES 
('Dimitri', 22, 99080811111, 'Fizyka'),
('Sasha', 25, 99080811112, 'Informatyka'),
('Vasili', 25, 96980811113, 'Chemia'),
('Putin', 25, 96980811114, 'Prawo');

CREATE TABLE ListaStudentow.dbo.studenci
(
[Pesel] BIGINT PRIMARY KEY,
[Imie] VARCHAR(200),
[Wiek] INT,
[AvatarSrc] VARCHAR(200),
[Kierunek] VARCHAR(200)
);

INSERT INTO ListaStudentow.dbo.studenci([Imie], [Wiek], [Pesel], [Kierunek], [AvatarSrc]) VALUES 
('Adam', 22, 99080811111, 'Fizyka', 'src\1.jpg'),
('Jacek', 25, 99080811112, 'Informatyka', 'src\1.jpg'),
('Marek', 25, 96980811113, 'Chemia', 'src\1.jpg'),
('Grzegorz', 25, 96980811114, 'Fizyka', 'src\1.jpg'),
('Kacper', 25, 99080811115, 'Chemia', 'src\1.jpg'),
('Jordan', 25, 99080811116, 'Informatyka', 'src\1.jpg'),
('Samuel', 25, 99080811117, 'Fizyka', 'src\1.jpg'),
('Stefan', 23, 99249873612, 'Prawo', 'src\1.jpg'),
('Daria', 22, 99080811120, 'Fizyka', 'src\1.jpg');

SELECT * FROM ListaStudentow.dbo.studenci;