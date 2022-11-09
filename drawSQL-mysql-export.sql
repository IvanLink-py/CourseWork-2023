CREATE TABLE `Voice Model`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `SecondPerSymbol` DOUBLE NOT NULL,
    `SecondPerWord` DOUBLE NOT NULL,
    `SecondPerComa` DOUBLE NOT NULL,
    `SecondPerDot` DOUBLE NOT NULL,
    `SecondPerDigit` DOUBLE NOT NULL,
    `SecondPerHyphen` DOUBLE NOT NULL
);

ALTER TABLE
    `Voice Model`
ADD
    PRIMARY KEY `voice model_id_primary`(`id`);

CREATE TABLE `Person`(
    `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `FirstName` VARCHAR(255) NOT NULL,
    `SecondName` VARCHAR(255) NOT NULL,
    `LastName` VARCHAR(255) NULL,
    `Post` VARCHAR(255) NOT NULL,
    `VoiceModel` INT NULL
);

ALTER TABLE
    `Person`
ADD
    PRIMARY KEY `person_id_primary`(`ID`);

CREATE TABLE `Post`(`Name` VARCHAR(255) NOT NULL);

ALTER TABLE
    `Post`
ADD
    PRIMARY KEY `post_name_primary`(`Name`);

CREATE TABLE `Script`(
    `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Duration` TIME NOT NULL,
    `Author` INT NOT NULL
);

ALTER TABLE
    `Script`
ADD
    PRIMARY KEY `script_id_primary`(`ID`);

CREATE TABLE `Project`(
    `ID` INT NOT NULL,
    `Name` VARCHAR(255) NOT NULL,
    `Description` TEXT NOT NULL,
    `Script` INT NOT NULL,
    `Type` ENUM('') NOT NULL,
    `Path` TEXT NOT NULL
);

ALTER TABLE
    `Project`
ADD
    PRIMARY KEY `project_id_primary`(`ID`);

CREATE TABLE `Material`(
    `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Filming` INT NULL,
    `Name` VARCHAR(255) NOT NULL,
    `Description` TEXT NULL,
    `Path` TEXT NOT NULL,
    `Duration` TIME NULL,
    `Type` ENUM('') NOT NULL,
    `Size` DOUBLE NOT NULL,
    `Camera` INT NULL
);

ALTER TABLE
    `Material`
ADD
    PRIMARY KEY `material_id_primary`(`ID`);

CREATE TABLE `Project Import`(
    `Project_ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Material_ID` INT NOT NULL
);

ALTER TABLE
    `Project Import`
ADD
    PRIMARY KEY `project import_project_id_primary`(`Project_ID`);

ALTER TABLE
    `Project Import`
ADD
    PRIMARY KEY `project import_material_id_primary`(`Material_ID`);

CREATE TABLE `Filming`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Description` TEXT NULL,
    `Address` TEXT NULL,
    `Time` DATETIME NULL,
    `Status` ENUM('') NOT NULL
);

ALTER TABLE
    `Filming`
ADD
    PRIMARY KEY `filming_id_primary`(`id`);

CREATE TABLE `Tag`(
    `ID` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Parrent` INT NULL
);

ALTER TABLE
    `Tag`
ADD
    PRIMARY KEY `tag_id_primary`(`ID`);

CREATE TABLE `Material tag`(
    `Material` INT UNSIGNED NOT NULL,
    `tag` INT UNSIGNED NOT NULL
);

ALTER TABLE
    `Material tag`
ADD
    PRIMARY KEY `material tag_material_primary`(`Material`);

ALTER TABLE
    `Material tag`
ADD
    PRIMARY KEY `material tag_tag_primary`(`tag`);

CREATE TABLE `Filming tag`(
    `Filming` INT UNSIGNED NOT NULL,
    `tag` INT NOT NULL
);

ALTER TABLE
    `Filming tag`
ADD
    PRIMARY KEY `filming tag_filming_primary`(`Filming`);

ALTER TABLE
    `Filming tag`
ADD
    PRIMARY KEY `filming tag_tag_primary`(`tag`);

CREATE TABLE `Project tag`(
    `Project` INT UNSIGNED NOT NULL,
    `tag` INT UNSIGNED NOT NULL
);

ALTER TABLE
    `Project tag`
ADD
    PRIMARY KEY `project tag_project_primary`(`Project`);

ALTER TABLE
    `Project tag`
ADD
    PRIMARY KEY `project tag_tag_primary`(`tag`);

CREATE TABLE `Filming crew`(
    `Filming` INT NOT NULL,
    `Person` INT NOT NULL,
    `Role` ENUM('') NOT NULL
);

ALTER TABLE
    `Filming crew`
ADD
    PRIMARY KEY `filming crew_filming_primary`(`Filming`);

ALTER TABLE
    `Filming crew`
ADD
    PRIMARY KEY `filming crew_person_primary`(`Person`);

ALTER TABLE
    `Filming crew`
ADD
    PRIMARY KEY `filming crew_role_primary`(`Role`);

CREATE TABLE `Camera`(
    `id` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Name` VARCHAR(255) NOT NULL,
    `Model` VARCHAR(255) NOT NULL
);

ALTER TABLE
    `Camera`
ADD
    PRIMARY KEY `camera_id_primary`(`id`);

CREATE TABLE `Project crew`(
    `Project` INT UNSIGNED NOT NULL AUTO_INCREMENT,
    `Person` INT UNSIGNED NOT NULL,
    `Role` ENUM('') NOT NULL
);

ALTER TABLE
    `Project crew`
ADD
    PRIMARY KEY `project crew_project_primary`(`Project`);

ALTER TABLE
    `Project crew`
ADD
    PRIMARY KEY `project crew_person_primary`(`Person`);

ALTER TABLE
    `Project crew`
ADD
    PRIMARY KEY `project crew_role_primary`(`Role`);

ALTER TABLE
    `Person`
ADD
    CONSTRAINT `person_voicemodel_foreign` FOREIGN KEY(`VoiceModel`) REFERENCES `Voice Model`(`id`);

ALTER TABLE
    `Person`
ADD
    CONSTRAINT `person_post_foreign` FOREIGN KEY(`Post`) REFERENCES `Post`(`Name`);

ALTER TABLE
    `Project`
ADD
    CONSTRAINT `project_script_foreign` FOREIGN KEY(`Script`) REFERENCES `Script`(`ID`);

ALTER TABLE
    `Script`
ADD
    CONSTRAINT `script_author_foreign` FOREIGN KEY(`Author`) REFERENCES `Person`(`ID`);

ALTER TABLE
    `Tag`
ADD
    CONSTRAINT `tag_parrent_foreign` FOREIGN KEY(`Parrent`) REFERENCES `Tag`(`ID`);

ALTER TABLE
    `Material`
ADD
    CONSTRAINT `material_filming_foreign` FOREIGN KEY(`Filming`) REFERENCES `Filming`(`id`);

ALTER TABLE
    `Material`
ADD
    CONSTRAINT `material_camera_foreign` FOREIGN KEY(`Camera`) REFERENCES `Camera`(`id`);