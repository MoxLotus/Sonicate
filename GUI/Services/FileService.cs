using Avalonia.Controls;
using Avalonia.Platform.Storage;
using System.Threading.Tasks;

namespace Sonicate.GUI.Services;

public class FileService(Window target) : IFileService
{
    private readonly Window _target = target;

    public async Task<IStorageFolder?> OpenFolderPickerAsync(string title)
    {
        var folders = await _target.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = title,
            AllowMultiple = false
        });

        return folders[0];
    }
}
