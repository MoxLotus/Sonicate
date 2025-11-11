using Avalonia.Platform.Storage;
using Sonicate.Core.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sonicate.GUI.Services;

public interface IFileService
{
    public Task<List<FileDescriptor>> GetFileDescriptorsFromFolderAsync(string title);
}
