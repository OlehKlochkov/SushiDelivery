using System.ComponentModel;

namespace SushiDelivery.Domain.Enumerations
{
    /// <summary>
    /// Product categories.
    /// </summary>
    public enum Categories
    {
        [Description("Sushi")]
        Sushi,

        [Description("Rolls")]
        Rolls,

        [Description("Sushi Sets")]
        SushiSet,

        [Description("Woks")]
        Wok,

        [Description("Pizza")]
        Pizza,

        [Description("Dessert")]
        Dessert,

        [Description("Drink")]
        Drink,
    }
}
