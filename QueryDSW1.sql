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
(
 'Thriller',
 'Michael Jackson',
 '1982-01-01',
 120.50,
 10,
 'https://eatkxdfnzqpqmtljjxye.supabase.co/storage/v1/object/public/previews/Michael_Jackson_Thriller.mp3',
 'Thriller es el álbum más vendido de la historia. Combina pop, funk y rock con canciones icónicas como Billie Jean y Beat It, consolidando a Michael Jackson como el Rey del Pop.',
 '2010-01-01 00:00:00',
 'http://dojiw2m9tvv09.cloudfront.net/69046/product/default1736899652259430.jpg'
),
(
 'Back in Black',
 'AC/DC',
 '1980-01-01',
 110.00,
 8,
 'https://eatkxdfnzqpqmtljjxye.supabase.co/storage/v1/object/public/previews/ACDC_Back_In_Black.mp3',
 'Back in Black es uno de los discos de rock más influyentes de todos los tiempos. Con riffs poderosos y energía pura, marcó una nueva etapa para AC/DC.',
 '2010-01-01 00:00:00',
 'https://dojiw2m9tvv09.cloudfront.net/69046/product/default2110.png'
),
(
 'The Dark Side of the Moon',
 'Pink Floyd',
 '1973-01-01',
 150.00,
 5,
 'https://eatkxdfnzqpqmtljjxye.supabase.co/storage/v1/object/public/previews/Pink_Floyd_Brain_Damage.mp3',
 'Un álbum conceptual que explora el tiempo, la vida y la locura. The Dark Side of the Moon es una obra maestra del rock progresivo.',
 '2010-01-01 00:00:00',
 'https://upload.wikimedia.org/wikipedia/en/3/3b/Dark_Side_of_the_Moon.png'
),
(
 'Abbey Road',
 'The Beatles',
 '1969-01-01',
 140.50,
 12,
 'https://eatkxdfnzqpqmtljjxye.supabase.co/storage/v1/object/public/previews/The_Beatles_Here_Comes_The_Sun.mp3',
 'Abbey Road es uno de los álbumes más emblemáticos de The Beatles, famoso por su icónica portada y canciones que marcaron la historia de la música.',
 '2010-01-01 00:00:00',
 'https://vinilos.pe/wp-content/uploads/2022/07/BEATLES-ABBEY.png'
),
(
 'Nevermind',
 'Nirvana',
 '1991-01-01',
 130.00,
 7,
 'https://eatkxdfnzqpqmtljjxye.supabase.co/storage/v1/object/public/previews/Nirvana_Smells_Like_Teen_Spirit.mp3',
 'Nevermind redefinió el rock alternativo y llevó el grunge al público masivo, convirtiéndose en un álbum clave de los años 90.',
 '2010-01-01 00:00:00',
 'https://vinilos.pe/wp-content/uploads/2020/02/ab67616d0000b273e175a19e530c898d167d39bf.jpeg'
),
(
 'Hit ’Em Up (Dirty)',
 '2Pac',
 '1996-04-06',
 35.00,
 10,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/2Pac%20-%20Hit%20''Em%20Up%20(Dirty)%20(Music%20Video)%20HD.mp3',
 'Vinilo de hip-hop clásico de los años 90, uno de los temas más influyentes de 2Pac.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab67616d0000b273d81a092eb373ded457d94eec'
),
(
 'Bailé Inolvidable',
 'Bad Bunny',
 '2023-10-10',
 40.00,
 15,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/BAD%20BUNNY%20-%20BAILE%20INoLVIDABLE.mp3',
 'Vinilo de música urbana latina con el estilo moderno y experimental de Bad Bunny.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab67616d0000b273bbd45c8d36e0e045ef640411'
),
(
 'The Scientist',
 'Coldplay',
 '2002-08-26',
 38.00,
 12,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/Coldplay%20-%20The%20Scientist%20(Official%204K%20Video).mp3',
 'Vinilo de rock alternativo con una de las baladas más icónicas de Coldplay.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab67616d0000b273de09e02aa7febf30b7c02d82'
),
(
 'Feid',
 'Feid',
 '2020-01-01',
 32.00,
 20,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/Feid.mp3',
 'Vinilo de música urbana y reguetón del artista colombiano Feid.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab6775700000ee854d2eb38e9cd5faedea8ba62b'
),
(
 'Mi Dolor',
 'Chacalón y La Nueva Crema',
 '1980-01-01',
 45.00,
 8,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/Mi%20Dolor.mp3',
 'Vinilo de música chicha peruana, tema emblemático de Chacalón y patrimonio cultural popular.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab67616d0000b273d5cf7c26dd51abda8051ee8e'
),
(
 'Californication',
 'Red Hot Chili Peppers',
 '1999-06-08',
 42.00,
 14,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/Red%20Hot%20Chili%20Peppers%20-%20Californication.mp3',
 'Vinilo de rock alternativo con uno de los álbumes más exitosos de la banda.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab67616d0000b273a9249ebb15ca7a5b75f16a90'
),
(
 'Tú con Él',
 'Frankie Ruiz',
 '1991-01-01',
 37.00,
 9,
 'https://alqgvomuoufkeoccamjz.supabase.co/storage/v1/object/public/DSWGrupo1/Tu%20Con%20el.mp3',
 'Vinilo de salsa romántica, un clásico inolvidable de Frankie Ruiz.',
 '2010-01-01 00:00:00',
 'https://i.scdn.co/image/ab67616d0000b27368b7273bd630c08dc4a683d8'
);


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
