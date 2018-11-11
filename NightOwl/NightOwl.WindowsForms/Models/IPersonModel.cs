
using NightOwl.WindowsForms.Components;

namespace NightOwl.WindowsForms.Models
{
    public interface IPersonModel
    {
        Person CurrentPerson { get; set; }
        void Add( string name, string bdate, string mdate, string addinfo);
        void GroupPersonsByCreator();
    }
}