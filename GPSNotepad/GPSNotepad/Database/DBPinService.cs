using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GPSNotepad.Model;
using GPSNotepad.Model.Entities;
using GPSNotepad.Model.Interfaces;
using System.Linq;

namespace GPSNotepad.Database
{
    public class DBPinService : IPermanentPinService
    {
        #region ---IPermanentPinService Implementation---
        public async Task<bool> CreatePin(Pin pin)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var context = new Context())
                {
                    context.Pins.Add(pin);
                    context.SaveChangesAsync();
                }
                return true;
            });
        }

        public async Task<List<Pin>> GetAllPinsForUser(Guid user_id)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var context = new Context())
                {
                    var pins =

                    (from pin in context.Pins
                     where pin.UserId == user_id
                     select pin).ToList();
                    return pins;
                }
            });
        }

        public async void UpdatePin(Pin pin)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var context = new Context())
                {
                    context.Pins.Update(pin);
                    context.SaveChangesAsync();
                }
            });
        }

        public async void DeletePin(Pin pin)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var context = new Context())
                {
                    context.Pins.Remove(pin);
                    context.SaveChangesAsync();
                }
            });
        } 
        #endregion

    }
}
