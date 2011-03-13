using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SJ.Core;
using NHibernate;

namespace SJ.AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            ISession session = NHHelper.GetSession();
            ITransaction tx = session.BeginTransaction();

            for (int i = 0; i < 10; i++)
            {
                Job job = new Job() { Title = "Job Title, " + i };
                session.SaveOrUpdate(job);
            }

            tx.Commit();
            session.Flush();

            foreach (Job job in session.QueryOver<Job>().List())
            {
                Console.WriteLine(string.Format("Job: {0}, {1}", job.Id, job.Title));
            }

            Console.WriteLine("press enter");
            Console.ReadLine();
        }
    }
}
