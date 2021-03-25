-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema Colegio
-- -----------------------------------------------------
DROP SCHEMA IF EXISTS `Colegio` ;

-- -----------------------------------------------------
-- Schema Colegio
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `Colegio` DEFAULT CHARACTER SET utf8 ;
USE `Colegio` ;

-- -----------------------------------------------------
-- Table `Colegio`.`Alumno`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Colegio`.`Alumno` ;

CREATE TABLE IF NOT EXISTS `Colegio`.`Alumno` (
  `idAlumno` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(25) NOT NULL,
  `Apellidos` VARCHAR(50) NOT NULL,
  `Genero` VARCHAR(25) NOT NULL,
  `Fecha_Nacimiento` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`idAlumno`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Colegio`.`Profesor`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Colegio`.`Profesor` ;

CREATE TABLE IF NOT EXISTS `Colegio`.`Profesor` (
  `idProfesor` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(25) NOT NULL,
  `Apellidos` VARCHAR(45) NOT NULL,
  `Genero` VARCHAR(25) NOT NULL,
  PRIMARY KEY (`idProfesor`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Colegio`.`Grado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Colegio`.`Grado` ;

CREATE TABLE IF NOT EXISTS `Colegio`.`Grado` (
  `idGrado` INT NOT NULL AUTO_INCREMENT,
  `Nombre` VARCHAR(25) NOT NULL,
  `idProfesor` INT NOT NULL,
  PRIMARY KEY (`idGrado`),
  INDEX `fk_Grado_Profesor_idx` (`idProfesor` ASC) ,
  CONSTRAINT `fk_Grado_Profesor`
    FOREIGN KEY (`idProfesor`)
    REFERENCES `Colegio`.`Profesor` (`idProfesor`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Colegio`.`Alumno_Grado`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `Colegio`.`Alumno_Grado` ;

CREATE TABLE IF NOT EXISTS `Colegio`.`Alumno_Grado` (
  `idAlumno_Grado` INT NOT NULL AUTO_INCREMENT,
  `Seccion` CHAR(1) NOT NULL,
  `idGrado` INT NOT NULL,
  `idAlumno` INT NOT NULL,
  PRIMARY KEY (`idAlumno_Grado`),
  INDEX `fk_Alumno_Grado_Alumno1_idx` (`idAlumno` ASC) ,
  INDEX `fk_Alumno_Grado_Grado1_idx` (`idGrado` ASC) ,
  CONSTRAINT `fk_Alumno_Grado_Alumno1`
    FOREIGN KEY (`idAlumno`)
    REFERENCES `Colegio`.`Alumno` (`idAlumno`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Alumno_Grado_Grado1`
    FOREIGN KEY (`idGrado`)
    REFERENCES `Colegio`.`Grado` (`idGrado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
