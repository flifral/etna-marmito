-- MySQL dump 10.16  Distrib 10.1.29-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: localhost    Database: NET5
-- ------------------------------------------------------
-- Server version	10.1.29-MariaDB-6

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Mitos`
--

DROP TABLE IF EXISTS `Mitos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Mitos` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `AuthorId` bigint(20) NOT NULL,
  `Category` int(11) NOT NULL,
  `Content` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=33 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Mitos`
--

LOCK TABLES `Mitos` WRITE;
/*!40000 ALTER TABLE `Mitos` DISABLE KEYS */;
INSERT INTO `Mitos` VALUES (9,0,0,'Test mito bla bla bla'),(17,4,0,'Pardon, je pourrai pas venir, je suis débordé(e)'),(18,4,0,'Désolé, je suis pressé(e) là, mais je supporte totalement votre cause, vous faites du chouette boulot'),(19,4,0,'Pardon monsieur, je n’ai rien sur moi'),(20,4,0,'Je peux pas, j’ai aqua-poney ce jour-là !'),(21,5,1,'Si tu fais pas ce que je te dis le père Noël te donnera pas de cadeaux'),(22,5,1,'Ah au fait papa m’a dit que c’était à toi de mettre la table'),(23,5,1,'Mais ça fait que cinq minutes que j’ai commencé à jouer !'),(24,5,2,'J’ai attrapé la grippe'),(25,5,2,'Ma voiture ne démarre pas'),(26,5,2,'Plus de bus ni de métro, c’est la grève ! Y en a marre !'),(27,5,2,'Ma grand-mère est morte'),(28,6,3,'Tu sais, quand on a une fille en tête, les autres n’existent plus'),(29,6,3,'J’espère que tu seras plus heureuse avec lui que tu ne l’as été avec moi...'),(30,6,3,'On s’est quitté d’un commun accord'),(31,6,3,'Si je te trompais, tu crois vraiment que je t’aurais dit avoir passé la soirée avec elle hier soir ?'),(32,6,3,'Je te jure...c’est la première fois que ça m’arrive !');
/*!40000 ALTER TABLE `Mitos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `Id` bigint(20) NOT NULL AUTO_INCREMENT,
  `Email` text,
  `Name` text,
  `Password` text,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8mb4;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES (1,'machin@machadin.jfr','blaaa','pass'),(2,'bcxbcx@ggl','blblblbl','dsdfsdfsd'),(3,'test@test.test','TestUser','test'),(4,'fauque_x@etna-alternance.net','Fauque','fauque'),(5,'miksa_t@etna-alternance.net','Miksa','miksa'),(6,'bebel_f@etna-alternance.net','Bebel','bebel'),(7,'yotest@gmail.com','YoTest','yotest');
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-05-16 10:24:39
