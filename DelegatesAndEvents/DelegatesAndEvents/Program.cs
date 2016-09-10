using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DelegatesAndEvents
{
    //option1
    //public delegate void WorkedPerformedHandler(int hours, WorkType worktType);

    public delegate int WorkedPerformedHandler(int hours, WorkType worktType);

    public enum WorkType { GoMeetings, Golf, GenerateReports} 
    class Program
    {
        static void Main(string[] args)
        {
            WorkedPerformedHandler del1 = new WorkedPerformedHandler(Method1);
            WorkedPerformedHandler del2 = new WorkedPerformedHandler(Method2);
            WorkedPerformedHandler del3 = new WorkedPerformedHandler(Method3);

            //option1
            //del1(10, WorkType.GenerateReports);
            //del2(20, WorkType.Golf);

            //option2
            //del1 += del2;
            //del1 += del3;

            //option3
            del1 += del2 + del3;

            int finalHours = DoWork(del1);
            Console.WriteLine("Final Hours "+ finalHours);
            Console.ReadLine();

        }

        static int DoWork(WorkedPerformedHandler d)
        {
            return d(5, WorkType.GoMeetings);
        }

        static int Method1(int hours, WorkType worktType)
        {
            Console.WriteLine("Method1 Called " + hours.ToString() + " hours for " + worktType.ToString());
            return hours + 1;
        }

        static int Method2(int hours, WorkType worktType)
        {
            Console.WriteLine("Method2 Called " + hours.ToString() + " hours for " + worktType.ToString());
            return hours + 2;
        }

        static int Method3(int hours, WorkType worktType)
        {
            Console.WriteLine("Method3 Called " + hours.ToString() + " hours for " + worktType.ToString());
            return hours + 3;
        }

    }
}
