using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

public class Location {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Name { get; set; }
    
    public int NumberOfMarkers { get; set; }
    /* EF CORE Relations */
    public ICollection<Marker> Markers { get; set; }
}