-- MySQL dump 10.13  Distrib 8.0.19, for Win64 (x86_64)
--
-- Host: 134.122.111.200    Database: sys
-- ------------------------------------------------------
-- Server version	5.7.30-0ubuntu0.18.04.1

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
-- Table structure for table `currency_participants`
--

DROP TABLE IF EXISTS `currency_participants`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `currency_participants` (
  `currency_id` int(11) NOT NULL,
  `streamer_id` int(11) NOT NULL,
  `discord_guild` varchar(32) DEFAULT NULL,
  `twitch_name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`currency_id`,`streamer_id`),
  UNIQUE KEY `discord_guild_UNIQUE` (`discord_guild`),
  UNIQUE KEY `twitch_name_UNIQUE` (`twitch_name`),
  KEY `streamer_idx` (`streamer_id`),
  CONSTRAINT `currency` FOREIGN KEY (`currency_id`) REFERENCES `currency_config` (`currency_id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `streamer` FOREIGN KEY (`streamer_id`) REFERENCES `streamer_account` (`streamer_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `currency_participants`
--

LOCK TABLES `currency_participants` WRITE;
/*!40000 ALTER TABLE `currency_participants` DISABLE KEYS */;
INSERT INTO `currency_participants` VALUES (1,1,'678011054172798991','jccjaminima'),(2,1,'443459293245865985','harbonator');
/*!40000 ALTER TABLE `currency_participants` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-19 15:24:08
