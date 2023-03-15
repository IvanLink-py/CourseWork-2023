using System.Text.RegularExpressions;
using NfModels.Services.Expressions.Base;

namespace NfModels.Services.Expressions;

/// <summary>
/// Выражение поиска по пути
/// </summary>
public class PathExpression : Expression
{
    private string _path = "";

    /// <summary>
    /// Парсинг текста выражения в объект
    /// </summary>
    /// <param name="text">Текст выражения</param>
    /// <returns>Выражения поиска по пути</returns>
    public static Expression Parse(string text)
    {
        var reg = new Regex(@"(?<=:).*$");
        var match = reg.Match(text);
        var path = match.Value;
        return new PathExpression
        {
            _path = path
        };
    }

    public override Filter ToFilter()
    {
        return Filter.Path(_path);
    }
}