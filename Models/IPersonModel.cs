namespace nightOwl.Models
{
    public interface IPersonModel
    {
        string AdditionalInfo { get; set; }
        string BirthDate { get; set; }
        double CoordX { get; set; }
        double CoordY { get; set; }
        string LastSeenDate { get; set; }
        string MissingDate { get; set; }
        string Name { get; set; }
    }
}