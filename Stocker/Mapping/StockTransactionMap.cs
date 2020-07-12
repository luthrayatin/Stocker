using System;
using Mapping;
using Stocker.Database.Models;
using Stocker.Models.Api;
using Amount = Stocker.Models.Api.Amount;

namespace Stocker.Mapping
{
    public class TransactionRequestToDbMap : IMap<AddStockTransactionRequest, User, StockTransaction>
    {
        private readonly IMap<Amount, Database.Models.Amount> _amountMapper;

        public TransactionRequestToDbMap(IMap<Amount, Database.Models.Amount> amountMapper)
        {
            _amountMapper = amountMapper;
        }

        public StockTransaction Map(AddStockTransactionRequest source, User user)
        {
            return new StockTransaction
            {
                Commission = _amountMapper.Map(source.Commission),
                ConversionRateUserToStock = source.ConversionRate,
                Date = source.TransactionDate,
                LoggedAt = DateTimeOffset.Now,
                Quantity = source.Quantity,
                PricePerUnit = _amountMapper.Map(source.PricePerUnit),
                StockId = source.StockId,
                StockExchangeId = source.StockExchangeId,
                TradingPlatformId = source.TradingPlatformId,
                UserCurrencyId = source.UserCurrencyId,
                UserId = user.Id
            };
        }
    }

    public class TransactionDbToApiMap : IMap<StockTransaction, Transaction>
    {
        private readonly IMap<Database.Models.Amount, Models.Api.Amount> _amountMapper;

        public TransactionDbToApiMap(IMap<Database.Models.Amount, Models.Api.Amount> amountMapper)
        {
            _amountMapper = amountMapper;
        }

        public Transaction Map(StockTransaction source)
        {
            return new Transaction
            {
                CommissionUser = new Models.Api.Amount
                {
                    CurrencyCode = source.UserCurrency.Code,
                    ValueMinor = source.Commission.ValueMinor / source.ConversionRateUserToStock,
                    CurrencyId = source.UserCurrencyId
                },
                TradingPlatform = source.TradingPlatform.Name,
                Date = source.Date,
                LoggedAt = DateTimeOffset.Now,
                PricePerUnit = _amountMapper.Map(source.PricePerUnit),
                PricePerUnitUser = new Models.Api.Amount
                {
                    CurrencyCode = source.UserCurrency.Code,
                    ValueMinor = source.PricePerUnit.ValueMinor / source.ConversionRateUserToStock,
                    CurrencyId = source.UserCurrencyId
                },
                Quantity = source.Quantity,
                StockId = source.StockId
            };
        }
    }
}