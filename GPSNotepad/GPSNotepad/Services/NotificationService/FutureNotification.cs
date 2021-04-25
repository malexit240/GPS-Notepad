using System;
using System.Collections.Generic;

namespace GPSNotepad
{
    public class FutureNotification
    {
        #region ---Public Static Methods---
        public static FutureNotification Create(string pinName, string notificationText, DateTime TimeToNotify)
        {
            return Create(new FutureNotification()
            {
                Id = new Random().Next(),
                NotificationTitle = pinName,
                NotificationText = notificationText,
                TimeToNotify = TimeToNotify
            });
        }

        public static FutureNotification Create(FutureNotification note)
        {
            NotificationJob.NotificationsShedulde.Add(note);
            NotificationJob.NotificationsShedulde.Sort(new Comparer());

            return note;
        }
        #endregion

        #region ---Public Properties---
        public int Id { get; set; }

        public string NotificationTitle { get; set; }

        public string NotificationText { get; set; }

        public DateTime TimeToNotify { get; set; }

        public bool IsFired { get; set; }
        #endregion

        #region ---Internal Classes---
        class Comparer : IComparer<FutureNotification>
        {
            public int Compare(FutureNotification x, FutureNotification y)
            {
                return x.TimeToNotify.CompareTo(y.TimeToNotify);
            }
        }
        #endregion
    }
}