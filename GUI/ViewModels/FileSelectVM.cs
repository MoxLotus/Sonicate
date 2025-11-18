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
using Sonicate.Core.DTOs;

namespace Sonicate.GUI.ViewModels;

public class FileSelectVM : MainVM.Child
{
    protected static IFileService GetFileAccessService()
    {
        if (App.Current?.Services?.GetService(typeof(IFileService)) is not IFileService fileAccessService) throw new NullReferenceException("Missing File Service instance.");
        return fileAccessService;
    }

    private string _selectedFilePath = "";
    public string SelectedFilePath
    {
        get => _selectedFilePath;
        set => this.RaiseAndSetIfChanged(ref _selectedFilePath, value);
    }

    public ReactiveCommand<Unit, Unit> SelectFilesCommand { get; }

    public FileSelectVM()
    {
        SelectFilesCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            Debug.WriteLine("SelectFilesCommand executed");
            IFileService fileAccessService = GetFileAccessService();
            var files = await fileAccessService.GetFileDescriptorsFromFolderAsync("Select Folder");
            Debug.WriteLine($"Number of files selected: {files.Count}");
            if (files.Count == 0) return;
            SelectedFilePath = files[0].Path.LocalPath ?? "";
            //TODO: Initialize metadata service correctly
            Debug.WriteLine("Initializing FFmpegMetadataService");
            FFmpegMetadataService serv = new();
            ScrollSyncService scrollSync = new();
            MediaFiles.Clear();
            foreach (var file in files)
                MediaFiles.Add(new(file, await serv.GetMetadataAsync(file.Path.LocalPath), scrollSync));
            Parent.PropagateData();
        });
    }

    private ObservableCollection<MediaInfoVM> _mediaFiles = [];
    public ObservableCollection<MediaInfoVM> MediaFiles
    {
        get => _mediaFiles;
        private set => this.RaiseAndSetIfChanged(ref _mediaFiles, value);
    }
}
