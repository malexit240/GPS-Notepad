using System;
using System.Collections.Generic;
using System.Linq;
using GPSNotepad.Entities;
using GPSNotepad.Repositories;
using Xamarin.Essentials;
using Microsoft.EntityFrameworkCore;

namespace GPSNotepad
{
    public static class NotificationLoader
    {
        public static List<Pin> GetAllPins()
        {
            var token = Preferences.Get("SessionToken", Guid.Empty.ToString());

            User user;
            List<Pin> pins = new List<Pin>();
            List<PlaceEvent> events = new List<PlaceEvent>();

            using var context = new Context();

            user = context.Users.Where(u => u.SessionToken == token).Select(u => u).FirstOrDefault();

            if (user != null)
            {
                pins = context.Pins.Where(p => p.UserId == user.UserId).Select(p => p).ToList();

                foreach (var pin in pins)
                {
                    pin.Events = context.Events.Where(e => e.PinId == pin.PinId).Select(e => e).ToList();
                }
            }

            return pins;
        }
    }
}
