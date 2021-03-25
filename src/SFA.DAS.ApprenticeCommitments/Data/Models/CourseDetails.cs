#nullable enable

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class CourseDetails
    {
#pragma warning disable CS8618 // Private constructor for entity framework
        private CourseDetails()
        {
        }
#pragma warning disable CS8618

        public CourseDetails(string name, int level, string? option)
        {
            Name = name;
            Level = level;
            Option = option;
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public string? Option { get; set; }
    }
}