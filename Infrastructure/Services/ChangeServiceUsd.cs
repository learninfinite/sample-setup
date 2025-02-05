using Application.Abstractions;
using Domain.Entities;
using Domain.Enums;
using Domain.Shared;
using Domain.ValueObjects;

namespace Infrastructure.Services;

public sealed class ChangeServiceUsd : ICurrencyChangeService
{
    decimal[] _denominations =
        { 100, 50, 20, 10, 5, 2, 1, .25M, .10M, .05M, .01M };

    public CurrencyType CurrencyType => CurrencyType.USD;

    public Result<List<ChangeUnit>> MakeChange(Transaction transaction)
    {
        var result = new List<ChangeUnit>();

        decimal change = transaction.PaymentAmount - transaction.Total;

        if (change == 0) return Result.Success(result);

        if (change < 0)
        {
            return Result.Failure<List<ChangeUnit>>(
                new Error("ChangeService.MakeChange", $"Outstanding balance of {change:C2}"));
        }

        foreach (var unit in _denominations)
        {
            if (unit > change) continue;

            var diff = (int)(change / unit);
            var amount = diff * unit;
            var changeUnit = ChangeUnit.Create(GetUnitDisplay(unit), diff, amount);

            if (!changeUnit.IsSuccess)
                return Result.Failure<List<ChangeUnit>>(changeUnit.Error);

            result.Add(changeUnit.Value);

            change -= amount;
        }

        return Result.Success(result);
    }

    private string GetUnitDisplay(decimal unit) => unit switch
    {
        >= 1 => $"{unit} Dollar Bill",
        < 1 => $"{unit} Cents"
    };
}
