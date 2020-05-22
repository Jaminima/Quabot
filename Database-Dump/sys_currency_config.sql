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
-- Table structure for table `currency_config`
--

DROP TABLE IF EXISTS `currency_config`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `currency_config` (
  `currency_id` int(11) NOT NULL AUTO_INCREMENT,
  `streamer_id` int(11) NOT NULL,
  `currency_name` varchar(16) DEFAULT 'tokens',
  `simple_responses` varchar(256) DEFAULT 'discord¬Discord Link',
  `custom_emotes` varchar(256) DEFAULT 'coin¬<:Meatloath:686982222825521156>¬MeatLoath',
  `fish_rewards` varchar(256) DEFAULT 'corpse¬10¬1000;potato¬90¬0',
  `fish_wait` int(10) unsigned DEFAULT '30',
  `fish_cost` int(10) unsigned DEFAULT '200',
  `message_reward_delay` int(10) unsigned DEFAULT '120',
  `message_reward` int(10) unsigned DEFAULT '50',
  `default_balance` int(10) unsigned DEFAULT '1000',
  `balance_commands` varchar(128) DEFAULT 'bal;balance',
  `pay_commands` varchar(128) DEFAULT 'pay',
  `fish_commands` varchar(128) DEFAULT 'fish;fishing',
  `gamble_commands` varchar(128) DEFAULT 'gamble',
  `gamble_odds` int(10) unsigned DEFAULT '40',
  PRIMARY KEY (`currency_id`),
  UNIQUE KEY `cur_id_UNIQUE` (`currency_id`),
  KEY `currency owner_idx` (`streamer_id`),
  CONSTRAINT `currency owner` FOREIGN KEY (`streamer_id`) REFERENCES `streamer_account` (`streamer_id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2020-05-22 12:54:45
