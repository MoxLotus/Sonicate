using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class OutputOptions(string filename)
{
    public string Filename { get; set; } = filename;
    public string Format = "mkv";
}
