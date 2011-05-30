using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SJ.Core;
using NHibernate;
using log4net.Config;
using System.IO;

namespace SJ.AppConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlConfigurator.Configure(new FileInfo("log4net.config"));
            CreateData();
        }

        private static void CreateData()
        {
            ISession session = NHHelper.Instance.GetCurrentSession();
            ITransaction tx = session.BeginTransaction();

            for (int i = 0; i < 10; i++)
            {
                Job job = new Job() { Title = "Job Title, " + i };

                for (int j = 0; j < 3; j++)
                {
                    job.AddComment(new Comment()
                    {
                        Value = "Comment, " + j
                    });


                }

                session.SaveOrUpdate(job);

            }

            tx.Commit();
            session.Flush();


            foreach (Job innerJob in session.QueryOver<Job>().List())
            {
                Console.WriteLine(string.Format("Job: {0}, {1}", innerJob.ID, innerJob.Title));
            }

            Console.WriteLine("press enter");
            Console.ReadLine();
        }
    }
}
