-- MySQL dump 10.13  Distrib 5.6.17, for Win64 (x86_64)
--
-- Host: localhost    Database: college
-- ------------------------------------------------------
-- Server version	5.6.23-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary table structure for view `balance`
--

DROP TABLE IF EXISTS `balance`;
/*!50001 DROP VIEW IF EXISTS `balance`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `balance` (
  `Regno` tinyint NOT NULL,
  `StudentName` tinyint NOT NULL,
  `Semname` tinyint NOT NULL,
  `Fees` tinyint NOT NULL,
  `amount` tinyint NOT NULL,
  `balance` tinyint NOT NULL,
  `Course` tinyint NOT NULL,
  `Department` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `batch`
--

DROP TABLE IF EXISTS `batch`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `batch` (
  `Batchid` tinyint(4) NOT NULL AUTO_INCREMENT,
  `BatchYear` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`Batchid`),
  UNIQUE KEY `BatchYear_UNIQUE` (`BatchYear`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `batch`
--

LOCK TABLES `batch` WRITE;
/*!40000 ALTER TABLE `batch` DISABLE KEYS */;
INSERT INTO `batch` VALUES (3,2009),(2,2010);
/*!40000 ALTER TABLE `batch` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `class_view`
--

DROP TABLE IF EXISTS `class_view`;
/*!50001 DROP VIEW IF EXISTS `class_view`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `class_view` (
  `Courseid` tinyint NOT NULL,
  `DepartmentId` tinyint NOT NULL,
  `Course` tinyint NOT NULL,
  `Department` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `course`
--

DROP TABLE IF EXISTS `course`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `course` (
  `Courseid` tinyint(4) NOT NULL AUTO_INCREMENT,
  `DepartmentId` tinyint(4) DEFAULT NULL,
  `Course` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Courseid`),
  UNIQUE KEY `Course_UNIQUE` (`Course`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `course`
--

LOCK TABLES `course` WRITE;
/*!40000 ALTER TABLE `course` DISABLE KEYS */;
INSERT INTO `course` VALUES (2,2,'B.SC'),(3,1,'B.B.A'),(4,3,'B.SC(CS)'),(5,3,'BCA'),(6,3,'BSC(MC)'),(7,1,'B.A');
/*!40000 ALTER TABLE `course` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `department`
--

DROP TABLE IF EXISTS `department`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `department` (
  `Depid` tinyint(4) NOT NULL AUTO_INCREMENT,
  `Department` varchar(195) DEFAULT NULL,
  PRIMARY KEY (`Depid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `department`
--

LOCK TABLES `department` WRITE;
/*!40000 ALTER TABLE `department` DISABLE KEYS */;
INSERT INTO `department` VALUES (1,'Tamil'),(2,'English'),(3,'Department of Computer Science and Applicatios');
/*!40000 ALTER TABLE `department` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `fees`
--

DROP TABLE IF EXISTS `fees`;
/*!50001 DROP VIEW IF EXISTS `fees`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `fees` (
  `amtid` tinyint NOT NULL,
  `Batch` tinyint NOT NULL,
  `Courseid` tinyint NOT NULL,
  `Semname` tinyint NOT NULL,
  `SemId` tinyint NOT NULL,
  `Fees` tinyint NOT NULL,
  `Feesid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `feesamt`
--

DROP TABLE IF EXISTS `feesamt`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `feesamt` (
  `amtid` mediumint(9) NOT NULL AUTO_INCREMENT,
  `Feesid` mediumint(9) DEFAULT NULL,
  `SemId` tinyint(4) DEFAULT NULL,
  `Fees` mediumint(4) DEFAULT NULL,
  PRIMARY KEY (`amtid`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feesamt`
--

LOCK TABLES `feesamt` WRITE;
/*!40000 ALTER TABLE `feesamt` DISABLE KEYS */;
INSERT INTO `feesamt` VALUES (1,6,1,2013),(2,10,2,200),(3,10,1,1000),(4,11,1,1500),(5,11,2,5000),(6,11,3,1200),(7,14,1,2000),(8,14,2,500),(9,14,2,2345),(11,NULL,1,2000),(12,NULL,2,3000),(13,NULL,1,2000),(14,NULL,2,5000),(15,21,1,22),(16,21,2,33),(17,21,3,44);
/*!40000 ALTER TABLE `feesamt` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Temporary table structure for view `feesstruct`
--

DROP TABLE IF EXISTS `feesstruct`;
/*!50001 DROP VIEW IF EXISTS `feesstruct`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `feesstruct` (
  `Semname` tinyint NOT NULL,
  `Feesid` tinyint NOT NULL,
  `SemId` tinyint NOT NULL,
  `Fees` tinyint NOT NULL,
  `amtid` tinyint NOT NULL
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `feestruct`
--

DROP TABLE IF EXISTS `feestruct`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `feestruct` (
  `Feesid` tinyint(4) NOT NULL AUTO_INCREMENT,
  `Courseid` tinyint(4) NOT NULL,
  `Batch` smallint(6) NOT NULL,
  PRIMARY KEY (`Feesid`)
) ENGINE=InnoDB AUTO_INCREMENT=22 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `feestruct`
--

LOCK TABLES `feestruct` WRITE;
/*!40000 ALTER TABLE `feestruct` DISABLE KEYS */;
INSERT INTO `feestruct` VALUES (6,3,2003),(10,2,2004),(11,2,2025),(14,3,2025),(19,5,2025),(20,6,2025),(21,6,2025);
/*!40000 ALTER TABLE `feestruct` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `payment`
--

DROP TABLE IF EXISTS `payment`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `payment` (
  `Paymentid` mediumint(9) NOT NULL AUTO_INCREMENT,
  `StudentId` tinyint(4) DEFAULT NULL,
  `Amount` mediumint(8) NOT NULL,
  `Date` date DEFAULT NULL,
  `amtid` tinyint(4) DEFAULT NULL,
  PRIMARY KEY (`Paymentid`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `payment`
--

LOCK TABLES `payment` WRITE;
/*!40000 ALTER TABLE `payment` DISABLE KEYS */;
INSERT INTO `payment` VALUES (1,1,2000,'2015-12-12',1),(2,2,4000,'2015-01-13',4),(3,2,1200,'2015-01-13',2),(4,1,1135,'2015-12-12',3),(5,1,5600,'2015-12-12',3),(6,1,135,'2015-12-12',1);
/*!40000 ALTER TABLE `payment` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `semesters`
--

DROP TABLE IF EXISTS `semesters`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `semesters` (
  `Semid` tinyint(4) NOT NULL,
  `Semname` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Semid`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `semesters`
--

LOCK TABLES `semesters` WRITE;
/*!40000 ALTER TABLE `semesters` DISABLE KEYS */;
INSERT INTO `semesters` VALUES (1,'Semester-I'),(2,'Semester-II'),(3,'Semester-V');
/*!40000 ALTER TABLE `semesters` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `shopdetails`
--

DROP TABLE IF EXISTS `shopdetails`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `shopdetails` (
  `Contact1` varchar(15) DEFAULT NULL,
  `Contact2` varchar(15) DEFAULT NULL,
  `Shopname` varchar(100) DEFAULT NULL,
  `Addressline1` varchar(100) DEFAULT NULL,
  `Addressline2` varchar(100) DEFAULT NULL,
  `Addressline3` varchar(100) DEFAULT NULL,
  `Email` varchar(30) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `shopdetails`
--

LOCK TABLES `shopdetails` WRITE;
/*!40000 ALTER TABLE `shopdetails` DISABLE KEYS */;
INSERT INTO `shopdetails` VALUES ('9715227755','9715999533','SRI SANTHOSHI ARTS AND SCIENCE','Paiyambadi Village','Polambakkam(Po)','Maduranthakam(Tk),Kanchipuram(DT)-603 309.','ddurai123@yahoo.co.in');
/*!40000 ALTER TABLE `shopdetails` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `student`
--

DROP TABLE IF EXISTS `student`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `student` (
  `Studentid` mediumint(8) NOT NULL AUTO_INCREMENT,
  `StudentName` varchar(45) DEFAULT NULL,
  `Regno` varchar(45) DEFAULT NULL,
  `MobileNo` varchar(20) DEFAULT NULL,
  `FeesId` tinyint(4) DEFAULT NULL,
  `Batch` smallint(6) DEFAULT NULL,
  PRIMARY KEY (`Studentid`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `student`
--

LOCK TABLES `student` WRITE;
/*!40000 ALTER TABLE `student` DISABLE KEYS */;
INSERT INTO `student` VALUES (1,'Duraimanikam','Rk01',NULL,1,NULL),(2,'Arun','Rk02',NULL,2,NULL),(3,'Raja','Rk03',NULL,2,NULL);
/*!40000 ALTER TABLE `student` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping routines for database 'college'
--
/*!50003 DROP PROCEDURE IF EXISTS `PROBATCH` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `PROBATCH`(
Pbatchyear varchar(50),
Pbatchid smallint
)
BEGIN
insert into `batch`(batchid,
BatchYear) values(Pbatchid,Pbatchyear)
ON DUPLICATE KEY UPDATE
batchid=values(batchid),
BatchYear=values(BatchYear);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PROCOURSE` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `PROCOURSE`(
PROCourse varchar(50),
PROCourseid smallint,
PRODepartmentid smallint
)
BEGIN
INSERT INTO `college`.`course` (`Courseid`, `DepartmentId`, `Course`) VALUES (PROCourseid,PRODepartmentid, PROCourse)
ON DUPLICATE KEY UPDATE
Courseid=values(Courseid),
DepartmentId=values(DepartmentId),
Course=values(Course);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PRODEPARTMENT` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = latin1 */ ;
/*!50003 SET character_set_results = latin1 */ ;
/*!50003 SET collation_connection  = latin1_swedish_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `PRODEPARTMENT`(
PROdepartment varchar(50),
PROdepid smallint
)
BEGIN
insert into `department`(depid,
department) values(PROdepid,
PROdepartment
)
ON DUPLICATE KEY UPDATE
depid=values(depid),
department=values(department);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PROFEES` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `PROFEES`(
PROFeesid varchar(50),
PROCourseid smallint,
PROBatch smallint
)
BEGIN
INSERT INTO `college`.`feestruct` (`Feesid`, `Courseid`, `Batch`) VALUES (PROFeesid,PROCourseid,PROBatch)
ON DUPLICATE KEY UPDATE
Feesid=values(Feesid),
Courseid=values(Courseid),
Batch=values(Batch);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PROFEESSTRUCTURE` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `PROFEESSTRUCTURE`(
PROFeesid varchar(50),
PROamtid mediumint,
PROSemId smallint,
PROFees mediumint,
PROCourseId mediumint,
PROBatch smallint
)
BEGIN
INSERT INTO `college`.`feesamt` (`amtid`, `Feesid`, `SemId`, `Fees`) VALUES (PROamtid,PROFeesid, PROSemId,PROFees)
ON DUPLICATE KEY UPDATE
amtid=values(amtid),
Feesid=values(Feesid),
SemId=values(SemId),
Fees=values(Fees);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `PROSEMESTERS` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = latin1 */ ;
/*!50003 SET character_set_results = latin1 */ ;
/*!50003 SET collation_connection  = latin1_swedish_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `PROSEMESTERS`(
PSemname varchar(50),
PSemid smallint
)
BEGIN
insert into `semesters`(Semid,
Semname) values(PSemid,
PSemname)
ON DUPLICATE KEY UPDATE
Semid=values(Semid),
Semname=values(Semname);
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS `REPSEMFEES` */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'STRICT_TRANS_TABLES,NO_AUTO_CREATE_USER,NO_ENGINE_SUBSTITUTION' */ ;
DELIMITER ;;
CREATE DEFINER=`root`@`localhost` PROCEDURE `REPSEMFEES`(IN PROBatch smallint)
BEGIN
SELECT        class_view.Course, class_view.Department, fees.Semname, fees.Fees, fees.Batch
FROM            class_view INNER JOIN
                             (SELECT        amtid, Batch, Courseid, Semname, SemId, Fees, Feesid
                               FROM            fees fees_1
                               WHERE        (Batch = PROBatch)) fees ON class_view.Courseid = fees.Courseid;
