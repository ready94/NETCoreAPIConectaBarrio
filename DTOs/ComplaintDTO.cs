using NETCoreAPIConectaBarrio.Enums;

namespace NETCoreAPIConectaBarrio.DTOs
{
    public class ComplaintDTO
    {
        public EnumComplaintTypes IdComplaintType { get; set; }
        public EnumComplaintPriority IdPriority { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
    }
}
