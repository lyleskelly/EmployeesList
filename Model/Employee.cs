using System.ComponentModel.DataAnnotations;

namespace InterviewTest.Model
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
