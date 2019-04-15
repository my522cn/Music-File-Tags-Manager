using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Shell32;
using System.Collections.ObjectModel;

namespace MP3File
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<Song> items = new ObservableCollection<Song>();
        public MainWindow()
        {
            InitializeComponent();
            datagrid.ItemsSource = items;
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                items.Clear();
                foreach (var file in ofd.FileNames)
                {
                    FileInfo fInfo = new FileInfo(file);

                    ShellClass sh = new ShellClass();
                    Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(file));
                    FolderItem item = dir.ParseName(System.IO.Path.GetFileName(file));

                    //iCol 对应文件详细属性汇总
                    //ID  => DETAIL - NAME
                    //0   => Name
                    //1   => Size     // MP3 文件大小
                    //2   => Type
                    //3   => Date modified
                    //4   => Date created
                    //5   => Date accessed
                    //6   => Attributes
                    //7   => Offline status
                    //8   => Offline availability
                    //9   => Perceived type
                    //10  => Owner
                    //11  => Kinds
                    //12  => Date taken
                    //13  => Artists   // MP3 歌手
                    //14  => Album     // MP3 专辑
                    //15  => Year
                    //16  => Genre
                    //17  => Conductors
                    //18  => Tags
                    //19  => Rating
                    //20  => Authors
                    //21  => Title     // MP3 歌曲名
                    //22  => Subject
                    //23  => Categories
                    //24  => Comments
                    //25  => Copyright
                    //26  => #
                    //27  => Length    // MP3 时长
                    //28  => Bit rate
                    //29  => Protected
                    //30  => Camera model
                    //31  => Dimensions
                    //32  => Camera maker
                    //33  => Company
                    //34  => File description
                    //35  => Program name
                    //36  => Duration
                    //37  => Is online
                    //38  => Is recurring
                    //39  => Location
                    //40  => Optional attendee addresses
                    //41  => Optional attendees
                    //42  => Organizer address
                    //43  => Organizer name
                    //44  => Reminder time
                    //45  => Required attendee addresses
                    //46  => Required attendees
                    //47  => Resources
                    //48  => Free / busy status
                    //49  => Total size
                    //50  => Account name
                    //51  => Computer
                    //52  => Anniversary
                    //53  => Assistant's name
                    //54  => Assistant's phone
                    //55  => Birthday
                    //56  => Business address
                    //57  => Business city
                    //58  => Business country/ region
                    //59  => Business P.O.box
                    //60  => Business postal code
                    //61  => Business state or province
                    //62  => Business street
                    //63  => Business fax
                    //64  => Business home page
                    //65  => Business phone
                    //66  => Callback number
                    //67  => Car phone
                    //68  => Children
                    //69  => Company main phone
                    //70  => Department
                    //71  => E - mail Address
                    //72  => E - mail2
                    //73  => E - mail3
                    //74  => E - mail list
                    //75  => E - mail display name
                    //76  => File as
                    //77  => First name
                    //78  => Full name
                    //79  => Gender
                    //80  => Given name
                    //81  => Hobbies
                    //82  => Home address
                    //83  => Home city
                    //84  => Home country/ region
                    //85  => Home P.O.box
                    //86  => Home postal code
                    //int iCol = 1;
                    //string det = dir.GetDetailsOf(item, iCol);
                    string Title = dir.GetDetailsOf(item, 21);
                    string Artists = dir.GetDetailsOf(item, 13);
                    string Album = dir.GetDetailsOf(item, 14);
                    string Length = dir.GetDetailsOf(item, 27);
                    string BitRate = dir.GetDetailsOf(item, 28);
                    string Genre = dir.GetDetailsOf(item, 16);
                    string Year = dir.GetDetailsOf(item, 15);
                    string Charp = dir.GetDetailsOf(item, 26);

                    string File = System.IO.Path.GetFileNameWithoutExtension(file);
                    string Extension = System.IO.Path.GetExtension(file);

                    MediaTags t = new MediaTags(file);

                    items.Add(new Song()
                    {
                        Path = file,
                        Title = t.Title,
                        Artists = t.Author,
                        Album = t.AlbumTitle,
                        Length = Length,
                        BitRate = t.BitRate,
                        Genre = t.Genre,
                        Year = t.Year.ToString(),
                        Charp = t.TrackNumber.ToString(),
                        File = File,
                        Extension = Extension,
                    });
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            foreach (var song in items)
            {
                FileInfo fInfo = new FileInfo(song.Path);

                ShellClass sh = new ShellClass();
                Folder dir = sh.NameSpace(System.IO.Path.GetDirectoryName(song.Path));
                FolderItem item = dir.ParseName(System.IO.Path.GetFileName(song.Path));

                FileSummary.SetProperty(song.Path, "1111", SummaryPropId.Title);
            }
        }
    }
}
