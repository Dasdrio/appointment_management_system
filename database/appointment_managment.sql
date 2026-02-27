-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Host: mysql
-- Erstellungszeit: 27. Feb 2026 um 06:57
-- Server-Version: 8.0.44
-- PHP-Version: 8.3.26

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `appointment_managment`
--

DELIMITER $$
--
-- Prozeduren
--
CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_count_todays_appointments_from_now` (IN `p_doctor_ID` INT)   SELECT Count(*) FROM Appointments
WHERE doctor_ID = p_doctor_ID
AND day(date_and_time) = day(now())
AND hour(date_and_time) >= hour(now())
AND MINUTE(date_and_time) >= MINUTE(NOW())$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_delete_appointment` (IN `p_appointment_id` INT)   DELETE FROM Appointments 
WHERE appointment_ID = p_appointment_ID$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_find_day` (IN `p_date_and_time` DATETIME, IN `p_doctor_ID` INT)   SELECT COUNT(CAST(date_and_time AS DATE)), CAST(date_and_time AS DATE) FROM `Appointments` 
WHERE MONTH(date_and_time) = MONTH(p_date_and_time) 
AND YEAR(date_and_time) = YEAR(p_date_and_time)
AND doctor_ID = p_doctor_ID
GROUP BY CAST(date_and_time AS DATE)$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_get_appointments_from_doctor` (IN `p_doctor_ID` INT, IN `p_date_and_time` DATETIME)   SELECT appointment_ID, date_and_time, name, surname, description FROM Appointments 
JOIN Persons ON patient_ID = person_ID
WHERE doctor_ID = p_doctor_ID AND date_and_time >= p_date_and_time$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_get_appointments_from_patient` (IN `p_patient_ID` INT, IN `p_date_and_time` DATETIME)   SELECT appointment_ID, date_and_time, name, surname, description FROM Appointments 
JOIN Persons ON doctor_ID = person_ID
WHERE patient_ID = p_patient_ID AND date_and_time >= p_date_and_time$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_get_appointments_on_day` (IN `p_doctor_ID` INT, IN `p_date_and_time` DATETIME)   SELECT date_and_time FROM Appointments
WHERE doctor_ID = p_doctor_ID
AND CAST(date_and_time AS DATE) = CAST(p_date_and_time AS DATE)$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_get_appointment_by_ID` (IN `p_appointment_ID` INT)   SELECT date_and_time, name, surname, description FROM Appointments 
JOIN Persons ON patient_ID = person_ID
WHERE appointment_ID = p_appointment_ID$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_insert_appointment` (IN `p_date_and_time` DATETIME, IN `p_patient_ID` INT UNSIGNED, IN `p_doctor_ID` INT UNSIGNED, IN `p_description` TEXT)   INSERT INTO Appointments VALUES(null, p_date_and_time, p_patient_ID, p_doctor_ID, p_description)$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_appointments_update_description` (IN `p_appointment_ID` INT, IN `p_description` TEXT CHARSET utf8mb4)   UPDATE Appointments
SET description = p_description
WHERE appointment_ID = p_appointment_ID$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_persons_get_doctor_by_specialization` (IN `p_specialization` ENUM('GENERAL_PRACTICE','PEDIATRICS','OPHTALMOLOGY','DERMATOLOGY','CARDIOLOGY','OTOLARYNGOLOGY'))   SELECT person_ID, name, surname FROM Persons WHERE specialization = p_specialization$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_persons_get_information_by_email` (IN `p_email` VARCHAR(100))   SELECT * FROM Persons WHERE email = p_email$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_persons_get_password_hash_by_email` (IN `p_email` VARCHAR(100))   SELECT password_hash FROM Persons WHERE email = p_email$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_persons_insert_person` (IN `p_name` VARCHAR(50), IN `p_surname` VARCHAR(50), IN `p_email` VARCHAR(100), IN `p_password_hash` VARCHAR(64))   INSERT INTO Persons Values(null, "PATIENT", p_name, p_surname, p_email, p_password_hash)$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Appointments`
--

