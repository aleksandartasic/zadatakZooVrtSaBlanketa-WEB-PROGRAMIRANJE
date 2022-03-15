using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace zoo.Models{
    [Table("Lokacija")]
    public class Lokacija
    {
        [Key]
        [Column("ID")]    
        public int ID{get;set;}

        [Column("X")]
        public int X{get;set;}

        [Column("Y")]
        public int Y{get;set;}

        [Column("Kapacitet")]
        public int Kapacitet{get;set;}
        
        [Column("MaxKapacitet")]
        public int MaxKapacitet{get;set;}

        [Column("Tip")]
        public string Tip{get;set;}

        [Column("Vrsta")]
        public string Vrsta{get;set;}

        [JsonIgnore]     ////sluzi da spreci serijalizaciju by puflovic
        public virtual Vrt Vrt{get;set;}/////////////!!!!!!!!!!!!!!!!!!!
      /*  
        public List<DestinacijaAvion> DestinacijaAvion{get;set;}
       */
        public Lokacija()
        {
            
        }
    }
}