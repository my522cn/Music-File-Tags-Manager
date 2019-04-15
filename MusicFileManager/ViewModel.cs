using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MicroMvvm;

namespace MusicFileManager
{
    public class ViewModel : ObservableObject
    {

        BackgroundWorker worker = new BackgroundWorker();

        private int _Maximum = 100;
        public int Maximum
        {
            get
            {
                return _Maximum;
            }
            set
            {
                if (this._Maximum != value)
                {
                    this._Maximum = value;
                    RaisePropertyChanged(nameof(Maximum));
                    RaisePropertyChanged(nameof(CountAll));
                }
            }
        }

        public double CountAll => _Maximum / 2;
        public double Count => _ProgressPercentage / 2;
        public string Percentage => (Count / CountAll).ToString("P4");

        private int _ProgressPercentage = 0;
        public int ProgressPercentage
        {
            get
            {
                return _ProgressPercentage;
            }
            set
            {
                if (this._ProgressPercentage != value)
                {
                    this._ProgressPercentage = value;
                    RaisePropertyChanged(nameof(ProgressPercentage));
                    RaisePropertyChanged(nameof(Count));
                }
            }
        }


        private string _Message;
        public string Message
        {
            get
            {
                return _Message;
            }
            set
            {
                if (this._Message != value)
                {
                    this._Message = value;
                    RaisePropertyChanged(nameof(Message));
                }
            }
        }

        private ObservableCollection<MediaTags> _Items = new ObservableCollection<MediaTags>();
        public ObservableCollection<MediaTags> Items
        {
            get
            {
                return _Items;
            }
            set
            {
                if (this._Items != value)
                {
                    this._Items = value;
                    RaisePropertyChanged(nameof(Items));
                }
            }
        }


        private bool _vFileName;
        public bool vFileName
        {
            get
            {
                return _vFileName;
            }
            set
            {
                if (this._vFileName != value)
                {
                    this._vFileName = value;
                    RaisePropertyChanged(nameof(vFileName));
                }
            }
        }



        public ICommand WindowsClosing => new RelayCommand(() =>
        {
            if (worker != null && worker.IsBusy)
            {
                worker.CancelAsync();
                worker.Dispose();
            }
        });

        public ICommand OpenCommand => new RelayCommand(() =>
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = true;
            ofd.Filter = "MP3|*.mp3|FLAC|*.flac|WAV|*.wav|所有支持|*.mp3;*.flac;*.wav";
            ofd.FilterIndex = 4;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Items.Clear();
                foreach (var file in ofd.FileNames)
                {
                    MediaTags song = new MediaTags(file);
                    song.FileName = Path.GetFileNameWithoutExtension(file);
                    song.OldName = Path.GetFileNameWithoutExtension(file);
                    song.Extension = Path.GetExtension(file);
                    song.DirectoryName = Path.GetDirectoryName(file);
                    Items.Add(song);
                }
                Maximum = Items.Count * 2;
                ProgressPercentage = 0;

                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.WorkerSupportsCancellation = true;
                worker.DoWork += LoadFiles;
                worker.ProgressChanged += ProgressChanged;
                worker.RunWorkerCompleted += RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
        });

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {

            }
            else
            {
                //MessageBox.Show("OK!");
            }

        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressPercentage = e.ProgressPercentage;
            Message = e.UserState.ToString();
        }

        private void LoadFiles(object sender, DoWorkEventArgs e)
        {
            int i = 0;
            foreach (var song in Items)
            {
                worker.ReportProgress(++i, $"({Percentage}) Reading <{song.FileName}.{song.Extension}>");
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                    return;
                }
                song.Init();
                worker.ReportProgress(++i, "");
            }
        }

        public ICommand SaveCommand => new RelayCommand(() =>
        {
            List<string> filenames = new List<string>();
            List<string> fileExists = new List<string>();
            foreach (var song in Items)
            {
                if (!filenames.Contains(song.Path + "\\" + song.FileName + song.Extension))
                {
                    filenames.Add(song.Path + "\\" + song.FileName + song.Extension);
                }
                else
                {
                    fileExists.Add(song.Path + "\\" + song.FileName + song.Extension);
                }
            }

            if (fileExists.Count > 0)
            {
                //MessageBox eMb = new ErrorMessageBox();
                //eMb.Title = "Same File Name??";
                //foreach (var item in fileExists)
                //{
                //    eMb.Msg.AppendText(item + "\n");
                //}
                //eMb.Show();
                //return;
            }

            foreach (var song in Items)
            {
                if (song.FlagModify)
                {
                    song.Commit(song.Path + "\\" + song.OldName + song.Extension);
                    if (song.FileName != song.OldName)
                    {
                        File.Move(song.Path + "\\" + song.OldName + song.Extension, song.Path + "\\" + song.FileName + song.Extension);
                        song.OldName = song.FileName;
                    }
                    song.FlagModify = false;
                }
            }
        });

    }
}
