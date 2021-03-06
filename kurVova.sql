USE [master] 
GO
DROP DATABASE [kur_Vova]
GO
CREATE DATABASE [kur_Vova]
GO
USE [kur_Vova]
GO

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Enum_Attribute_desctription]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Enum] DROP CONSTRAINT [FK_Enum_Attribute_desctription];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Attribute_Types]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [AttributeDescription] DROP CONSTRAINT [FK_Attribute_Types];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Attribute_Attribute_desctription]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Attribute] DROP CONSTRAINT [FK_Attribute_Attribute_desctription];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Attribute_Products]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Attribute] DROP CONSTRAINT [FK_Attribute_Products];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Types_Categories]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Types] DROP CONSTRAINT [FK_Types_Categories];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Products_Delivery]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Delivery];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Products_Manufacturer]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Manufacturer];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Products_Storage]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Storage];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[FK_Products_Types]') AND OBJECTPROPERTY(id, 'IsForeignKey') = 1)
ALTER TABLE [Products] DROP CONSTRAINT [FK_Products_Types];



IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Enum]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Enum];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[AttributeDescription]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [AttributeDescription];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Storage]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Storage];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Delivery]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Delivery];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Manufacturer]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Manufacturer];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Attribute]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Attribute];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Categories]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Categories];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Types]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Types];

IF EXISTS (SELECT * FROM dbo.sysobjects WHERE id = object_id('[Products]') AND  OBJECTPROPERTY(id, 'IsUserTable') = 1)
DROP TABLE [Products];


CREATE TABLE [Enum] ( 
	[Id] int identity(1,1)  NOT NULL,
	[AttributeId] int NOT NULL,
	[Value] nvarchar(20) NOT NULL
);

CREATE TABLE [AttributeDescription] ( 
	[Id] int identity(1,1)  NOT NULL,
	[TypeId] int NOT NULL,
	[Name] nvarchar(20) NOT NULL,
	[AttributeType] int NOT NULL
);

CREATE TABLE [Storage] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Serial] bigint NOT NULL
);

CREATE TABLE [Delivery] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Name] nvarchar(50) NOT NULL
);

CREATE TABLE [Manufacturer] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Info] text NULL
);

CREATE TABLE [Attribute] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Value] nvarchar(50) NOT NULL,
	[ProductId] int NOT NULL,
	[AttributeDescriptionId] int NOT NULL
);

CREATE TABLE [Categories] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[Picture] nvarchar(50) NULL
);

CREATE TABLE [Types] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Name] nvarchar(50) NOT NULL,
	[CategoryId] int NULL
);

CREATE TABLE [Products] ( 
	[Id] int identity(1,1)  NOT NULL,
	[Name] nvarchar(45) NOT NULL,
	[Price] int NOT NULL,
	[Warranty] nvarchar(50) NULL,
	[Picture] nvarchar(50) NULL,
	[Count] int NULL,
	[TypeId] int NULL,
	[ManufacturerId] int NULL,
	[DeliveryId] int NULL,
	[StorageId] int NULL
);


ALTER TABLE [Enum] ADD CONSTRAINT [PK_Enum] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [AttributeDescription] ADD CONSTRAINT [PK_Attribute] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Storage] ADD CONSTRAINT [PK_Storage] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Delivery] ADD CONSTRAINT [PK_Delivery] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Manufacturer] ADD CONSTRAINT [PK_Manufacturer] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Attribute] ADD CONSTRAINT [PK_Attributes] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Categories] ADD CONSTRAINT [PK_Category] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Types] ADD CONSTRAINT [PK_Type] 
	PRIMARY KEY CLUSTERED ([Id]);

ALTER TABLE [Products] ADD CONSTRAINT [PK_Product] 
	PRIMARY KEY CLUSTERED ([Id]);



ALTER TABLE [Enum] ADD CONSTRAINT [FK_Enum_Attribute_desctription] 
	FOREIGN KEY ([AttributeId]) REFERENCES [AttributeDescription] ([Id]);

ALTER TABLE [AttributeDescription] ADD CONSTRAINT [FK_Attribute_Types] 
	FOREIGN KEY ([TypeId]) REFERENCES [Types] ([Id]);

ALTER TABLE [Attribute] ADD CONSTRAINT [FK_Attribute_Attribute_desctription] 
	FOREIGN KEY ([AttributeDescriptionId]) REFERENCES [AttributeDescription] ([Id]);

ALTER TABLE [Attribute] ADD CONSTRAINT [FK_Attribute_Products] 
	FOREIGN KEY ([ProductId]) REFERENCES [Products] ([Id]);

ALTER TABLE [Types] ADD CONSTRAINT [FK_Types_Categories] 
	FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Delivery] 
	FOREIGN KEY ([DeliveryId]) REFERENCES [Delivery] ([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Manufacturer] 
	FOREIGN KEY ([ManufacturerId]) REFERENCES [Manufacturer] ([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Storage] 
	FOREIGN KEY ([StorageId]) REFERENCES [Storage] ([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION;

ALTER TABLE [Products] ADD CONSTRAINT [FK_Products_Types] 
	FOREIGN KEY ([TypeId]) REFERENCES [Types] ([Id])
	ON DELETE NO ACTION ON UPDATE NO ACTION;
