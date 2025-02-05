using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.Enums;

public enum CurrencyType
{
    [Description("US Dollar")]
    USD = 0,
    [Description("Japanese Yen")]
    JPY,
    [Description("Mexican Peso")]
    MXN
}
