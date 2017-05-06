INSERT INTO Groups (Title) 
values ('Администратор'), ('Пользователь'), ('Гость');

INSERT INTO Accounts (Name, MiddleName, LastName, Email, Phone, Login, Password, group_Id) 
values ('Андрей', 'Казимирович', 'Макась', 'mak@mail.ru', NULL, 'admin', '111', '1'),
('Олег', 'Игоревич', 'Фролов', 'malic@mail.ru', NULL, 'adm', '111', '1'),
('Игорь', 'Алексеевич', 'Плотников', 'alex@tut.by', NULL, 'zzz', '111', '2'),
('Андрей', 'Олегович', 'Юрков', 'makmen@mail.ru', NULL, 'www', '111', '2');

INSERT INTO Races (Title, Description, Status, Distance, Created, account_Id) 
values ('100 метров', 'Короткий забег на 100 метров с припятствиями', 'wait', 100, '2016-28-11 15:30:00', '1'),
('200 метров', 'Короткий забег на 200 метров с припятствиями', 'wait', 200, '2016-12-11 13:40:00', '1'),
('300 метров', 'Короткий забег на 300 метров с припятствиями', 'wait', 300,  '2016-12-11 12:20:00', '1'),
('400 метров', 'Короткий забег на 400 метров с припятствиями', 'wait', 400,  '2016-22-10 15:50:00', '1'),
('500 метров', 'Короткий забег на 500 метров с припятствиями', 'wait', 500,  '2016-28-11 14:20:00', '1'),
('600 метров', 'Короткий забег на 600 метров с припятствиями', 'wait', 600,  '2016-28-11 19:15:00', '1'),
('800 метров', 'Короткий забег на 700 метров с припятствиями', 'wait', 700,  '2016-28-11 20:20:00', '1'),
('800 метров', 'Короткий забег на 800 метров с припятствиями', 'wait', 800,  '2016-28-11 17:20:00', '1'),
('900 метров', 'Короткий забег на 900 метров с припятствиями', 'wait', 900,  '2016-28-11 17:30:00', '1');

INSERT INTO Participants (Num, Jokey, Trener, Horse, Run, race_Id) 
values ('25632', 'Jim Corner', 'Tony Lorenson', 'Pinto', 0, '2'),
('32645', 'Tony Lorenson', 'Jim Kretow', 'Yakut horse', 0, '3'),
('42126', 'Lucy Partner', 'Vasya Gornov', 'Pinto', 0, '3'),
('23145', 'Alan Liner', 'Omi Kliner', 'Yakut horse', 0, '2'),
('36945', 'Ren Tvenson', 'Tin Ramsor', 'Paso Fino', 0, '2'),
('23945', 'Terry Joran', 'Omi Kliner', 'Marwari', 0, '2'),
('12365', 'Tom Loyer', 'Ali Baron', 'Percheron', 0, '2'),
('32925', 'Kim Dozov', 'Vasya Gornov', 'Haflinger', 0, '3'),
('58643', 'Alex Lima', 'Andrei Zubov', 'Knabstrupper', 0, '3'),
('52345', 'Lin Cu Sa', 'Alina Rebus', 'Rocky Mountain', 0, '3'),
('19126', 'Da Vi Arto', 'Cherry Zens', 'Haflinger', 0, '3'),
('25365', 'Ina Vlasovs', 'Di Meno Cruzo', 'Knabstrupper', 0, '4');