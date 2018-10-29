namespace nightOwl.Models
{
    public interface IPersonModel
    {
         Person CurrentPerson { get; set; }
        Person FindPerson(string name);
        void Add(string name, string bdate, string mdate, string addinfo);
    }
}