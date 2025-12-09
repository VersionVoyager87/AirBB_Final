using System;

namespace AirBB.Models.ViewModels
{
    public class FilterCriteria
    {
        public string ActiveLocation { get; set; } = "all";
        public string StartDate { get; set; } = DateTime.Today.ToString("MM/dd/yyyy");
        public string EndDate { get; set; } = DateTime.Today.AddDays(2).ToString("MM/dd/yyyy");
        public int GuestCount { get; set; } = 1;
    }
}
