using System.Text;
using ServiceReleaseManager.Infrastructure.GitHub.Entities;

namespace ServiceReleaseManager.Infrastructure.GitHub.Converters;

/// <summary>
/// Converts a list of GitHub entities to a readable changelog
/// </summary>
public class ChangeLogConverter : IChangeLogConverter
{
  /// <summary>
  /// Converts a list of GitHub entities to a readable changelog
  /// </summary>
  /// <param name="list">The list of GitHub entities</param>
  /// <param name="prefix">Prefix of each element in the list</param>
  /// <typeparam name="T">Type of GitHub entity</typeparam>
  /// <returns>A readable changelog</returns>
  public string Convert<T>(IEnumerable<T> list, string prefix) where T : GitHubEntity
  {
    var builder = new StringBuilder();
    foreach (var entity in list)
    {
      builder.Append(prefix);
      builder.Append(entity.Title.Replace("\n", "\n\t"));
      builder.AppendLine();
    }

    return builder.ToString();
  }
}
