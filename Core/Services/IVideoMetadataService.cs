using FFMpegCore;
using FFMpegCore.Enums;
using FFMpegCore.Pipes;
using Sonicate.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

namespace Sonicate.Core.Services;

public interface IVideoMetadataService
{
    public abstract Task<ContainerInfo> GetMetadataAsync(string filePath);
}
