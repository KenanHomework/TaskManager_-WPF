using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManager__WPF.Commands;
using TaskManager__WPF.Enums;
using TaskManager__WPF.MVVM.Models;
using TaskManager__WPF.MVVM.Views;
using TaskManager__WPF.Services;

namespace TaskManager__WPF.MVVM.ViewModels
{
    public class ProcessesVM
    {
        private ObservableCollection<TMProcess> processes;
        #region Members

        public Processes Window { get; set; }

        public ObservableCollection<TMProcess> Processes { get => processes; set { processes = value; OnPropertyChanged(); } }

        public TMTotalCount TotalCount { get; set; }

        #endregion

        #region Commands

        public RelayCommand EndTaskCommand { get; set; }

        public RelayCommand RunNewTaskCommand { get; set; }

        public RelayCommand AddBlackListCommand { get; set; }

        public RelayCommand ConfigBlackListCommand { get; set; }

        #endregion

        #region PropertyChangedEventHandler

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        #endregion


        #region Methods

        public void CheckProcesses()
        {
            int count = 0;
            Processes.ToList().ForEach(p =>
            {
                try
                {
                    if (!App.BlackList.Contains(p.Path))
                        return;

                    Process process = Process.GetProcessById(p.Id);
                    try
                    {
                        process.Kill(); count++;

                    }
                    catch { }

                }
                catch (Exception) { }
            });

            if (count == 0)
                return;

            CMessageBox.Show($"Killed {count} blacklisted Process(s)", CMessageTitle.Info, CMessageButton.Ok, CMessageButton.None);
        }

        public void RefreshProcesses()
        {
            List<Process> processes = Process.GetProcesses().ToList();
            Processes = new(ProcessService.ToListTMProcess(processes));
            TotalCount = ProcessService.GetTMTotalCount(processes);

            CheckProcesses();

            Window.TotalHandleCount.Text = TotalCount.TotalHandle.ToString();
            Window.TotalProcessesCount.Text = TotalCount.TotalProcess.ToString();
            Window.TotalThreadCount.Text = TotalCount.TotalThread.ToString();
        }

        public void ConfigBlackListRun(object param)
        {
            Window.dispatcherTimer.Stop();
            ConfigBlacklist blacklist = new();
            blacklist.ShowDialog();
            Window.dispatcherTimer.Start();
        }

        public void AddBlackListRun(object param)
        {
            AskProcessPath ask = new();
            if (Window.ProcessesListView.SelectedItem != null)
                ask.ProcessPath.Text = ((TMProcess)Window.ProcessesListView.SelectedItem).Path;
            ask.ShowDialog();

            if (!ask.Result)
                return;

            App.AddToBlackList(ask.ProcessPath.Text);

            JSONService.Write("BlackList.json", App.BlackList);
            RefreshProcesses();
        }

        public void RunNewTaskRun(object param)
        {
            AskProcessPath ask = new();
            ask.ShowDialog();
            if (!ask.Result)
                return;

            try
            {
                Process.Start(ask.ProcessPath.Text);
            }
            catch (Exception) { }
        }

        public void EndTaskRun(object param)
        {

            if (Window.ProcessesListView.SelectedIndex < 0)
                return;

            try
            {
                Process pr = Process.GetProcessById(((TMProcess)Window.ProcessesListView.SelectedItem).Id);
                pr.Kill();
                Window.ProcessesListView.SelectedIndex = -1;
            }
            catch (Exception) { }
        }

        public bool EndTaskCanRun(object param) => Window.ProcessesListView.SelectedIndex >= 0;

        #endregion

        public ProcessesVM()
        {
            EndTaskCommand = new(EndTaskRun, EndTaskCanRun);
            RunNewTaskCommand = new(RunNewTaskRun);
            AddBlackListCommand = new(AddBlackListRun);
            ConfigBlackListCommand = new(ConfigBlackListRun);
        }

    }
}
