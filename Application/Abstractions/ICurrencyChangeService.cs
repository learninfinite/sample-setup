using Domain.Enums;
using Domain.Shared;
using Domain.ValueObjects;

namespace Application.Abstractions;

public interface ICurrencyChangeService
{
    CurrencyType CurrencyType { get; }

    Result<List<ChangeUnit>> MakeChange(Domain.Entities.Transaction transaction);
}
