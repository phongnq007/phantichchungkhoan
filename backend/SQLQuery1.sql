DROP PROCEDURE IF EXISTS [dbo].[SyncPriceBoardData]
GO

CREATE PROCEDURE [dbo].[SyncPriceBoardData]
AS
begin
	UPDATE t
SET t.Price = s.Price
FROM dbo.PriceBoard t
JOIN dbo.PriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.Price
	,GETDATE()
FROM dbo.PriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		)
end
