using ServiceReleaseManager.Infrastructure.GitHub.Entities;

namespace ServiceReleaseManager.Infrastructure.GitHub.Converters;

/// <summary>
///   Converts a list of GitHub entities to a readable changelog
/// </summary>
public interface IChangeLogConverter
{
  /// <summary>
  ///   Converts a list of GitHub entities to a readable changelog
  /// </summary>
  /// <param name="list">The list of GitHub entities</param>
  /// <param name="prefix">Prefix of each element in the list</param>
  /// <typeparam name="T">Type of GitHub entity</typeparam>
  /// <returns>A readable changelog</returns>
  string Convert<T>(IEnumerable<T> list, string prefix) where T : GitHubEntity;
}
