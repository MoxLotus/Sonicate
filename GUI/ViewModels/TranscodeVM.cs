using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sonicate.GUI.ViewModels;

public class TranscodeVM : MainVM.Child
{
    public ObservableCollection<string> Options { get; } =
        [
            "libx264",
            "libx265",
        ];

    private string _selectedOption = "libx265";
    public string SelectedOption
    {
        get => _selectedOption;
        set => this.RaiseAndSetIfChanged(ref _selectedOption, value);
    }

    public string SelectedNumberFormatted => $"CRF: {SelectedNumber} ";

    private int _selectedNumber = 21;
    public int SelectedNumber
    {
        get => _selectedNumber;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedNumber, value);
            this.RaisePropertyChanged(nameof(SelectedNumberFormatted));
        }
    }
}
