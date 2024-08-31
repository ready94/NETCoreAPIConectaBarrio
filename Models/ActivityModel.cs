using System.Data;

namespace NETCoreAPIConectaBarrio.Models
{
    public class ActivityModel
    {
        public int IdEvent { get; set; }
        public int IdEventType { get; set; }
        public int IdEventSubCategory { get; set; }
        public string Location { get; set; }
        public int MaxPerson { get; set; }
        public int ActualPerson { get; set; }
        public int CreationUser { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreationDate { get; set; }

        public ActivityModel() { }

        public ActivityModel(DataRow row) 
        { 
            IdEvent = row.Field<int>("IDEVENT");
            IdEventType = row.Field<int>("IDEVENT_TYPE");
            IdEventSubCategory = row.Field<int>("IDEVENT_SUBCATEGORY");
            Location = row.Field<string>("LOCATION");
            MaxPerson = row.Field<int>("MAX_PERSON");
            ActualPerson = row.Field<int>("ACTUAL_PERSON");
            CreationUser = row.Field<int>("CREATION_USER");
            EventDate = row.Field<DateTime>("EVENT_DATE");
            CreationDate = row.Field<DateTime>("CREATION_DATE");
        }

    }
}
