CREATE TABLE `auditlog` (
  `Id` char(36) NOT NULL,
  `EntityName` varchar(100) NOT NULL,
  `ChangeType` varchar(20) NOT NULL,
  `ChangedBy` varchar(100) DEFAULT NULL,
  `Timestamp` datetime NOT NULL,
  `Description` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;


