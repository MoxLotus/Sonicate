using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class FileDescriptor(string name, Uri path)
{
    public string Name { get; } = name;
    public Uri Path { get; } = path;
}
