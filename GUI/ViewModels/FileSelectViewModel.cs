using Avalonia.Platform.Storage;
using Sonicate.GUI.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

using Sonicate.Core.Services;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace Sonicate.GUI.ViewModels;

public class FileSelectViewModel : ViewModelBase
{
    private string _selectedFilePath = "";
    public string SelectedFilePath
    {
        get => _selectedFilePath;
        set => this.RaiseAndSetIfChanged(ref _selectedFilePath, value);
    }

    public ReactiveCommand<Unit, Unit> SelectFilesCommand { get; }

    public FileSelectViewModel()
    {
        SelectFilesCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var folder = await DoOpenFolderPickerAsync();
            SelectedFilePath = folder?.Path.LocalPath ?? "";
            List<IStorageFile> files = [];
            await foreach (var item in folder?.GetItemsAsync())
                if (item is IStorageFile file)
                    files.Add(file);
            //TODO: Initialize metadata service correctly
            FFmpegMetadataService serv = new();
            MediaFiles.Clear();
            foreach (var file in files)
                MediaFiles.Add(new(file, await serv.GetMetadataAsync(file.Path.LocalPath)));
        });
    }

    private async Task<IStorageFolder?> DoOpenFolderPickerAsync()
    {
        IFileService fileAccessService = GetFileAccessService();
        //TODO: Fetch children
        return await fileAccessService.OpenFolderPickerAsync("Select Folder");
    }

    private ObservableCollection<MediaInfoViewModel> _mediaFiles = new();
    public ObservableCollection<MediaInfoViewModel> MediaFiles
    {
        get => _mediaFiles;
        private set => this.RaiseAndSetIfChanged(ref _mediaFiles, value);
    }
}
