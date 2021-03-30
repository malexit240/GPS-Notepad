using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using GPSNotepad.Resources;
using GPSNotepad.Model.Interfaces;

namespace GPSNotepad.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public LocalizedResources TextResources { get; private set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            TextResources = new LocalizedResources(typeof(GPSNotepad.Resources.UITextResources), (App.Current.Container.Resolve<ISettingsManagerService>()).Language);
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }
    }
}
