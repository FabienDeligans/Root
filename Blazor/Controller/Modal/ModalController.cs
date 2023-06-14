using Blazored.Modal;
using Blazored.Modal.Services;
using Microsoft.AspNetCore.Components;

namespace Blazor.Controller.Modal
{
    public class ModalController : ComponentBase
    {
        [CascadingParameter]
        public IModalService _modalService { get; set; } = default!;

        public ModalController(IModalService modalService)
        {
            _modalService = modalService;
        }

        #region ShowModal
        public async Task<object?> ShowModal<T>() where T : IComponent
        {
            return await ShowModal<T>("");
        }
        public async Task<object?> ShowModal<T>(string title) where T : IComponent
        {
            return await ShowModal<T>(title, new ModalParameters(), new ModalOptions());
        }
        public async Task<object?> ShowModal<T>(string title, ModalOptions options) where T : IComponent
        {
            return await ShowModal<T>(title, new ModalParameters(), options);
        }
        public async Task<object?> ShowModal<T>(string title, ModalParameters parameters) where T : IComponent
        {
            return await ShowModal<T>(title, parameters, new ModalOptions());
        }
        #endregion

        public async Task<object?> ShowModal<T>(string title, ModalParameters parameters, ModalOptions options) where T : IComponent
        {
            var modal = _modalService.Show<T>(title, parameters, options);
            var result = await modal.Result;

            return result.Confirmed ? result.Data : null;
        }

        #region ShowModalAlert
        public async Task ShowModalAlert(string text, int millisecond, string alert)
        {
            await ShowModalAlert(text, millisecond, alert, new ModalParameters(), new ModalOptions());
        }
        public async Task ShowModalAlert(string text, int millisecond, string alert, ModalOptions options)
        {
            await ShowModalAlert(text, millisecond, alert, new ModalParameters(), options);
        }
        public async Task ShowModalAlert(string title, int millisecond, string alert, ModalParameters parameters)
        {
            await ShowModalAlert(title, millisecond, alert, parameters, new ModalOptions());
        }
        #endregion

        public async Task ShowModalAlert(string text, int millisecond, string alert, ModalParameters parameters, ModalOptions options)
        {
            parameters.Add(nameof(AlertComponent.Message), text);
            parameters.Add(nameof(AlertComponent.ClassCSS), alert);

            options.Class = alert;
            options.UseCustomLayout = true;

            var modal = _modalService.Show<AlertComponent>("", parameters, options);

            await Task.Delay(millisecond);

            modal.Close();
        }

        public async Task ShowSpinner(Func<Task> func)
        {
            var modalOption = new ModalOptions()
            {
                UseCustomLayout = true,
                DisableBackgroundCancel = true,
                HideCloseButton = true,
                HideHeader = true,
                Position = ModalPosition.Middle,
                SizeCustomClass = "fullscreen-modal"
            };
            var modal = _modalService.Show<Spinner>("", modalOption);

            try
            {
                await func.Invoke();
            }
            finally
            {
                modal.Close();
            }

        }
    }

    public static class Alert
    {
        public const string Primary = "alert alert-primary";
        public const string Secondary = "alert alert-secondary";
        public const string Success = "alert alert-success";
        public const string Danger = "alert alert-danger";
        public const string Warning = "alert alert-warning";
        public const string Info = "alert alert-info";
        public const string Light = "alert alert-light";
        public const string Dark = "alert alert-dark";

    }
}
