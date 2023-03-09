/*
SQLyog Community v13.2.0 (64 bit)
MySQL - 8.0.32 : Database - ecommercewebproject
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`ecommercewebproject` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `ecommercewebproject`;

/*Table structure for table `buynow` */

DROP TABLE IF EXISTS `buynow`;

CREATE TABLE `buynow` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Lname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` varchar(200) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `City` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `State` varchar(50) NOT NULL,
  `PostalCode` int NOT NULL,
  `Country` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `CardId` int DEFAULT NULL,
  `CreId` int DEFAULT NULL,
  `MerId` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `CardId` (`CardId`),
  KEY `CreId` (`CreId`),
  CONSTRAINT `buynow_ibfk_1` FOREIGN KEY (`CardId`) REFERENCES `card` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `buynow_ibfk_2` FOREIGN KEY (`CreId`) REFERENCES `credentials` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `buynow` */

insert  into `buynow`(`Id`,`Fname`,`Lname`,`Address`,`City`,`State`,`PostalCode`,`Country`,`CardId`,`CreId`,`MerId`) values 
(1,'Nayab','Ali','Subhan town near board office street no 12 Gujranwala','Gujranwala','Punjab',5434,'Pakistan',25,1,'nayab8609@gmail.com');

/*Table structure for table `card` */

DROP TABLE IF EXISTS `card`;

CREATE TABLE `card` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Name` varchar(50) NOT NULL,
  `Description` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Price` int NOT NULL,
  `ImagePath` varchar(500) NOT NULL,
  `MercantId` varchar(100) NOT NULL,
  `Quantity` int NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `card_ibfk_1` (`MercantId`),
  CONSTRAINT `card_ibfk_1` FOREIGN KEY (`MercantId`) REFERENCES `merchantcredentials` (`Email`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=34 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `card` */

insert  into `card`(`Id`,`Name`,`Description`,`Price`,`ImagePath`,`MercantId`,`Quantity`) values 
(25,'Casual shoes','CORE MEN BLACK SUEDE LEATHER HIGH CUT CHUKKA BOOTS\r\n\r\nSole Material: ThermoPlastic Rubber (TPR)\r\n\r\nUpper Material: 100% Sueded Leathe',2000,'~/Content/Upload/1052519073causal shoes.jfif','nayab8609@gmail.com',28),
(26,'laptop ','Dell Inspiron 2-in-1 x360 7586 Intel Core i7 8565U 16GB RAM 512GB SSD Nvidia MX150 2GB GPU 15.6\" FHD Touch Display Win10 Laptop - Black (International Warranty) .',100000,'~/Content/Upload/2061724767laptop.jpeg','nayab8609@gmail.com',100000),
(27,'Iphone 14 pro max','Apple iPhone 14 Pro Max is now available in Pakistan with decent specifications, features and price in Pakistan is starting from',540000,'~/Content/Upload/999983635iphone.jpg','nayab8609@gmail.com',25),
(28,'Gaming Pc','Buy New Gaming PC in Pakistan at best rates. We provide Gaming Computers, Components, and peripherals at the best rates in Pakistan.',500000,'~/Content/Upload/1495299281gaming oc.jpg','nayab8609@gmail.com',50),
(29,'Red T shirt','a high school or college athlete kept out of varsity competition for one year to develop skills and extend eligibility.',1000,'~/Content/Upload/1746119959redshirt.jpg','aliali@gmail.com',100),
(30,'Grey Eye lens','We offer the most affordable eye lens price in Pakistan with an extensive collection of contact lenses ranging from all types and all colors of eye lenses',2000,'~/Content/Upload/2027007097gret eye lens.jfif','aliali@gmail.com',20),
(31,'Smart watch','Get the lowest smart watches price in Pakistan. Browse and Shop the latest smart watch models at the best price in Pakistan only from PriceOye',12000,'~/Content/Upload/1918091220smart watch.jpg','aliali@gmail.com',32),
(32,'Mac book air','Our most popular laptop, MacBook Air is supercharged by M1 and M2 chips. Featuring FaceTime HD camera, Touch ID, and vibrant Retina display.',600000,'~/Content/Upload/2069996660macbok.jpg','aliali@gmail.com',4),
(33,'Black pent','Want to style the black pants in a different and innovative way? Here are 29 amazing ideas about how to style black pants outfits.',2000,'~/Content/Upload/110901184blackpent.jpg','aliali@gmail.com',43);

/*Table structure for table `credentials` */

DROP TABLE IF EXISTS `credentials`;

CREATE TABLE `credentials` (
  `id` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(50) NOT NULL,
  `Lname` varchar(50) NOT NULL,
  `Country` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `RefId` varchar(100) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `credentials` */

insert  into `credentials`(`id`,`Fname`,`Lname`,`Country`,`Email`,`Password`,`RefId`) values 
(1,'Client','1','America','nayab8609@gmail.com','12345','nayab8609@gmail.com'),
(2,'Client','2','America','nnn@gmail.com','12345','nnn@gmail.com'),
(3,'mehtab','ali','pakistan','a@gmail.com','4321','a@gmail.com');

/*Table structure for table `installment` */

DROP TABLE IF EXISTS `installment`;

CREATE TABLE `installment` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Fname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Lname` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Address` varchar(1000) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `City` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `State` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PostalCode` int NOT NULL,
  `Country` varchar(50) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `RemainingQty` int NOT NULL,
  `CardId` int DEFAULT NULL,
  `CreId` int DEFAULT NULL,
  `MerId` varchar(50) DEFAULT NULL,
  `NoOfInstallments` int NOT NULL,
  `PerInstallment` int NOT NULL,
  `Confirm` varchar(50) NOT NULL,
  `Cancel` varchar(50) NOT NULL,
  PRIMARY KEY (`Id`),
  KEY `CardId` (`CardId`),
  KEY `CreId` (`CreId`),
  CONSTRAINT `installment_ibfk_1` FOREIGN KEY (`CardId`) REFERENCES `card` (`Id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `installment_ibfk_2` FOREIGN KEY (`CreId`) REFERENCES `credentials` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `installment` */

insert  into `installment`(`Id`,`Fname`,`Lname`,`Address`,`City`,`State`,`PostalCode`,`Country`,`RemainingQty`,`CardId`,`CreId`,`MerId`,`NoOfInstallments`,`PerInstallment`,`Confirm`,`Cancel`) values 
(4,'mehtab','Ali','jalapur jatan','Gujrat','Punjab',3322,'Pakistan',25,27,1,'nayab8609@gmail.com',3,900001,'Accepted','false');

/*Table structure for table `merchantcredentials` */

DROP TABLE IF EXISTS `merchantcredentials`;

CREATE TABLE `merchantcredentials` (
  `Id` int NOT NULL,
  `Fname` varchar(50) NOT NULL,
  `Lname` varchar(50) NOT NULL,
  `Country` varchar(100) NOT NULL,
  `Email` varchar(100) NOT NULL,
  `Password` varchar(100) NOT NULL,
  `RefId` varchar(100) NOT NULL,
  PRIMARY KEY (`Email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

/*Data for the table `merchantcredentials` */

insert  into `merchantcredentials`(`Id`,`Fname`,`Lname`,`Country`,`Email`,`Password`,`RefId`) values 
(0,'ali','ali','Bangladesh','aliaffli@gmail.com','asdf','aliaffli@gmail.com'),
(2,'Merchant','2','Pakistan','aliali@gmail.com','12345','aliali@gmail.com'),
(0,'Muhammad','Arshad','Pakistan','arshad@gmail.com','arshad123','arshad@gmail.com'),
(0,'f','f','f','f@gmail.com','123321','f@gmail.com'),
(0,'Ittyab','Rehman','Pakistan','ittyab@gmail.com','12345','ittyab@gmail.com'),
(0,'kami','bhai','Pakistan','kami@gmail.com','12345','kami@gmail.com'),
(0,'nayab','ali','pakistan','nayab1122@gmail.com','112211','nayab1122@gmail.com'),
(1,'Merchant','1','pakistan','nayab8609@gmail.com','12345','nayab8609@gmail.com'),
(0,'nnn','lll','ccc','uuu@gmail.com','ppppp','uuu@gmail.com');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
