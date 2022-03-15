using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace zoo.Models{
    [Table("Vrt")]
    public class Vrt
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [StringLength(100)] 
        [Required(ErrorMessage="Neophodno je uneti naziv aviokompanije!")]
        [Column("Naziv")]
        public string Naziv{get;set;}

        
        [Required(ErrorMessage="Neophodno je uneti n aviokompanije!")]
        [Column("n")]
        public int N{get;set;}
        
        [Required(ErrorMessage="Neophodno je uneti m aviokompanije!")]
        [Column("m")]
        public int M{get;set;}
        
        [Required(ErrorMessage="Neophodno je uneti kapacitet aviokompanije!")]
        [Column("kapacitet")]
        public int Kapacitet{get;set;}
        
        public List<Lokacija> Lokacije{get;set;}

    public Vrt()
    {
        this.Lokacije=new List<Lokacija>();
    }
    }
}