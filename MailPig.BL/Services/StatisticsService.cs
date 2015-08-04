namespace MailPig.BL.Services
{
    using Core;
    using DAL.Core;
    using Model.Entities;
    using Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StatisticsService : ServiceBase
    {
        public StatisticsService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public WelcomePageStatisticsModel GetWelcomePageStatistics()
        {
            IRepository<Group> groupRepo = UnitOfWork.Repository<Group>();
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();
            IRepository<SentEmail> sentMailRepo = UnitOfWork.Repository<SentEmail>();

            return new WelcomePageStatisticsModel
            {
                Groups = groupRepo.Query.Count(),
                Subscribers = subscriberRepo.Query.Count(),
                SentMails = sentMailRepo.Query.Count()
            };
        }

        public DashboardPageStatisticsModel GetDashboardStatistics()
        {
            DashboardPageStatisticsModel stats = new DashboardPageStatisticsModel();
            IRepository<Group> groupRepo = UnitOfWork.Repository<Group>();
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();
            IRepository<SentEmail> sentEmailRepo = UnitOfWork.Repository<SentEmail>();
            IRepository<GroupSubscription> subscriptionsRepo = UnitOfWork.Repository<GroupSubscription>();

            stats.GroupCount = groupRepo.Query.Count();
            stats.BiggestGroup = groupRepo.Query
                .OrderByDescending(g => g.GroupSubscriptions
                    .Count(gs => !gs.DateLeft.HasValue))
                .Select(
                    g =>
                        new TableGroupModel
                        {
                            Id = g.Id,
                            Name = g.Name,
                            CurrentSubscribers = g.GroupSubscriptions.Count(gs => !gs.DateLeft.HasValue)
                        })
                .FirstOrDefault();

            stats.SubscriptionsCount = subscriptionsRepo.Query.Count(gs => !gs.DateLeft.HasValue);

            stats.SubscriberCount = subscriberRepo.Query.Count();
            stats.TopSubscribers = subscriberRepo.Query
                .OrderByDescending(s => s.GroupSubscriptions.Count(gs => !gs.DateLeft.HasValue))
                .Take(5)
                .Select(s => new SubscriberModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Surname = s.Surname,
                    Email = s.Email
                })
                .ToList();

            stats.SentMailsCount = sentEmailRepo.Query.Count();
            //sentEmailRepo.Query
            //    .GroupBy(s => DbFunctions.TruncateTime(s.DateSent))
            //    .OrderByDescending(s => s.Key)
            //    .Take(7)
            //    .Select(s=>new )

            return stats;
        }

        // TODO add statistics
    }
}