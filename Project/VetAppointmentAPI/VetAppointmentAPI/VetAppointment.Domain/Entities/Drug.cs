using VetAppointment.Domain.Helpers;

namespace VetAppointment.Domain.Entities
{
    public class Drug
    {
        public Drug(string title)
        {
            DrugId = Guid.NewGuid();
            Title = title;
        }

        public Guid DrugId { get; private set; }
        public string Title { get; private set; }

        public Result UpdateDrugInformation(string name)
        {
            if (name == null)
                return Result.Failure("Name cannot be null");
            if (name.Length < 2)
                return Result.Failure("Name length cannot be less than 2");
            Title = name;
            return Result.Success();
        }
    }
}
