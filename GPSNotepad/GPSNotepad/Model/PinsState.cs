﻿using System;
using System.Collections.Generic;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;
using Prism.Mvvm;
using Xamarin.Forms;

namespace GPSNotepad.Model
{
    public class PinsState : BindableBase
    {
        static PinsState _instance;
        public static PinsState Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PinsState();
                return _instance;
            }
        }

        public PinsState()
        {

            _pins = new List<Pin>();
            Intiialize();

        }

        public async void Intiialize()
        {
            await App.Current.Container.Resolve<IPermanentPinService>().GetAllPinsForUser(CurrentUser.Instance.UserId).ContinueWith((result) =>
            {
                Pins = result.Result;
                MessagingCenter.Send(App.Current, "pin_state_updated");
            });
        }

        private List<Pin> _pins;

        public List<Pin> Pins
        {
            get => _pins;

            set => SetProperty(ref _pins, value);
        }

        public void Create(Pin pin)
        {
            Pins.Add(pin);
            App.Current.Container.Resolve<IPermanentPinService>().CreatePin(pin);
        }

        public void Update(Pin pin)
        {
            var index = Pins.IndexOf(pin);

            if (index == -1)
                return;

            Pins.RemoveAt(index);
            Pins.Insert(index, pin);
            App.Current.Container.Resolve<IPermanentPinService>().UpdatePin(pin);
        }

        public void Delete(Pin pin)
        {
            Pins.Remove(pin);
            App.Current.Container.Resolve<IPermanentPinService>().DeletePin(pin);
        }
    }
}