CREATE TABLE `Appointments` (
  `appointment_ID` int UNSIGNED NOT NULL,
  `date_and_time` datetime NOT NULL,
  `patient_ID` int UNSIGNED NOT NULL,
  `doctor_ID` int UNSIGNED NOT NULL,
  `description` varchar(256) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Trigger `Appointments`
--
DELIMITER $$
CREATE TRIGGER `DELETE_appointment` BEFORE DELETE ON `Appointments` FOR EACH ROW INSERT INTO appointment_managment_log VALUES(NULL, CURRENT_USER, "Appointments", CURRENT_TIME, old.appointment_ID, "DELETE")
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `INSERT_appointment` AFTER INSERT ON `Appointments` FOR EACH ROW INSERT INTO appointment_managment_log VALUES(NULL, CURRENT_USER, "Appointments", CURRENT_TIME, new.appointment_ID, "INSERT")
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `UPDATE_appointment` AFTER UPDATE ON `Appointments` FOR EACH ROW INSERT INTO appointment_managment_log VALUES(NULL, CURRENT_USER, "Appointments", CURRENT_TIME, new.appointment_ID, "UPDATE")
$$
DELIMITER ;

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `appointment_managment_log`
--

CREATE TABLE `appointment_managment_log` (
  `log_ID` int UNSIGNED NOT NULL,
  `name_user` varchar(100) NOT NULL,
  `name_table` varchar(50) NOT NULL,
  `trigger_time` datetime NOT NULL,
  `target_ID` int UNSIGNED NOT NULL,
  `action_performed` varchar(10) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `appointment_managment_log`
--

INSERT INTO `appointment_managment_log` (`log_ID`, `name_user`, `name_table`, `trigger_time`, `target_ID`, `action_performed`) VALUES
(1, 'root@%', 'Persons', '2026-01-07 09:03:35', 10, 'INSERT'),
(2, 'root@%', 'Persons', '2026-01-07 09:08:53', 10, 'DELETE'),
(3, 'root@%', 'Persons', '2026-02-05 07:17:21', 11, 'INSERT'),
(4, 'root@%', 'Persons', '2026-02-05 07:17:28', 11, 'DELETE');

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `Persons`
--

CREATE TABLE `Persons` (
  `person_ID` int UNSIGNED NOT NULL,
  `specialization` enum('PATIENT','GENERAL_PRACTICE','PEDIATRICS','OPHTALMOLOGY','DERMATOLOGY','CARDIOLOGY','OTOLARYNGOLOGY') NOT NULL,
  `name` varchar(50) NOT NULL,
  `surname` varchar(50) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password_hash` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Daten für Tabelle `Persons`
--

INSERT INTO `Persons` (`person_ID`, `specialization`, `name`, `surname`, `email`, `password_hash`) VALUES
(1, 'PATIENT', 'Peterson', 'Peter', 'peter.pet@fakemail.com', '026ad9b14a7453b7488daa0c6acbc258b1506f52c441c7c465474c1a564394ff'),
(2, 'PATIENT', 'Anna', 'Schmidt', 'anna.schmidt@example.com', '7f92063f461ca1991c0f734dca11d2c98d30e14f08c0e5a7f8e631eca3bc1b52'),
(3, 'GENERAL_PRACTICE', 'Markus', 'Weber', 'markus.weber@gpclinic.de', '6a4b2689e485802ff40115e0c87ab3dc10388bc11481389335ee8c3d3e131db3'),
(4, 'PEDIATRICS', 'Julia', 'Klein', 'julia.klein@kinderarzt.de', '4b47ec6afcde5a3a682b5d7bd1a02c2fb99955fb58171ad7d89422276425bcc5'),
(5, 'CARDIOLOGY', 'Thomas', 'Müller', 'thomas.mueller@herzpraxis.de', '9d223ac5aa6904c5d332ef09d0d9b3c0ea5be2bf545dacb75cba1f6ff2a5af2d'),
(6, 'DERMATOLOGY', 'Sabine', 'Fischer', 'sabine.fischer@hautarzt.de', '2896e8434d1338a6b62024ae572045196e3d440ff7bcf4e8a40f2068b57ecd92'),
(7, 'OPHTALMOLOGY', 'Leon', 'Becker', 'leon.becker@augenzentrum.de', 'd69353eb0f11cd791c5f116277b2c518cee943936de05f782de97861f810c4ba'),
(8, 'OTOLARYNGOLOGY', 'Nina', 'Krüger', 'nina.krueger@hno-praxis.de', 'be6c8c1f5886aee3187673c93d53094e2140a738517c5920a6d4de55617af193'),
(9, 'PATIENT', 'Jonas', 'Meier', 'jonas.meier@example.com', 'a33f9b123c8d14e4006d4f34af1ebe692bdef82a8449b1e91cd8c81ce4721e35');

--
-- Trigger `Persons`
--
DELIMITER $$
CREATE TRIGGER `DELETE_person` BEFORE DELETE ON `Persons` FOR EACH ROW INSERT INTO appointment_managment_log VALUES(NULL, CURRENT_USER, "Persons", CURRENT_TIME, old.person_ID, "DELETE")
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `INSERT_person` AFTER INSERT ON `Persons` FOR EACH ROW INSERT INTO appointment_managment_log VALUES(NULL, CURRENT_USER, "Persons", CURRENT_TIME, new.person_ID, "INSERT")
$$
DELIMITER ;
DELIMITER $$
CREATE TRIGGER `UPDATE_person` AFTER UPDATE ON `Persons` FOR EACH ROW INSERT INTO appointment_managment_log VALUES(NULL, CURRENT_USER, "Persons", CURRENT_TIME, new.person_ID, "UPDATE")
$$
DELIMITER ;

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `Appointments`
--
ALTER TABLE `Appointments`
  ADD PRIMARY KEY (`appointment_ID`),
  ADD KEY `patient_ID` (`patient_ID`),
  ADD KEY `doctor_ID` (`doctor_ID`);

--
-- Indizes für die Tabelle `appointment_managment_log`
--
ALTER TABLE `appointment_managment_log`
  ADD PRIMARY KEY (`log_ID`);

--
-- Indizes für die Tabelle `Persons`
--
ALTER TABLE `Persons`
  ADD PRIMARY KEY (`person_ID`),
  ADD UNIQUE KEY `email` (`email`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `Appointments`
--
ALTER TABLE `Appointments`
  MODIFY `appointment_ID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT für Tabelle `appointment_managment_log`
--
ALTER TABLE `appointment_managment_log`
  MODIFY `log_ID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT für Tabelle `Persons`
--
ALTER TABLE `Persons`
  MODIFY `person_ID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- Constraints der exportierten Tabellen
--

--
-- Constraints der Tabelle `Appointments`
--
ALTER TABLE `Appointments`
  ADD CONSTRAINT `Appointments_ibfk_1` FOREIGN KEY (`patient_ID`) REFERENCES `Persons` (`person_ID`),
  ADD CONSTRAINT `Appointments_ibfk_2` FOREIGN KEY (`doctor_ID`) REFERENCES `Persons` (`person_ID`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
