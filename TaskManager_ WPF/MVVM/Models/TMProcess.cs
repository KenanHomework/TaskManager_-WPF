using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TaskManager__WPF.Services;

namespace TaskManager__WPF.MVVM.Models
{
    public class TMProcess:IComparable<TMProcess>
    {
        public TMProcess(int id, string name, string path, BitmapFrame icon, int handleCount, int threadCount)
        {
            Id = id;
            Name = name;
            Path = path;
            Icon = icon;
            HandleCount = handleCount;
            ThreadCount = threadCount;
        }

        public TMProcess(Process process)
        {
            Id = process.Id;
            Name = process.ProcessName;
            try
            {
                Path = process.MainModule.FileName;
                Icon = ProcessService.GetIconFromPath(Path);
            }
            catch (Exception) {

                Icon = BitmapFrame.Create(new Uri(@"https://res.cloudinary.com/kysbv/image/upload/v1666556490/General/process_icon.png"));
            }
            HandleCount = process.HandleCount;
            ThreadCount = process.Threads.Count;
        }

        #region Members

        public int Id { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public BitmapFrame Icon { get; set; }

        public int HandleCount { get; set; }

        public int ThreadCount { get; set; }


        #endregion

        #region Methods

        public int CompareTo(TMProcess? other) => Name.CompareTo(other.Name);

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


    }
}
