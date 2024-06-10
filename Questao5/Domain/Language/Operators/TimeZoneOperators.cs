namespace Questao5.Domain.Language.Operators
{
    public class TimeZoneOperators
    {
        // Em razão da simplicidade, porém, recomenda-se UTC.
        public static DateTime GetCurrentDateTime() => DateTime.Now;
    }
}
