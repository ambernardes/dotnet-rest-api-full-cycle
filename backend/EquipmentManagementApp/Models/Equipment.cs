using Microsoft.AspNetCore.Mvc;

namespace EquipmentManagementApp.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Category { get; set; }

        public string Status { get; set; }

        public string Location { get; set; }

        public DateTime LastMaintenanceDate { get; set; }

        public decimal RentalRate { get; set; }
    }
}
