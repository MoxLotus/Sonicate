using Avalonia.Platform.Storage;
using GUI.Services;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace GUI.ViewModels
{
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
                var files = await DoOpenFolderPickerAsync();
                SelectedFilePath = files?.Path.LocalPath ?? "";
            });
        }

        private async Task<IStorageFolder?> DoOpenFolderPickerAsync()
        {
            IFileService fileAccessService = GetFileAccessService();
            //TODO: Fetch children
            return await fileAccessService.OpenFolderPickerAsync("Select Folder");
        }
    }
}
