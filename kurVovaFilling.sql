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

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal12', 500000, N'2 years', N'Files/222.jpg', 8, 1, 1, 1, NULL);

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal 092', 300000, N'2 years', N'Files/223.jpg', 8, 1, 1, 1, NULL);

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal 901', 30000, N'2 years', N'Files/224.jpg', 8, 1, 1, 1, NULL);

GO
INSERT INTO [dbo].[Products] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal 2322', 676300, N'2 years', N'Files/225.jpg', 8, 1, 1, 1, NULL);



GO
INSERT INTO [dbo].[Attributes] ([Name], [Price], [Warranty], [Picture], [Count], [TypeId], [DeliveryId], [ManufacturerId], [StorageId])
VALUES (N'Tefal12', 500000, N'2 years', N'Files/222.jpg', 8, 1, 1, 1, NULL);