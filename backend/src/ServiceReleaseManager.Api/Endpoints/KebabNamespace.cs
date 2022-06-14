using System.Text;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ServiceReleaseManager.Api.Endpoints;

public static class KebabNamspace
{
  public static MvcOptions UseKebabCaseNamespaceRouteToken(this MvcOptions options)
  {
    options.Conventions.Add(new CustomRouteToken(
      "namespace", (Func<ControllerModel, string>)(c =>
      {
        string str = c.ControllerType.Namespace ?? String.Empty;
        return str.Split(new[] { '.' }).Last().PascalToKebabCase();
      })));
    return options;
  }

  private static string PascalToKebabCase(this string str)
  {
    if (string.IsNullOrEmpty(str))
      return string.Empty;

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
      this._tokenRegex = "(\\[" + tokenName + "])(?<!\\[\\1(?=]))";
      this._valueGenerator = valueGenerator;
    }

    public void Apply(ApplicationModel application)
    {
      foreach (ControllerModel controller in (IEnumerable<ControllerModel>)application.Controllers)
      {
        string? tokenValue = this._valueGenerator(controller);
        this.UpdateSelectors((IEnumerable<SelectorModel>)controller.Selectors, tokenValue);
        this.UpdateSelectors(
          controller.Actions.SelectMany<ActionModel, SelectorModel>(
            (Func<ActionModel, IEnumerable<SelectorModel>>)(a =>
              (IEnumerable<SelectorModel>)a.Selectors)), tokenValue);
      }
    }

    private void UpdateSelectors(IEnumerable<SelectorModel> selectors, string? tokenValue)
    {
      foreach (SelectorModel selectorModel in selectors.Where<SelectorModel>(
                 (Func<SelectorModel, bool>)(s => s.AttributeRouteModel != null)))
      {
        if (selectorModel.AttributeRouteModel == null)
        {
          continue;
        }

        selectorModel.AttributeRouteModel.Template =
          this.InsertTokenValue(selectorModel.AttributeRouteModel.Template, tokenValue);
        selectorModel.AttributeRouteModel.Name =
          this.InsertTokenValue(selectorModel.AttributeRouteModel.Name, tokenValue);
      }
    }

    private string? InsertTokenValue(string? template, string? tokenValue) => template == null
      ? template
      : Regex.Replace(template, this._tokenRegex, tokenValue);
  }
}
