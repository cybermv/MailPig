namespace MailPig.TestConsole
{
    using DAL.Context;
    using DAL.Core;
    using Model.Entities;
    using System;
    using System.Linq;

    internal class Program
    {
        private static void Main(string[] args)
        {
            //IUnitOfWork uow = new UnitOfWork(new MailPigContext());

            //IRepository<Group> groupRepo = uow.Repository<Group>();

            //Console.WriteLine("Sequence initially contains {0} elements.", groupRepo.Query.Count());

            Console.Read();
        }
    }
}