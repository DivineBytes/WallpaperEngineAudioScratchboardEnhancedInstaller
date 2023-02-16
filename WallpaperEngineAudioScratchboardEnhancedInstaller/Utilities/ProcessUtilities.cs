﻿using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace WallpaperAudioScratchboardEnhancedInstaller.Utilities
{
    /// <summary>
    /// The <see cref="ProcessUtilities"/> class.
    /// </summary>
    public static class ProcessUtilities
    {
        /// <summary>
        /// The start process.
        /// </summary>
        /// <param name="processStartInfo">The process start info.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool StartProcess(ProcessStartInfo processStartInfo)
        {
            if (processStartInfo == null)
            {
                throw new ArgumentNullException(nameof(processStartInfo), "Cannot be null");
            }

            try
            {
                Process process = Process.Start(processStartInfo);
                return true;
            }
            catch (Win32Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            return false;
        }

        /// <summary>
        /// The start process.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="runAsAdmin">The run as admin.</param>
        /// <param name="createNoWindow">The create no window.</param>
        /// <param name="shellExecute">The shell execute.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool StartProcess(string fileName, string arguments = "", bool runAsAdmin = false, bool createNoWindow = false, bool shellExecute = false)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName), "Cannot be null");
            }

            FileInfo fileInfo = new FileInfo(fileName);
            string workingDirectory = fileInfo.DirectoryName;

            return StartProcess(fileName, arguments, workingDirectory, runAsAdmin, createNoWindow, shellExecute);
        }

        /// <summary>
        /// The start process.
        /// </summary>
        /// <param name="fileName">The file name.</param>
        /// <param name="arguments">The arguments.</param>
        /// <param name="workingDirectory">The working directory.</param>
        /// <param name="runAsAdmin">The run as admin.</param>
        /// <param name="createNoWindow">The create no window.</param>
        /// <param name="shellExecute">The shell execute.</param>
        /// <returns>The <see cref="bool" />.</returns>
        public static bool StartProcess(string fileName, string arguments = "", string workingDirectory = "", bool runAsAdmin = false, bool createNoWindow = false, bool shellExecute = false)
        {
            if (fileName == null)
            {
                throw new ArgumentNullException(nameof(fileName), "Cannot be null");
            }

            if (workingDirectory == null)
            {
                throw new ArgumentNullException(nameof(workingDirectory), "Cannot be null");
            }

            ProcessStartInfo processStartInfo = new ProcessStartInfo(fileName)
            {
                Arguments = arguments,
                CreateNoWindow = createNoWindow,
                UseShellExecute = shellExecute,
                WorkingDirectory = workingDirectory
            };

            if (runAsAdmin)
            {
                processStartInfo.Verb = "runas";
            }

            return StartProcess(processStartInfo);
        }
    }
}