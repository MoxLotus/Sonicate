using Avalonia.Platform.Storage;
using System.Threading.Tasks;

namespace GUI.Services;

public interface IFileService
{
    public Task<IStorageFolder?> OpenFolderPickerAsync(string title);
}
