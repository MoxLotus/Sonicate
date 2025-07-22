using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.Core.DTOs;

public class TaskOptions(string outfile)
{
    public OutputOptions Output { get; set; } = new OutputOptions(outfile);
    internal List<InputOptions> Inputs { get; set; } = [];
    public void AddInput(InputOptions input)
    {
        Inputs.Add(input);
    }
}
