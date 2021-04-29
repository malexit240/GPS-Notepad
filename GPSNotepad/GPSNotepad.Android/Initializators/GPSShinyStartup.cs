using Shiny.Jobs;
using Shiny;
using Microsoft.Extensions.DependencyInjection;
using System;


namespace GPSNotepad.Droid
{
    public class GPSShinyStartup : ShinyStartup
    {

        public override void ConfigureServices(IServiceCollection services, IPlatform platform)
        {
            services.UseNotifications();
            services.RegisterJob(new JobInfo(typeof(GPSNotepad.NotificationJob)) { RunOnForeground = true });
            services.UseJobForegroundService(TimeSpan.FromMinutes(1));
        }
    }




}

