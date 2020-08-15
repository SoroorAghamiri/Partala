using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FluentScheduler;

namespace Tools
{
    public class Timer : IDisposable
    {
        // private Registry registry;
        public static readonly Timer GlobalTimer = new Timer();
        private DateTime start;
        private List<string> jobs = new List<string>();
        IDCounter idCounter;

        public Timer()
        {
            idCounter = new IDCounter();
            // registry = new Registry();

            JobManager.Start();
            this.start = DateTime.UtcNow;
        }

        public TimeSpan getTimeSpan()
        {
            return DateTime.UtcNow - start;
        }

        public void cancelTask(string jobID)
        {
            JobManager.RemoveJob(jobID);
        }
        public void cancellAllTasks()
        {
            foreach (var j in jobs)
            {
                cancelTask(j);
            }
        }

        public string addIntervalSeccondAction(int interval, Action action)
        {
            return addIntervalMiliSeccondAction(interval * 1000, action);
        }
        public string addIntervalMiliSeccondAction(int interval, Action action)
        {
            string JobName = idCounter.getNextID().ToString();
            JobManager.AddJob(action, (sch) =>
            {
                sch.WithName(JobName);
                sch.ToRunEvery(interval).Milliseconds();
            });
            jobs.Add(JobName);

            return JobName;
        }

        public string addSecondAction(int delay, Action action)
        {
            return addMiliSecondAction(delay * 1000, action);
        }

        public string addMiliSecondAction(int delay, Action action)
        {
            string JobName = idCounter.getNextID().ToString();
            JobManager.AddJob(action, (sch) =>
            {
                sch.WithName(JobName);
                sch.ToRunNow().DelayFor(delay).Milliseconds();
            });
            jobs.Add(JobName);

            return JobName;
        }

        public void Dispose()
        {
            cancellAllTasks();
        }
    }
}
