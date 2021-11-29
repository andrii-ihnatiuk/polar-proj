using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

public class User: IdentityUser {   
    public int Score { get; set; }
    /* EF CORE Relations */
    public ICollection<Marker> Markers { get; set; }

}