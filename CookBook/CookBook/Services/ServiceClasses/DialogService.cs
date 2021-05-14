using Acr.UserDialogs;
using CookBook.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CookBook.Services.ServiceClasses
{
    // Serwis w zasadzie potrzebny do wygodniejszego używania okienek dialogowych
    public class DialogService : IDialogService
    {
        public Task<string> ActionSheet(string message, string cancel, string delete, string[] options)
        {
            return UserDialogs.Instance.ActionSheetAsync(message, cancel, delete, CancellationToken.None, options);
        }

        public Task<bool> ConfirmDialog(string message, string title, string optionOk, string optionNo)
        {
            return UserDialogs.Instance.ConfirmAsync(message, title, optionOk, optionNo);
        }

        public Task<Acr.UserDialogs.PromptResult> InputDialog(string message, string title, string optionOk, string optionNo)
        {
            return UserDialogs.Instance.PromptAsync(message, title, optionOk, optionNo);
        }

        public Task ShowDialog(string message, string title, string buttonLabel)
        {
            return UserDialogs.Instance.AlertAsync(message, title, buttonLabel);
        }
    }
}
