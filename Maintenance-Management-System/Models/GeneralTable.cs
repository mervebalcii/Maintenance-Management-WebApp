namespace DatabaseLab.Models
{
    public class GeneralTable
    {

        public int Id { get; set; }
        public virtual Vehicle VehicleNavigation { get; set; }

        public virtual Maintenance MaintenanceNavigation { get; set; }

        public virtual Customer CustomerNavigation { get; set; }


    }
}
