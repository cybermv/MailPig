namespace MailPig.BL.Models
{
    using System;
    using System.Collections.Generic;

    public class WelcomePageStatisticsModel
    {
        public int Groups { get; set; }

        public int Subscribers { get; set; }

        public int SentMails { get; set; }
    }

    public class DashboardPageStatisticsModel
    {
        public int GroupCount { get; set; }

        public TableGroupModel BiggestGroup { get; set; }

        public int SubscriberCount { get; set; }

        public int SubscriptionsCount { get; set; }

        public IEnumerable<SubscriberModel> TopSubscribers { get; set; }

        public int SentMailsCount { get; set; }

        public IEnumerable<KeyValuePair<DateTime, int>> SentMailsHistory { get; set; }
    }
}