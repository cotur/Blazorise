﻿#region Using directives
using System;
using Microsoft.AspNetCore.Components;
#endregion

namespace Blazorise.Snackbar
{
    /// <summary>
    /// Holds the snackbar message state.
    /// </summary>
    public class SnackbarOptions
    {
        /// <summary>
        /// Custom key for snack bar message.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// Custom snackbar template to render.
        /// </summary>
        public RenderFragment MessageTemplate { get; set; }

        /// <summary>
        /// Custom close button text.
        /// </summary>
        public string CloseButtonText { get; set; }

        /// <summary>
        /// Custom close button icon. Can be either enum <see cref="IconName"/> or string eg. "fa-times".
        /// </summary>
        public object CloseButtonIcon { get; set; }

        /// <summary>
        /// Custom action button text.
        /// </summary>
        public string ActionButtonText { get; set; }

        /// <summary>
        /// Custom action button icon. Can be either enum <see cref="IconName"/> or string eg. "fa-times".
        /// </summary>
        public object ActionButtonIcon { get; set; }
    }
}
