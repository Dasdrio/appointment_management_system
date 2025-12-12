-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Host: mysql
-- Erstellungszeit: 12. Dez 2025 um 10:39
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
CREATE DEFINER=`root`@`%` PROCEDURE `sp_chose_ appointment` (IN `p_doctor_ID` INT, IN `p_date_and_time` INT)   SELECT date_and_time FROM Appointments
WHERE doctor_ID = p_doctor_ID AND p_date_and_time < date_and_time
ORDER BY date_and_time DESC$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_doctor_view_appointments` (IN `p_doctor_ID` INT, IN `p_date_and_time` INT)   SELECT date_and_time, name, surname FROM Appointments
Join Persons 
ON Appointments.patient_ID = Persons.person_ID
WHERE doctor_ID = p_doctor_ID AND p_date_and_time < date_and_time
ORDER BY date_and_time DESC$$

CREATE DEFINER=`root`@`%` PROCEDURE `sp_patient_view_appointments` (IN `p_patient_ID` INT, IN `p_date_and_time` INT)   SELECT date_and_time, name, specialization FROM Appointments
Join Persons 
ON Appointments.doctor_ID = Persons.person_ID
WHERE patient_ID = p_patient_ID AND p_date_and_time < date_and_time
ORDER BY date_and_time DESC$$

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
(1, 'PATIENT', 'Peterson', 'Peter', 'peter.pet@fakemail.com', 'ea72c79594296e45b8c2a296644d988581f58cfac6601d122ed0a8bd7c02e8bf');

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
-- Indizes für die Tabelle `Persons`
--
ALTER TABLE `Persons`
  ADD PRIMARY KEY (`person_ID`);

--
-- AUTO_INCREMENT für exportierte Tabellen
--

--
-- AUTO_INCREMENT für Tabelle `Appointments`
--
ALTER TABLE `Appointments`
  MODIFY `appointment_ID` int UNSIGNED NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT für Tabelle `Persons`
--
ALTER TABLE `Persons`
  MODIFY `person_ID` int UNSIGNED NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

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
