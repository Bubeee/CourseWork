USE [master]
GO
DROP DATABASE [kur]
GO
CREATE DATABASE [kur]
GO
USE [kur]
GO

CREATE TABLE [category] ( 
	[id] int identity(1,1)  NOT NULL,
	[name] nvarchar(40) NOT NULL,
	[picture] nvarchar(100)
)
;

CREATE TABLE [delivery] ( 
	[id] int identity(1,1)  NOT NULL,
	[name] nvarchar(40) NOT NULL
)
;

CREATE TABLE [field] ( 
	[id] int identity(1,1)  NOT NULL,
	[type_product_id] int NOT NULL,
	[view_name] nvarchar(40) NOT NULL,
	[field_type] int NOT NULL
)
;

CREATE TABLE [log] ( 
	[id] bigint identity(1,1)  NOT NULL,
	[user_id] int NOT NULL,
	[goods_id] int NOT NULL,
	[serial] varchar(30) NOT NULL,
	[time] int NOT NULL,
	[price] int NOT NULL,
	[warranty] smallint
)
;

CREATE TABLE [log_archive] ( 
	[id] bigint identity(1,1)  NOT NULL,
	[user_id] int NOT NULL,
	[goods_id] int NOT NULL,
	[serial] nvarchar(30) NOT NULL,
	[time] int NOT NULL,
	[price] int NOT NULL,
	[warranty] smallint
)
;

CREATE TABLE [manufacturer] ( 
	[id] int identity(1,1)  NOT NULL,
	[name] nvarchar(30) NOT NULL,
	[info] text
)
;

CREATE TABLE [product] ( 
	[id] int identity(1,1)  NOT NULL,
	[type_id] int NOT NULL,
	[model] nvarchar(50) NOT NULL,
	[manufacturer_id] int NOT NULL,
	[price] int NOT NULL,
	[warranty] nvarchar(50) NOT NULL,
	[delivery_id] int NOT NULL,
	[picture] nvarchar(100),
	[count] int NOT NULL
)
;

CREATE TABLE [storage] ( 
	[goods_id] int NOT NULL,
	[serial] varchar(30) NOT NULL
)
;

CREATE TABLE [type_product] ( 
	[id] int identity(1,1)  NOT NULL,
	[category_id] int NOT NULL,
	[view_name] nvarchar(50) NOT NULL
)
;

CREATE TABLE [user] ( 
	[id] int identity(1,1)  NOT NULL,
	[name] nvarchar(30) NOT NULL,
	[login] varchar(30) NOT NULL,
	[password] varchar(100) NOT NULL,
	[money] int NOT NULL
)
;


ALTER TABLE [category]
	ADD CONSTRAINT [UQ_category_name] UNIQUE ([name])
;

ALTER TABLE [delivery]
	ADD CONSTRAINT [UQ_delivery_name] UNIQUE ([name])
;

ALTER TABLE [log]
	ADD CONSTRAINT [UQ_log_serial] UNIQUE ([serial])
;

ALTER TABLE [log_archive]
	ADD CONSTRAINT [UQ_log_archive_serial] UNIQUE ([serial])
;

ALTER TABLE [manufacturer]
	ADD CONSTRAINT [UQ_manufacturer_name] UNIQUE ([name])
;

ALTER TABLE [product]
	ADD CONSTRAINT [UQ_goods_model] UNIQUE ([model])
;

ALTER TABLE [storage]
	ADD CONSTRAINT [UQ_storage_serial] UNIQUE ([serial])
;

ALTER TABLE [type_product]
	ADD CONSTRAINT [UQ_type_goods_view_name] UNIQUE ([view_name])
;

ALTER TABLE [user]
	ADD CONSTRAINT [UQ_user_login] UNIQUE ([login])
;

ALTER TABLE [user]
	ADD CONSTRAINT [UQ_user_name] UNIQUE ([name])
;

ALTER TABLE [category] ADD CONSTRAINT [PK_category] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [delivery] ADD CONSTRAINT [PK_delivery] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [field] ADD CONSTRAINT [PK_fields] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [log] ADD CONSTRAINT [PK_log] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [log_archive] ADD CONSTRAINT [PK_log_archive] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [manufacturer] ADD CONSTRAINT [PK_manufacturer] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [product] ADD CONSTRAINT [PK_goods] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [type_product] ADD CONSTRAINT [PK_type_goods] 
	PRIMARY KEY CLUSTERED ([id])
;

ALTER TABLE [user] ADD CONSTRAINT [PK_user] 
	PRIMARY KEY CLUSTERED ([id])
;



ALTER TABLE [field] ADD CONSTRAINT [FK_fields_type_goods] 
	FOREIGN KEY ([type_product_id]) REFERENCES [type_product] ([id])
;

ALTER TABLE [log] ADD CONSTRAINT [FK_log_goods] 
	FOREIGN KEY ([goods_id]) REFERENCES [product] ([id])
;

ALTER TABLE [log] ADD CONSTRAINT [FK_log_user] 
	FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
;

ALTER TABLE [log_archive] ADD CONSTRAINT [FK_log_archive_goods] 
	FOREIGN KEY ([user_id]) REFERENCES [product] ([id])
;

ALTER TABLE [log_archive] ADD CONSTRAINT [FK_log_archive_user] 
	FOREIGN KEY ([user_id]) REFERENCES [user] ([id])
;

ALTER TABLE [product] ADD CONSTRAINT [FK_goods_delivery] 
	FOREIGN KEY ([delivery_id]) REFERENCES [delivery] ([id])
;

ALTER TABLE [product] ADD CONSTRAINT [FK_goods_manufacturer] 
	FOREIGN KEY ([manufacturer_id]) REFERENCES [manufacturer] ([id])
;

ALTER TABLE [product] ADD CONSTRAINT [FK_goods_type_goods] 
	FOREIGN KEY ([type_id]) REFERENCES [type_product] ([id])
;

ALTER TABLE [storage] ADD CONSTRAINT [FK_storage_goods] 
	FOREIGN KEY ([goods_id]) REFERENCES [product] ([id])
;

ALTER TABLE [type_product] ADD CONSTRAINT [FK_type_goods_category] 
	FOREIGN KEY ([category_id]) REFERENCES [category] ([id])
;