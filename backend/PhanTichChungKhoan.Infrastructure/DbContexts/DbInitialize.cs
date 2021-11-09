using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PhanTichChungKhoan.Infrastructure.DbContexts
{
    public class DbInitialize
    {
        public static void SeedData(PriceBoardDbContext dbContext)
        {
			dbContext.Database.ExecuteSqlRaw(@"DROP PROCEDURE IF EXISTS [dbo].[SyncPriceBoardData]");

            dbContext.Database.ExecuteSqlRaw(@"
CREATE PROCEDURE [dbo].[SyncPriceBoardData]
	@ExchangeName varchar(10)
AS
BEGIN
IF @ExchangeName = 'HOSE'
BEGIN
	UPDATE t
SET t.Price = s.Price, t.CompanyName = s.CompanyName
FROM dbo.PriceBoard t
JOIN dbo.HosePriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,CompanyName
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.CompanyName
	,t.Price
	,GETDATE()
FROM dbo.HosePriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		)
END
--=============
IF @ExchangeName = 'HNX'
BEGIN
	UPDATE t
SET t.Price = s.Price, t.CompanyName = s.CompanyName
FROM dbo.PriceBoard t
JOIN dbo.HnxPriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,CompanyName
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.CompanyName
	,t.Price
	,GETDATE()
FROM dbo.HnxPriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		)
END
--=============
IF @ExchangeName = 'UPCOM'
BEGIN
	UPDATE t
SET t.Price = s.Price, t.CompanyName = s.CompanyName
FROM dbo.PriceBoard t
JOIN dbo.UpcomPriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,CompanyName
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.CompanyName
	,t.Price
	,GETDATE()
FROM dbo.UpcomPriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		)
END
END
");

			dbContext.Database.ExecuteSqlRaw(@"DROP PROCEDURE IF EXISTS [dbo].[SyncPriceBoardDataHose]");

			dbContext.Database.ExecuteSqlRaw(@"
CREATE PROCEDURE [dbo].[SyncPriceBoardDataHose]
AS
BEGIN
	UPDATE t
SET t.Price = s.Price, t.CompanyName = s.CompanyName
FROM dbo.PriceBoard t
JOIN dbo.HosePriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price;

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,CompanyName
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.CompanyName
	,t.Price
	,GETDATE()
FROM dbo.HosePriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		);

--=============
	UPDATE t
SET t.Price = s.Price, t.CompanyName = s.CompanyName
FROM dbo.PriceBoard t
JOIN dbo.HnxPriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price;

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,CompanyName
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.CompanyName
	,t.Price
	,GETDATE()
FROM dbo.HnxPriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		);

--=============
	UPDATE t
SET t.Price = s.Price, t.CompanyName = s.CompanyName
FROM dbo.PriceBoard t
JOIN dbo.UpcomPriceBoardTemp s ON t.Exchange = s.Exchange
	AND t.Symbol = s.Symbol
WHERE t.Price <> s.Price;

INSERT INTO dbo.PriceBoard (
	Exchange
	,Symbol
	,CompanyName
	,Price
	,UpdatedDate
	)
SELECT t.Exchange
	,t.Symbol
	,t.CompanyName
	,t.Price
	,GETDATE()
FROM dbo.UpcomPriceBoardTemp t
WHERE NOT EXISTS (
		SELECT TOP 1 1
		FROM dbo.PriceBoard p
		WHERE p.Exchange = t.Exchange
			AND p.Symbol = t.Symbol
		);
END
");
		}
    }
}
