﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.IO.Compression;
using System.Threading;

namespace GameLauncherUpdate {
    public partial class Form1 : Form {
        string tempNameZip = Path.GetTempFileName();

        public Form1() {
            InitializeComponent();
        }

        public void error(string error) {
            button1.Enabled = false;
            information.Text = error.ToString();
            Delay.WaitSeconds(2);
            this.Close();
            Application.Exit();
        }

        public void success(string success) {
            information.Text = success.ToString();
        }

        public void update() {
            Delay.WaitSeconds(1);

            if (File.Exists("GameLauncher.exe")) {
                var versionInfo = FileVersionInfo.GetVersionInfo("GameLauncher.exe");
                string version = versionInfo.ProductVersion;
                version = "1.9.0.0";

                success("Found version " + version + ". Checking for update...");

                var client = new WebClient();
                Uri StringToUri = new Uri("http://nfsw.metonator.ct8.pl/checkUpdate.php?version=" + version);
                client.CancelAsync();
                client.DownloadStringAsync(StringToUri);
                client.DownloadStringCompleted += (sender2, e2) => {
                    try {
                        CheckVersion json = JsonConvert.DeserializeObject<CheckVersion>(e2.Result);

                        if(json.update.info != false) {
                            Thread thread = new Thread(() => {
                                WebClient client2 = new WebClient();
                                client2.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                                client2.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);
                                client2.DownloadFileAsync(new Uri(json.update.download), tempNameZip);
                            });
                            thread.Start();
                        } else {
                            error("Starting GameLauncher.exe");
                            Process.Start(@"GameLauncher.exe");
                        }
                    } catch(Exception ex) {
                        error("Failed to update. " + ex.Message);    
                    }
                };
            } else {
                error("Failed to find GameLauncher.exe");
            }
        }

        private string FormatFileSize(long byteCount) {
            double[] numArray = new double[] { 1073741824, 1048576, 1024, 0 };
            string[] strArrays = new string[] { "GB", "MB", "KB", "Bytes" };
            for (int i = 0; i < (int)numArray.Length; i++) {
                if ((double)byteCount >= numArray[i]) {
                    return string.Concat(string.Format("{0:0.00}", (double)byteCount / numArray[i]), strArrays[i]);
                }
            }

            return "0 Bytes";
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e) {
            this.BeginInvoke((MethodInvoker)delegate {
                double bytesIn = double.Parse(e.BytesReceived.ToString());
                double totalBytes = double.Parse(e.TotalBytesToReceive.ToString());
                double percentage = bytesIn / totalBytes * 100;
                information.Text = "Downloaded " + FormatFileSize(e.BytesReceived) + " of " + FormatFileSize(e.TotalBytesToReceive);
                downloadProgress.Style = ProgressBarStyle.Blocks;
                downloadProgress.Value = int.Parse(Math.Truncate(percentage).ToString());
            });
        }

        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e) {
            this.BeginInvoke((MethodInvoker)delegate {
                downloadProgress.Style = ProgressBarStyle.Marquee;

                string updatePath = Path.GetDirectoryName(Application.ExecutablePath) + "\\";
                using (ZipArchive archive = ZipFile.OpenRead(tempNameZip)) {
                    int numFiles = archive.Entries.Count;
                    int current = 1;

                    downloadProgress.Style = ProgressBarStyle.Blocks;

                    foreach (ZipArchiveEntry entry in archive.Entries) {
                        string fullName = entry.FullName;

                        if (fullName.Substring(fullName.Length - 1) == "/") {
                            string folderName = fullName.Remove(fullName.Length - 1);
                            if (Directory.Exists(folderName)) {
                                Directory.Delete(folderName, true);
                            }

                            Directory.CreateDirectory(folderName);
                        } else {
                            if (fullName != "Newtonsoft.Json.dll" && fullName != "GameLauncherUpdate.exe" && fullName != "GameLauncherUpdate.pdb") {
                                if (File.Exists(fullName)) {
                                    File.Delete(fullName);
                                }

                                information.Text = "Extracting: " + fullName;
                                entry.ExtractToFile(Path.Combine(updatePath, fullName));
                                Delay.WaitMSeconds(200);
                            }
                        }

                        downloadProgress.Value = (int)((long)100 * current / numFiles);
                        current++;
                    }
                }

                error("Update completed. Starting GameLauncher.exe");
                Process.Start(@"GameLauncher.exe");
            });
        }

        private void button1_Click(object sender, EventArgs e) {
            error("Rolling back update...");
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.BeginInvoke((MethodInvoker)delegate {
                update();
            });
        }
    }
}
