using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DatabaseLab.Models
{
    //private const string RegularExpression = @"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$";
    public partial class Customer
    {
        public Customer()
        {
            GeneralTables = new HashSet<GeneralTable>();
        }

        [Required(ErrorMessage = "Zorunlu alan")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Zorunlu alan")]
        //[RegularExpression(@"[A-z^şŞıİçÇöÖüÜĞğ\s]*", ErrorMessage = "Geçersiz metin girişi")]
      //  [Required(ErrorMessage = "{0} alanı gereklidir.")]
        //[RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "{0} alanı sadece büyük ve küçük harflerden oluşabilir.")]
        public string? CName { get; set; }


        [Required(ErrorMessage = "Zorunlu alan")]
      //  [RegularExpression(@"[A-z^şŞıİçÇöÖüÜĞğ\s]*", ErrorMessage = "Geçersiz metin girişi")]
        public string? CLastName { get; set; }


        //[Required(ErrorMessage = "{0} alanı gereklidir.")]
        //[Phone(ErrorMessage = "Geçersiz {0} girdiniz.")]
        //[MinLength(11, ErrorMessage = "{0} 11 basamaklı olmalıdır."), MaxLength(10, ErrorMessage = "{0} 11 basamaklı olmalıdır.")]
        //[Required(ErrorMessage = "Zorunlu alan")]
        //[Display(Name = "Telefon Numarası")]
        [Required]
        [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
        public double? CNumber { get; set; }




      // [Required(ErrorMessage = "Zorunlu alan")]
        public string? Cinsiyet { get; set; }



        [ReadOnly(true)]
        public DateTime? Date { get; set; }

        // public virtual Vehicle VehicIdFkNavigation { get; set; }

        public virtual ICollection<GeneralTable> GeneralTables { get; set; }

    }
}
