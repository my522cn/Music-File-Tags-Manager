using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MicroMvvm;

namespace MP3File
{
    public class ViewModel : ObservableObject
    {

        BackgroundWorker worker = new BackgroundWorker();
        List<string> AllShowFiles = new List<string>();


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
                }
            }
        }


        private int _Value = 0;
        public int Value
        {
            get
            {
                return _Value;
            }
            set
            {
                if (this._Value != value)
                {
                    this._Value = value;
                    RaisePropertyChanged(nameof(Value));
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



        public ICommand OpenCommand => new RelayCommand(() =>
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Items.Clear();
                AllShowFiles.Clear();
                foreach (var file in ofd.FileNames)
                {
                    if (!AllShowFiles.Contains(file))
                    {
                        AllShowFiles.Add(file);
                    }
                }

                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                Maximum = AllShowFiles.Count;
                Value = 0;
                worker.DoWork += LoadFiles;
                //worker.ProgressChanged += ProgressChanged;
                //worker.RunWorkerCompleted += RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
        });

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }

        private void LoadFiles(object sender, DoWorkEventArgs e)
        {
            foreach (var file in AllShowFiles)
            {
                worker.ReportProgress(Value++);
                MediaTags song = new MediaTags(file);
                song.FileName = Path.GetFileNameWithoutExtension(file);
                song.OldName = Path.GetFileNameWithoutExtension(file);
                song.Extension = Path.GetExtension(file);
                song.Path = Path.GetDirectoryName(file);
                Items.Add(song);
            }
        }

        public ICommand AppendCommand => new RelayCommand(() =>
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                foreach (var file in ofd.FileNames)
                {
                    if (!AllShowFiles.Contains(file))
                    {
                        AllShowFiles.Add(file);
                    }
                }

                worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                Maximum = AllShowFiles.Count;
                Value = 0;
                worker.DoWork += LoadFiles;
                //worker.ProgressChanged += ProgressChanged;
                //worker.RunWorkerCompleted += RunWorkerCompleted;
                worker.RunWorkerAsync();
            }
        });

        public ICommand SaveCommand => new RelayCommand(() =>
        {
            //datagrid.CellStyle = (Style)this.FindResource("DataGridCellDefault");
            //datagrid.CellStyle = Resources["DataGridCellDefault"] as Style;
            return;

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
                ErrorMessageBox eMb = new ErrorMessageBox();
                eMb.Title = "Same File Name??";
                foreach (var item in fileExists)
                {
                    eMb.Msg.AppendText(item + "\n");
                }
                eMb.Show();
                return;
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
