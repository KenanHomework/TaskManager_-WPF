using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using TaskManager__WPF.MVVM.Models;

namespace TaskManager__WPF.Services
{
    public abstract class ProcessService
    {

        public static BitmapFrame GetIconFromPath(string path)
        {
            if (string.IsNullOrEmpty(path))
                throw new ArgumentNullException("path");

            return BitmapService.ToBitMapFrame(Icon.ExtractAssociatedIcon(path).ToBitmap());
        }

        public static TMTotalCount GetTMTotalCount(List<Process> list)
        {
            TMTotalCount tMTotal = new();
            list.ForEach(p =>
            {
                tMTotal.TotalProcess++;
                tMTotal.TotalHandle += p.HandleCount;
                tMTotal.TotalThread += p.Threads.Count;
            });
            return tMTotal;
        }

        public static List<TMProcess> GetAllTMProcesses() => ToListTMProcess(Process.GetProcesses().ToList());

        public static List<TMProcess> ToListTMProcess(List<Process> processes)
        {
            List<TMProcess> list = new();

            processes.ForEach(p => { list.Add(new(p)); });

            list.Sort();

            return list;
        }

    }
}
