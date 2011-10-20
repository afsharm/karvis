using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Core
{
    public class KScheduler : IKScheduler
    {
        IKGlobalModel _globalModel;
        IKMailModel _mailModel;

        const int hourInterval = 2;

        public KScheduler()
        {
            _globalModel = new KGlobalModel();
            _mailModel = new KMailModel();
        }

        public void Trigger()
        {
            string value = _globalModel.GetValue(KGlobalConstants.LastScheduleRun);

            DateTime lastRun = DateTime.Now;
            DateTime.TryParse(value, out lastRun);

            DateTime currentTime = DateTime.Now;
            if ((currentTime - lastRun).Hours > hourInterval)
            {
                _mailModel.DoSchedule();
            }

            _globalModel.SetValue(KGlobalConstants.LastScheduleRun, currentTime.ToString());
        }
    }
}
