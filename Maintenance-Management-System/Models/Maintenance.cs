using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseLab.Models
{
    public partial class Maintenance
    {
        public Maintenance()
        {
           
            GeneralTables = new HashSet<GeneralTable>();
           
        }

        [ForeignKey("StudentIdFkNavigation")]

       
        public int VehicleId { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public double? Bakım { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public double? Bakım2 { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        public double? Bakım3 { get; set; }

        public double? TotalCost { get; set; }

        // public int StudentIdFk { get; set; }
        //  [Range(1, 8)]

        //public bool IsChech { get; set; }


        public double totalC
        {
            
            get { return (double)(this.Bakım + this.Bakım2 + this.Bakım3) ; }
        }

        
        public virtual Vehicle StudentIdFkNavigation { get; set; }

        public virtual ICollection<GeneralTable> GeneralTables { get; set; }
    }
    /*
    [Keyless]
    public class ProductModel
    {
        public List<Maintenance> Maintenances { get; set; }
    }
    */
}
