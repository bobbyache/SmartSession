﻿using Caliburn.Micro;
using SmartClient.Domain;
using SmartClient.Domain.Data;
using SmartGoals.Services;
using System;
using System.Collections.Generic;
using System.Windows;

namespace SmartGoals
{
    /* Caliburn Videos
     * -------------------------------------------------------------------------------------------------
     * Current Status: https://youtu.be/o3--INqJRVE?list=PL3JeBX8MKjuHhSFbPOwbrxvdiRC1Lsrkb&t=553
     * -------------------------------------------------------------------------------------------------
     * AutoMapper: https://docs.automapper.org/en/stable/Getting-started.html
     * Live Charts: https://lvcharts.net/
     * WPF in C# with MVVM using Caliburn Micro: https://www.youtube.com/watch?v=laPFq3Fhs8k
     * Documentation: https://caliburnmicro.com/documentation/
     * Caliburn Micro Introduction Series: https://www.youtube.com/watch?v=vVFXQ1fvFTc&list=PL3JeBX8MKjuHhSFbPOwbrxvdiRC1Lsrkb
     * 
     * Validation Strategy?
     * -------------------------------------------------------------------------------------------------
     * https://gist.github.com/canton7/6727693
     * ... however, you've already got a good working implementation going with your MySql solution... so do a spike with that first
     * Also check this:
     * https://github.com/AIexandr/Caliburn.Micro.Validation
     * and this:
     * https://www.codeproject.com/Articles/97564/Attributes-based-Validation-in-a-WPF-MVVM-Applicat
     * */
    public class Bootstrapper : BootstrapperBase
    {
        private SimpleContainer simpleContainer = new SimpleContainer();

        public Bootstrapper()
        {
            Initialize();
        }

        protected override object GetInstance(Type service, string key)
        {
            return simpleContainer.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return simpleContainer.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            simpleContainer.BuildUp(instance);
        }

        protected override void Configure()
        {
            simpleContainer.Singleton<IGoalRepository, GoalRepository>();
            simpleContainer.Singleton<GoalManager, GoalManager>();

            simpleContainer.Singleton<IWindowManager, WindowManager>();
            simpleContainer.Singleton<IEventAggregator, EventAggregator>();
            simpleContainer.Singleton<IDialogService, DialogService>();
            simpleContainer.Singleton<ISettingsService, SettingsService>();

            simpleContainer.Singleton<IntroViewModel>();
            simpleContainer.Singleton<CreateGoalViewModel>();
            simpleContainer.Singleton<MainMenuViewModel>();
            simpleContainer.Singleton<ExampleViewModel>();
            simpleContainer.Singleton<GoalDashboardViewModel>();
            simpleContainer.Singleton<BottomMenuViewModel>();
            simpleContainer.Singleton<TaskDashboardViewModel>();
            simpleContainer.Singleton<ShellViewModel>();
            simpleContainer.Singleton<HeaderViewModel>();
            simpleContainer.Singleton<ContentViewModel>();
        }

        protected override void OnExit(object sender, EventArgs e)
        {
            base.OnExit(sender, e);
        }

        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            DisplayRootViewFor<ShellViewModel>();
        }
    }
}
