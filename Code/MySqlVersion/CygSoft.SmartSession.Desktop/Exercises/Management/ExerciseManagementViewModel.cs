﻿using AutoMapper;
using CygSoft.SmartSession.Desktop.Supports.Messages;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Exercises;
using CygSoft.SmartSession.Infrastructure.Enums;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.ObjectModel;

namespace CygSoft.SmartSession.Desktop.Exercises.Management
{
    public class ExerciseManagementViewModel : ViewModelBase
    {
        private IExerciseService exerciseService;
        private IDialogViewService dialogService;

        public ExerciseManagementViewModel(IExerciseService exerciseService, 
            IDialogViewService dialogService)
        {
            this.exerciseService = exerciseService ?? throw new ArgumentNullException("Service must be provided.");
            this.dialogService = dialogService ?? throw new ArgumentNullException("Dialog service must be provided.");

            AddExerciseCommand = new RelayCommand(AddExercise, () => true);
            DeleteExerciseCommand = new RelayCommand(DeleteExercise, () => SelectedExercise != null);
            EditExerciseCommand = new RelayCommand(EditExercise, () => SelectedExercise != null);
            SelectExerciseCommand = new RelayCommand(SelectExercise, () => SelectedExercise != null);
            FindCommand = new RelayCommand(Find, true);
            PracticeExerciseCommand = new RelayCommand(() => PracticeExercise(), () => true);
        }

        protected virtual void SelectExercise() { }

        public RelayCommand AddExerciseCommand { get; private set; }
        public RelayCommand DeleteExerciseCommand { get; private set; }
        public RelayCommand EditExerciseCommand { get; private set; }

        public RelayCommand SelectExerciseCommand { get; private set; }

        public RelayCommand PracticeExerciseCommand { get; private set; }

        public RelayCommand FindCommand { get; private set; }

        private ExerciseListItemModel selectedExercise;
        public ExerciseListItemModel SelectedExercise
        {
            get { return selectedExercise; }
            set
            {
                Set(() => SelectedExercise, ref selectedExercise, value);
                AddExerciseCommand.RaiseCanExecuteChanged();
                EditExerciseCommand.RaiseCanExecuteChanged();
                DeleteExerciseCommand.RaiseCanExecuteChanged();
            }
        }

        private string findText;
        public string FindText
        {
            get
            {
                return findText;
            }
            set
            {
                Set(() => FindText, ref findText, value);
            }
        }

        private bool isItemsControlOpen;
        public bool IsItemsControlOpen
        {
            get
            {
                return isItemsControlOpen;
            }
            set
            {
                Set(() => IsItemsControlOpen, ref isItemsControlOpen, value);
            }
        }

        public ObservableCollection<ExerciseListItemModel> ExerciseList { get; private set; } = new ObservableCollection<ExerciseListItemModel>();

        private void PracticeExercise()
        {
            Messenger.Default.Send(new StartPracticingExerciseMessage(SelectedExercise.Id));
        }

        private void Find()
        {
            ExerciseList.Clear();

            var exerciseSearchCriteria = new ExerciseSearchCriteria();
            exerciseSearchCriteria.Title = FindText;

            foreach (var exercise in exerciseService.Find(exerciseSearchCriteria))
            {
                ExerciseList.Add(Mapper.Map<ExerciseListItemModel>(exercise));
            }
        }

        private void EditExercise()
        {
            var exercise = this.exerciseService.Get(SelectedExercise.Id);
            Messenger.Default.Send(new StartEditingExerciseMessage(exercise));
        }

        private void DeleteExercise()
        {
            try
            {
                if (dialogService.YesNoPrompt("Delete Exercise","Sure you'd like to delete this exercise?"))
                {
                    exerciseService.Remove(SelectedExercise.Id);
                    ExerciseList.Remove(SelectedExercise);
                }
            }
            catch
            {
                dialogService.ExclamationMessage("Deletion Error", 
                    "Cannot delete an exercise that has already been added to routine or/and has been practiced. Archive instead.");
            }
        }

        private void AddExercise()
        {
            Messenger.Default.Send(new StartEditingExerciseMessage(exerciseService.Create()));
        }
    }
}
