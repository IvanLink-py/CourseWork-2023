using System.Text.RegularExpressions;

namespace NfModels.Services.Expressions.Base;

/// <summary>
/// Базовый класс выражения поисковой строки
/// </summary>
public abstract class Expression
{
    /// <summary>
    /// Преобразование из выражения фильтр 
    /// </summary>
    /// <returns>Фильтр</returns>
    public abstract Filter ToFilter();

    /// <summary>
    /// Метод выполняющий подбор конкретного типа выражения по тексту
    /// </summary>
    /// <param name="text">Текст выражения</param>
    /// <returns>Выражение</returns>
    public static Expression ExceptionSelection(string text)
    {
        if (new Regex(@"^(((t(ag)?)|(т([еэ]г)?)):.*)$", RegexOptions.IgnoreCase).IsMatch(text))
            return TagExpression.Parse(text);
        if (new Regex(@"^((п(уть?)?)|(p(ath)?)):.*$", RegexOptions.IgnoreCase).IsMatch(text))
            return PathExpression.Parse(text);
        if (new Regex(@"^((тип)|(is?)|(type)):.*$", RegexOptions.IgnoreCase).IsMatch(text))
            return TypeExpression.Parse(text);
        if (new Regex(@"^((с(татус)?)|(s(tatus)?)):.*$", RegexOptions.IgnoreCase).IsMatch(text))
            return StatusExpression.Parse(text);
        if (new Regex(@"^((создан)|(created))([>:=<]).*$", RegexOptions.IgnoreCase).IsMatch(text))
            return CreationDateExpression.Parse(text);
        if (new Regex(@"^(работал:|work:)([^-]+)?(-(.+))?$", RegexOptions.IgnoreCase).IsMatch(text))
            return CrewExpression.Parse(text);
        return TextExpression.Parse(text);
    }
}

/// <summary>
/// Тип сравнения
/// </summary>
public enum ComparisonType
{
    Less, // Меньше
    Eq,  // Равно
    Greater // Больше
}