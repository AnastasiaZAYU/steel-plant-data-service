IF NOT EXISTS (SELECT 1 FROM SteelGrades)
BEGIN
	INSERT INTO SteelGrades (GradeName, TargetTemperature) 
	VALUES ('S235JR', 1600), ('S355J2', 1580), ('30HGSA', 1620);
END