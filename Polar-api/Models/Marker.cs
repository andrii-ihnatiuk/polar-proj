using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Marker {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string QrCode { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(25)")]
    public string Type { get; set; }
    /* EF CORE Relations */
    public Story Story { get; set; }
    public ICollection<User> Users { get; set; }
    public int? LocationId { get; set; } // Foreign key
    public Location Location { get; set; }

}