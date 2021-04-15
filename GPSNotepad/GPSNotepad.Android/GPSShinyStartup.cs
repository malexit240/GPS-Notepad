using Shiny.Jobs;
using Shiny;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace GPSNotepad.Droid
{
    public class GPSShinyStartup : ShinyStartup
    {
        public override void ConfigureServices(IServiceCollection builder)
        {
            builder.UseNotifications();
            builder.RegisterJob(new JobInfo(typeof(GPSNotepad.NotificationJob)) { RunOnForeground = true });
            builder.UseJobForegroundService(TimeSpan.FromMinutes(1));
        }
    }




}

