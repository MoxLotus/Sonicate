using Sonicate.GUI.Services;
using ReactiveUI;
using System;

namespace Sonicate.GUI.ViewModels;

public class ViewModelBase : ReactiveObject
{
    protected IFileService GetFileAccessService()
    {
        IFileService? fileAccessService = App.Current?.Services?.GetService(typeof(IFileService)) as IFileService;
        if (fileAccessService is null) throw new NullReferenceException("Missing File Service instance.");
        return fileAccessService;
    }
}
