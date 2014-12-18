Use [kur_Vova]

GO
INSERT INTO [dbo].[Categories] ([Name], [Picture])
VALUES (N'Home Tech', N'Files/4.jpg') 

GO
INSERT INTO [dbo].[Types] ([Name], [CategoryId]) 
VALUES (N'Irons', 1)
INSERT INTO [dbo].[Types] ([Name], [CategoryId]) 
VALUES (N'Washing machines', 1)

GO 
INSERT INTO [dbo].[Delivery] ([Name]) 
VALUES (N'Free')

GO 
INSERT INTO [dbo].[Manufacturer] ([Name], [Info]) 
VALUES (N'Tefal', N'Poland')

----------- Attribute Descriptions

GO
INSERT INTO [dbo].[AttributeDescription] ([TypeId], [Name], [AttributeType])
VALUES (1, N'Weight (kilo)', 3)
GO
INSERT INTO [dbo].[AttributeDescription] ([TypeId], [Name], [AttributeType])
VALUES (1, N'Length (mm)', 2)
GO
INSERT INTO [dbo].[AttributeDescription] ([TypeId], [Name], [AttributeType])
VALUES (1, N'Power (Vt)', 2)
GO
INSERT INTO [dbo].[AttributeDescription] ([TypeId], [Name], [AttributeType])
VALUES (1, N'Material', 4)
GO
INSERT INTO [dbo].[AttributeDescription] ([TypeId], [Name], [AttributeType])
VALUES (1, N'Steam supply', 2)

------- Products

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal12', 500000, N'2 years', N'Files/222.jpg', 8, 1, 1, 1, NULL);

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal 092', 300000, N'2 years', N'Files/223.jpg', 2, 1, 1, 1, NULL);

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal 901', 30000, N'2 years', N'Files/224.jpg', 4, 1, 1, 1, NULL);

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal 2322', 676300, N'2 years', N'Files/225.jpg', 21, 1, 1, 1, NULL);



---- Attribute Values

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (1, 1, N'3.4')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (1, 2, N'123')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (1, 3, N'12')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (1, 4, N'титан')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (1, 5, N'yes')


INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (2, 1, N'3.4')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (2, 2, N'123')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (2, 3, N'12')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (2, 4, N'титан')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (2, 5, N'yes')


INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (3, 1, N'3.4')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (3, 2, N'123')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (3, 3, N'12')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (3, 4, N'титан')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (3, 5, N'yes')


INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (4, 1, N'3.4')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (4, 2, N'123')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (4, 3, N'12')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (4, 4, N'титан')

INSERT INTO [dbo].[Attribute] (ProductId, AttributeDescriptionId, Value)
VALUES (4, 5, N'yes')