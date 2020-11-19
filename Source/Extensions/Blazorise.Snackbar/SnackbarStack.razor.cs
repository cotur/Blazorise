﻿#region Using directives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Timers;
using Blazorise.Snackbar.Utils;
using Microsoft.AspNetCore.Components;
#endregion

namespace Blazorise.Snackbar
{
    public partial class SnackbarStack : BaseComponent
    {
        #region Members

        private class SnackbarInfo
        {
            public SnackbarInfo( string message,
                string title,
                SnackbarColor color,
                string key,
                RenderFragment messageTemplate,
                string closeButtonText,
                object closeButtonIcon,
                string actionButtonText,
                object actionButtonIcon )
            {
                Message = message;
                Title = title;
                Color = color;
                Key = key ?? Guid.NewGuid().ToString();
                MessageTemplate = messageTemplate;
                CloseButtonText = closeButtonText;
                CloseButtonIcon = closeButtonIcon;
                ActionButtonText = actionButtonText;
                ActionButtonIcon = actionButtonIcon;
            }

            public string Message { get; }

            public string Title { get; }

            public SnackbarColor Color { get; }

            public string Key { get; }

            public RenderFragment MessageTemplate { get; }

            public string CloseButtonText { get; }

            public object CloseButtonIcon { get; }

            public string ActionButtonText { get; }

            public object ActionButtonIcon { get; }

            public bool Visible { get; } = true;
        }

        private SnackbarStackLocation location = SnackbarStackLocation.Center;

        private List<SnackbarInfo> snackbarInfos = new List<SnackbarInfo>();

        #endregion

        #region Methods

        protected override void BuildClasses( ClassBuilder builder )
        {
            builder.Append( "snackbar-stack" );
            builder.Append( $"snackbar-stack-{Location.GetName()}" );

            base.BuildClasses( builder );
        }

        public Task PushAsync( string message, SnackbarColor color = SnackbarColor.None, Action<SnackbarOptions> options = null )
        {
            return PushAsync( message, null, color, options );
        }

        public Task PushAsync( string message, string title = null, SnackbarColor color = SnackbarColor.None, Action<SnackbarOptions> options = null )
        {
            var snackbarOptions = CreateDefaultOptions();
            options?.Invoke( snackbarOptions );

            snackbarInfos.Add( new SnackbarInfo( message, title, color,
                snackbarOptions.Key,
                snackbarOptions.MessageTemplate,
                snackbarOptions.CloseButtonText,
                snackbarOptions.CloseButtonIcon,
                snackbarOptions.ActionButtonText,
                snackbarOptions.ActionButtonIcon ) );

            return InvokeAsync( () => StateHasChanged() );
        }

        private Task OnSnackbarClosed( string key, SnackbarCloseReason closeReason )
        {
            var info = snackbarInfos.FirstOrDefault( x => x.Key == key );

            if ( info != null )
                snackbarInfos.Remove( info );

            StateHasChanged();

            return Closed.InvokeAsync( new SnackbarClosedEventArgs( key, closeReason ) );
        }

        protected virtual SnackbarOptions CreateDefaultOptions()
        {
            return new SnackbarOptions
            {
                Key = Guid.NewGuid().ToString(),
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Defines the snackbar stack location.
        /// </summary>
        [Parameter]
        public SnackbarStackLocation Location
        {
            get => location;
            set
            {
                location = value;

                DirtyClasses();
            }
        }

        /// <summary>
        /// Allow snackbar to show multiple lines of text.
        /// </summary>
        [Parameter] public bool Multiline { get; set; }

        /// <summary>
        /// Defines the interval(in milliseconds) after which the snackbars will be automatically closed.
        /// </summary>
        [Parameter] public double Interval { get; set; } = 3000;

        /// <summary>
        /// Defines a text to show for snackbar close button. Leave as null to not show it!
        /// </summary>
        [Parameter] public string CloseButtonText { get; set; }

        /// <summary>
        /// Defines an icon to show for snackbar close button. Leave as null to not show it!
        /// </summary>
        [Parameter] public object CloseButtonIcon { get; set; }

        /// <summary>
        /// Defines a text to show for snackbar action button. Leave as null to not show it!
        /// </summary>
        [Parameter] public string ActionButtonText { get; set; }

        /// <summary>
        /// Defines an icon to show for snackbar action button. Leave as null to not show it!
        /// </summary>
        [Parameter] public object ActionButtonIcon { get; set; }

        /// <summary>
        /// Occurs after the snackbar has closed.
        /// </summary>
        [Parameter] public EventCallback<SnackbarClosedEventArgs> Closed { get; set; }

        /// <summary>
        /// Specifies the content to be rendered inside this <see cref="SnackbarStack"/>.
        /// </summary>
        [Parameter] public RenderFragment ChildContent { get; set; }

        #endregion
    }
}
