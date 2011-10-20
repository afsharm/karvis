using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace Karvis.Core
{
    public class KSingleton
    {
        private static KSingleton instance;
        private Timer _timer;
        private IKGlobalModel _globalModel;
        private IKScheduler _kscheduler;

        private KSingleton() { Initialize(); }

        public static KSingleton Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new KSingleton();
                }
                return instance;
            }
        }

        private void Initialize()
        {
            _globalModel = new KGlobalModel();
            _kscheduler = new KScheduler();
            _timer = new Timer();
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
        }

        private void _timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            _kscheduler.Trigger();
        }

        private double TryGetInterval()
        {
            try
            {
                return Convert.ToDouble(_globalModel.GetValue(KGlobalConstants.ScheduleInterval));
            }
            catch
            {
                //default one hour
                return 60 * 60 * 1000;
            }
        }

        public void ConfigTimer()
        {
            double interval = TryGetInterval();
            _timer.Interval = interval;
            _timer.Stop();
            _timer.Start();
        }
    }
}
