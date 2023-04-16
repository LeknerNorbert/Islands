-- phpMyAdmin SQL Dump
-- version 5.2.0
-- https://www.phpmyadmin.net/
--
-- Gép: 127.0.0.1
-- Létrehozás ideje: 2023. Ápr 14. 19:17
-- Kiszolgáló verziója: 10.4.27-MariaDB
-- PHP verzió: 8.1.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `island`
--

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `buildings`
--

CREATE TABLE `buildings` (
  `Id` int(11) NOT NULL,
  `BuildingType` int(11) NOT NULL DEFAULT 0,
  `XCoordinate` int(11) NOT NULL,
  `YCoordinate` int(11) NOT NULL,
  `Level` int(11) NOT NULL,
  `BuildDate` datetime(6) NOT NULL,
  `LastCollectDate` datetime(6) NOT NULL,
  `PlayerId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `buildings`
--

INSERT INTO `buildings` (`Id`, `BuildingType`, `XCoordinate`, `YCoordinate`, `Level`, `BuildDate`, `LastCollectDate`, `PlayerId`) VALUES
(1, 0, 7, 8, 1, '2023-04-14 19:07:21.294214', '2023-04-14 19:07:21.294301', 5),
(2, 0, 12, 6, 1, '2023-04-14 19:08:04.671543', '2023-04-14 19:08:04.671546', 6),
(3, 0, 8, 5, 1, '2023-04-14 19:08:40.788280', '2023-04-14 19:08:40.788283', 7),
(4, 0, 9, 7, 1, '2023-04-14 19:09:23.049349', '2023-04-14 19:09:23.049352', 8),
(5, 0, 7, 8, 1, '2023-04-14 19:10:12.061088', '2023-04-14 19:10:12.061090', 9),
(6, 0, 5, 7, 1, '2023-04-14 19:10:53.110989', '2023-04-14 19:10:53.110993', 10),
(7, 0, 5, 7, 1, '2023-04-14 19:11:52.692326', '2023-04-14 19:11:52.692328', 11),
(8, 2, 10, 9, 1, '2023-04-14 19:11:57.832247', '2023-04-14 19:11:57.832250', 11),
(9, 0, 8, 5, 1, '2023-04-14 19:12:37.742518', '2023-04-14 19:12:37.742520', 12),
(10, 2, 7, 11, 1, '2023-04-14 19:12:40.713399', '2023-04-14 19:12:40.713401', 12),
(11, 0, 12, 2, 1, '2023-04-14 19:13:11.950374', '2023-04-14 19:13:11.950377', 13),
(12, 2, 9, 7, 1, '2023-04-14 19:13:16.118122', '2023-04-14 19:13:16.118125', 13),
(13, 0, 7, 8, 1, '2023-04-14 19:14:04.919886', '2023-04-14 19:14:04.919888', 14),
(14, 2, 16, 8, 1, '2023-04-14 19:14:08.155982', '2023-04-14 19:14:08.155985', 14),
(15, 0, 6, 3, 1, '2023-04-14 19:14:51.786542', '2023-04-14 19:14:51.786545', 15),
(16, 2, 10, 9, 1, '2023-04-14 19:14:54.181566', '2023-04-14 19:14:54.181569', 15),
(17, 0, 8, 5, 1, '2023-04-14 19:15:30.202688', '2023-04-14 19:15:30.202690', 16),
(18, 2, 17, 12, 1, '2023-04-14 19:15:33.567788', '2023-04-14 19:15:33.567790', 16),
(19, 0, 9, 7, 1, '2023-04-14 19:16:12.096668', '2023-04-14 19:16:12.096671', 17),
(20, 2, 15, 4, 1, '2023-04-14 19:16:14.505601', '2023-04-14 19:16:14.505604', 17),
(21, 0, 7, 8, 1, '2023-04-14 19:17:13.131412', '2023-04-14 19:17:13.131415', 18),
(22, 2, 16, 8, 1, '2023-04-14 19:17:16.004091', '2023-04-14 19:17:16.004093', 18),
(23, 0, 9, 7, 1, '2023-04-14 19:17:53.515495', '2023-04-14 19:17:53.515498', 19),
(24, 2, 13, 10, 1, '2023-04-14 19:17:55.575678', '2023-04-14 19:17:55.575681', 19);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `exchanges`
--

CREATE TABLE `exchanges` (
  `Id` int(11) NOT NULL,
  `Item` int(11) NOT NULL,
  `Amount` int(11) NOT NULL,
  `ReplacementItem` int(11) NOT NULL,
  `ReplacementAmount` int(11) NOT NULL,
  `PublishDate` datetime(6) NOT NULL,
  `PlayerId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `notifications`
--

CREATE TABLE `notifications` (
  `Id` int(11) NOT NULL,
  `Title` longtext NOT NULL,
  `Message` longtext NOT NULL,
  `PlayerId` int(11) DEFAULT NULL,
  `Coins` int(11) NOT NULL DEFAULT 0,
  `CreateDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000',
  `Experience` int(11) NOT NULL DEFAULT 0,
  `Irons` int(11) NOT NULL DEFAULT 0,
  `Stones` int(11) NOT NULL DEFAULT 0,
  `Woods` int(11) NOT NULL DEFAULT 0,
  `IsOpened` tinyint(1) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `notifications`
--

INSERT INTO `notifications` (`Id`, `Title`, `Message`, `PlayerId`, `Coins`, `CreateDate`, `Experience`, `Irons`, `Stones`, `Woods`, `IsOpened`) VALUES
(1, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 1, 100, '2023-04-13 00:38:26.881701', 100, 100, 100, 100, 1),
(2, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 2, 100, '2023-04-14 19:01:35.096289', 100, 100, 100, 100, 1),
(3, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 3, 100, '2023-04-14 19:03:46.905809', 100, 100, 100, 100, 0),
(4, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 4, 100, '2023-04-14 19:03:58.211406', 100, 100, 100, 100, 0),
(5, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 5, 100, '2023-04-14 19:04:33.316458', 100, 100, 100, 100, 0),
(6, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 6, 100, '2023-04-14 19:05:38.768296', 100, 100, 100, 100, 0),
(7, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 7, 100, '2023-04-14 19:06:15.008998', 100, 100, 100, 100, 0),
(8, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 8, 100, '2023-04-14 19:06:55.266273', 100, 100, 100, 100, 0),
(9, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 9, 100, '2023-04-14 19:07:51.504472', 100, 100, 100, 100, 0),
(10, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 10, 100, '2023-04-14 19:08:22.872466', 100, 100, 100, 100, 0),
(11, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 11, 100, '2023-04-14 19:09:24.021649', 100, 100, 100, 100, 0),
(12, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 12, 100, '2023-04-14 19:10:11.274661', 100, 100, 100, 100, 0),
(13, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 13, 100, '2023-04-14 19:10:51.731943', 100, 100, 100, 100, 0),
(14, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 14, 100, '2023-04-14 19:11:29.141562', 100, 100, 100, 100, 0),
(15, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 15, 100, '2023-04-14 19:12:22.442737', 100, 100, 100, 100, 0),
(16, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 16, 100, '2023-04-14 19:13:06.580931', 100, 100, 100, 100, 0),
(17, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 17, 100, '2023-04-14 19:13:44.556917', 100, 100, 100, 100, 0),
(18, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 18, 100, '2023-04-14 19:14:49.941422', 100, 100, 100, 100, 0),
(19, 'Gratulálunk a sikeres sziget választásodhoz - ajándékokkal!', 'Kedves Islander,\r\n\r\n                Gratulálunk, hogy sikerült kiválasztanod a tökéletes szigetet, hogy kezdetét vegye a varázslatos kalandod! Reméljük, hogy élvezed az élményt, és izgalommal várod, hogy felfedezd a szigeted rejtelmeit.\r\n                Az indulásodhoz szeretnénk néhány hasznos tárggyal segíteni, amelyek megkönnyítik az első lépéseket a szigeten.\r\n                Az Islanders csapata mindig itt van, hogy támogasson téged az utazásod során, és segítsen minden kérdésben vagy problémában. Ne habozz megkeresni minket bármikor, ha szükséged van rá!\r\n                Köszönjük, hogy velünk tartasz a kalandban, és jó szórakozást kívánunk a szigeten!\r\n\r\n                Üdvözlettel,\r\n                Az Islanders csapata', 19, 100, '2023-04-14 19:15:30.131769', 100, 100, 100, 100, 0);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `players`
--

CREATE TABLE `players` (
  `Id` int(11) NOT NULL,
  `Experience` int(11) NOT NULL DEFAULT 0,
  `Coins` int(11) NOT NULL,
  `Woods` int(11) NOT NULL,
  `Stones` int(11) NOT NULL,
  `Irons` int(11) NOT NULL,
  `SelectedIsland` int(11) NOT NULL,
  `LastExpeditionDate` datetime(6) NOT NULL,
  `LastBattleDate` datetime(6) NOT NULL,
  `Strength` int(11) NOT NULL,
  `Intelligence` int(11) NOT NULL,
  `Agility` int(11) NOT NULL DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `players`
--

INSERT INTO `players` (`Id`, `Experience`, `Coins`, `Woods`, `Stones`, `Irons`, `SelectedIsland`, `LastExpeditionDate`, `LastBattleDate`, `Strength`, `Intelligence`, `Agility`) VALUES
(1, 100, 100, 100, 100, 100, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 9, 5, 6),
(2, 2500, 1000, 1000, 1000, 1000, 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 7, 8, 5),
(3, 2500, 1000, 1000, 1000, 1000, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 4, 6, 10),
(4, 2500, 1000, 1000, 1000, 1000, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 9, 5, 6),
(5, 3700, 530, 670, 900, 780, 3, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 10, 6, 10),
(6, 3800, 600, 900, 670, 780, 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 7, 8, 5),
(7, 3800, 600, 670, 780, 900, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 4, 6, 10),
(8, 5100, 550, 780, 900, 670, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 13, 5, 13),
(9, 5200, 530, 670, 900, 780, 3, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 13, 6, 13),
(10, 5200, 600, 900, 670, 780, 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 13, 8, 13),
(11, 6700, 200, 750, 320, 540, 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 15, 8, 15),
(12, 6800, 180, 320, 540, 750, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 15, 6, 15),
(13, 6800, 130, 540, 750, 320, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 15, 5, 15),
(14, 8400, 80, 320, 750, 540, 3, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 18, 6, 18),
(15, 8600, 200, 750, 320, 540, 0, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 18, 8, 18),
(16, 8600, 180, 320, 540, 750, 1, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 18, 6, 19),
(17, 10000, 1130, 1540, 1750, 1320, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 20, 5, 20),
(18, 11200, 1080, 1320, 1750, 1540, 3, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 20, 6, 20),
(19, 11200, 1130, 1540, 1750, 1320, 2, '0001-01-01 00:00:00.000000', '0001-01-01 00:00:00.000000', 21, 5, 20);

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `users`
--

CREATE TABLE `users` (
  `Id` int(11) NOT NULL,
  `Username` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `PasswordHash` longblob NOT NULL DEFAULT X,
  `PasswordSalt` longblob NOT NULL DEFAULT X,
  `EmailValidationToken` longtext NOT NULL,
  `EmailValidationTokenExpiration` datetime(6) NOT NULL,
  `EmailValidationDate` datetime(6) NOT NULL DEFAULT '0001-01-01 00:00:00.000000'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `users`
--

INSERT INTO `users` (`Id`, `Username`, `Email`, `PasswordHash`, `PasswordSalt`, `EmailValidationToken`, `EmailValidationTokenExpiration`, `EmailValidationDate`) VALUES
(1, 'fireball', 'fireball@fireball.hu', 0x42c0ad9dcf7f2bf9d73d11823b25b584702fcdac703e9381e40c2cd0c6a72af849eb2cbff30935ef70f7b9870adcb3df2d06461c2796cd2ba518aeaa6e3bdcd4, 0x684e2cba4bfda318057bd6a0381eead6bf01a22950a692adfa42200175efccd3bca6eb217d7f55533f7ce920f5f12ab7915044441e19d5591791b6315983e454f10cdd80b08a078b90e393ed12791cea6fc815c36304ff4bffc347d379c896b5a473b280e7f5fa7bfb216bd19ecb74fd41f85581138d190e2b852bbbdde02452, '0CE5572D002FEA35336E1399A195FBF3CFD84B9F131D1A675DF0A34258CE62EB5A628F6DF7F5DF0D00E803F4F05DEC6AEB8301BC87FA3F762C9C0DD260EF3202', '2023-04-13 00:47:35.581648', '2023-04-13 00:48:35.581648'),
(2, 'Fernando', 'fernando@islanders.com', 0x4478863f2b9ff9aadfd7af160f10a0a36dd48534746f96c9ddb462f5aee66c47485c8ba01a399a8426b3ba86dfb548d9d20878a88e2bfa58bdc5c9b4c3ef6653, 0x12c34007a5f54988b3e5a8ade2ccb3f68acf8ea403b1578908ac0cfe5f588fd6b38893acd5a7a92f19bf2a70b0ca456df5ac7a7989b39e41a4dd10f04dbc4218dfe4485cf563be843dd348a84334f0f581c23b2184c03d7b88cdeedb60c54d4a27232e57d3fcc02d80b3d382db66bf756d43b2440628105b4ac48f027beacd00, '41D233C5334E37443D4D53C129DA89453A585299B5BF9D9F3CC9C76DDC94A66FBF7FCD90D791B3F71E2596C80B6C85E89068A3F8DFCC9E150454E426466B365A', '2023-04-14 19:03:04.982303', '2023-04-14 19:04:04.982303'),
(3, 'Frank', 'Frank@islanders.com', 0x2e3feea1a06a02d6474a3847f0df1e3cda0ccb8f258e8349bce89148f968046d90a15d6fadd48f4eb9de0365eeb1168a35e24d9d46b21c2c76100e7f736d5a63, 0x85fe3cc73bc1dcb2720e138d5f34149d05c7864b926bfc2faf65b4a9f730d22b023339029d030b8ee14878bbeb940f009b47059021d01cf27275eb1cbcc93c6bb21ca8057cfe31519e6c53d29d1ad056c5a00d92f59f3394294726240948ce05606cc463afe7a2dc36093dbd6a824df3b9128b95e692ac36e23e2b86ef347d89, '1FFD0802EE67A981237B2E28CCC507E7623DC842089BC76438E53D415235B740AE7F3AAA15599A004E63B87620342D5A46463EA14B621572633090D05E15B0EB', '2023-04-14 19:03:24.979589', '2023-04-14 19:04:04.982303'),
(4, 'Ildiko', 'ildiko@islanders.com', 0x3832a67a442b4d410fbdc1c56804c00356de1a44371eab4df28c7c26df763715e24f2ccd8021a0cd01d1eb72e39352870e00581039aefdf47f32c9e060d3bf3b, 0xe0eb9575f9a752c35d63cc0a99bf3b3cb248d05f4abf511155fe37580f77672e622cc4a78fe9a41865d423ac36ca57b6972f4c4d58db3064a861cc7ad240f36bcef93d59debfd5f69b596c2037bcd34bc4950506f4d2a12be230830d974ed77efd167f551517cf1a50011652eee44eb610e42aaa01140c67a6314daca572a222, '8001B1B28490F763D49F996439F2D6BC8B9EFF40EAFF37F3F1BCD35C52D0F28F96720660A2080E790D5D441DC3614C06A2C6387372300BF9D121ADAAC2F1F65D', '2023-04-14 19:03:38.849121', '2023-04-14 19:04:04.982303'),
(5, 'Edit', 'edit@islanders.com', 0xa4605e9cca93c2406ba3e93a7e95ab518fe95ccccc0c9b4d87a051018db8f380243075fbf4961821e3f5a276bc3d1651ccad5f1c50cc723a904c73b5f3af888c, 0x3db255328506ea0746a8a85b484813d57c269b1d37916e69ceee55a86e9f3c10d0b85e477b78a90cb89b336e9af3b80996d59b90dbf94cc7e9a1817945ce67f3279bc29c854792d5b7b460ae27275348be98370d9f1506020dea51da66087e755bad6a3e399c08c4b9a874983b9774f6de9862ad11d02c6c1348d3bed2743977, 'D1F2A5D95DAA2B28E8B347DA78248465377915697FE52DDAF57C052B8998E8F0FCDA9F2CEB5C4565B350A0C5DFD10378C9D5F8AD029D32F428F9DECD5373AE8C', '2023-04-14 19:03:52.307471', '2023-04-14 19:04:04.982303'),
(6, 'Janos', 'janos@islanders.com', 0x47a00e76073cf60839e605d2f22ea2fedd07232710a51368a0d06c2d9cca1a5eced8459e28baca6de829b790e9c5e7f02dc666d089269bc70529b2ce1ae38f52, 0xb34aea83f8d94e8461cdc8f8b8403e85a5320716e2293b6a968fa43d3b8556981838069584c95b6f9847ff42842016f6426d72623e503d044ffe0f77b407bda3b3dbe7125f87200578905a665ba252fc7b8835c28e159696401baaaab8c15ab33671540880aa9860c733e39a7f85bcd0149f9a77ab9c4d17778bb0107b83f6d6, 'F3E6821FB94D8C2AA38637374AD588A980E286DB0A3CA242F293CB93C1A95629D264E1188653077DC15C5E0A43EBFB67F7D150B23BDF742DDD5790C365385E57', '2023-04-14 19:04:03.588383', '2023-04-14 19:04:04.982303'),
(7, 'Megaman9', 'megaman@islanders.com', 0x6c24f34af63973c0cd19ee75f2e501265268357db63dff02252dc4be58dd33e6c99ae78ab7324b05b7075a67db92a130b50a61af290b9c33a28fd667f198b7e9, 0x1aa25da3839c90d2c02545cf6dbc0742b28b44761210e55b02209fef17a07606e0b30fa1d1b8b52df931912f486aaeed1d522053ee4df5cd9200a76f7607d66c8522aea3020134b9c36e8600e6079c8445c137c67946b2362baa5432bd968d0a593094614597d357e102f080af98081c9cbf6c3b2bad10bfd6d35105cf9fd3d0, 'D22B883C9FC16AB1EDB4AE38229BA89D24DB8CDC85BB38132B97E5D08143C1B30F64728F1E8B224BDB9D4963140A361062B4E98896AB9EE6EEA3C133638BABF1', '2023-04-14 19:04:19.039785', '2023-04-14 19:05:04.982303'),
(8, 'TotallyNotABot', 'totallynotabot@islanders.com', 0xb08dfa9a4548d6893a4f8e5132929b35d826316e4683a9acb766e24d5c0c8a8880b0c02158a3aea1e8ef8a9ab262f15b5788f4051d7d1da4543b08d691b2d47d, 0xd3bd4a50acd62074f7a04bbb1f2302dbb44fff2de3527198c8a91c28d5b944f3cf79a3b017cac670b11b0f4cc0bc3a7f93bf134f10c6db0f655716ab730d39fdff6a10c87db3f0dd15212c39b2601d015732a07ad2e3de71aa35ef23ef168ee95a6fae60b21dce72512ba0a3f7911b5961d0102fcab986f31cba64ee7c12f7a1, 'DA8DEAD415CFF6A557A129DFCDEF3FCD0D165A53D6C543C7C8F379B57AD5072AE03E27E13654EA637F08BACC0097CE2C384B4D8D2FFD79A23DAE9D9BF554C436', '2023-04-14 19:04:35.260224', '2023-04-14 19:05:04.982303'),
(9, 'Borban', 'borban@islanders.com', 0xaeca9ce74370ba1b8024a1d76cbc16e3656ae63bf0ae1a46854fb858bc4c8c821996845ba32bd915950e546eaf4e4194cce15f1477a4d67ec22cab2139a50e50, 0x3d88d84f8a17c85d28d5aa96a78c9dc5a5f47d0faeaa13557efb95fcf5c2c0774ad9f87e515182f9ca3eb9dcb24b3f004a4a30e221483d715f66b714f4aad39865c2ca705dfbfddd5fbf79853b2f0c38cee7982737d37d0386aa0503adce4e26e1349684d7aca96539a6caaecfe49e0d691d9b079cd477186eaf82c23f60fdd8, '871FF189FE252DE19381D16AC7CC54F13888F8EC28C94DF24CF7FBC7597B390748EE7485593A17E0B6A02155045994A4ABFDA8CE64EF012618116B6EEF927743', '2023-04-14 19:04:58.397385', '2023-04-14 19:05:04.982303'),
(10, 'Biatorbagy', 'biatorbagy@islanders.com', 0x451ee921907bcff87c48d684b88eeb2969d20273786f285d4e75c040666a74b2ed96d320470633d8649104ae5ea41eb86dd3906deafdf9be042c9ac69ba575cc, 0x02d62ebc917ce930529da9e22e21f0f02ecae1bfe53a6699b99955b974bab2f95d174cbf22a2f8be6f8d06959b6f5e2f66fee34bf18132ead5d30338b2b32ec0fdb2f6259f38d90da042bb8c0cc3cb2ab972a402523df8cf079b0746fe959da0d380b27b663b3baeaec34b9fb9b5928349fbccf79bf43216c9dce8a6eea79e76, 'BD174369C663AB793E839DB3AAD6DA900DF16259A954371B3667E2D37C4C51A83F3654B4F36512E942360DFD27B0E527F7CA21C0D6851C0B798936B2EF3B73DF', '2023-04-14 19:05:19.043481', '2023-04-14 19:06:04.982303'),
(11, 'NotBot1', 'notbot1@islanders.com', 0x2c4b5a437e4ed9151e831f713f7ecad97b7ceabb355ffe23f417a7716fadd98c76d490b36b28f9593435fc7b2f2e2300c79d2e81228e191476b32786dda3508f, 0xd855ce55a1faf945aa516785ebe7ac2d1d74e512c8c61b0f53b95d48aeef880427cf62172e59e25c4aeebb1f251e9b8d6f8c644cd23e04b01ce61d457b0cafd4405d890001ede658b5d7f0c7e344b5d981a6a76a29445ba702ebccaa1e309f427827d6686278583140b5a457f55dd6641996f7f6d97b4b188f4ec03d744bbcaa, '5B06F284A0BC4F71881EA15378CEF9571E5E12F808444A7BAD6D84D7D2482CB65C2A2157F07DAF8DE1CA7FF1A1FAD4DC93B53DCB6D578FF8F3851F54E2DE22EC', '2023-04-14 19:05:32.414255', '2023-04-14 19:06:04.982303'),
(12, 'Kissandi', 'kissandi@islanders.com', 0x2074afa6dfe1305ef13f09eda3869b5a4a569c08c46721070d7f71c7c76e5f0d8bcc928b380a122df429fe131230572339693fda905225540d078aa1bec75037, 0x525a399060abe36b9aff0e0962f458a4def299a71d2934d0476c425ee935883e60f3040704d17d62d4852d69aea894de864641cb60a48f4e7e4933a4e07bbfc590e764e265eebee9caed9d487f1827d0f9fe76a859847b766889091238081a62da7f485c6f9d32fb0ed509c8c9cbec7ce2303e0d339eb2f098592552648fd2d9, 'DCC9CC85929623B7348BB2018C601A2352B64B37FED15F84891D76AA9281CD7C3287EEC87869E3A92B7AA9CE967D2D4FC55DDA626941DD54E36B3C6D03E2AF16', '2023-04-14 19:05:49.960416', '2023-04-14 19:06:04.982303'),
(13, 'tubby', 'tubby@islanders.com', 0x200df8837f5ca1253f6216a85ea2b924f8b56ee42cf2bedf9a8e62110e349fa51acd1f5ca02738516553d4d1098515044eae54f71e7d9b6061bf8b2b031610f0, 0xade10e52af0242ffa6a5c275a4536ba45dc6e3079bbad0ff21be5ed75b4d7d39ad86c7e5931064e7cce9a1690f7e0e6eaf93b05674c435e0aa094f057a31ef74402c3e5991e602e57536cd9fb1fb413c156eef3c34a4ff3cde3230d6416c889912e96c9e9035501a8312a7c943654394a59ccff315e83131bfce5c3330cded17, '3F455BED9A6E71C30CA96F8FE3829BA263B40A95BB7C5EFBB42C9C2516E73E50E39A00BD5FD50E98E506278CE5A05AF3E0CBA0DFDAFB64B02882317993B6D515', '2023-04-14 19:06:19.594597', '2023-04-14 19:07:04.982303'),
(14, 'Norbert', 'norbert@islanders.com', 0x1944b3b8d794865ec436b5d6585b0eafad68f349e623d18ca2f40d542741a285209967a3ce43b2cb76eacf8dd009b4de0a2f2c9bcee18f3932536156d8fe8254, 0xd843113dd13e6d1df6e8f50b1664a66d0a68f6c4a607b7295f65540d51e93c9df02f7f8c621acf4fa9c26e3e633204933b78c5ec663fd18dd88de220d0660be504c9d42197c7f73316a426b12bfe21dc9d3b50300570328eb53cbd42047a15f846bdef54d61c431165458881903a8366491913c175e25954ea697f281b2a9378, 'AB94E271B909C13A81147EA036986570F29735991A6A38252104BA7624863B9F68E49C6305F3AD7152322C6DA2AA9C1B439ACA437BC10EB0AB8CB8D9E690194C', '2023-04-14 19:06:29.418501', '2023-04-14 19:07:04.982303'),
(15, 'balazs', 'balazs@islanders.com', 0x19bf2d71afa6c360526358f50d766496fc89797c9a7592c9fd1eeca142b6a76469c807370266188d2202f2f32bdf80852299e4e33cec5808a5f0240c86523a63, 0xb9316e814da6b5efbeb804d54167618a51254c59d57e5825cc300a799edeeb18cfa8031cc464b9257c2f1dfb2b93b5972a3aa4ee81bffef7c580ac2544598b8e784745df47fd85caf78b171ff7c37ba9305d881dd4c5e0c6d8395a07e1dd65fef9bfd3815a11d1b3dc6cfaf0101d6966c604caa55f019dc4ef089cd6ce993104, 'E0A959AB8093DFC072FC3D3312690627BCCA486921986F44F5ABDF29331676F6342FA2B188755FFE569C50FA56179D1F9805CE8965C3CF78615739FEEB0BF47F', '2023-04-14 19:06:39.755157', '2023-04-14 19:07:04.982303'),
(16, 'Mutans99', 'mutans99@islanders.com', 0xb35d5e887d646f8c3e430157706da233d627f2427d59cad2e6a20903a377c274b9233d12d11a08c63fe519f36ee2808c444b6223deb434a5b66c1667a4fca654, 0x601fa7d2e231d749c638be17692040e0ba3084769d865b55b48c1bf5136ac2243434406f8de239a27b5453c568cd5f3ea390d4b91b8b931e271853fb6c433f48ac10f45980aad8ce9849f0fe585179e530810d51d8c51a7b50b270c08f57a6a167fbd92c51058c6961d54f9c7f098a94807d069676428eddd8527a19120cd65f, '87D76DEA8A1829EA81AA3605B6BB602E6B9FB1837F92229F8111DFF0B6B7CD2C23D71DBF613CEB36DC3C582B3883311F64D091C41E0B6B504F0307336C0CA507', '2023-04-14 19:06:54.455974', '2023-04-14 19:07:04.982303'),
(17, 'Kotelezo', 'kotelezo@islanders.com', 0x4c52bae2329dd6de510ec2ea25fc4ac4e833e663c4a1da3f85498af1d7c566a7c7c11c74bb289fd32b26056b43cc16de005bb4a8e0bf9ede618cfc5bafd8e3b2, 0xa67242de02a3d1ab5d42829780bfa44751c7c7832f5013ec163e6540212965e907b8338d1e17e89a11a0abccec80a000287093bda9bc2270c9343de77ca6e623cc14d555f2d4ed74d1b7d760f86f6121efe76afff0b3b2a1c4102c560fa8f622583aa9969152c4d674a6b0c1ee62def637381435e2707d085e6c0d2fc08ecfbc, 'FE3FB4AD4C7EFF58573845F2DD8DDAD1EFC1E3B2B1E6C24F62266CDEFE192FED9F29A39FF991BB146121C63A99ED67CD1E1C0F75D0F2AC794610B403A2004DE0', '2023-04-14 19:07:09.093206', '2023-04-14 19:08:04.982303'),
(18, 'JoeMama', 'joemama@islanders.com', 0x5ead31ec6935185f9ffe071a0dd24f37a9b05ae7b4bef70c64f884d050294c7f2a00222d64e2c92e902789c77aacf1b15f286e8e5fda7bb01a47007810c5d1b9, 0x6461da4d6435ac7723c0750be6a8cf17072d38c221c7268ed9bd9917c7fa350f92ea6b92f7313ae9f501e9789ed09305433ae06544c7ecb02eba22e58af2d383145ec406a1c231f0c73f9ba2c5d21850252fda612f3b16dfa94073a45f2befadb9903efad095c56e3c2fa2da852fdd9ff6eefc8b42c609ebafb7f7cdc432b562, '7EE2C60985620472E35A483A879CD9F97765EADB8ACB886C5D5FC52578B78A6C40C494E6F0C1AF9355ECA5BC876AAD147159D29296ED43BEE1825B25C5FC844B', '2023-04-14 19:07:19.980097', '2023-04-14 19:08:04.982303'),
(19, 'ChangedLater', 'changedlater@islanders.com', 0x21b7dfc65cb81d60e080281ee58ae3037e8697632a1e054be0570cf6c9ddba10303d4fee45a1cfd34ea966af419809b3ec6f18be3733e52834c1a9f8eced70d8, 0x417321df744f7c4621cef03e81b2ef7c96123a4c838bf141e7c5b7ee2d5ec5a4d9ccd4609d820df8408b2885772b5c0369a3d9de0842fcb16bdd34af3697c307be7eda18048d09034dc6b3032e69aee47b6f7184200807e97d64337f81225a49aee3f6fc46a461433888ab6a529a81e92c4db0845213a83dcc0185b8fc0f32da, 'C4E65B9BAADC5A1AC36AB159C0DA77683942AB68D587EC47C5D412A4F35877E8F6409C0BD4295E1FFDED5B9B722C21551AEF0DB577F6B383ABEFA0F287BF6EFB', '2023-04-14 19:07:32.593391', '2023-04-14 19:08:04.982303');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- A tábla adatainak kiíratása `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20230210233755_sql-server-init', '6.0.12'),
('20230211001041_role-fix', '6.0.12'),
('20230211004142_role-fix2', '6.0.12'),
('20230211170350_classified-ad-remove', '6.0.12'),
('20230214004047_player-table-update', '6.0.12'),
('20230302020905_x', '6.0.12'),
('20230321233858_btype', '6.0.12'),
('20230405195611_notification', '6.0.12');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `buildings`
--
ALTER TABLE `buildings`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Buildings_PlayerId` (`PlayerId`);

--
-- A tábla indexei `exchanges`
--
ALTER TABLE `exchanges`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Exchanges_PlayerId` (`PlayerId`);

--
-- A tábla indexei `notifications`
--
ALTER TABLE `notifications`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IX_Notifications_PlayerId` (`PlayerId`);

--
-- A tábla indexei `players`
--
ALTER TABLE `players`
  ADD PRIMARY KEY (`Id`);

--
-- A tábla indexei `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`Id`),
  ADD UNIQUE KEY `IX_Users_Email` (`Email`),
  ADD UNIQUE KEY `IX_Users_Username` (`Username`);

--
-- A tábla indexei `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- A kiírt táblák AUTO_INCREMENT értéke
--

--
-- AUTO_INCREMENT a táblához `buildings`
--
ALTER TABLE `buildings`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=25;

--
-- AUTO_INCREMENT a táblához `exchanges`
--
ALTER TABLE `exchanges`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT a táblához `notifications`
--
ALTER TABLE `notifications`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT a táblához `users`
--
ALTER TABLE `users`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `buildings`
--
ALTER TABLE `buildings`
  ADD CONSTRAINT `FK_Buildings_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`Id`);

--
-- Megkötések a táblához `exchanges`
--
ALTER TABLE `exchanges`
  ADD CONSTRAINT `FK_Exchanges_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`Id`);

--
-- Megkötések a táblához `notifications`
--
ALTER TABLE `notifications`
  ADD CONSTRAINT `FK_Notifications_Players_PlayerId` FOREIGN KEY (`PlayerId`) REFERENCES `players` (`Id`);

--
-- Megkötések a táblához `players`
--
ALTER TABLE `players`
  ADD CONSTRAINT `FK_Players_Users_Id` FOREIGN KEY (`Id`) REFERENCES `users` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;