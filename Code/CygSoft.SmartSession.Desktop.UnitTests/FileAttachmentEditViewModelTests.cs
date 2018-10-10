﻿using CygSoft.SmartSession.Desktop.Attachments;
using CygSoft.SmartSession.Desktop.Supports.Services;
using CygSoft.SmartSession.Domain.Attachments;
using Moq;
using NUnit.Framework;
using System;

namespace CygSoft.SmartSession.Desktop.UnitTests
{
    [TestFixture]
    public class FileAttachmentEditViewModelTests
    {
        // https://stackoverflow.com/questions/7121867/how-can-i-validate-multiple-properties-when-any-of-them-changes

        // Thanks Vincent.  I meant that you could avoid using BindingGroups altogether by implementing IDataErrorInfo and setting the ValidatesOnDataError property of Binding to true.  
        // https://blogs.msdn.microsoft.com/vinsibal/2008/08/12/wpf-3-5-sp1-feature-bindinggroups-with-item-level-validation/

        [Test]
        public void FileAttachmentCreateViewModel_Save_Correctly_Handles_SourceFilePath_After_Saving()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentCreateViewModel createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            createViewModel.StartEdit(null);


            var fileAttachmentCreateModel = (FileAttachmentCreateModel)createViewModel.FileAttachment;
            fileAttachmentCreateModel.SourceFilePath = @"C:\SomeOtherFolder\Files\new_file.gp";
            createViewModel.SaveCommand.Execute(null);

            var fileAttachment = fileAttachmentCreateModel.FileAttachment;
            Assert.That(fileAttachmentCreateModel.SourceFilePath, Is.Null);
            Assert.That(fileAttachment.SourceFilePath, Is.EqualTo(@"C:\SomeOtherFolder\Files\new_file.gp"));
        }

        [Test]
        public void FileAttachmentCreateViewModel_StartEdit_Correctly_Initializes_SourceFilePath()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentCreateViewModel createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            createViewModel.StartEdit(null);

            var fileAttachmentCreateModel = (FileAttachmentCreateModel)createViewModel.FileAttachment;
            var fileAttachment = fileAttachmentCreateModel.FileAttachment;