END ;;
DELIMITER ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;

--
-- Final view structure for view `balance`
--

/*!50001 DROP TABLE IF EXISTS `balance`*/;
/*!50001 DROP VIEW IF EXISTS `balance`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `balance` AS select `student`.`Regno` AS `Regno`,`student`.`StudentName` AS `StudentName`,`fees`.`Semname` AS `Semname`,`fees`.`Fees` AS `Fees`,ifnull(sum(`payment`.`Amount`),0) AS `amount`,(`fees`.`Fees` - ifnull(sum(`payment`.`Amount`),0)) AS `balance`,`class_view`.`Course` AS `Course`,`class_view`.`Department` AS `Department` from (((`fees` join `student` on((`fees`.`Feesid` = `student`.`FeesId`))) join `class_view` on((`fees`.`Courseid` = `class_view`.`Courseid`))) left join `payment` on((`fees`.`amtid` = `payment`.`amtid`))) group by `fees`.`amtid`,`student`.`Studentid`,`student`.`Regno`,`class_view`.`Course`,`class_view`.`Department` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `class_view`
--

/*!50001 DROP TABLE IF EXISTS `class_view`*/;
/*!50001 DROP VIEW IF EXISTS `class_view`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `class_view` AS select `course`.`Courseid` AS `Courseid`,`course`.`DepartmentId` AS `DepartmentId`,`course`.`Course` AS `Course`,`department`.`Department` AS `Department` from (`course` join `department` on((`course`.`DepartmentId` = `department`.`Depid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `fees`
--

/*!50001 DROP TABLE IF EXISTS `fees`*/;
/*!50001 DROP VIEW IF EXISTS `fees`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `fees` AS select `feesamt`.`amtid` AS `amtid`,`feestruct`.`Batch` AS `Batch`,`feestruct`.`Courseid` AS `Courseid`,`semesters`.`Semname` AS `Semname`,`feesamt`.`SemId` AS `SemId`,`feesamt`.`Fees` AS `Fees`,`feestruct`.`Feesid` AS `Feesid` from ((`feestruct` join `feesamt` on((`feestruct`.`Feesid` = `feesamt`.`Feesid`))) join `semesters` on((`feesamt`.`SemId` = `semesters`.`Semid`))) group by `feesamt`.`amtid`,`feesamt`.`Fees`,`feestruct`.`Batch`,`feestruct`.`Courseid`,`semesters`.`Semname`,`feesamt`.`SemId`,`feestruct`.`Feesid` */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;

