namespace MailPig.Web.Controllers.Api
{
    using BL.Models;
    using BL.Services;
    using Core;
    using System.Collections.Generic;
    using System.Web.Http;

    public class GroupsController : MailPigApiControllerBase
    {
        public GroupService GroupService { get; set; }

        public GroupsController()
        {
            GroupService = new GroupService(UnitOfWork);
        }

        // GET: api/Groups
        public IEnumerable<TableGroupModel> Get()
        {
            return GroupService.GetAllGroups();
        }

        // GET: api/Groups/5
        public GroupModel Get(int id)
        {
            return GroupService.GetGroupById(id);
        }

        // POST: api/Groups
        public IHttpActionResult Post([FromBody]GroupModel model)
        {
            GroupModel created = GroupService.CreateNewGroup(model);
            if (created != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // PUT: api/Groups/5
        public IHttpActionResult Put(int id, [FromBody]GroupModel model)
        {
            GroupModel edited = GroupService.EditGroup(model);
            if (edited != null)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        // DELETE: api/Groups/5
        public IHttpActionResult Delete(int id)
        {
            bool removed = GroupService.RemoveGroup(id);
            if (removed)
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