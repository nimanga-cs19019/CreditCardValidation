namespace Notes.API.Models.Entities
{
    public class Note
    {
        public Guid Id { get; set; }
        public string CardNo { get; set; }
        public string Type { get; set; }
        public bool IsValidated { get; set; }
    }
}
