using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CookBook.Services.Interfaces
{
    // Serwis, który pozwala na wyświetlanie okienek z error messages
    public interface IDialogService
    {
        Task ShowDialog(string message, string title, string buttonLabel);
        Task<bool> ConfirmDialog(string message, string title, string optionOk, string optionNo);
        Task<string> ActionSheet(string message, string cancel, string delete, string[] options);
    }
}
