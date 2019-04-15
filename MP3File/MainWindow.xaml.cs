using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Collections;
using System.Windows.Controls.Primitives;
using System.ComponentModel;

namespace MP3File
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        BackgroundWorker worker = new BackgroundWorker();
        List<string> AllShowFiles = new List<string>();
        //delegate MediaTags Del(MediaTags song, string file);

        //ObservableCollection<MediaTags> _items = new ObservableCollection<MediaTags>();

       

        public MainWindow()
        {
            InitializeComponent();
        }

        string preValue = "";
        private void dataGrid_BeginningEdit(object sender, DataGridBeginningEditEventArgs e)
        {
            //将修改前的值保存起来
            preValue = (e.Column.GetCellContent(e.Row) as TextBlock).Text;
        }

        private void dataGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            TextBox tb = e.EditingElement as TextBox;
            string newValue = tb.Text;
            //如果修改后的值和修改前的值不一样
            if (preValue != newValue)
            {
                MediaTags song = e.Row.Item as MediaTags;
                song.FlagModify = true;
                DataGridCell cell = (DataGridCell)e.EditingElement.Parent;
                //cell.Background = Brushes.LightPink;
                CellModify(cell);
            }
        }

        private void datagrid_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            TextBox tb = sender as TextBox;
            MessageBox.Show(tb.Text);
        }


        private void Close_Click(object sender, RoutedEventArgs e)
        {
            //for (int i = datagrid.SelectedItems.Count - 1; i >= 0; i--)
            //{
            //    MediaTags song = datagrid.SelectedItems[i] as MediaTags;
            //    Items.Remove(song);
            //    AllShowFiles.Remove(song.Path + "\\" + song.FileName + song.Extension);
            //}
        }

        private void Select_Click(object sender, RoutedEventArgs e)
        {
            datagrid.Focus();
            datagrid.SelectAll();
        }

        private void datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FileNameBox.Items.Clear();
            ExtensionBox.Items.Clear();
            TitleBox.Items.Clear();
            AuthorBox.Items.Clear();
            AlbumTitleBox.Items.Clear();
            AlbumArtistBox.Items.Clear();
            YearBox.Items.Clear();
            GenreBox.Items.Clear();
            TrackNumberBox.Items.Clear();
            DurationBox.Items.Clear();
            BitRateBox.Items.Clear();
            SubTitleBox.Items.Clear();
            RatingBox.Items.Clear();
            CommentBox.Items.Clear();

            MediaTags song = datagrid.SelectedItems[0] as MediaTags;

            FileNameBox.Text = song.FileName;
            AddItems(FileNameBox.Items, song.FileName);

            ExtensionBox.Text = song.Extension;
            AddItems(ExtensionBox.Items, song.Extension);

            TitleBox.Text = song.Title;
            AddItems(TitleBox.Items, song.Title);

            AuthorBox.Text = song.Author;
            AddItems(AuthorBox.Items, song.Author);

            AlbumTitleBox.Text = song.AlbumTitle;
            AddItems(AlbumTitleBox.Items, song.AlbumTitle);

            AlbumArtistBox.Text = song.AlbumArtist;
            AddItems(AlbumArtistBox.Items, song.AlbumArtist);

            YearBox.Text = song.Year.ToString();
            AddItems(YearBox.Items, song.Year);

            GenreBox.Text = song.Genre;
            AddItems(GenreBox.Items, song.Genre);

            TrackNumberBox.Text = song.TrackNumber.ToString();
            AddItems(TrackNumberBox.Items, song.TrackNumber);

            DurationBox.Text = song.Duration;
            AddItems(DurationBox.Items, song.Duration);

            BitRateBox.Text = song.BitRate;
            AddItems(BitRateBox.Items, song.BitRate);

            SubTitleBox.Text = song.SubTitle;
            AddItems(SubTitleBox.Items, song.SubTitle);

            RatingBox.Text = song.Rating.ToString();
            AddItems(RatingBox.Items, song.Rating);

            CommentBox.Text = song.Comment;
            AddItems(CommentBox.Items, song.Comment);

            for (int i = 1; i < datagrid.SelectedItems.Count; i++)
            {
                MediaTags song2 = datagrid.SelectedItems[i] as MediaTags;
                if (song.FileName != song2.FileName)
                {
                    FileNameBox.Text = "*";
                    AddItems(FileNameBox.Items, song2.FileName);
                }
                if (song.Extension != song2.Extension)
                {
                    ExtensionBox.Text = "*";
                    AddItems(ExtensionBox.Items, song2.Extension);
                }
                if (song.Title != song2.Title)
                {
                    TitleBox.Text = "*";
                    AddItems(TitleBox.Items, song2.Title);
                }
                if (song.Author != song2.Author)
                {
                    AuthorBox.Text = "*";
                    AddItems(AuthorBox.Items, song2.Author);
                }
                if (song.AlbumTitle != song2.AlbumTitle)
                {
                    AlbumTitleBox.Text = "*";
                    AddItems(AlbumTitleBox.Items, song2.AlbumTitle);
                }
                if (song.AlbumArtist != song2.AlbumArtist)
                {
                    AlbumArtistBox.Text = "*";
                    AddItems(AlbumArtistBox.Items, song2.AlbumArtist);
                }
                if (song.Year != song2.Year)
                {
                    YearBox.Text = "*";
                    AddItems(YearBox.Items, song2.Year);
                }
                if (song.Genre != song2.Genre)
                {
                    GenreBox.Text = "*";
                    AddItems(GenreBox.Items, song2.Genre);
                }
                if (song.TrackNumber != song2.TrackNumber)
                {
                    TrackNumberBox.Text = "*";
                    AddItems(TrackNumberBox.Items, song2.TrackNumber);
                }
                if (song.Duration != song2.Duration)
                {
                    DurationBox.Text = "*";
                    AddItems(DurationBox.Items, song2.Duration);
                }
                if (song.BitRate != song2.BitRate)
                {
                    BitRateBox.Text = "*";
                    AddItems(BitRateBox.Items, song2.BitRate);
                }
                if (song.SubTitle != song2.SubTitle)
                {
                    SubTitleBox.Text = "*";
                    AddItems(SubTitleBox.Items, song2.SubTitle);
                }
                if (song.Rating != song2.Rating)
                {
                    RatingBox.Text = "*";
                    AddItems(RatingBox.Items, song2.Rating);
                }
                if (song.Comment != song2.Comment)
                {
                    CommentBox.Text = "*";
                    AddItems(CommentBox.Items, song2.Comment);
                }
            }
        }

        private void AddItems(ItemCollection items, object text)
        {
            if (!items.Contains(text))
            {
                items.Add(text);
            }
        }

        private void BulkEdit_Click(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < datagrid.SelectedItems.Count; i++)
            {
                MediaTags song = datagrid.SelectedItems[i] as MediaTags;
                if (FileNameBox.Text != "*")
                {
                    song.FileName = FileNameBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 0);
                    CellModify(cell);
                }
                if (TitleBox.Text != "*")
                {
                    song.Title = TitleBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 2);
                    CellModify(cell);
                }
                if (AuthorBox.Text != "*")
                {
                    song.Author = AuthorBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 3);
                    CellModify(cell);
                }
                if (AlbumTitleBox.Text != "*")
                {
                    song.AlbumTitle = AlbumTitleBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 4);
                    CellModify(cell);
                }
                if (AlbumArtistBox.Text != "*")
                {
                    song.AlbumArtist = AlbumArtistBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 5);
                    CellModify(cell);
                }
                if (YearBox.Text != "*")
                {
                    song.Year = uint.Parse(YearBox.Text);
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 6);
                    CellModify(cell);
                }
                if (GenreBox.Text != "*")
                {
                    song.Genre = GenreBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 7);
                    CellModify(cell);
                }
                if (TrackNumberBox.Text != "*")
                {
                    song.TrackNumber = uint.Parse(TrackNumberBox.Text);
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 8);
                    CellModify(cell);
                }
                if (SubTitleBox.Text != "*")
                {
                    song.SubTitle = SubTitleBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 11);
                    CellModify(cell);
                }
                if (RatingBox.Text != "*" && RatingBox.Text != "")
                {
                    song.Rating = uint.Parse(RatingBox.Text);
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 12);
                    CellModify(cell);
                }
                if (CommentBox.Text != "*")
                {
                    song.Comment = CommentBox.Text;
                    song.FlagModify = true;
                    DataGridCell cell = GetCell(i, 13);
                    CellModify(cell);
                }
            }
        }
        private DataGridCell GetCell(int rowIndex, int columnIndex)
        {
            DataGridRow rowContainer = GetRow(rowIndex);
            if (rowContainer != null)
            {
                DataGridCellsPresenter presenter = GetVisualChild<DataGridCellsPresenter>(rowContainer);
                DataGridCell cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                if (cell == null)
                {
                    datagrid.ScrollIntoView(rowContainer, datagrid.Columns[columnIndex]);
                    cell = (DataGridCell)presenter.ItemContainerGenerator.ContainerFromIndex(columnIndex);
                }
                return cell;
            }
            return null;
        }
        private T GetVisualChild<T>(Visual parent) where T : Visual
        {
            T child = default(T);
            int numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < numVisuals; i++)
            {
                Visual v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T;
                if (child == null)
                {
                    child = GetVisualChild<T>(v);
                }
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        private DataGridRow GetRow(int rowIndex)
        {
            DataGridRow rowContainer = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            if (rowContainer == null)
            {
                datagrid.UpdateLayout();
                datagrid.ScrollIntoView(datagrid.Items[rowIndex]);
                rowContainer = (DataGridRow)datagrid.ItemContainerGenerator.ContainerFromIndex(rowIndex);
            }
            return rowContainer;
        }

        private void CellModify(DataGridCell cell)
        {
            //if (cell.Background != Brushes.LightPink)
            //{
            cell.Background = Brushes.LightPink;
            //}
            //else
            //{
            //    cell.Background = Brushes.Red;
            //}
        }
    }
}