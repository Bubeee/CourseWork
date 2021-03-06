/****** Script for SelectTopNRows command from SSMS  ******/
SELECT [P].[Id] AS [ProductId]
      ,[P].[Name] AS [ProductName]
      ,[Price]
      ,[Warranty]
      ,[Picture]
      ,[Count]
      ,[T].[Id] AS [TypeId]
      ,[T].[Name] AS [TypeName]
      ,[M].[Name] AS [ManufName]
	  ,[M].[Info] AS [ManufInfo]
      ,[D].[Name] AS [DeliveryName]
      ,[S].[Serial] AS [StorageSerial]
  FROM [kur_Vova].[dbo].[Products] AS [P]
  LEFT JOIN [kur_Vova].[dbo].[Manufacturer] AS [M]
  ON [P].[ManufacturerId] = [M].[Id]
  LEFT JOIN [kur_Vova].[dbo].[Delivery] AS [D]
  ON [P].[DeliveryId] = [D].[Id]
  LEFT JOIN [kur_Vova].[dbo].[Storage] AS [S]
  ON [P].[StorageId] = [S].[Id]
  INNER JOIN [kur_Vova].[dbo].[Types] AS [T]
  ON [P].[TypeId] = [T].[Id]



SELECT [AD].[Name], [A].[Value]
FROM [kur_Vova].[dbo].[Attribute] AS [A]
INNER JOIN [kur_Vova].[dbo].[AttributeDescription] AS [AD]
ON [A].[AttributeDescriptionId] = [AD].[Id]
WHERE [A].[ProductId] = 1



