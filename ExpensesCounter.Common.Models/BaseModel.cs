using System;

namespace ExpensesCounter.Common.Models
{
    public abstract class BaseModel
    {
        public int Id { get; set; }

        public DateTime CreatedAtUtc { get; set; }
        public DateTime UpdatedAtUtc { get; set; }
    }
}
