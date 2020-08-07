CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(95) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
);

CREATE TABLE `Awards` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Company` longtext CHARACTER SET utf8mb4 NULL,
    `Year` int NOT NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Awards` PRIMARY KEY (`Id`)
);

CREATE TABLE `Educations` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `SchoolName` longtext CHARACTER SET utf8mb4 NULL,
    `DateRange` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Educations` PRIMARY KEY (`Id`)
);

CREATE TABLE `FAQs` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_FAQs` PRIMARY KEY (`Id`)
);

CREATE TABLE `FeatureSkills` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Icons` longtext CHARACTER SET utf8mb4 NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Body` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_FeatureSkills` PRIMARY KEY (`Id`)
);

CREATE TABLE `Interests` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Interests` PRIMARY KEY (`Id`)
);

CREATE TABLE `Langagues` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Langagues` PRIMARY KEY (`Id`)
);

CREATE TABLE `Messages` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    `ServiceType` longtext CHARACTER SET utf8mb4 NULL,
    `Text` longtext CHARACTER SET utf8mb4 NULL,
    `isRead` tinyint(1) NOT NULL,
    CONSTRAINT `PK_Messages` PRIMARY KEY (`Id`)
);

CREATE TABLE `Projects` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Resume` longtext CHARACTER SET utf8mb4 NULL,
    `Type` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Projects` PRIMARY KEY (`Id`)
);

CREATE TABLE `Requirements` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Requirements` PRIMARY KEY (`Id`)
);

CREATE TABLE `Reviews` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `Company` longtext CHARACTER SET utf8mb4 NULL,
    `Body` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    `Status` longtext CHARACTER SET utf8mb4 NULL,
    `Url` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Reviews` PRIMARY KEY (`Id`)
);

CREATE TABLE `Skills` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Skills` PRIMARY KEY (`Id`)
);

CREATE TABLE `Subscribers` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Email` longtext CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_Subscribers` PRIMARY KEY (`Id`)
);

CREATE TABLE `Tags` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Tags` PRIMARY KEY (`Id`)
);

CREATE TABLE `Users` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Username` longtext CHARACTER SET utf8mb4 NULL,
    `CustomUserTitle` longtext CHARACTER SET utf8mb4 NULL,
    `FullName` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `Location` longtext CHARACTER SET utf8mb4 NULL,
    `PasswordHash` longblob NULL,
    `PasswordSalt` longblob NULL,
    `Created` datetime(6) NOT NULL,
    `LastActive` datetime(6) NOT NULL,
    `Phone` longtext CHARACTER SET utf8mb4 NULL,
    `ShortResume` longtext CHARACTER SET utf8mb4 NULL,
    `FullResume` longtext CHARACTER SET utf8mb4 NULL,
    `GithubUrl` longtext CHARACTER SET utf8mb4 NULL,
    `TwiterProfile` longtext CHARACTER SET utf8mb4 NULL,
    `FacebookProfile` longtext CHARACTER SET utf8mb4 NULL,
    `LinkedInProfile` longtext CHARACTER SET utf8mb4 NULL,
    `StackOverflowProfile` longtext CHARACTER SET utf8mb4 NULL,
    `RedditProfile` longtext CHARACTER SET utf8mb4 NULL,
    `CodepenProfile` longtext CHARACTER SET utf8mb4 NULL,
    `ServiceAndPricingTable` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Users` PRIMARY KEY (`Id`)
);

CREATE TABLE `WorkExperiences` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NULL,
    `Resume` longtext CHARACTER SET utf8mb4 NULL,
    `Company` longtext CHARACTER SET utf8mb4 NULL,
    `DateRange` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_WorkExperiences` PRIMARY KEY (`Id`)
);

