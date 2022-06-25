using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ServiceReleaseManager.Api.Endpoints;

public static class KebabNamespace
{
  public static MvcOptions UseKebabCaseNamespaceRouteToken(this MvcOptions options)
  {
    options.Conventions.Add(new CustomRouteToken(
      "namespace", (Func<ControllerModel, string>)(c =>
      {
        var str = c.ControllerType.Namespace ?? String.Empty;
        return str.Split(new[] { '.' }).Last().PascalToKebabCase();
      })));
    return options;
  }

  private static string PascalToKebabCase(this string str)
  {
    if (string.IsNullOrEmpty(str))
    {
      return string.Empty;
    }

    var builder = new StringBuilder();
    builder.Append(char.ToLower(str.First()));

    foreach (var c in str.Skip(1))
    {
      if (char.IsUpper(c))
      {
        builder.Append('-');
        builder.Append(char.ToLower(c));
      }
      else
      {
        builder.Append(c);
      }
    }

    return builder.ToString();
  }

  private class CustomRouteToken : IApplicationModelConvention
  {
    private readonly string _tokenRegex;
    private readonly Func<ControllerModel, string?> _valueGenerator;

    public CustomRouteToken(string tokenName, Func<ControllerModel, string?> valueGenerator)
    {
      _tokenRegex = "(\\[" + tokenName + "])(?<!\\[\\1(?=]))";
      _valueGenerator = valueGenerator;
    }

    public void Apply(ApplicationModel application)
    {
      foreach (var controller in application.Controllers)
      {
        var tokenValue = _valueGenerator(controller);
        UpdateSelectors(controller.Selectors, tokenValue);
        UpdateSelectors(
          controller.Actions.SelectMany(
            a =>
              a.Selectors), tokenValue);
      }
    }

    private void UpdateSelectors(IEnumerable<SelectorModel> selectors, string? tokenValue)
    {
      foreach (var selectorModel in selectors.Where(
                 s => s.AttributeRouteModel != null))
      {
        if (selectorModel.AttributeRouteModel == null)
        {
          continue;
        }

        selectorModel.AttributeRouteModel.Template =
          InsertTokenValue(selectorModel.AttributeRouteModel.Template, tokenValue);
        selectorModel.AttributeRouteModel.Name =
          InsertTokenValue(selectorModel.AttributeRouteModel.Name, tokenValue);
      }
    }

    private string? InsertTokenValue(string? template, string? tokenValue)
    {
      return template == null
        ? template
        : Regex.Replace(template, _tokenRegex, tokenValue);
    }
  }
}