            Assert.That(fileAttachmentCreateModel.SourceFilePath, Is.Null);
            Assert.That(fileAttachment.SourceFilePath, Is.Null);
        }

        [Test]
        public void FileAttachmentCreateViewModel_StartEdit_Is_Correctly_Initialized_And_Presented()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentCreateViewModel createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            createViewModel.StartEdit(null);

            var fileAttachmentModel = createViewModel.FileAttachment;
            var fileAttachment = fileAttachmentModel.FileAttachment;

            Assert.That(fileAttachmentModel.Id, Is.EqualTo(0));
            Assert.That(fileAttachmentModel.IsDirty, Is.EqualTo(false));
            Assert.That(fileAttachmentModel.Notes, Is.EqualTo(null));
            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(null));
            Assert.That(fileAttachmentModel.Title, Is.EqualTo(null));
            Assert.That(fileAttachmentModel.FileAttachment, Is.Not.EqualTo(null));

            Assert.That(fileAttachment.Id, Is.EqualTo(0));
            Assert.That(fileAttachment.Notes, Is.EqualTo(null));
            Assert.That(fileAttachment.Extension, Is.EqualTo(null));
            Assert.That(fileAttachment.Title, Is.EqualTo(null));
        }

        [Test]
        public void FileAttachmentUpdateViewModel_StartEdit_Correctly_Sets_FileName()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentUpdateViewModel updateViewModel = GetFileAttachmentUpdateViewModel(2);
            updateViewModel.StartEdit(2);

            Assert.That(updateViewModel.FileAttachment.FileName, Is.EqualTo("27098039-5725-4564-92FD-2F222621D688.txt"));
        }

        [Test]
        public void FileAttachmentUpdateViewModel_When_Passed_Null_FileAttachment_Throws_Exception()
        {
            TestDelegate proc = () => new FileAttachmentUpdateModel(null);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>(), "FileAttachment is a required constructor parameter and cannot be null.");
        }


        [Test]
        public void FileAttachmentCreateViewModel_When_Passed_Null_FileAttachment_Throws_Exception()
        {
            TestDelegate proc = () => new FileAttachmentCreateModel(null);
            Assert.That(proc, Throws.TypeOf<ArgumentNullException>(), "FileAttachment is a required constructor parameter and cannot be null.");
        }

        [Test]
        public void FileAttachmentUpdateViewModel_StartEdit_Is_Correctly_Initialized_And_Presented()
        {
            FileAttachmentUpdateViewModel createViewModel = GetFileAttachmentUpdateViewModel(2);
            createViewModel.StartEdit(2);

            var fileAttachmentModel = createViewModel.FileAttachment;
            var fileAttachment = fileAttachmentModel.FileAttachment;

            Guid guid;

            Assert.That(fileAttachmentModel.Id, Is.EqualTo(2));
            Assert.That(fileAttachmentModel.IsDirty, Is.EqualTo(false));
            Assert.IsTrue(Guid.TryParse(fileAttachmentModel.FileId, out guid));
            Assert.That(fileAttachmentModel.Notes, Is.EqualTo("Here are some notes."));
            Assert.That(fileAttachmentModel.Extension, Is.EqualTo(".txt"));
            Assert.That(fileAttachmentModel.Title, Is.EqualTo("file_title"));
            Assert.That(fileAttachmentModel.FileAttachment, Is.Not.EqualTo(null));

            Assert.That(fileAttachment.Id, Is.EqualTo(2));
            Assert.That(fileAttachment.Notes, Is.EqualTo("Here are some notes."));
            Assert.That(fileAttachment.Extension, Is.EqualTo(".txt"));
            Assert.That(fileAttachment.Title, Is.EqualTo("file_title"));
        }

        [Test]
        public void FileAttachmentUpdateViewModel_Changes_Notes_IsDirty()
        {
            FileAttachmentUpdateViewModel updateViewModel = GetFileAttachmentUpdateViewModel(2);
            updateViewModel.StartEdit(2);

            var fileAttachmentModel = updateViewModel.FileAttachment;
            fileAttachmentModel.Notes = "Changed notes.";

            Assert.IsTrue(fileAttachmentModel.IsDirty);
        }

        [Test]
        public void FileAttachmentUpdateViewModel_Changes_Title_IsDirty()
        {
            FileAttachmentUpdateViewModel updateViewModel = GetFileAttachmentUpdateViewModel(2);

            updateViewModel.StartEdit(2);

            var fileAttachmentModel = updateViewModel.FileAttachment;
            fileAttachmentModel.Title = "file_title_changed";

            Assert.IsTrue(fileAttachmentModel.IsDirty);
        }


        [Test]
        public void FileAttachmentCreateViewModel_Changes_Notes_IsDirty()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentCreateViewModel createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            createViewModel.StartEdit(null);

            var fileAttachmentModel = createViewModel.FileAttachment;
            fileAttachmentModel.Notes = "Changed notes.";

            Assert.IsTrue(fileAttachmentModel.IsDirty);
        }

        [Test]
        public void FileAttachmentCreateViewModel_Changes_Title_IsDirty()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentCreateViewModel createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            createViewModel.StartEdit(null);

            var fileAttachmentModel = createViewModel.FileAttachment;
            fileAttachmentModel.Title = "file_title";

            Assert.IsTrue(fileAttachmentModel.IsDirty);
        }

        [Test]
        public void FileAttachmentCreateViewModel_Changes_FilePath_IsDirty()
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            FileAttachmentCreateViewModel createViewModel = new FileAttachmentCreateViewModel(fileAttachmentService.Object, dialogService.Object);
            createViewModel.StartEdit(null);

            var fileAttachmentModel = createViewModel.FileAttachment as FileAttachmentCreateModel;
            fileAttachmentModel.SourceFilePath = @"C:\SourcePath\file.txt";

            Assert.IsTrue(fileAttachmentModel.IsDirty);
        }

        private FileAttachmentUpdateViewModel GetFileAttachmentUpdateViewModel(int id)
        {
            Mock<IFileAttachmentService> fileAttachmentService = new Mock<IFileAttachmentService>();
            fileAttachmentService.Setup(s => s.Get(It.IsAny<int>()))
            .Returns(new FileAttachment
            {
                Id = id,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
                FileId = "27098039-5725-4564-92FD-2F222621D688",
                Extension = ".txt",
                Title = "file_title",
                Notes = "Here are some notes."
            });

            Mock<IDialogViewService> dialogService = new Mock<IDialogViewService>();

            return new FileAttachmentUpdateViewModel(fileAttachmentService.Object, dialogService.Object);
        }
    }
}
