namespace MailPig.DAL.Migrations
{
    using Context;
    using Model.Entities;
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MailPigContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MailPigContext context)
        {
            //if (System.Diagnostics.Debugger.IsAttached == false)
            //    System.Diagnostics.Debugger.Launch();

            context.Groups.AddOrUpdate(
                g => g.Name,
                new Group { Name = "Prva grupa" },
                new Group { Name = "Druga grupa" },
                new Group { Name = "Treæa grupa" },
                new Group { Name = "Èetvrta grupa" }
                );

            context.Subscribers.AddOrUpdate(
                s => s.Name,
                new Subscriber { Name = "Pero", Surname = "Periæ", Email = "pero.peric@mail.com" },
                new Subscriber { Name = "Marko", Surname = "Mariæ", Email = "marko.maric@mail.com" },
                new Subscriber { Name = "Ana", Surname = "Aniæ", Email = "ana.anic@mail.com" },
                new Subscriber { Name = "Jovo", Surname = "Joviæ", Email = "jovo.jovic@mail.com" },
                new Subscriber { Name = "Tanja", Surname = "Tanjiæ", Email = "tanja.tanjic@mail.com" });

            context.SaveChanges();

            int[] groupIds = context.Groups.Select(g => g.Id).ToArray();
            int[] subscribersIds = context.Subscribers.Select(s => s.Id).ToArray();

            DateTime someDate = new DateTime(2011, 3, 18);

            context.GroupSubscriptions.RemoveRange(context.GroupSubscriptions.ToArray());

            context.GroupSubscriptions.AddOrUpdate(
                new GroupSubscription { GroupId = groupIds[0], SubscriberId = subscribersIds[0], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[0], SubscriberId = subscribersIds[4], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[0], SubscriberId = subscribersIds[3], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[1], SubscriberId = subscribersIds[1], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[1], SubscriberId = subscribersIds[2], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[1], SubscriberId = subscribersIds[4], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[2], SubscriberId = subscribersIds[4], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[2], SubscriberId = subscribersIds[3], DateJoined = someDate },
                new GroupSubscription { GroupId = groupIds[3], SubscriberId = subscribersIds[0], DateJoined = someDate });

            context.Emails.AddOrUpdate(
                e => e.Subject,
                new Email { Subject = "Prvi email", Body = "Ovo je tijelo prvog emaila" },
                new Email { Subject = "Drugi email", Body = "Ovo je tijelo drugog emaila! Ha!" },
                new Email { Subject = "Najkul email", Body = "<h1>Naslov</h1><br>Ja sam kul jer imam naslov" },
                new Email { Subject = "Dosadni email", Body = "Ja sam jako dosadan email" },
                new Email
                {
                    Subject = "Veseli email",
                    Body =
                        "Jeeee ja sam jako veseli email i imam u sebi html vidi: <span style=\"color:red;\">crveno<span/>"
                });

            context.SaveChanges();
        }
    }
}