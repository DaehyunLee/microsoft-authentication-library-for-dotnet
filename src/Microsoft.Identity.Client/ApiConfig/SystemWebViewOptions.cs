﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using Microsoft.Identity.Client.Core;

namespace Microsoft.Identity.Client
{
    // Default browser WebUI is not available on mobile (Android, iOS, UWP), but allow it at runtime
    // to avoid MissingMethodException

    /// <summary>
    /// Options for using the default OS browser as a separate process to handle interactive auth.
    /// MSAL will be listening for the OS browser to finish authenticating, but it cannot close the browser.
    /// It can however respond with a 200 OK message or a 302 Redirect, which can be configured here.
    /// For more details see https://aka.ms/msal-net-os-browser
    /// </summary>
#if NET_CORE || NETSTANDARD || DESKTOP || RUNTIME
    public
#else
    internal
#endif
    class SystemWebViewOptions
    {
        /// <summary>
        /// When the user finishes authenticating, MSAL will respond with a 200 OK message,
        /// which the browser will show to the user. 
        /// </summary>
        public string HtmlMessageSuccess { get; set; }

        /// <summary>
        /// When the user finishes authenticating, but an error occurred,
        /// MSAL will respond with a 200 OK message, which the browser will show to the user.
        /// You can use a string format e.g. "An error has occurred: {0} details: {1}"
        /// </summary>
        public string HtmlMessageError { get; set; }

        /// <summary>
        /// When the user finishes authenticating, MSAL will redirect the browser to the given Uri
        /// </summary>
        /// <remarks>Takes precedence over <see cref="HtmlMessageSuccess"/></remarks>
        public Uri BrowserRedirectSuccess { get; set; }

        /// <summary>
        /// When the user finishes authenticating, but an error occurred, MSAL will redirect the browser to the given url
        /// </summary>
        /// <remarks>Takes precedence over <see cref="HtmlMessageError"/></remarks>
        public Uri BrowserRedirectError { get; set; }

        internal void LogParameters(ICoreLogger logger)
        {
            logger.Info("DefaultBrowserOptions configured");

            logger.InfoPii("HtmlMessageSuccess " + HtmlMessageSuccess,
                "HtmlMessageSuccess? " + !String.IsNullOrEmpty(HtmlMessageSuccess));
            logger.InfoPii("HtmlMessageError " + HtmlMessageError,
               "HtmlMessageError? " + !String.IsNullOrEmpty(HtmlMessageError));
            logger.InfoPii("BrowserRedirectSuccess " + BrowserRedirectSuccess,
               "BrowserRedirectSuccess? " + (BrowserRedirectSuccess != null));
            logger.InfoPii("BrowserRedirectError " + BrowserRedirectError,
               "BrowserRedirectError? " + (BrowserRedirectError != null));
        }
    }
}