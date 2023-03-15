using NfModels.Services.Expressions.Base;

namespace NfModels.Services.Expressions;

/// <summary>
/// Выражение поиска по тексту
/// </summary>
public class TextExpression : Expression
{
    private string _text = "";

    /// <summary>
    /// Парсинг текста выражения в объект
    /// </summary>
    /// <param name="text">Текст выражения</param>
    /// <returns>Выражения поиска по тексту</returns>
    public static Expression Parse(string text)
    {
        return new TextExpression
        {
            _text = text
        };
    }

    public override Filter ToFilter()
    {
        return Filter.Contains(_text);
    }

    public static TextExpression operator +(TextExpression a, TextExpression b)
    {
        return new TextExpression
        {
            _text = a._text + " " + b._text
        };
    }
}