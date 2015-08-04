namespace MailPig.BL.Services
{
    using Core;
    using DAL.Core;
    using Model.Entities;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    public class GroupService : ServiceBase
    {
        public GroupService(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<TableGroupModel> GetAllGroups()
        {
            return UnitOfWork.Repository<Group>().Query
                .Select(g => new TableGroupModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    CurrentSubscribers = g.GroupSubscriptions.Count(gs => !gs.DateLeft.HasValue)
                })
                .ToList();
        }

        public GroupModel GetGroupById(int id)
        {
            Group foundGroup = UnitOfWork.Repository<Group>().FindById(id);

            if (foundGroup != null)
            {
                return new GroupModel
                {
                    Id = foundGroup.Id,
                    Name = foundGroup.Name
                };
            }
            return null;
        }

        public GroupWithSubscribersModel GetGroupWithSubscribersById(int id)
        {
            IRepository<Group> groupRepo = UnitOfWork.Repository<Group>();

            GroupWithSubscribersModel groupWithSubscribers = groupRepo.Query
                .Where(g => g.Id == id)
                .Include(g => g.GroupSubscriptions)
                .Select(g => new GroupWithSubscribersModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    Subscribers = g.GroupSubscriptions
                        .Where(s => !s.DateLeft.HasValue)
                        .Select(s => new SubscriberModel
                        {
                            Id = s.SubscriberId,
                            Name = s.Subscriber.Name,
                            Surname = s.Subscriber.Surname,
                            Email = s.Subscriber.Email
                        })
                })
                .SingleOrDefault();

            return groupWithSubscribers;
        }

        public bool EditGroupSubscriptions(GroupWithSubscribersModel groupWithSubscribers)
        {
            IRepository<GroupSubscription> groupSubscriptionsRepo = UnitOfWork.Repository<GroupSubscription>();

            if (groupWithSubscribers.Subscribers == null)
            {
                groupWithSubscribers.Subscribers = new List<SubscriberModel>();
            }

            List<SubscriberModel> chosenSubscriptions = groupWithSubscribers.Subscribers
                .Distinct(new SubscriberModelEqualityComparer())
                .ToList();
            List<GroupSubscription> currentSubscriptions = groupSubscriptionsRepo.Query
                .Where(gs => gs.GroupId == groupWithSubscribers.Id)
                .ToList();

            foreach (GroupSubscription currentSub in currentSubscriptions)
            {
                if (chosenSubscriptions.Any(s => s.Id == currentSub.Id))
                {
                    var sub = currentSub;
                    chosenSubscriptions.Remove(chosenSubscriptions.Find(s => s.Id == sub.Id));
                }
                else
                {
                    currentSub.DateLeft = DateTime.Now;
                    groupSubscriptionsRepo.Update(currentSub);
                }
            }

            foreach (SubscriberModel subcriberToAdd in chosenSubscriptions)
            {
                GroupSubscription subscription = new GroupSubscription
                {
                    SubscriberId = subcriberToAdd.Id,
                    GroupId = groupWithSubscribers.Id,
                    DateJoined = DateTime.Now
                };
                groupSubscriptionsRepo.Insert(subscription);
            }

            return true;
        }

        public GroupModel CreateNewGroup(GroupModel newGroup)
        {
            Group toInsert = new Group
            {
                Name = newGroup.Name
            };

            Group inserted = UnitOfWork.Repository<Group>().Insert(toInsert);

            if (inserted != null)
            {
                newGroup.Id = inserted.Id;
                return newGroup;
            }
            return null;
        }

        public GroupModel EditGroup(GroupModel groupToEdit)
        {
            IRepository<Group> groupRepo = UnitOfWork.Repository<Group>();

            Group foundGroup = groupRepo.FindById(groupToEdit.Id);

            if (foundGroup != null)
            {
                foundGroup.Name = groupToEdit.Name;
                UnitOfWork.Repository<Group>().Update(foundGroup);
                return groupToEdit;
            }
            return null;
        }

        public bool RemoveGroup(GroupModel groupToRemove)
        {
            return RemoveGroup(groupToRemove.Id);
        }

        public bool RemoveGroup(int groupId)
        {
            IRepository<Group> groupRepo = UnitOfWork.Repository<Group>();
            IRepository<GroupSubscription> groupSubscriptionsRepo = UnitOfWork.Repository<GroupSubscription>();

            Group toRemove = groupRepo.FindById(groupId);

            if (toRemove == null)
            {
                return false;
            }

            List<GroupSubscription> subscriptionsToRemove = groupSubscriptionsRepo.Query
                .Where(gs => gs.GroupId == groupId)
                .ToList();

            foreach (GroupSubscription subscription in subscriptionsToRemove)
            {
                bool deleted = groupSubscriptionsRepo.Delete(subscription.Id);
                if (!deleted)
                {
                    return false;
                }
            }

            return groupRepo.Delete(toRemove.Id);
        }
    }

    internal class SubscriberModelEqualityComparer : IEqualityComparer<SubscriberModel>
    {
        public bool Equals(SubscriberModel x, SubscriberModel y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(SubscriberModel obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}