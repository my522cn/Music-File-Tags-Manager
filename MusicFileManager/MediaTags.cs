using System;
using System.Linq;
using Microsoft.WindowsAPICodePack.Shell;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;
using System.ComponentModel;
using MicroMvvm;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace MusicFileManager
{
    [CategoryOrder("Album", 1)]
    [CategoryOrder("Information", 2)]
    [CategoryOrder("File", 3)]
    [CategoryOrder("Other", 4)]
    public class MediaTags : ObservableObject
    {
        private bool _FlagModify;
        [BrowsableAttribute(false)]
        public bool FlagModify
        {
            get
            {
                return _FlagModify;
            }
            set
            {
                if (this._FlagModify != value)
                {
                    this._FlagModify = value;
                    RaisePropertyChanged(nameof(FlagModify));
                }
            }
        }

        private string _Path;
        [BrowsableAttribute(false)]
        public string Path
        {
            get
            {
                return _Path;
            }
            set
            {
                if (this._Path != value)
                {
                    this._Path = value;
                    RaisePropertyChanged(nameof(Path));
                }
            }
        }

        private string _Extension;
        [Category("File")]
        [DisplayName("Extension")]
        [Description("Extension")]
        [PropertyOrder(2)]
        [ParenthesizePropertyNameAttribute(false)]
        public string Extension
        {
            get
            {
                return _Extension;
            }
            set
            {
                if (this._Extension != value)
                {
                    this._Extension = value;
                    RaisePropertyChanged(nameof(Extension));
                }
            }
        }

        private string _FileName;
        [Category("File")]
        [DisplayName("FileName")]
        [Description("File Name")]
        [PropertyOrder(1)]
        [ParenthesizePropertyNameAttribute(false)]
        public string FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                if (this._FileName != value)
                {
                    this._FileName = value;
                    RaisePropertyChanged(nameof(FileName));
                }
            }
        }

        private string _OldName;
        [BrowsableAttribute(false)]
        public string OldName
        {
            get
            {
                return _OldName;
            }
            set
            {
                if (this._OldName != value)
                {
                    this._OldName = value;
                    RaisePropertyChanged(nameof(OldName));
                }
            }
        }

        private string _DirectoryName;
        [Category("File")]
        [DisplayName("Directory")]
        [Description("Directory Name")]
        [PropertyOrder(3)]
        [ParenthesizePropertyNameAttribute(false)]
        public string DirectoryName
        {
            get
            {
                return _DirectoryName;
            }
            set
            {
                if (this._DirectoryName != value)
                {
                    this._DirectoryName = value;
                    RaisePropertyChanged(nameof(DirectoryName));
                }
            }
        }


        #region Mp3文件属性
        /// <summary>
        /// 标题
        /// </summary>
        [MediaProperty("Title")]
        [Category("Information")]
        [DisplayName("Title")]
        [Description("Title")]
        [PropertyOrder(3)]
        [ParenthesizePropertyNameAttribute(false)]
        public string Title
        {
            get
            {
                return _Title;
            }
            set
            {
                if (this._Title != value)
                {
                    this._Title = value;
                    RaisePropertyChanged(nameof(Title));
                }
            }
        }
        private string _Title;

        /// <summary>
        /// 子标题
        /// </summary>
        [MediaProperty("Media.SubTitle")]
        [Category("Information")]
        [DisplayName("SubTitle")]
        [Description("SubTitle")]
        [PropertyOrder(4)]
        [ParenthesizePropertyNameAttribute(false)]
        public string SubTitle
        {
            get
            {
                return _SubTitle;
            }
            set
            {
                if (this._SubTitle != value)
                {
                    this._SubTitle = value;
                    RaisePropertyChanged(nameof(SubTitle));
                }
            }
        }
        private string _SubTitle;

        /// <summary>
        /// 星级
        /// </summary>
        [MediaProperty("Rating")]
        [Category("Information")]
        [DisplayName("Rating")]
        [Description("Rating")]
        [PropertyOrder(9)]
        [ParenthesizePropertyNameAttribute(false)]
        public uint? Rating
        {
            get
            {
                return _Rating;
            }
            set
            {
                if (this._Rating != value)
                {
                    this._Rating = value;
                    RaisePropertyChanged(nameof(Rating));
                }
            }
        }
        private uint? _Rating;

        /// <summary>
        /// 备注
        /// </summary>
        [MediaProperty("Comment")]
        [Category("Information")]
        [DisplayName("Comment")]
        [Description("Comment")]
        [PropertyOrder(10)]
        [ParenthesizePropertyNameAttribute(false)]
        public string Comment
        {
            get
            {
                return _Comment;
            }
            set
            {
                if (this._Comment != value)
                {
                    this._Comment = value;
                    RaisePropertyChanged(nameof(Comment));
                }
            }
        }
        private string _Comment;

        /// <summary>
        /// 艺术家
        /// </summary>
        [MediaProperty("Author")]
        [Category("Information")]
        [DisplayName("Author")]
        [Description("Author")]
        [PropertyOrder(5)]
        [ParenthesizePropertyNameAttribute(false)]
        public string Author
        {
            get
            {
                return _Author;
            }
            set
            {
                if (this._Author != value)
                {
                    this._Author = value;
                    RaisePropertyChanged(nameof(Author));
                }
            }
        }
        private string _Author;

        /// <summary>
        /// 唱片集
        /// </summary>
        [MediaProperty("Music.AlbumTitle")]
        [Category("Album")]
        [DisplayName("Title")]
        [Description("Album Title")]
        [PropertyOrder(1)]
        [ParenthesizePropertyNameAttribute(false)]
        public string AlbumTitle
        {
            get
            {
                return _AlbumTitle;
            }
            set
            {
                if (this._AlbumTitle != value)
                {
                    this._AlbumTitle = value;
                    RaisePropertyChanged(nameof(AlbumTitle));
                }
            }
        }
        private string _AlbumTitle;

        /// <summary>
        /// 唱片集艺术家
        /// </summary>
        [MediaProperty("Music.AlbumArtist")]
        [Category("Album")]
        [DisplayName("Artist")]
        [Description("Album Artist")]
        [PropertyOrder(2)]
        [ParenthesizePropertyNameAttribute(false)]
        public string AlbumArtist
        {
            get
            {
                return _AlbumArtist;
            }
            set
            {
                if (this._AlbumArtist != value)
                {
                    this._AlbumArtist = value;
                    RaisePropertyChanged(nameof(AlbumArtist));
                }
            }
        }
        private string _AlbumArtist;

        /// <summary>
        /// 年
        /// </summary>
        [MediaProperty("Media.Year")]
        [Category("Information")]
        [DisplayName("Year")]
        [Description("Year")]
        [PropertyOrder(8)]
        [ParenthesizePropertyNameAttribute(false)]
        [DefaultValue("*")]
        public uint? Year
        {
            get
            {
                return _Year;
            }
            set
            {
                if (this._Year != value)
                {
                    this._Year = value;
                    RaisePropertyChanged(nameof(Year));
                }
            }
        }
        private uint? _Year;

        /// <summary>
        /// 流派
        /// </summary>
        [MediaProperty("Music.Genre")]
        [Category("Information")]
        [DisplayName("Genre")]
        [Description("Genre")]
        [PropertyOrder(7)]
        [ParenthesizePropertyNameAttribute(false)]
        public string Genre
        {
            get
            {
                return _Genre;
            }
            set
            {
                if (this._Genre != value)
                {
                    this._Genre = value;
                    RaisePropertyChanged(nameof(Genre));
                }
            }
        }
        private string _Genre;

        /// <summary>
        /// #
        /// </summary>
        [MediaProperty("Music.TrackNumber")]
        [Category("Information")]
        [DisplayName("Track")]
        [Description("Track Number")]
        [PropertyOrder(6)]
        [ParenthesizePropertyNameAttribute(false)]
        public uint? TrackNumber
        {
            get
            {
                return _TrackNumber;
            }
            set
            {
                if (this._TrackNumber != value)
                {
                    this._TrackNumber = value;
                    RaisePropertyChanged(nameof(TrackNumber));
                }
            }
        }
        private uint? _TrackNumber;

        /// <summary>
        /// 播放时间
        /// </summary>
        [MediaProperty("Media.Duration")]
        [Category("Other")]
        [DisplayName("Duration")]
        [Description("Duration")]
        [ParenthesizePropertyNameAttribute(false)]
        public string Duration
        {
            get
            {
                return _Duration;
            }
            private set
            {
                if (this._Duration != value)
                {
                    this._Duration = value;
                    RaisePropertyChanged(nameof(Duration));
                }
            }
        }
        private string _Duration;

        /// <summary>
        /// 比特率
        /// </summary>
        [MediaProperty("Audio.EncodingBitrate")]
        [Category("Other")]
        [DisplayName("BitRate")]
        [Description("BitRate")]
        [ParenthesizePropertyNameAttribute(false)]
        public string BitRate
        {
            get
            {
                return _BitRate;
            }
            private set
            {
                if (this._BitRate != value)
                {
                    this._BitRate = value;
                    RaisePropertyChanged(nameof(BitRate));
                }
            }
        }
        private string _BitRate;
        #endregion

        public MediaTags(string mediaPath)
        {
            this.Path = mediaPath;
        }
        //public MediaTags(string mediaPath)
        //{
        //    //var obj = ShellObject.FromParsingName(mp3Path);   //缩略图，只读
        //    //obj.Thumbnail.Bitmap.Save(@"R:\2.jpg));

        //    Init(mediaPath);
        //}

        public void Init()
        {
            using (var obj = ShellObject.FromParsingName(Path))
            {
                var mediaInfo = obj.Properties;
                foreach (var properItem in this.GetType().GetProperties())
                {
                    var mp3Att = properItem.GetCustomAttributes(typeof(MediaPropertyAttribute), false).FirstOrDefault();
                    if (mp3Att != null)
                    {
                        var shellProper = mediaInfo.GetProperty("System." + mp3Att);
                        var value = shellProper == null ? null : shellProper.ValueAsObject;

                        if (value == null)
                        {
                            continue;
                        }

                        if (shellProper.ValueType == typeof(string[]))    //艺术家，流派等多值属性
                        {
                            properItem.SetValue(this, string.Join(";", value as string[]), null);
                        }
                        else if (properItem.PropertyType != shellProper.ValueType)    //一些只读属性，类型不是string，但作为string输出，避免转换 如播放时间，比特率等
                        {
                            //properItem.SetValue(this, value == null ? "" : shellProper.FormatForDisplay(PropertyDescriptionFormat.Default), null);
                            properItem.SetValue(this, value == null ? "" : shellProper.FormatForDisplay(PropertyDescriptionFormatOptions.None), null);
                        }
                        else
                        {
                            properItem.SetValue(this, value, null);
                        }
                    }
                }
            }
        }

        public void Commit(string mp3Path)
        {
            var old = new MediaTags(mp3Path);

            using (var obj = ShellObject.FromParsingName(mp3Path))
            {
                var mediaInfo = obj.Properties;
                foreach (var proper in this.GetType().GetProperties())
                {
                    var oldValue = proper.GetValue(old, null);
                    var newValue = proper.GetValue(this, null);

                    if (oldValue == null && newValue == null)
                    {
                        continue;
                    }

                    //if (oldValue == null || !oldValue.Equals(newValue))
                    if ((newValue != null) && (newValue.ToString().Trim().Length > 0) && (newValue != oldValue))
                    {
                        var mp3Att = proper.GetCustomAttributes(typeof(MediaPropertyAttribute), false).FirstOrDefault();
                        if (mp3Att != null)
                        {
                            var shellProper = mediaInfo.GetProperty("System." + mp3Att);
                            Console.WriteLine(mp3Att);
                            SetPropertyValue(shellProper, newValue);
                        }
                    }
                }
            }
        }

        #region SetPropertyValue
        static void SetPropertyValue(IShellProperty prop, object value)
        {
            try
            {
                if (prop.ValueType == typeof(string[]))        //只读属性不会改变，故与实际类型不符的只有string[]这一种
                {
                    string[] values = (value as string).Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    (prop as ShellProperty<string[]>).Value = values;
                }
                if (prop.ValueType == typeof(string))
                {
                    (prop as ShellProperty<string>).Value = value as string;
                }
                else if (prop.ValueType == typeof(ushort?))
                {
                    (prop as ShellProperty<ushort?>).Value = value as ushort?;
                }
                else if (prop.ValueType == typeof(short?))
                {
                    (prop as ShellProperty<short?>).Value = value as short?;
                }
                else if (prop.ValueType == typeof(uint?))
                {
                    (prop as ShellProperty<uint?>).Value = value as uint?;
                }
                else if (prop.ValueType == typeof(int?))
                {
                    (prop as ShellProperty<int?>).Value = value as int?;
                }
                else if (prop.ValueType == typeof(ulong?))
                {
                    (prop as ShellProperty<ulong?>).Value = value as ulong?;
                }
                else if (prop.ValueType == typeof(long?))
                {
                    (prop as ShellProperty<long?>).Value = value as long?;
                }
                else if (prop.ValueType == typeof(DateTime?))
                {
                    (prop as ShellProperty<DateTime?>).Value = value as DateTime?;
                }
                else if (prop.ValueType == typeof(double?))
                {
                    (prop as ShellProperty<double?>).Value = value as double?;
                }
            }
            catch (Exception)
            {
                //
            }
        }
        #endregion

        #region MediaPropertyAttribute
        [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
        sealed class MediaPropertyAttribute : Attribute
        {
            public string PropertyKey { get; private set; }
            public MediaPropertyAttribute(string propertyKey)
            {
                this.PropertyKey = propertyKey;
            }

            public override string ToString()
            {
                return PropertyKey;
            }
        }
        #endregion
    }
}
