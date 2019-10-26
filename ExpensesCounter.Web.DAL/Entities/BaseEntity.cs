using System;

namespace ExpensesCounter.Web.DAL.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        public DateTimeOffset CreatedDateUtc { get; set; }
        public DateTimeOffset UpdatedDateUtc { get; set; }
    }
}