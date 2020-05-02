﻿using Caliburn.Micro;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace SmartGoals
{
    public class ShellViewModel : Conductor<Screen>.Collection.OneActive, IHandle<NavigateToMessage>
    {
        private readonly IEventAggregator eventAggregator;
        private readonly MainMenuViewModel mainMenuViewModel;
        private readonly ExampleViewModel exampleViewModel;
        private readonly GoalDashboardViewModel goalDashboardViewModel;

        public BottomMenuViewModel BottomMenuViewModel { get; }

        public ShellViewModel(IEventAggregator eventAggregator, 
            MainMenuViewModel mainMenuViewModel, 
            ExampleViewModel exampleViewModel, 
            BottomMenuViewModel bottomMenuViewModel,
            GoalDashboardViewModel goalDashboardViewModel
            )
        {
            this.eventAggregator = eventAggregator;
            this.mainMenuViewModel = mainMenuViewModel;
            this.exampleViewModel = exampleViewModel;
            BottomMenuViewModel = bottomMenuViewModel;
            this.goalDashboardViewModel = goalDashboardViewModel;
        }

        protected override Task OnActivateAsync(CancellationToken cancellationToken)
        {
            eventAggregator.SubscribeOnUIThread(this);
            // Will set this.ActiveItem for View
            return ActivateItemAsync(this.mainMenuViewModel, cancellationToken);
            // return base.OnActivateAsync(cancellationToken);
        }

        public override Task DeactivateItemAsync(Screen item, bool close, CancellationToken cancellationToken = default)
        {
            eventAggregator.Unsubscribe(this);
            return base.DeactivateItemAsync(item, close, cancellationToken);
        }

        public Task HandleAsync(NavigateToMessage message, CancellationToken cancellationToken)
        {
            if (message.NavigateTo == NavigateTo.Examples)
            {
                this.ActiveItem = exampleViewModel;
            }
            else if (message.NavigateTo == NavigateTo.GoalDashboard)
            {
                this.ActiveItem = goalDashboardViewModel;
            }

            return Task.CompletedTask;
        }
    }
}