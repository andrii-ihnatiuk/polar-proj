using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class User {
    public int Id { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Login { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Password { get; set; }

    [Required]
    [Column(TypeName = "nvarchar(255)")]
    public string Username { get; set; }
    
    public int Score { get; set; }
    /* EF CORE Relations */
    public ICollection<Marker> Markers { get; set; }

}