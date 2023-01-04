using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLab.Models
{
    public partial class Vehicle
    {

        
        public Vehicle()
        {
            //   Customers = new HashSet<Customer>();
            //  Students = new HashSet<Student>();
            GeneralTables = new HashSet<GeneralTable>();
            Maintenances = new HashSet<Maintenance>();
        }

        [Required(ErrorMessage = "Zorunlu alan")]
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public DateTime? Date { get; set; }

       // [Required(ErrorMessage = "Zorunlu alan")]
        public int? CustomerId { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public string? Plaka { get; set; }

        [NotMapped]
        public string FullName
        {
            get { return "id = " +this.VehicleId + " | " + this.Model + " | " + this.Plaka ; }
        }


        // public virtual ICollection<Customer> Customers { get; set; }
        public virtual ICollection<Maintenance> Maintenances { get; set; }

        public virtual ICollection<GeneralTable> GeneralTables { get; set; }
    }
}
