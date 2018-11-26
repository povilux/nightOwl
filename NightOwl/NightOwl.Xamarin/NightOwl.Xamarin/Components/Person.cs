namespace NightOwl.Xamarin.Components
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string BirthDate { get; set; }
        public string MissingDate { get; set; }
        public string AdditionalInfo { get; set; }
        public User Creator { get; set; }
    }
}
