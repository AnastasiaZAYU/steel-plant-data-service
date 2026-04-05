CREATE TABLE [dbo].[SteelGrades]
(
	[GradeID] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[GradeName] NVARCHAR(50) NOT NULL,
	[TargetTemperature] INT NOT NULL
)
