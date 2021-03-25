#nullable enable

using System;
using static SFA.DAS.ApprenticeCommitments.Extensions.DateCalculations;

namespace SFA.DAS.ApprenticeCommitments.Data.Models
{
    public class CourseDetails
    {
#pragma warning disable CS8618 // Private constructor for entity framework

        private CourseDetails()
        {
        }

#pragma warning disable CS8618

        public CourseDetails(
            string name, int level, string? option,
            DateTime plannedStartDate, DateTime plannedEndDate)
        {
            Name = name;
            Level = level;
            Option = option;
            PlannedStartDate = plannedStartDate;
            PlannedEndDate = plannedEndDate;
        }

        public string Name { get; set; }
        public int Level { get; set; }
        public string? Option { get; set; }
        public DateTime PlannedEndDate { get; set; }
        public DateTime PlannedStartDate { get; set; }

        public int DurationInMonths => DifferenceInMonths(PlannedStartDate, PlannedEndDate);
    }
}