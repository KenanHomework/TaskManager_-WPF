using SimpleInjector;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TaskManager__WPF.MVVM.Models;
using TaskManager__WPF.MVVM.ViewModels;
using TaskManager__WPF.Services;

namespace TaskManager__WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        #region Members

        public static List<string> BlackList { get; set; } = new();

        public static Container Container = new();

        public static string SuccesSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661935108/WolfTaxi/success-sound-effect.mp3";
        public static string ErrorSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661936264/WolfTaxi/error-sound.mp3";
        public static string NotificationSoundEffect = "https://res.cloudinary.com/kysbv/video/upload/v1661940169/WolfTaxi/notification-sound.mp3";

        #endregion

        #region Methods

        void Register()
        {
            Container.RegisterSingleton<ProcessesVM>();
            Container.RegisterSingleton<ConfigBlacklistVM>();

            Container.Verify();
        }

        public static void AddToBlackList(string path)
        {
            if (BlackList.Contains(path))
                return;
            BlackList.Add(path);
        }

        #endregion

        public App()
        {
            Register();
            try
            {
                BlackList = JSONService.Read<List<string>>("BlackList.json");
            }
            catch (Exception) { }
        }
    }
}
