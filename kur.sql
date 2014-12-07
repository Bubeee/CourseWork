
--�������� ��������
INSERT INTO [delivery] (name) VALUES (N'���������');

--�������� ����������
INSERT INTO [manufacturer] (name,info) VALUES (N'Intel',N'�������� intel');

--�������� ���������
INSERT INTO [category] (name,picture) VALUES (N'��������� �������',N'Content/Pictures/picture.jpg')



--�������� ���� ������
INSERT INTO [type_product] 
	(table_name,category_id,view_name) 
	VALUES
	('type_product1',1,N'����')

--�������� ����� ����
INSERT INTO [field] 
	([type_product_id],[view_name],[field_type])
	VALUES
	(1,N'���',2),
	(1,N'����',1)

--������� ���� 3000
INSERT INTO [product]
	([type_id],[model],[manufacturer_id],[price],[warranty],[delivery_id],[picture],[count])
	VALUES
	(1,N'���� 3000',1,1000000,12,1,N'Content/Pictures/picture.jpg',10)

--�������� ������� ���� ����
CREATE TABLE [type_product1]
	(
		product_id int,
		col1 int,
		col2 nvarchar(30)
	)

--������� ���� 3000
INSERT INTO [type_product1]
	(product_id,col1,col2)
	VALUES
	(1,700,N'���������')




--������� ������2
INSERT INTO [product]
	([type_id],[model],[manufacturer_id],[price],[warranty],[delivery_id],[picture],[count])
	VALUES
	(1,N'MODEL-2',1,1000000,12,1,N'Content/Pictures/picture.jpg',10)

INSERT INTO [type_product1]
	(product_id,col1,col2)
	VALUES
	(1,N'���-��2_1',N'���-��2_2')





--�������� ��� ������ ������
--�������� ���� ������2
INSERT INTO [type_product] 
	(table_name,category_id,view_name) 
	VALUES
	('type_product2',1,N'���_������2')

--�������� �����2
INSERT INTO [field] 
	([type_product_id],[col_name],[view_name],[field_type])
	VALUES
	(1,'col1',N'��������2_1',1),
	(1,'col2',N'��������2_2',1)

--�������� ������� ���� ������2
CREATE TABLE [type_product2]
	(
		product_id int,
		col1 nvarchar(30),
		col2 nvarchar(30)
	)


--������� ������2
INSERT INTO [product]
	([type_id],[model],[manufacturer_id],[price],[warranty],[delivery_id],[picture],[count])
	VALUES
	(1,N'MODEL-1',1,1000000,12,1,N'Content/Pictures/picture.jpg',10)

INSERT INTO [type_product2]
	(product_id,col1,col2,col3)
	VALUES
	(1,N'���-��3_1',N'���-��3_2',N'���-��3_2')



 ------------------------------------		�������			------------------------------------

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

 
 ------------------------------------		�������			------------------------------------

 
--����� ������ 3 �������
--�������� ����
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

--�������� �������� ������� ������
DECLARE @producttypeid INT; 
SET @producttypeid = 1;
SELECT [table_name] FROM [type_product] WHERE id = @producttypeid;

--�������������� ����
SELECT * FROM type_product1 AS [t] WHERE [t].[product_id]=@productid
;
--�������� �������������� �����
SELECT [view_name] FROM [field] WHERE [type_product_id] = @producttypeid ORDER BY [id]






