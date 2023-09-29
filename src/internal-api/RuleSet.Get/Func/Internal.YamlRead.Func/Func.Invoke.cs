using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace GarageGroup.Platform.DataverseToSqlSync;

partial class YamlReadFunc
{
    public async Task<ConfigurationYaml> InvokeAsync(string filePath, CancellationToken cancellationToken = default)
    {
        var fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
        var yaml = await File.ReadAllTextAsync(fullPath, cancellationToken).ConfigureAwait(false);

        if (string.IsNullOrEmpty(yaml))
        {
            return default;
        }

        return YamlDeserializer.Deserialize<ConfigurationYaml>(yaml);
    }
}