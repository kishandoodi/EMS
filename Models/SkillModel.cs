using System;

namespace WebApp_complete.Models
{
    public class SkillModel
    {
        public int Id { get; set; }
        public string SkillName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}