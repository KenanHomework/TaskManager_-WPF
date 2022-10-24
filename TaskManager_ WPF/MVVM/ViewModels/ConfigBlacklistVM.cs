using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager__WPF.Commands;
using TaskManager__WPF.MVVM.Models;
using TaskManager__WPF.MVVM.Views;
using TaskManager__WPF.Services;

namespace TaskManager__WPF.MVVM.ViewModels
{
    public class ConfigBlacklistVM
    {

        public ConfigBlacklist Window { get; set; }

        #region Commands

        public RelayCommand AddBlackListCommand { get; set; }

        public RelayCommand DeleteBlackListCommand { get; set; }

        #endregion

        #region Methods

        public void AddBlackListRun(object param)
        {
            AskProcessPath ask = new();
            ask.ShowDialog();

            if (!ask.Result)
                return;

            App.AddToBlackList(ask.ProcessPath.Text);
        }

        public void DeleteBlackListRun(object param)
        {
            if (Window.BlacklistListView.SelectedItems.Count <= 0)
                return;

            foreach (string item in Window.BlacklistListView.SelectedItems)
            {
                try { App.BlackList.Remove(item); }
                catch (Exception) { }
            }

            Window.BlacklistListView.ItemsSource = null;
            Window.BlacklistListView.ItemsSource = App.BlackList;
        }

        public bool DeleteBlackListCanRun(object param) => Window.BlacklistListView.SelectedItems.Count > 0;

        #endregion

        public ConfigBlacklistVM()
        {
            AddBlackListCommand = new(AddBlackListRun);
            DeleteBlackListCommand = new(DeleteBlackListRun, DeleteBlackListCanRun);
        }
    }
}
