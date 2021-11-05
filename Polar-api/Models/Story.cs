using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Story {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "text")]
    public string Text { get; set; }

    [Column(TypeName = "nvarchar(255)")]
    public string Image { get; set; } // Default is set to null in Context
    /* EF CORE Relations */
    public int MarkerId { get; set; } // Foreign key
    public Marker Marker { get; set; }

}