INSERT INTO Groups (Title) 
values ('�������������'), ('������������'), ('�����');

INSERT INTO Accounts (Name, MiddleName, LastName, Email, Phone, Login, Password, group_Id) 
values ('������', '�����������', '������', 'mak@mail.ru', NULL, 'admin', '111', '1'),
('����', '��������', '������', 'malic@mail.ru', NULL, 'adm', '111', '1'),
('�����', '����������', '���������', 'alex@tut.by', NULL, 'zzz', '111', '2'),
('������', '��������', '�����', 'makmen@mail.ru', NULL, 'www', '111', '2');

INSERT INTO Races (Title, Description, Status, Distance, Created, account_Id) 
values ('100 ������', '�������� ����� �� 100 ������ � �������������', 'wait', 100, '2016-28-11 15:30:00', '1'),
('200 ������', '�������� ����� �� 200 ������ � �������������', 'wait', 200, '2016-12-11 13:40:00', '1'),
('300 ������', '�������� ����� �� 300 ������ � �������������', 'wait', 300,  '2016-12-11 12:20:00', '1'),
('400 ������', '�������� ����� �� 400 ������ � �������������', 'wait', 400,  '2016-22-10 15:50:00', '1'),
('500 ������', '�������� ����� �� 500 ������ � �������������', 'wait', 500,  '2016-28-11 14:20:00', '1'),
('600 ������', '�������� ����� �� 600 ������ � �������������', 'wait', 600,  '2016-28-11 19:15:00', '1'),
('800 ������', '�������� ����� �� 700 ������ � �������������', 'wait', 700,  '2016-28-11 20:20:00', '1'),
('800 ������', '�������� ����� �� 800 ������ � �������������', 'wait', 800,  '2016-28-11 17:20:00', '1'),
('900 ������', '�������� ����� �� 900 ������ � �������������', 'wait', 900,  '2016-28-11 17:30:00', '1');

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