CREATE TABLE `Photos` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Url` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `DateAdded` datetime(6) NOT NULL,
    `IsMain` tinyint(1) NOT NULL,
    `UserId` char(36) NOT NULL,
    `PublicId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_Photos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Photos_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Posts` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Title` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Description` longtext CHARACTER SET utf8mb4 NOT NULL,
    `Text` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CreatedAt` datetime(6) NOT NULL,
    `PublishedAt` datetime(6) NULL,
    `ReadingTime` int NOT NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Posts` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Posts_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Products` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Name` longtext CHARACTER SET utf8mb4 NULL,
    `ProjectIntro` longtext CHARACTER SET utf8mb4 NULL,
    `ShortResume` longtext CHARACTER SET utf8mb4 NULL,
    `ClientName` longtext CHARACTER SET utf8mb4 NULL,
    `BodyText` longtext CHARACTER SET utf8mb4 NULL,
    `Industry` longtext CHARACTER SET utf8mb4 NULL,
    `Type` longtext CHARACTER SET utf8mb4 NULL,
    `Url` longtext CHARACTER SET utf8mb4 NULL,
    `ProductPhoto` longtext CHARACTER SET utf8mb4 NULL,
    `ProductDescription` longtext CHARACTER SET utf8mb4 NULL,
    `Size` int NOT NULL,
    `UserId` char(36) NOT NULL,
    CONSTRAINT `PK_Products` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Products_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `Comments` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Body` longtext CHARACTER SET utf8mb4 NOT NULL,
    `CommenterName` longtext CHARACTER SET utf8mb4 NULL,
    `Email` longtext CHARACTER SET utf8mb4 NULL,
    `Published` tinyint(1) NOT NULL,
    `PostId` char(36) NOT NULL,
    CONSTRAINT `PK_Comments` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Comments_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `Posts` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `PostPhotos` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Url` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `DateAdded` datetime(6) NOT NULL,
    `IsMain` tinyint(1) NOT NULL,
    `PostId` char(36) NOT NULL,
    `PublicId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_PostPhotos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_PostPhotos_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `Posts` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `PostTags` (
    `PostId` char(36) NOT NULL,
    `TagId` char(36) NOT NULL,
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    CONSTRAINT `PK_PostTags` PRIMARY KEY (`PostId`, `TagId`),
    CONSTRAINT `FK_PostTags_Posts_PostId` FOREIGN KEY (`PostId`) REFERENCES `Posts` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_PostTags_Tags_TagId` FOREIGN KEY (`TagId`) REFERENCES `Tags` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ProductPhotos` (
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    `Url` longtext CHARACTER SET utf8mb4 NULL,
    `Description` longtext CHARACTER SET utf8mb4 NULL,
    `DateAdded` datetime(6) NOT NULL,
    `IsMain` tinyint(1) NOT NULL,
    `ProductId` char(36) NOT NULL,
    `PublicId` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_ProductPhotos` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_ProductPhotos_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`Id`) ON DELETE CASCADE
);

CREATE TABLE `ProductRequirements` (
    `ProductId` char(36) NOT NULL,
    `RequirementId` char(36) NOT NULL,
    `Id` char(36) NOT NULL,
    `DateCreated` datetime(6) NOT NULL,
    `DateModified` datetime(6) NOT NULL,
    CONSTRAINT `PK_ProductRequirements` PRIMARY KEY (`ProductId`, `RequirementId`),
    CONSTRAINT `FK_ProductRequirements_Products_ProductId` FOREIGN KEY (`ProductId`) REFERENCES `Products` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_ProductRequirements_Requirements_RequirementId` FOREIGN KEY (`RequirementId`) REFERENCES `Requirements` (`Id`) ON DELETE CASCADE
);

CREATE INDEX `IX_Comments_PostId` ON `Comments` (`PostId`);

CREATE INDEX `IX_Photos_UserId` ON `Photos` (`UserId`);

CREATE INDEX `IX_PostPhotos_PostId` ON `PostPhotos` (`PostId`);

CREATE INDEX `IX_Posts_UserId` ON `Posts` (`UserId`);

CREATE INDEX `IX_PostTags_TagId` ON `PostTags` (`TagId`);

CREATE INDEX `IX_ProductPhotos_ProductId` ON `ProductPhotos` (`ProductId`);

CREATE INDEX `IX_ProductRequirements_RequirementId` ON `ProductRequirements` (`RequirementId`);

CREATE INDEX `IX_Products_UserId` ON `Products` (`UserId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20200806123141_Initial', '3.0.0');