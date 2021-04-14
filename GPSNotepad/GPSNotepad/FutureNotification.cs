using System;
using System.Collections.Generic;

namespace GPSNotepad
{
    public class FutureNotification
    {
        public int Id { get; set; }
        public DateTime TimeToNotify { get; set; }

        public string NotificationText { get; set; }

        public bool IsFired { get; set; }

        public static FutureNotification Create(string text, DateTime TimeToNotify)
        {
            return Create(new FutureNotification()
            {
                Id = new Random().Next(),
                NotificationText = text,
                TimeToNotify = TimeToNotify
            });
        }

        public static FutureNotification Create(FutureNotification note)
        {
            NotificationJob.NotificationsShedulde.Add(note);
            NotificationJob.NotificationsShedulde.Sort(new Comparer());

            return note;
        }

        class Comparer : IComparer<FutureNotification>
        {
            public int Compare(FutureNotification x, FutureNotification y)
            {
                return x.TimeToNotify.CompareTo(y.TimeToNotify);
            }
        }
    }
}
