using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Karvis.Domain;
using Karvis.Domain.Tasks;
using Razmyar.SharpLite.Tasks;
using SharpLite.Domain.DataInterfaces;

namespace Karvis.Tasks
{
   public class JobTask : CrudTask<Job>, IJobTask 
    {
       public JobTask(IRepository<Job> repository) : base(repository)
       {
       }
    }
}
