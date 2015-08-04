namespace MailPig.BL.Services
{
    using Core;
    using DAL.Core;
    using Model.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SubscriberService : ServiceBase
    {
        public SubscriberService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IEnumerable<SubscriberGroupsModel> GetAllSubscribers()
        {
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();

            return subscriberRepo.Query
                .Select(s => new SubscriberGroupsModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Surname = s.Surname,
                    Email = s.Email,
                    GroupsIn = s.GroupSubscriptions
                        .Where(gs => !gs.DateLeft.HasValue)
                        .Select(g => g.Group.Name)
                        .ToList()
                })
                .ToList();
        }

        public IEnumerable<SubscriberGroupsModel> GetSubscribersFor(int groupId)
        {
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();

            return subscriberRepo.Query
                .Where(s => s.GroupSubscriptions.Any(gs => gs.GroupId == groupId &&
                                                           !gs.DateLeft.HasValue))
                .Select(s => new SubscriberGroupsModel
                {
                    Id = s.Id,
                    Name = s.Name,
                    Surname = s.Surname,
                    Email = s.Email,
                    GroupsIn = s.GroupSubscriptions
                        .Where(gs => !gs.DateLeft.HasValue)
                        .Select(g => g.Group.Name)
                        .ToList()
                });

            //IRepository<Group> groupRepo = UnitOfWork.Repository<Group>();

            //Group group = groupRepo.Query.SingleOrDefault(g => g.Id == groupId);

            //if (group == null)
            //{
            //    return null;
            //}

            //return UnitOfWork.Repository<GroupSubscription>()
            //    .Query
            //    .Where(gs => gs.GroupId == groupId && !gs.DateLeft.HasValue)
            //    .Select(gs => new SubscriberModel
            //    {
            //        Id = gs.SubscriberId,
            //        Name = gs.Subscriber.Name,
            //        Surname = gs.Subscriber.Surname,
            //        Email = gs.Subscriber.Email
            //    })
            //    .ToList();
        }

        public IEnumerable<SubscriberHistoryModel> GetHistoryFor(int subscriberId)
        {
            Subscriber subscriber = UnitOfWork.Repository<Subscriber>().FindById(subscriberId);

            if (subscriber == null)
            {
                return null;
            }

            IRepository<GroupSubscription> groupSubscriptionRepo = UnitOfWork.Repository<GroupSubscription>();

            return groupSubscriptionRepo.Query
                .Where(gs => gs.SubscriberId == subscriberId)
                .OrderBy(gs => gs.DateJoined)
                .Select(gs => new SubscriberHistoryModel
                {
                    Id = gs.Id,
                    GroupName = gs.Group.Name,
                    DateJoined = gs.DateJoined,
                    DateLeft = gs.DateLeft
                });
        }

        public SubscriberModel GetById(int id)
        {
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();
            Subscriber subscriber = subscriberRepo.FindById(id);

            if (subscriber == null)
            {
                return null;
            }

            return new SubscriberModel
            {
                Id = subscriber.Id,
                Name = subscriber.Name,
                Surname = subscriber.Surname,
                Email = subscriber.Email
            };
        }

        public SubscriberModel AddSubscriber(SubscriberWithFirstGroupModel model)
        {
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();
            IRepository<GroupSubscription> groupSubRepo = UnitOfWork.Repository<GroupSubscription>();

            Subscriber newSub = new Subscriber
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email
            };

            Subscriber insertedSub = subscriberRepo.Insert(newSub);

            if (insertedSub == null)
            {
                return null;
            }

            model.Id = insertedSub.Id;

            if (model.FirstGroupId.HasValue)
            {
                GroupSubscription subscription = new GroupSubscription
                {
                    GroupId = model.FirstGroupId.Value,
                    SubscriberId = model.Id,
                    DateJoined = DateTime.Now
                };

                GroupSubscription createdSubscription = groupSubRepo.Insert(subscription);
                if (createdSubscription == null)
                {
                    return null;
                }
            }

            return model;
        }

        public SubscriberModel EditSubscriber(SubscriberModel model)
        {
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();

            Subscriber subscriber = subscriberRepo.FindById(model.Id);
            if (subscriber == null)
            {
                return null;
            }

            subscriber.Name = model.Name;
            subscriber.Surname = model.Surname;
            subscriber.Email = model.Email;

            return subscriberRepo.Update(subscriber) != null ? model : null;
        }

        public bool RemoveSubscriber(int subscriberId)
        {
            IRepository<Subscriber> subscriberRepo = UnitOfWork.Repository<Subscriber>();

            Subscriber subToDelete = subscriberRepo.FindById(subscriberId);
            if (subToDelete == null)
            {
                return false;
            }

            return subscriberRepo.Delete(subToDelete.Id);
        }

        public bool AddSubscription(int subscriberId, int groupId)
        {
            IRepository<GroupSubscription> subscriptionsRepo = UnitOfWork.Repository<GroupSubscription>();

            GroupSubscription newSubscription = new GroupSubscription
            {
                SubscriberId = subscriberId,
                GroupId = groupId,
                DateJoined = DateTime.Now
            };

            GroupSubscription insertedSubscription = subscriptionsRepo.Insert(newSubscription);
            return insertedSubscription != null;
        }

        public bool RemoveSubscription(int subscriptionId)
        {
            IRepository<GroupSubscription> subscriptionRepo = UnitOfWork.Repository<GroupSubscription>();
            GroupSubscription toRemove = subscriptionRepo.FindById(subscriptionId);

            if (toRemove == null)
            {
                return false;
            }

            return subscriptionRepo.Delete(toRemove.Id);
        }
    }
}