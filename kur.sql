
--создание доставки
INSERT INTO [delivery] (name) VALUES (N'Самовывоз');

--создание поставщика
INSERT INTO [manufacturer] (name,info) VALUES (N'Intel',N'Описание intel');

--создание категории
INSERT INTO [category] (name,picture) VALUES (N'Категория техники',N'Content/Pictures/picture.jpg')



--создание типа товара
INSERT INTO [type_product] 
	(table_name,category_id,view_name) 
	VALUES
	('type_product1',1,N'утюг')

--создание полей утюг
INSERT INTO [field] 
	([type_product_id],[view_name],[field_type])
	VALUES
	(1,N'вес',2),
	(1,N'цвет',1)

--вставка утюг 3000
INSERT INTO [product]
	([type_id],[model],[manufacturer_id],[price],[warranty],[delivery_id],[picture],[count])
	VALUES
	(1,N'утюг 3000',1,1000000,12,1,N'Content/Pictures/picture.jpg',10)

--создание таблицы типа утюг
CREATE TABLE [type_product1]
	(
		product_id int,
		col1 int,
		col2 nvarchar(30)
	)

--вставка утюг 3000
INSERT INTO [type_product1]
	(product_id,col1,col2)
	VALUES
	(1,700,N'бирюзовый')




--вставка товара2
INSERT INTO [product]
	([type_id],[model],[manufacturer_id],[price],[warranty],[delivery_id],[picture],[count])
	VALUES
	(1,N'MODEL-2',1,1000000,12,1,N'Content/Pictures/picture.jpg',10)

INSERT INTO [type_product1]
	(product_id,col1,col2)
	VALUES
	(1,N'хар-ка2_1',N'хар-ка2_2')





--создание ещё одного товара
--создание типа товара2
INSERT INTO [type_product] 
	(table_name,category_id,view_name) 
	VALUES
	('type_product2',1,N'тип_товара2')

--создание полей2
INSERT INTO [field] 
	([type_product_id],[col_name],[view_name],[field_type])
	VALUES
	(1,'col1',N'значение2_1',1),
	(1,'col2',N'значение2_2',1)

--создание таблицы типа товара2
CREATE TABLE [type_product2]
	(
		product_id int,
		col1 nvarchar(30),
		col2 nvarchar(30)
	)


--вставка товара2
INSERT INTO [product]
	([type_id],[model],[manufacturer_id],[price],[warranty],[delivery_id],[picture],[count])
	VALUES
	(1,N'MODEL-1',1,1000000,12,1,N'Content/Pictures/picture.jpg',10)

INSERT INTO [type_product2]
	(product_id,col1,col2,col3)
	VALUES
	(1,N'хар-ка3_1',N'хар-ка3_2',N'хар-ка3_2')



 ------------------------------------		ОТЛАДКА			------------------------------------

 CREATE TABLE s
 (
	s nvarchar(1000)
 )

 DECLARE @i int ;
 SET @i = 1;
 WHILE (@i < 10000)
BEGIN
   INSERT INTO [s] VALUES 
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff'),
   (N'asjdhflkjwqebrjhewvclfjhsdavfljshadcflsjdafgvladksjfvhcjgqwlfvl dsjhfgvlajshdgvlhsdagvjsdahfvsladfff');
   SET @i = @i + 1
END

 
 ------------------------------------		ЗАПРОСЫ			------------------------------------

 
--выбор товара 3 запроса
--основные поля
DECLARE @productid INT; 
SET @productid = 1;
WITH main_attributes AS
(
	SELECT [p].[id],[p].[type_id],[p].model,[p].[price],[p].[warranty],[p].[picture],[p].[count],[d].name AS [delivery],[m].name AS [manufacturer] 
	FROM 
	[product] AS [p] 
	JOIN [manufacturer] AS [m] ON [manufacturer_id]=[m].[id]
	JOIN [delivery]AS [d] ON [delivery_id]=[d].[id]
	WHERE [p].[id]=@productid
)
SELECT * FROM [main_attributes]

--получить название таблицы товара
DECLARE @producttypeid INT; 
SET @producttypeid = 1;
SELECT [table_name] FROM [type_product] WHERE id = @producttypeid;

--дополнительные поля
SELECT * FROM type_product1 AS [t] WHERE [t].[product_id]=@productid
;
--названия дополнительных полей
SELECT [view_name] FROM [field] WHERE [type_product_id] = @producttypeid ORDER BY [id]






