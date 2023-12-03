CREATE DATABASE  IF NOT EXISTS `ezrental` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `ezrental`;
-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: localhost    Database: ezrental
-- ------------------------------------------------------
-- Server version	8.0.34

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20231011065825_Inital Create','7.0.11');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `adfacility`
--

DROP TABLE IF EXISTS `adfacility`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `adfacility` (
  `AdFacilityId` int NOT NULL AUTO_INCREMENT,
  `FacilityId` int NOT NULL,
  `AdId` int NOT NULL,
  PRIMARY KEY (`AdFacilityId`),
  KEY `fk_AdId` (`AdId`),
  KEY `fk_FacilityId` (`FacilityId`),
  CONSTRAINT `fk_AdId` FOREIGN KEY (`AdId`) REFERENCES `advertisement` (`AdId`),
  CONSTRAINT `fk_FacilityId` FOREIGN KEY (`FacilityId`) REFERENCES `facilities` (`FacilityId`)
) ENGINE=InnoDB AUTO_INCREMENT=20 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `adfacility`
--

LOCK TABLES `adfacility` WRITE;
/*!40000 ALTER TABLE `adfacility` DISABLE KEYS */;
INSERT INTO `adfacility` VALUES (1,1,1),(2,2,2),(3,3,3),(4,4,4),(5,5,5),(6,6,6),(7,7,7),(8,8,8);
/*!40000 ALTER TABLE `adfacility` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `advertisement`
--

DROP TABLE IF EXISTS `advertisement`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `advertisement` (
  `AdId` int NOT NULL AUTO_INCREMENT,
  `RentId` int NOT NULL,
  `Price` decimal(18,2) NOT NULL,
  `Description` varchar(200) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `StartDate` date NOT NULL,
  `EndDate` date NOT NULL,
  `Area` varchar(50) NOT NULL DEFAULT 'default_area',
  `Country` varchar(50) NOT NULL DEFAULT 'default_country',
  `City` varchar(50) NOT NULL DEFAULT 'default_city',
  PRIMARY KEY (`AdId`),
  KEY `fk_rent_advertisement` (`RentId`),
  CONSTRAINT `fk_rent_advertisement` FOREIGN KEY (`RentId`) REFERENCES `rent` (`RentId`)
) ENGINE=InnoDB AUTO_INCREMENT=57 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `advertisement`
--

LOCK TABLES `advertisement` WRITE;
/*!40000 ALTER TABLE `advertisement` DISABLE KEYS */;
INSERT INTO `advertisement` VALUES (1,1,65.39,'Nam congue, risus semper porta volutpat, quam pede lobortis ligula, sit amet eleifend pede libero quis orci.','2020-08-28','2023-06-07','default_area','default_country','default_city'),(2,2,288.68,'Vivamus vestibulum sagittis sapien.','2020-04-25','2023-03-23','default_area','default_country','default_city'),(3,3,262.41,'Curabitur in libero ut massa volutpat convallis.','2020-03-23','2023-10-08','default_area','default_country','default_city'),(4,4,150.59,'Nullam orci pede, venenatis non, sodales sed, tincidunt eu, felis.','2020-08-13','2023-06-07','default_area','default_country','default_city'),(5,5,168.29,'Ut tellus.','2020-01-03','2023-04-20','default_area','default_country','default_city'),(6,6,56.70,'In est risus, auctor sed, tristique in, tempus sit amet, sem.','2020-10-11','2023-09-16','default_area','default_country','default_city'),(7,7,159.19,'Morbi non lectus.','2020-06-28','2023-02-03','default_area','default_country','default_city'),(8,8,79.94,'Proin leo odio, porttitor id, consequat in, consequat ut, nulla.','2020-07-14','2023-08-12','default_area','default_country','default_city');
/*!40000 ALTER TABLE `advertisement` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `area`
--

DROP TABLE IF EXISTS `area`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `area` (
  `AreaId` int NOT NULL AUTO_INCREMENT,
  `AreaName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CityId` int NOT NULL,
  PRIMARY KEY (`AreaId`),
  KEY `fk_CityId` (`CityId`),
  CONSTRAINT `fk_CityId` FOREIGN KEY (`CityId`) REFERENCES `city` (`CityId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `area`
--

LOCK TABLES `area` WRITE;
/*!40000 ALTER TABLE `area` DISABLE KEYS */;
INSERT INTO `area` VALUES (1,'Alfama',1),(2,'Kowloon',2),(3,'Manhattan',3),(4,'Garden',4),(5,'Bastille',5);
/*!40000 ALTER TABLE `area` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `city`
--

DROP TABLE IF EXISTS `city`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `city` (
  `CityId` int NOT NULL AUTO_INCREMENT,
  `CityName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `CountryId` int NOT NULL,
  PRIMARY KEY (`CityId`),
  KEY `fk_CountryId` (`CountryId`),
  CONSTRAINT `fk_CountryId` FOREIGN KEY (`CountryId`) REFERENCES `country` (`CountryId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `city`
--

LOCK TABLES `city` WRITE;
/*!40000 ALTER TABLE `city` DISABLE KEYS */;
INSERT INTO `city` VALUES (1,'Lisbon',1),(2,'Hong Kong',8),(3,'New York',26),(4,'Karachi',27),(5,'Paris',9);
/*!40000 ALTER TABLE `city` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `country`
--

DROP TABLE IF EXISTS `country`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `country` (
  `CountryId` int NOT NULL AUTO_INCREMENT,
  `CountryName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`CountryId`)
) ENGINE=InnoDB AUTO_INCREMENT=41 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `country`
--

LOCK TABLES `country` WRITE;
/*!40000 ALTER TABLE `country` DISABLE KEYS */;
INSERT INTO `country` VALUES (1,'Portugal'),(2,'Peru'),(3,'Philippines'),(4,'Palestinian Territory'),(5,'Indonesia'),(6,'Ukraine'),(7,'Madagascar'),(8,'China'),(9,'France'),(10,'Czech Republic'),(11,'Panama'),(12,'Kazakhstan'),(13,'Japan'),(14,'Greece'),(15,'Sweden'),(16,'Guatemala'),(17,'Ecuador'),(18,'Venezuela'),(19,'Croatia'),(20,'Tajikistan'),(21,'Thailand'),(22,'Argentina'),(23,'Afghanistan'),(24,'Russia'),(25,'Hungary'),(26,'United States'),(27,'Pakistan'),(28,'Micronesia'),(29,'Brazil'),(30,'Nigeria'),(31,'Poland'),(32,'Serbia'),(33,'Myanmar'),(34,'Vietnam'),(35,'Laos'),(36,'Costa Rica'),(37,'Iran'),(38,'Syria'),(39,'Finland'),(40,'Uruguay');
/*!40000 ALTER TABLE `country` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `credentials`
--

DROP TABLE IF EXISTS `credentials`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `credentials` (
  `CredentialId` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Salt` varchar(80) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`CredentialId`),
  UNIQUE KEY `Username_UNIQUE` (`Username`),
  KEY `fk_UserId` (`UserId`),
  CONSTRAINT `fk_UserId` FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `credentials`
--

LOCK TABLES `credentials` WRITE;
/*!40000 ALTER TABLE `credentials` DISABLE KEYS */;
INSERT INTO `credentials` VALUES (1,53,'faizan','C4JTKPgpNNm8Fn/uVKD+ABNOh2nRofnag1IifyWOOAc=','fRRmf6iuF8X/rrW+LtzCHw=='),(2,54,'aahil','eyGA7WnCifaNZNACKEanDZ5BL4+Y8BmrXDfMAptcAL8=','hPT4O6dqYosaP+5NoAqHKA=='),(3,55,'umair','q8qlyKw2M9djN0D9iOLWh/7gyPb8H9veWYL4QaxjgwI=','kKpLWNFB3uoKb7tH+SCI3w==');
/*!40000 ALTER TABLE `credentials` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `facilities`
--

DROP TABLE IF EXISTS `facilities`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `facilities` (
  `FacilityId` int NOT NULL AUTO_INCREMENT,
  `FacilityName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`FacilityId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `facilities`
--

LOCK TABLES `facilities` WRITE;
/*!40000 ALTER TABLE `facilities` DISABLE KEYS */;
INSERT INTO `facilities` VALUES (1,'YIM Hostel'),(2,'Oxotel'),(3,'The Yard Hostel'),(4,'Caveland Hostel'),(5,'CODE Hostel'),(6,'Palmers Lodge Hostels'),(7,'Gili Mansion'),(8,'Fauna Luxury Hostel'),(9,'Mountain Hostel Tarter'),(10,'Swanky Mint Hostel');
/*!40000 ALTER TABLE `facilities` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `proposal`
--

DROP TABLE IF EXISTS `proposal`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `proposal` (
  `ProposalId` int NOT NULL AUTO_INCREMENT,
  `AdId` int NOT NULL,
  `RenteeId` int NOT NULL,
  `UserBackground` varchar(300) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Status` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`ProposalId`),
  KEY `fk_Proposal_AdId` (`AdId`),
  CONSTRAINT `fk_Proposal_AdId` FOREIGN KEY (`AdId`) REFERENCES `advertisement` (`AdId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `proposal`
--

LOCK TABLES `proposal` WRITE;
/*!40000 ALTER TABLE `proposal` DISABLE KEYS */;
/*!40000 ALTER TABLE `proposal` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `rent`
--

DROP TABLE IF EXISTS `rent`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `rent` (
  `RentId` int NOT NULL AUTO_INCREMENT,
  `RoomId` int NOT NULL,
  `RenterId` int NOT NULL,
  PRIMARY KEY (`RentId`),
  KEY `fk_rent_RoomId` (`RoomId`),
  CONSTRAINT `fk_rent_RoomId` FOREIGN KEY (`RoomId`) REFERENCES `room` (`RoomId`)
) ENGINE=InnoDB AUTO_INCREMENT=19 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `rent`
--

LOCK TABLES `rent` WRITE;
/*!40000 ALTER TABLE `rent` DISABLE KEYS */;
INSERT INTO `rent` VALUES (1,6,7),(2,1,2),(3,4,3),(4,4,1),(5,6,2),(6,3,1),(7,3,8),(8,4,1);
/*!40000 ALTER TABLE `rent` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `room`
--

DROP TABLE IF EXISTS `room`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `room` (
  `RoomId` int NOT NULL AUTO_INCREMENT,
  `Type` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `NumBeds` int NOT NULL,
  `SqArea` decimal(18,2) NOT NULL,
  `PicturePath` varchar(100) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  PRIMARY KEY (`RoomId`)
) ENGINE=InnoDB AUTO_INCREMENT=59 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `room`
--

LOCK TABLES `room` WRITE;
/*!40000 ALTER TABLE `room` DISABLE KEYS */;
INSERT INTO `room` VALUES (1,'Shared',3,116.60,'http://dummyimage.com/162x223.png/5fa2dd/ffffff'),(2,'Shared',1,182.49,'http://dummyimage.com/183x113.png/dddddd/000000'),(3,'Private',2,224.98,'http://dummyimage.com/242x110.png/5fa2dd/ffffff'),(4,'Private',2,462.08,'http://dummyimage.com/149x191.png/dddddd/000000'),(5,'Shared',3,289.50,'http://dummyimage.com/212x189.png/cc0000/ffffff'),(6,'Private',1,406.69,'http://dummyimage.com/229x151.png/dddddd/000000'),(8,'Shared',2,221.78,'http://dummyimage.com/244x222.png/5fa2dd/ffffff'),(9,'Private',2,201.21,'http://dummyimage.com/132x222.png/5fa2dd/ffffff'),(10,'Shared',1,205.72,'http://dummyimage.com/161x233.png/dddddd/000000'),(11,'Shared',3,473.43,'http://dummyimage.com/201x171.png/5fa2dd/ffffff'),(12,'Private',2,177.88,'http://dummyimage.com/178x108.png/dddddd/000000'),(13,'Private',3,347.48,'http://dummyimage.com/226x124.png/cc0000/ffffff'),(14,'Shared',1,405.06,'http://dummyimage.com/190x236.png/ff4444/ffffff'),(15,'Shared',2,255.57,'http://dummyimage.com/210x184.png/dddddd/000000'),(16,'Private',3,349.34,'http://dummyimage.com/211x229.png/ff4444/ffffff'),(17,'Private',3,497.87,'http://dummyimage.com/145x207.png/dddddd/000000'),(18,'Shared',3,182.39,'http://dummyimage.com/163x225.png/dddddd/000000'),(19,'Shared',1,202.26,'http://dummyimage.com/178x178.png/5fa2dd/ffffff'),(20,'Private',3,165.79,'http://dummyimage.com/199x238.png/ff4444/ffffff'),(21,'Private',3,238.53,'http://dummyimage.com/244x114.png/5fa2dd/ffffff'),(22,'Shared',3,176.80,'http://dummyimage.com/188x215.png/ff4444/ffffff'),(23,'Shared',1,291.15,'http://dummyimage.com/151x248.png/ff4444/ffffff'),(24,'Private',1,476.56,'http://dummyimage.com/144x223.png/cc0000/ffffff'),(25,'Private',3,425.09,'http://dummyimage.com/115x216.png/dddddd/000000'),(26,'Private',3,210.50,'http://dummyimage.com/123x173.png/5fa2dd/ffffff'),(27,'Private',1,459.43,'http://dummyimage.com/214x212.png/dddddd/000000'),(28,'Private',2,445.90,'http://dummyimage.com/241x208.png/ff4444/ffffff'),(29,'Private',2,491.61,'http://dummyimage.com/109x127.png/ff4444/ffffff'),(30,'Private',1,294.58,'http://dummyimage.com/109x183.png/cc0000/ffffff'),(31,'Shared',1,248.50,'http://dummyimage.com/152x183.png/cc0000/ffffff'),(32,'Private',1,456.60,'http://dummyimage.com/196x129.png/dddddd/000000'),(33,'Shared',3,432.45,'http://dummyimage.com/238x221.png/cc0000/ffffff'),(34,'Private',1,116.19,'http://dummyimage.com/161x238.png/ff4444/ffffff'),(35,'Private',1,121.80,'http://dummyimage.com/103x186.png/5fa2dd/ffffff'),(36,'Shared',3,153.78,'http://dummyimage.com/152x107.png/5fa2dd/ffffff'),(37,'Shared',3,351.57,'http://dummyimage.com/222x206.png/dddddd/000000'),(38,'Private',2,450.55,'http://dummyimage.com/117x226.png/dddddd/000000'),(39,'Private',1,335.85,'http://dummyimage.com/161x153.png/ff4444/ffffff'),(40,'Private',1,379.76,'http://dummyimage.com/127x249.png/ff4444/ffffff'),(41,'Private',3,380.72,'http://dummyimage.com/238x161.png/5fa2dd/ffffff'),(42,'Private',2,311.97,'http://dummyimage.com/162x174.png/5fa2dd/ffffff'),(43,'Shared',3,417.78,'http://dummyimage.com/204x100.png/dddddd/000000'),(44,'Shared',2,409.83,'http://dummyimage.com/242x202.png/cc0000/ffffff'),(45,'Private',3,379.44,'http://dummyimage.com/234x165.png/cc0000/ffffff'),(46,'Private',2,428.25,'http://dummyimage.com/171x129.png/5fa2dd/ffffff'),(47,'Private',3,153.39,'http://dummyimage.com/215x222.png/cc0000/ffffff'),(48,'Private',2,129.40,'http://dummyimage.com/219x192.png/dddddd/000000'),(49,'Private',3,296.58,'http://dummyimage.com/170x140.png/dddddd/000000'),(50,'Shared',1,368.85,'http://dummyimage.com/161x106.png/5fa2dd/ffffff'),(52,'Shared',1,116.60,'http://dummyimages.com/162x223.png/5fa2dd/ffffff');
/*!40000 ALTER TABLE `room` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `transaction`
--

DROP TABLE IF EXISTS `transaction`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `transaction` (
  `TransactionId` int NOT NULL AUTO_INCREMENT,
  `AdId` int NOT NULL,
  `ProposalId` int NOT NULL,
  PRIMARY KEY (`TransactionId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `transaction`
--

LOCK TABLES `transaction` WRITE;
/*!40000 ALTER TABLE `transaction` DISABLE KEYS */;
/*!40000 ALTER TABLE `transaction` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `user`
--

DROP TABLE IF EXISTS `user`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `user` (
  `UserId` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `LastName` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci DEFAULT NULL,
  `Address` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `PhoneNumber` varchar(20) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `Email` varchar(50) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  PRIMARY KEY (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=56 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `user`
--

LOCK TABLES `user` WRITE;
/*!40000 ALTER TABLE `user` DISABLE KEYS */;
INSERT INTO `user` VALUES (1,'Lina','Rimell','7509 Kingsford Trail','+51 (376) 182-3660','k200290@nu.edu.pk'),(2,'Alida','Forge','835 Meadow Valley Point','+7 (836) 121-0689','aforge1@ox.ac.uk'),(3,'Vite','Pee','5583 Michigan Avenue','+506 (135) 746-1794','vpee2@google.nl'),(4,'Sutherland','Eakly','8 Darwin Point','+506 (197) 246-7373','seakly3@flickr.com'),(5,'Tamarra','Learmonth','1 Rutledge Drive','+351 (840) 277-6897','tlearmonth4@exblog.jp'),(6,'Robinson','Parsand','9248 South Park','+63 (812) 521-8451','rparsand5@arizona.edu'),(7,'Kerwin','Chomiszewski','8655 John Wall Terrace','+86 (594) 365-4159','kchomiszewski6@ustream.tv'),(8,'Cyrus','Bickersteth','43 Butterfield Circle','+81 (623) 688-4099','cbickersteth7@hao123.com'),(9,'Sydney','Claybourn','68099 Havey Lane','+62 (982) 567-4609','sclaybourn8@techcrunch.com'),(10,'Virgilio','Dummett','939 Dixon Avenue','+62 (335) 728-0959','vdummett9@chicagotribune.com'),(11,'Conny','Osipenko','48 Anzinger Road','+62 (874) 445-7971','cosipenkoa@telegraph.co.uk'),(12,'Correy','Crecy','8 Sommers Lane','+86 (216) 646-6102','ccrecyb@microsoft.com'),(13,'Nannette','Oultram','20699 Oneill Terrace','+33 (705) 438-4748','noultramc@ameblo.jp'),(14,'Marlow','Smead','5 Nelson Park','+86 (510) 981-5900','msmeadd@tripod.com'),(15,'Sonia','Symcox','388 Mockingbird Junction','+62 (318) 956-0527','ssymcoxe@people.com.cn'),(16,'Cedric','Ansett','77474 Old Shore Street','+86 (841) 638-8180','cansettf@rakuten.co.jp'),(17,'Jeana','Chomicz','2802 Bartillon Hill','+63 (222) 719-9933','jchomiczg@skype.com'),(18,'Torrin','Ragot','51535 Steensland Road','+351 (228) 467-7632','tragoth@samsung.com'),(19,'Valle','Quickfall','06 Ludington Avenue','+30 (528) 331-0638','vquickfalli@constantcontact.com'),(20,'Lothario','Ceyssen','2704 Springs Plaza','+86 (908) 202-2694','lceyssenj@nydailynews.com'),(21,'Jacquelin','Strettle','4 Scott Drive','+380 (277) 560-8073','jstrettlek@simplemachines.org'),(22,'Park','Chastenet','1 Miller Point','+57 (502) 490-2156','pchastenetl@hatena.ne.jp'),(23,'Forrest','Collyer','84690 Sugar Alley','+7 (520) 320-4228','fcollyerm@patch.com'),(24,'Alexandros','Fotitt','11184 Carioca Park','+54 (365) 995-4625','afotittn@europa.eu'),(25,'Nata','Deeble','9 Doe Crossing Court','+1 (434) 923-3654','ndeebleo@theguardian.com'),(26,'Mareah','Boggas','35 Moland Terrace','+57 (711) 627-3835','mboggasp@blog.com'),(27,'Rita','Plewes','486 Muir Terrace','+33 (293) 661-0978','rplewesq@squidoo.com'),(28,'Agnella','Rodrig','1892 Bayside Point','+54 (698) 319-2004','arodrigr@nydailynews.com'),(29,'Kathryne','Severy','62693 Loeprich Terrace','+86 (127) 901-3349','kseverys@printfriendly.com'),(30,'Roberta','Truscott','6 Northview Junction','+389 (320) 695-6620','rtruscottt@blinklist.com'),(31,'Alisa','Wingham','5 Springview Crossing','+62 (403) 976-0090','awinghamu@list-manage.com'),(32,'Terri','Ivanchov','48 Sachtjen Avenue','+7 (376) 356-6639','tivanchovv@latimes.com'),(33,'Bette','Hynes','6 Oriole Road','+351 (994) 937-8552','bhynesw@weibo.com'),(34,'Jeannette','Christiensen','715 Sunnyside Road','+63 (102) 373-6561','jchristiensenx@t-online.de'),(35,'Alika','Filewood','9 Mayer Road','+86 (701) 818-7619','afilewoody@google.co.uk'),(36,'Bobbe','Tomadoni','3641 Buena Vista Crossing','+62 (414) 503-6521','btomadoniz@quantcast.com'),(37,'Mara','Gosney','95796 Spenser Alley','+7 (920) 703-0899','mgosney10@ezinearticles.com'),(38,'Puff','Perillo','25875 Huxley Plaza','+84 (653) 746-8756','pperillo11@nsw.gov.au'),(39,'Felecia','Marchetti','89 Stephen Point','+1 (316) 204-5054','fmarchetti12@4shared.com'),(40,'Miguela','Jeram','09 Gateway Pass','+81 (419) 731-1109','mjeram13@sphinn.com'),(41,'Felice','Davidsen','38661 Oxford Lane','+62 (895) 151-4242','fdavidsen14@auda.org.au'),(42,'Aime','Elie','9 Kipling Avenue','+33 (688) 634-8464','aelie15@rambler.ru'),(43,'Rubina','Eddison','38 Morningstar Way','+63 (365) 886-6152','reddison16@taobao.com'),(44,'Elmo','MacPaike','6545 Acker Park','+55 (190) 516-8941','emacpaike17@hud.gov'),(45,'Anders','Coviello','0 Ridge Oak Court','+226 (669) 477-8957','acoviello18@1688.com'),(46,'Della','Gritton','10280 Commercial Road','+504 (492) 127-8726','dgritton19@seesaa.net'),(47,'Pepe','Wrigglesworth','7983 Wayridge Point','+46 (353) 681-0027','pwrigglesworth1a@businessinsider.com'),(48,'Harmonie','Dunlap','23450 Dunning Center','+351 (775) 336-5333','hdunlap1b@tumblr.com'),(49,'Hoebart','Ankers','4 Utah Road','+33 (630) 770-2964','hankers1c@unesco.org'),(50,'Asa','Bhatia','1 Spaight Park','+62 (349) 464-3356','abhatia1d@creativecommons.org'),(51,'Aahil','Halani','Flat 303 Golden Residency Appt.','+923362406091','k200308@gmail.com'),(53,'faizan','Hakim','acbd','123456','faizanhakim@gmail.com'),(54,'Aaahil','Halani','acbd','123456','aahil@gmail.com'),(55,'Umair',NULL,'acbd','123456','umair@gmail.com');
/*!40000 ALTER TABLE `user` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `wishlist`
--

DROP TABLE IF EXISTS `wishlist`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `wishlist` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UserId` int NOT NULL,
  `AdId` int NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `Id_UNIQUE` (`Id`),
  KEY `FK_Ad_Wishlist` (`AdId`),
  KEY `FK_User_Wishlist` (`UserId`),
  CONSTRAINT `FK_Ad_Wishlist` FOREIGN KEY (`AdId`) REFERENCES `advertisement` (`AdId`),
  CONSTRAINT `FK_User_Wishlist` FOREIGN KEY (`UserId`) REFERENCES `user` (`UserId`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `wishlist`
--

LOCK TABLES `wishlist` WRITE;
/*!40000 ALTER TABLE `wishlist` DISABLE KEYS */;
INSERT INTO `wishlist` VALUES (1,53,1),(2,53,2),(3,54,3);
/*!40000 ALTER TABLE `wishlist` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2023-12-03 14:19:22
