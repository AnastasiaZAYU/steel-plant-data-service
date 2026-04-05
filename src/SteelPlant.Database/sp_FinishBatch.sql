CREATE PROCEDURE [dbo].[sp_FinishBatch]
	@BatchId INT,
	@FinalWeight DECIMAL (10, 2)
AS
BEGIN
	UPDATE SteelBatches
	SET WeightKG = @FinalWeight,
		Status = 'Finished'
	WHERE BatchID = @BatchId
	
	INSERT INTO ProductionLogs (BatchID, Message)
	VALUES (@BatchId, 'Batch finished with weight: ' + CAST(@FinalWeight AS NVARCHAR(20)));
END