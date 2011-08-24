﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Iesi.Collections.Generic;
using Iesi.Collections;
using System.Collections;

namespace Karvis.Core
{
    public class Job : Entity
    {
        public Job()
        {
        }

        public virtual string Title { set; get; }
        public virtual string Description { set; get; }
        public virtual int VisitCount { set; get; }
        public virtual int FeedCount { set; get; }
        public virtual string Tag { set; get; }
        public virtual DateTime? DateAdded { set; get; }
        public virtual string Url { set; get; }

        public virtual string DateAddedPersian
        {
            get
            {
                return GeneralHelper.ConvertToPersianDate(DateAdded);
            }
        }
    }
}