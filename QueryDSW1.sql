USE DSWGrupo01;

-- TABLA: Rol
CREATE TABLE Rol (
    Id_Rol INT IDENTITY (1,1) NOT NULL,
    Nombre NVARCHAR(50) NOT NULL,
    PRIMARY KEY (Id_Rol)
);

INSERT INTO Rol (Nombre) VALUES
('Administrador'),
('Cliente');


-- TABLA: Usuario
CREATE TABLE Usuario (
    Id_Usuario INT IDENTITY (1,1) NOT NULL,
    Id_Rol INT NOT NULL,
    Nombre VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    Contrasenia VARCHAR(100) NOT NULL,
    Dni CHAR(8) NOT NULL,
    Direccion VARCHAR(150) NOT NULL,
    Telefono CHAR(9) NOT NULL,
    Fecha_Registro DATETIME DEFAULT GETDATE() NOT NULL,
    PRIMARY KEY (Id_Usuario),
    FOREIGN KEY (Id_Rol) REFERENCES Rol(Id_Rol)
);

INSERT INTO Usuario (Id_Rol, Nombre, Email, Contrasenia, Dni, Direccion, Telefono)
VALUES
(1, 'Admin Prueba', 'prueba@admin.com', 'abc123', '12345678', 'Av. Lima 123', '987654321'),
(2, 'Estrella García', 'estrella@correo.com', 'abc123', '87654321', 'Jr. Sol 567', '912345678'),
(2, 'Oswaldo Pérez', 'oswaldo@correo.com', 'abc123', '22334455', 'Calle Luna 345', '934567812'),
(2, 'Ronald Torres', 'ronald@correo.com', 'abc123', '55667788', 'Av. Mar 889', '945612378'),
(2, 'Franco Cayetano', 'franco@correo.com', 'abc123', '11223344', 'Jr. Estrella 990', '956123478');


-- TABLA: Vinilo
CREATE TABLE Vinilo (
    Id INT IDENTITY (1,1) NOT NULL,
    titulo NVARCHAR(150),
    artista NVARCHAR(150),
    anio DATE,
    precio DECIMAL(10,2),
    stock INT,
    preview NVARCHAR(300),
    descripcion NVARCHAR(300),
    fecha_ingreso DATETIME,
    imagen_url NVARCHAR(MAX),
    PRIMARY KEY (Id)
);

INSERT INTO Vinilo (titulo, artista, anio, precio, stock, preview, descripcion, fecha_ingreso, imagen_url)
VALUES
('Thriller', 'Michael Jackson', '1/01/1982', 120.50, 10, 'http://dojiw2m9tvv09.cloudfront.net/69046/product/default1736899652259430.jpg', 'Vinilo clásico de MJ', '1/01/2010 00:00:00', 'http://dojiw2m9tvv09.cloudfront.net/69046/product/default1736899652259430.jpg'),
('Back in Black', 'AC/DC', '1/01/1980', 110.00, 8, 'https://dojiw2m9tvv09.cloudfront.net/69046/product/default2110.png', 'Rock legendario', '1/01/2010 00:00:00', 'https://dojiw2m9tvv09.cloudfront.net/69046/product/default2110.png'),
('The Dark Side of the Moon', 'Pink Floyd', '1/01/1973', 150.00, 5, 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png', 'Álbum icónico', '1/01/2010 00:00:00', 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'),
('Abbey Road', 'The Beatles', '1/01/1969', 140.50, 12, 'https://vinilos.pe/wp-content/uploads/2022/07/BEATLES-ABBEY.png', 'Clásico de The Beatles', '1/01/2010 00:00:00', 'https://vinilos.pe/wp-content/uploads/2022/07/BEATLES-ABBEY.png'),
('Nevermind', 'Nirvana', '1/01/1991', 130.00, 7, 'https://vinilos.pe/wp-content/uploads/2020/02/ab67616d0000b273e175a19e530c898d167d39bf.jpeg', 'Grunge histórico', '1/01/2010 00:00:00', 'https://vinilos.pe/wp-content/uploads/2020/02/ab67616d0000b273e175a19e530c898d167d39bf.jpeg');


-- TABLA: Carrito
CREATE TABLE Carrito (
    Id INT IDENTITY (1,1) NOT NULL,
    Id_Usuario INT NOT NULL,
    fecha_ingreso DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (Id),
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario)
);

INSERT INTO Carrito (Id_Usuario)
VALUES (1), (2), (3), (4), (5);


-- TABLA: CarritoProducto
CREATE TABLE CarritoProducto (
    Id INT IDENTITY (1,1) NOT NULL,
    carritoId INT NOT NULL,
    viniloId INT NOT NULL,
    cantidad INT NOT NULL,
    precio DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (carritoId) REFERENCES Carrito(Id),
    FOREIGN KEY (viniloId) REFERENCES Vinilo(Id)
);

INSERT INTO CarritoProducto (carritoId, viniloId, cantidad, precio)
VALUES
(1, 1, 1, 120.50),
(1, 3, 2, 150.00),
(2, 2, 1, 110.00),
(3, 5, 1, 130.00),
(4, 4, 3, 140.50);


-- TABLA: Venta
CREATE TABLE Venta (
    Id_Venta INT IDENTITY (1,1) NOT NULL,
    Id_Usuario INT NULL,
    Nombre_Destinatario NVARCHAR(100) NOT NULL,
    Email_Destinatario NVARCHAR(100) NOT NULL,
    Telefono_Destinatario NVARCHAR(20) NOT NULL,
    Direccion_Envio NVARCHAR(200) NOT NULL,
    Metodo_Pago NVARCHAR(20) NOT NULL,
    Total DECIMAL(10,2) NOT NULL,
    Fecha DATETIME DEFAULT GETDATE(),
    PRIMARY KEY (Id_Venta),
    FOREIGN KEY (Id_Usuario) REFERENCES Usuario(Id_Usuario)
);

INSERT INTO Venta (Id_Usuario, Nombre_Destinatario, Email_Destinatario, Telefono_Destinatario, Direccion_Envio, Metodo_Pago, Total)
VALUES
(1, 'Raul Lopez', 'raul@example.com', '987654321', 'Av Lima 123', 'Tarjeta', 300.50),
(2, 'Maria Torres', 'maria@example.com', '912345678', 'Jr Sol 567', 'Efectivo', 150.00),
(3, 'Luis Ramos', 'luis@example.com', '934567812', 'Calle Luna 345', 'Tarjeta', 220.00),
(4, 'Jose Perez', 'jose@example.com', '945612378', 'Av Mar 889', 'Yape', 90.00),
(5, 'Ana Diaz', 'ana@example.com', '956123478', 'Jr Estrella 990', 'Plin', 180.00);


-- TABLA: DetalleVenta
CREATE TABLE DetalleVenta (
    Id_Detalle_Venta INT IDENTITY (1,1) NOT NULL,
    Id_Venta INT NOT NULL,
    Id_Vinilo INT NOT NULL,
    Cantidad INT NOT NULL,
    Precio_Unitario DECIMAL(10,2) NOT NULL,
    PRIMARY KEY (Id_Detalle_Venta),
    FOREIGN KEY (Id_Venta) REFERENCES Venta(Id_Venta),
    FOREIGN KEY (Id_Vinilo) REFERENCES Vinilo(Id)
);

INSERT INTO DetalleVenta (Id_Venta, Id_Vinilo, Cantidad, Precio_Unitario)
VALUES
(1, 1, 1, 120.50),
(1, 3, 1, 150.00),
(2, 2, 1, 110.00),
(3, 5, 2, 130.00),
(4, 4, 1, 140.50);
