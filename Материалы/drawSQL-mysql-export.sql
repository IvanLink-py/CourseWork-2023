DROP DATABASE IF EXISTS NewsFactory;
CREATE DATABASE NewsFactory ;
USE NewsFactory;

--@block
DROP TABLE IF EXISTS `Voice Model`;
CREATE TABLE `Voice Model`(
    `id` INT PRIMARY KEY AUTO_INCREMENT,
    `SecondPerSymbol` DOUBLE NOT NULL,
    `SecondPerWord` DOUBLE NOT NULL,
    `SecondPerComa` DOUBLE NOT NULL,
    `SecondPerDot` DOUBLE NOT NULL,
    `SecondPerDigit` DOUBLE NOT NULL,
    `SecondPerHyphen` DOUBLE NOT NULL
);


--@block
DROP TABLE IF EXISTS `Person`;
CREATE TABLE `Person`(
    `ID` INT PRIMARY KEY AUTO_INCREMENT,
    `FirstName` VARCHAR(255) NOT NULL,
    `SecondName` VARCHAR(255) NOT NULL,
    `LastName` VARCHAR(255) NULL,
    `Post` VARCHAR(255) NOT NULL,
    `VoiceModel` INT NULL
);

--@block
DROP TABLE IF EXISTS `Post`;
CREATE TABLE `Post`(`Name` VARCHAR(255) PRIMARY KEY);

--@block
DROP TABLE IF EXISTS `Script`;
CREATE TABLE `Script`(
    `ID` INT PRIMARY KEY AUTO_INCREMENT,
    `Duration` TIME NOT NULL,
    `Author` INT NOT NULL
);

--@block
DROP TABLE IF EXISTS `Project`;
CREATE TABLE `Project`(
    `ID` INT PRIMARY KEY,
    `Name` VARCHAR(255) NOT NULL,
    `Description` TEXT NOT NULL,
    `Script` INT NOT NULL,
    `Type` ENUM('efir', 'report', 'advertising', 'film', 'other') NOT NULL,
    `Path` TEXT NOT NULL
);

--@block
DROP TABLE IF EXISTS `Material`;
CREATE TABLE `Material`(
    `ID` INT PRIMARY KEY AUTO_INCREMENT,
    `Filming` INT NULL,
    `Name` VARCHAR(255) NOT NULL,
    `Description` TEXT NULL,
    `Path` TEXT NOT NULL,
    `Duration` TIME NULL,
    `Type` ENUM('pictures', 'interview', 'combined') NULL,
    `Size` DOUBLE NOT NULL,
    `Camera` INT NULL
);


--@block
DROP TABLE IF EXISTS `Project Import`;
CREATE TABLE `Project Import`(
    `Project_ID` INT NOT NULL,
    `Material_ID` INT NOT NULL,
    PRIMARY KEY (`Project_ID`, `Material_ID`)
);


--@block
DROP TABLE IF EXISTS `Filming`;
CREATE TABLE `Filming`(
    `id` INT PRIMARY KEY AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Description` TEXT NULL,
    `Address` TEXT NULL,
    `Time` DATETIME NULL,
    `Status` ENUM('planned', 'canceled', 'in work', 'finished', 'other') NOT NULL
);


--@block
DROP TABLE IF EXISTS `Tag`;
CREATE TABLE `Tag`(
    `ID` INT PRIMARY KEY AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Parrent` INT NULL
);


--@block
DROP TABLE IF EXISTS `Material tag`;
CREATE TABLE `Material tag`(
    `Material` INT NOT NULL,
    `tag` INT NOT NULL,
    PRIMARY KEY(`Material`, `tag`)
);


--@block
DROP TABLE IF EXISTS `Filming tag`;
CREATE TABLE `Filming tag`(
    `Filming` INT NOT NULL,
    `tag` INT NOT NULL,
    PRIMARY KEY(`Filming`, `tag`)
);


--@block
DROP TABLE IF EXISTS `Project tag`;
CREATE TABLE `Project tag`(
    `Project` INT NOT NULL,
    `tag` INT NOT NULL,
    PRIMARY KEY(`Project`, `tag`)
);

--@block
DROP TABLE IF EXISTS `Filming crew`;
CREATE TABLE `Filming crew`(
    `Filming` INT NOT NULL,
    `Person` INT NOT NULL,
    `Role` ENUM('operator', 'reporter', 'driver', 'other') NOT NULL,
    PRIMARY KEY(`Filming`, `Person`, `Role`)
);


--@block
DROP TABLE IF EXISTS `Camera`;
CREATE TABLE `Camera`(
    `id` INT PRIMARY KEY AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Model` VARCHAR(255) NOT NULL
);

--@block
DROP TABLE IF EXISTS `Project crew`;
CREATE TABLE `Project crew`(
    `Project` INT NOT NULL AUTO_INCREMENT,
    `Person` INT NOT NULL,
    `Role` ENUM('author', 'editor', 'video engineer', 'other') NOT NULL,
    PRIMARY KEY(`Project`, `Person`, `Role`)
);
--@block
ALTER TABLE
    `Person`
ADD
    CONSTRAINT `person_voicemodel_foreign` FOREIGN KEY(`VoiceModel`) REFERENCES `Voice Model`(`id`);

--@block
ALTER TABLE
    `Person`
ADD
    CONSTRAINT `person_post_foreign` FOREIGN KEY(`Post`) REFERENCES `Post`(`Name`);

--@block
ALTER TABLE
    `Project`
ADD
    CONSTRAINT `project_script_foreign` FOREIGN KEY(`Script`) REFERENCES `Script`(`ID`);

--@block
ALTER TABLE
    `Script`
ADD
    CONSTRAINT `script_author_foreign` FOREIGN KEY(`Author`) REFERENCES `Person`(`ID`);

--@block
ALTER TABLE
    `Tag`
ADD
    CONSTRAINT `tag_parrent_foreign` FOREIGN KEY(`Parrent`) REFERENCES `Tag`(`ID`);

--@block
ALTER TABLE
    `Material`
ADD
    CONSTRAINT `material_filming_foreign` FOREIGN KEY(`Filming`) REFERENCES `Filming`(`id`);

--@block
ALTER TABLE
    `Material`
ADD
    CONSTRAINT `material_camera_foreign` FOREIGN KEY(`Camera`) REFERENCES `Camera`(`id`);