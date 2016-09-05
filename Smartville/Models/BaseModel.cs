using System;
using System.ComponentModel.DataAnnotations;

namespace Smartville.Models
{
    public class BaseModel
    {
        public BaseModel()
        {
            this.CreatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3));
            this.UpdatedAt = DateTimeOffset.UtcNow.ToOffset(TimeSpan.FromHours(-3));
        }

        public long Id { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset CreatedAt { get; set; }

        [DataType(DataType.Date)]
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
