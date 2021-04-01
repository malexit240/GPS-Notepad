using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace GPSNotepad.Model
{
    public struct ValidCoordinates
    {
        public double X;
        public double Y;
        public bool isValid;

        public bool TryParseRequest(string request)
        {
            var coordinates = Regex.Matches(request, @"([\-\+]?([\.]?(\d))+([Nn]|[Ss]|[Ww]|[Ee]))");
            if (coordinates.Count != 2 ||
                Regex.Matches(request, @"({\d{1,3}){1}([Nn]|[Ss]))").Count > 1 ||
                Regex.Matches(request, @"({\d{1,3}){1}([Ww]|[Ee]))").Count > 1)
                return false;

            if (double.TryParse(Regex.Match(coordinates[0].Value, @"[\-\+]?([\.]?(\d))").Value, out X) &&
                double.TryParse(Regex.Match(coordinates[1].Value, @"[\-\+]?([\.]?(\d))").Value, out Y))
                return false;

            isValid = true;
            return true;
        }
    }


    public class PinStateService : IPinService
    {

        public List<Pin> GetAllPinsForUser(Guid user_id)
        {
            return PinsState.Instance.Pins;
        }

        public async Task<List<Pin>> FindPin(string request)
        {
            return await Task.Run(() =>
            {


                return new List<Pin>();
            });
        }

        public Pin CreatePin(Guid user_id, string name)
        {
            var pin = new Pin()
            {
                PinId = Guid.NewGuid(),
                Name = name,
                UserId = user_id,
                Favorite = false
            };

            PinsState.Instance.Create(pin);

            return pin;
        }


        public Pin UpdatePin(Pin pin)
        {
            PinsState.Instance.Update(pin);
            return pin;
        }

        public void DeletePin(Pin pin)
        {
            PinsState.Instance.Delete(pin);
        }
    }
}
