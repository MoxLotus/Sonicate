using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.Extensions;

public static class DictionaryExtensions
{
    /// <summary>
    /// If the key exists in the dictionary, returns its value; otherwise, returns an empty string.
    /// </summary>
    public static string GetValueOrEmpty(this IDictionary<string, string> dictionary, string key)
    {
        return dictionary.TryGetValue(key, out string? value) ? value ?? string.Empty : string.Empty;
    }
}