--
-- Final view structure for view `feesstruct`
--

/*!50001 DROP TABLE IF EXISTS `feesstruct`*/;
/*!50001 DROP VIEW IF EXISTS `feesstruct`*/;
/*!50001 SET @saved_cs_client          = @@character_set_client */;
/*!50001 SET @saved_cs_results         = @@character_set_results */;
/*!50001 SET @saved_col_connection     = @@collation_connection */;
/*!50001 SET character_set_client      = utf8 */;
/*!50001 SET character_set_results     = utf8 */;
/*!50001 SET collation_connection      = utf8_general_ci */;
/*!50001 CREATE ALGORITHM=UNDEFINED */
/*!50013 DEFINER=`root`@`localhost` SQL SECURITY DEFINER */
/*!50001 VIEW `feesstruct` AS select `semesters`.`Semname` AS `Semname`,`feesamt`.`Feesid` AS `Feesid`,`feesamt`.`SemId` AS `SemId`,`feesamt`.`Fees` AS `Fees`,`feesamt`.`amtid` AS `amtid` from (`feesamt` join `semesters` on((`feesamt`.`SemId` = `semesters`.`Semid`))) */;
/*!50001 SET character_set_client      = @saved_cs_client */;
/*!50001 SET character_set_results     = @saved_cs_results */;
/*!50001 SET collation_connection      = @saved_col_connection */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2016-02-07 18:20:41
