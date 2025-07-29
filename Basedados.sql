-- --------------------------------------------------------
-- Anfitrião:                    hosting21.serverhs.org
-- Versão do servidor:           10.3.34-MariaDB - MariaDB Server
-- SO do servidor:               Linux
-- HeidiSQL Versão:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

CREATE TABLE IF NOT EXISTS `base_liquidacoes` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'sequencial, id unico',
  `cp_ae` int(11) NOT NULL,
  `nome_ae` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
  `estado` int(11) NOT NULL DEFAULT -1,
  `razao_interdicao` int(11) NOT NULL DEFAULT -1,
  `data_bloqueio` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `saldo_contas_cliente` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `entidade` int(11) DEFAULT -1,
  `cpae_liqui` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `ae_liqui` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `estado_liqui` int(11) DEFAULT -1,
  `saldo_conclusao_liqui` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `oficio_rgi` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `estado_notificacao` int(11) DEFAULT -1,
  `processos_crime_CDAJ` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `processo_inquerito` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `constar_site` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
  `obs_importante` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `prox_tarefas` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  `prox_resp_tarefas` varchar(255) CHARACTER SET utf8mb4 DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


CREATE TABLE IF NOT EXISTS `Login` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'sequencial, id unico',
  `user_id` varchar(255) CHARACTER SET latin1 NOT NULL COMMENT 'será usuario',
  `password` varchar(255) CHARACTER SET latin1 NOT NULL COMMENT 'senha do usuario',
  `last_login` datetime DEFAULT NULL COMMENT 'data do ultimo acesso',
  `last_ip` varchar(255) CHARACTER SET latin1 DEFAULT NULL COMMENT 'ultimo IP de onde o usuario se ligou, segurança [IP exterior / IP rede]',
  `admin` int(11) DEFAULT 0 COMMENT 'usuario é administrador 0/1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;


/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
