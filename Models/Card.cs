namespace CARDGENERATOR.Models
{
    public class Card : BaseEntity
    {
        public string Surname { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DateOfBirth { get; set; }
        public string Sex { get; set; }
        public string ExamNo { get; set; }
        public string SerialNo { get; set; }
        public string PIN { get; set; }
        public Status Status { get; set; }
    }

    public enum Status
    {
        Generated = 1,
        Purchased,
        Used
    }

}
