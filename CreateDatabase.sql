CREATE DATABASE ActividadClase08;
GO 

USE ActividadClase08;

CREATE TABLE Cuenta(
	DNI_TITULAR BIGINT UNIQUE NOT NULL,
	NOMBRE_TITULAR VARCHAR(100) NOT NULL,	
	SALDO DECIMAL(10,2),
	CONSTRAINT PK_CUENTA PRIMARY KEY(DNI_TITULAR)
);

CREATE TABLE Historial_Transferencias(
	DNI_CUENTA_ORIGEN BIGINT NOT NULL,
	DNI_CUENTA_DESTINO BIGINT NOT NULL,
	MONTO DECIMAL(10,2) NOT NULL,
	FECHA DATETIME NOT NULL,
	CONSTRAINT PK_HISTORIAL_TRANSFERENCIAS PRIMARY KEY(DNI_CUENTA_ORIGEN,DNI_CUENTA_DESTINO,FECHA)
);

ALTER TABLE Historial_Transferencias ADD 
CONSTRAINT FK_HISTORIAL_TRANSFERENCIAS_CUENTA
FOREIGN KEY(DNI_CUENTA_ORIGEN) REFERENCES Cuenta(DNI_TITULAR),
FOREIGN KEY(DNI_CUENTA_DESTINO) REFERENCES Cuenta(DNI_TITULAR);

INSERT INTO Cuenta VALUES
(11111111, 'UsuarioDePrueba', 500000),
(31579624, 'Oscar Valentin Gomez', 10000),
(24987126, 'Marta Serena Montiel', 50000),
(48974567, 'Carlos Jose Martinez', 5000),
(12469872, 'Patricia Beatriz Sanchez', 100000);

--SELECT * FROM Cuenta;