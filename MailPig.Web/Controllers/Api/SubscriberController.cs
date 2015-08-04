namespace MailPig.Web.Controllers.Api
{
    using BL.Models;
    using BL.Services;
    using Core;
    using System.Collections.Generic;
    using System.Web.Http;

    public class SubscriberController : MailPigApiControllerBase
    {
        public SubscriberService SubscriberService { get; set; }

        public SubscriberController()
        {
            SubscriberService = new SubscriberService(UnitOfWork);
        }

        // GET: api/Subscriber
        public IEnumerable<SubscriberGroupsModel> Get()
        {
            return SubscriberService.GetAllSubscribers();
        }

        // GET: api/Subscriber/5
        public SubscriberModel Get(int id)
        {
            return SubscriberService.GetById(id);
        }

        // POST: api/subscribe/8/to/5
        [HttpPost]
        [Route("api/subscribe/{subscriberId}/to/{groupId}")]
        public IHttpActionResult CreateSubscription(int subscriberId, int groupId)
        {
            bool isSubscribed = SubscriberService.AddSubscription(subscriberId, groupId);

            if (isSubscribed)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}