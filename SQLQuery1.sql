BULK
INSERT GlobalPlantList
FROM 'C:\Users\vmass\Desktop\SeedIndex.txt'
WITH
(
FIELDTERMINATOR = ',',
ROWTERMINATOR = '\n'
)
GO
--Check the content of the table.
SELECT *
FROM GlobalPlantList
GO
--Drop the table to clean up database.
--DROP TABLE CSVTest
GO

