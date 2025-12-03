using System.ComponentModel.DataAnnotations.Schema;

namespace GoBorderless.Data;

public class SampleTable
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}