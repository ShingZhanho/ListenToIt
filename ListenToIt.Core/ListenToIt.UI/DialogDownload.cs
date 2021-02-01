﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ListenToIt.UI {
    public partial class DialogDownload : Form {
        private DialogDownload() {
            InitializeComponent();
        }

        public DialogDownload(string word) : this() {
            // Search and download here
            var bgw = new BackgroundWorker {
                WorkerReportsProgress = false, 
                WorkerSupportsCancellation = true
            };
            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            var arguments = new List<object> {word};
            bgw.RunWorkerAsync(arguments);
        }

        public SearchResults Results { get; } = new SearchResults();

        /// <summary>
        /// Class for search results
        /// </summary>
        public class SearchResults {
            public SearchStatus Status = SearchStatus.CanceledOrNotFinished;
            public ErrorType Error = ErrorType.NoError;
            public List<PronunciationEntry> Entries;

            public class PronunciationEntry {
                public string Word, Pos, UkLink, UkFile, UkPrn, UsLink, UsFile, UsPrn;
            }

            public enum SearchStatus {
                Success = 0,
                Failed = 1,
                CanceledOrNotFinished = 2
            }

            public enum ErrorType {
                NoError = 0,
                WordNotFound = 1,
                UnknownError = 2,
                Canceled = 3
            }
        }

        // BackgroundWorker's methods
        private void bgw_DoWork(object sender, DoWorkEventArgs e) {
            var arguments = (List<object>) e.Argument;
            var results = new List<object>();

            // Search for word
            var proc = new Process() {
                StartInfo = new ProcessStartInfo() {
                    FileName = @".\Tools\dict_grabber.exe",
                    Arguments = $"--Action#Search --SearchWord#{arguments[0]} " + // Use arguments[0] as word to search
                                $"--OutDir#{Application.StartupPath}",
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };
            proc.Start();
            proc.WaitForExit();

            // Check for cancel
            if (e.Cancel) {
                results.Add(new SearchResults {
                    Status = SearchResults.SearchStatus.CanceledOrNotFinished,
                    Error = SearchResults.ErrorType.Canceled
                });
                e.Result = results;
                return;
            }

            // Get results
            JObject jo;
            try {
                jo = JObject.Parse(new StreamReader(Path.Combine(Application.StartupPath,
                    $"enquiry_results_{arguments[0]}.json")).ReadToEnd());
            } catch {
                    results.Add(new SearchResults {
                    Status = SearchResults.SearchStatus.Failed,
                    Error = SearchResults.ErrorType.UnknownError
                });
                e.Result = results;
                return;
            }

            // If returns error
            if (!(jo["error_code"] is null)) {
                var error = new SearchResults {
                    Status = SearchResults.SearchStatus.Failed
                };
                switch (jo["error_code"].ToString()) {
                    case "FAILURE_NOT_FOUND":
                        error.Error = SearchResults.ErrorType.WordNotFound;
                        break;
                    default:
                        error.Error = SearchResults.ErrorType.UnknownError;
                        break;
                }
                results.Add(error);
                return;
            }

            var dir = Path.Combine(Application.StartupPath, "audio", arguments[0].ToString());
            Directory.CreateDirectory(dir);

            foreach (var entry in jo["grabbed_data"]) {
                // Check for cancel
                if (e.Cancel) {
                    results.Add(new SearchResults {
                        Status = SearchResults.SearchStatus.CanceledOrNotFinished,
                        Error = SearchResults.ErrorType.Canceled
                    });
                    e.Result = results;
                    return;
                }

                // Download audio files
                var webClient = new WebClient();
                webClient.DownloadFile(entry["uk_audio_url"].ToString(), 
                    Path.Combine(dir, $"uk_{jo["grabbed_data"].ToList().IndexOf(entry)}.mp3.tmp"));
                webClient.DownloadFile(entry["us_audio_url"].ToString(),
                    Path.Combine(dir, $"us_{jo["grabbed_data"].ToList().IndexOf(entry)}.mp3.tmp"));
            }
        }

        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            var results = (List<object>) e.Result;

            // If canceled
            if (e.Cancelled) {

            }
        }
    }
}
