﻿// Marathon is licensed under the MIT License:
/* 
 * MIT License
 * 
 * Copyright (c) 2021 HyperBE32
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all
 * copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

using System;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Forms;
using System.Security.Principal;
using System.Collections.Generic;
using Marathon.Toolkit.Forms;

namespace Marathon.Toolkit
{
    static class Program
    {
        public static readonly string Version  = FileVersionInfo.GetVersionInfo(Assembly.GetExecutingAssembly().Location).FileVersion,
                                      CommitID = Application.ProductVersion,              // The informational version generated by continuous integration.
                                      Location = Application.ExecutablePath,              /* More convenient to store these here, rather than constantly requiring... */
                                      WorkingDirectory = Path.GetDirectoryName(Location); /* ...System.Windows.Forms every time I need to use these. */

        // Full dictionary of supported file types that can be used globally.
        public static Dictionary<string, string> FileTypes = new Dictionary<string, string>();

        // Grants public accessibility to the main workspace form.
        public static Workspace Workspace;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Parse the file types into the void so we can populate the dictionary.
            Resources.ParseFileTypesToFilter(Marathon.Properties.Resources.FileTypes);

            // Force culture info 'en-GB' to prevent errors with values altered by language-specific differences.
            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo("en-GB");

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#if !DEBUG
            // Redirect exceptions to Marathon's error handler in Release builds.
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => new ErrorHandler(e.Exception).ShowDialog();
#endif
            // Pass new workspace to the public accessor before running.
            Application.Run(Workspace = new Workspace());
        }

        /// <summary>
        /// Returns a friendly string of the file version and informational version.
        /// </summary>
        public static string GetFriendlyVersion()
            => $"Version {Version}-{CommitID.Substring(0, 7)}";

        /// <summary>
        /// Returns the version string for the title of the window based on flags.
        /// </summary>
        public static string GetExtendedInformation(string text = "", bool encased = true)
        {
            string version = GetFriendlyVersion();

            text += encased ? $" ({version})" : $" {version}";
#if DEBUG
            text += $" [Debug]";
#endif
            text += RunningAsAdmin() ? " <Administrator>" : string.Empty;

            return text;
        }

        /// <summary>
        /// Returns the process architecture.
        /// </summary>
        public static string Architecture()
            => Environment.Is64BitProcess ? "x64" : "x86";

        /// <summary>
        /// Returns the process role state.
        /// </summary>
        public static bool RunningAsAdmin()
            => new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

        /// <summary>
        /// Performs the requested action in the public workspace.
        /// </summary>
        /// <param name="action">Action to perform.</param>
        public static void InvokeWorkspace(Action action)
        {
            // Ensure it's safe to invoke first.
            if (Workspace == null || Workspace.Disposing || Workspace.IsDisposed)
                return;

            // Invoke function to public workspace.
            Workspace.Invoke(action);
        }

        /// <summary>
        /// Redirects the user to the GitHub issues page.
        /// </summary>
        /// <param name="title">Title used for the issue.</param>
        /// <param name="body">Text automatically added to the issue.</param>
        /// <param name="labels">Labels automatically added to the issue.</param>
        public static void InvokeFeedback(string title = "[Marathon.Toolkit]", string body = "", string labels = "")
        {
            // This doesn't look elegant, but it's the quickest way of doing it.
            Process.Start
            (
                Marathon.Properties.Resources.URL_GitHubIssueNew +
                Uri.EscapeUriString("?title=" + (string.IsNullOrEmpty(title) ? "[Marathon.Toolkit]" : title) + " ") + // Issue Title.
                (string.IsNullOrEmpty(body) ? string.Empty : $"&body={Uri.EscapeDataString(body)}") +                 // Issue Body.
                Uri.EscapeUriString("&labels=" + (string.IsNullOrEmpty(labels) ? string.Empty : labels))              // Issue Labels.
            );
        }
    }
}