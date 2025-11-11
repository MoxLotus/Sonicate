using Avalonia.Controls;
using Avalonia.Platform.Storage;
using DynamicData;
using FFMpegCore.Enums;
using ReactiveUI;
using Sonicate.Core.DTOs;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

namespace Sonicate.GUI.Services;

public class FileService(Window target) : IFileService
{
    private readonly Window _target = target;

    public async Task<IReadOnlyList<IStorageFolder>> OpenFolderPickerAsync(string title)
    {
        var folders = await _target.StorageProvider.OpenFolderPickerAsync(new FolderPickerOpenOptions()
        {
            Title = title,
            AllowMultiple = false
        });

        return folders;
    }

    public async Task<List<FileDescriptor>> GetFileDescriptorsFromFolderAsync(string title)
    {
        List<FileDescriptor> descriptors = [];

        IReadOnlyList<IStorageFolder> folder = await OpenFolderPickerAsync("Select Folder");
        await foreach (var item in folder[0].GetItemsAsync())
        // TODO: Handle Recursion?
            if (item is IStorageFile)
                descriptors.Add(new FileDescriptor(item.Name, item.Path));

        return descriptors;
    }
}
