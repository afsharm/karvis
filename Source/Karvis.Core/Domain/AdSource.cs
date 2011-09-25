using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Karvis.Core
{
    /// <summary>
    /// Advertise Source of a typical Job
    /// </summary>
    public enum AdSource : int
    {
        rahnama_com = 0, //نیازمندی‌های همشهری
        irantalent_com = 1, //irantalent.com
        Email = 2, //ایمیل
        developercenter_ir = 3, //developercenter.ir
        Misc = 4, //متفرقه
        All = 5, //همه
        karvis_ir = 6, //کارویس
        itjobs_ir = 7, //itjobs.ir
        agahi_ir = 8, //agahi.ir
        istgah_com = 9, //istgah.com
        nofaـir = 10, //nofa.ir,
        unp_ir = 11 // unp.ir
    }
}
