using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager__WPF.MVVM.Models
{
    public class TMTotalCount
    {
        public TMTotalCount(int totalThread, int totalProcess, int totalHandle)
        {
            TotalThread = totalThread;
            TotalProcess = totalProcess;
            TotalHandle = totalHandle;
        }

        public TMTotalCount()
        {

        }

        public int TotalThread { get; set; } = 0;

        public int TotalProcess { get; set; } = 0;

        public int TotalHandle { get; set; } = 0;


    }
}
