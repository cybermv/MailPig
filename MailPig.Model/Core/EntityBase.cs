namespace MailPig.Model.Core
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public abstract class EntityBase
    {
        protected EntityBase()
        {
            this.CreationDate = DateTime.Now;
        }

        [Key]
        public int Id { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}