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
            //NHHelper.SchemaExport();

            CreateData();
        }

        private static void CreateData()
        {
            ISession session = NHHelper.GetSession();
            ITransaction tx = session.BeginTransaction();

            for (int i = 0; i < 10; i++)
            {
                Job job = new Job() { Title = "Job Title, " + i };
                session.SaveOrUpdate(job);

                for (int j = 0; j < 3; j++)
                {
                    Comment comment = new Comment()
                    {
                        Value = "Comment, " + j,
                        ParentJob = job
                    };

                    session.SaveOrUpdate(comment);
                }


            }

            tx.Commit();
            session.Flush();


            foreach (Job innerJob in session.QueryOver<Job>().List())
            {
                Console.WriteLine(string.Format("Job: {0}, {1}", innerJob.Id, innerJob.Title));
            }

            Console.WriteLine("press enter");
            Console.ReadLine();
        }
    }
}
