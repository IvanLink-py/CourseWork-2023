--@block Должности
INSERT INTO
    post
VALUES
    ('Редактор'),
    ('Корреспондент'),
    ('Видео инженер'),
    ('Оператор'),
    ('Водитель');

--@block
SELECT
    *
FROM
    Post;

--@block Людишки
DELETE FROM person;
INSERT INTO person (
        FirstName,
        SecondName,
        LastName,
        Post)
VALUES 
    ('Даниил',      'Алексеев',   'Глебович',        'Редактор'),
    ('Злата',       'Сафонова',   'Григорьевна',     'Корреспондент'),
    ('Мирон',       'Степанов',   'Сергеевич',       'Корреспондент'),
    ('Олег',        'Соколов',    'Тимофеевич',      'Видео инженер'),
    ('София',       'Румянцева',  'Станиславовна',   'Видео инженер'),
    ('Михаил',      'Тихонов',    'Арсентьевич',     'Оператор'),
    ('Святослав',   'Поздняков',  'Максимович',      'Оператор'),
    ('Роман',       'Казанцев',   'Арсентьевич',     'Водитель');

--@block
SELECT *
FROM person;

--@block Основные теги
delete FROM tag order by ID desc;
INSERT INTO tag (Name, Parrent)
VALUES 
    ('Администрация', NULL),
    ('Образование', NULL);

--@block Вторичные теги
INSERT INTO
    tag (Name, Parrent)
    (SELECT 'Дума',        ID FROM Tag WHERE Name = 'Администрация') union 
    (SELECT 'Оперштаб',    ID FROM Tag WHERE Name = 'Администрация');

INSERT INTO
    tag (Name, Parrent)
    (SELECT 'Детский сад', ID FROM Tag WHERE Name = 'Образование') union 
    (SELECT 'Школа',       ID FROM Tag WHERE Name = 'Образование');

--@block
SELECT
    name
FROM
    Tag;

--@block
SELECT
    name
FROM
    Tag
    WHERE Parrent is NULL;

--@block
SELECT
    name
FROM
    Tag
WHERE
    Parrent IN (
        SELECT
            id
        FROM
            tag
        WHERE
            name = 'Образование'
    );

--@block
DELETE FROM
    `Filming crew`;

DELETE FROM
    `Filming tag`;

DELETE FROM
    Material
WHERE
    Filming IS NOT NULL;

DELETE FROM
    filming;

--@block Съёмки
INSERT INTO filming (Name, Description, Address, Time, Status)
VALUES (
    'Оперштаб 10-11-22',
    'Оперштаб 10-11-22',
    'г. Ирбит, ул. Орджоникидзе, 30',
    now(),
    'planned'
  );

INSERT INTO filming (Name, Description, Address, Time, Status)
VALUES (
    'Дума 04-11-22',
    'Дума 04-11-22',
    'г. Ирбит, ул. Орджоникидзе, 30',
    now(),
    'finished'
  );

INSERT INTO filming (Name, Description, Address, Time, Status)
VALUES (
    'Чистка памятников в школьном музее',
    'Ученики школы № 10 решили почистить памятники в школьном музее',
    'г. Ирбит, ул. Максима Горького, 3',
    now(),
    'finished'
  );

INSERT INTO filming (Name, Description, Address, Time, Status)
VALUES (
    'Ремонт детского сада № 3',
    NULL,
    'г. Ирбит, ул. Мальгина, 54',
    now(),
    'in work'
  );


--@block Добавление водителя всем съёмкам
SELECT Id FROM person WHERE Post = 'Водитель' LIMIT 1 into @driver;
SELECT @driver;

INSERT INTO `filming crew` (Filming, Person, Role)
SELECT id, @driver, 'driver' FROM filming;


--@block Вывод всех съёмок с водителем
SELECT
    filming.name,
    filming.status,
    person.SecondName,
    `filming crew`.role
FROM
    filming,
    `filming crew`,
    person
WHERE
    filming.id = `filming crew`.filming
    AND `filming crew`.Person = person.id 
    AND `filming crew`.role = 'driver';