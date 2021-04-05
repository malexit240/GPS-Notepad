using GPSNotepad.Model.Entities;
using System;
using System.Collections.Generic;
using GPSNotepad.Model.Interfaces;
using System.Threading.Tasks;

namespace GPSNotepad.Model
{
  // public class PinStateService : IPinService2
  // {
  //
  //     #region ---IPinService Implementation---
  //     public Task<List<Pin>> GetAllPinsForUser(Guid user_id) => PinState.Instance.GetPins();
  //     public void LoadUserPins(Guid user_id) => PinState.Instance.LoadPins(user_id);
  //     
  //
  //     public async Task<List<Pin>> FindPin(string request) // not implemented code
  //     {
  //         return await Task.Run(() =>
  //         {
  //             return new List<Pin>();
  //         });
  //     }
  //
  //     public Pin CreatePin(Guid user_id, string name)
  //     {
  //         var pin = new Pin()
  //         {
  //             PinId = Guid.NewGuid(),
  //             Name = name,
  //             UserId = user_id,
  //             Favorite = false
  //         };
  //
  //         PinState.Instance.Create(pin);
  //
  //         return pin;
  //     }
  //
  //     public void CreateOrUpdatePin(Pin pin)
  //     {
  //         if (!PinState.Instance.ContainsPin(pin.PinId))
  //             PinState.Instance.Create(pin);
  //         else
  //             PinState.Instance.Update(pin);
  //     }
  //
  //     public Pin UpdatePin(Pin pin)
  //     {
  //         PinState.Instance.Update(pin);
  //         return pin;
  //     }
  //
  //     public void DeletePin(Pin pin) => PinState.Instance.Delete(pin);
  //
  //
  //     #endregion
  //
  // }
}
