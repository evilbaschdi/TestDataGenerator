using EvilBaschdi.Core.Wpf.Mvvm.ViewModel.Command;

namespace TestDataGenerator.Wpf.Internal;

/// <summary>
/// </summary>
public static class CommandExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="execute"></param>
    /// <param name="text"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static DefaultCommand Create(Action execute, string text = null)
    {
        ArgumentNullException.ThrowIfNull(execute);

        return new()
               {
                   Command = new RelayCommand(_ => execute()),
                   Text = text
               };
    }
}