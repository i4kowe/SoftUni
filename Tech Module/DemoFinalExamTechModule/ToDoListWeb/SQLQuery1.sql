 /****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) [Id]
      ,[Title]
      ,[Comments]
  FROM [ToDoList].[dbo].[Tasks]