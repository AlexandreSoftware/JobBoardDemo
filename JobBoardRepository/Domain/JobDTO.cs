using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpressiveAnnotations.Attributes;
using Microsoft.VisualBasic;

namespace JobBoardRepository.Domain;

[Table("Job")]
public class JobDTO
{
    [Key]
    [Display(Name = "Id")]
    public int ProductId { get; set; }
    [Required]
    [Display(Name="Title")]
    [StringLength(100,ErrorMessage = "Title Must not Be Empty")]
    public string Title{ get; set; }
    [Display(Name = "SubTitle")]
    public string SubTitle{ get; set; }
    [Required]
    [Range(1,Int32.MaxValue,ErrorMessage ="Value Must be greater than 1")]
    public double MinPay{ get; set; }
    [AssertThat("MaxPay > MinPay")]
    public double MaxPay{ get; set; }
    [Required]
    [StringLength(900,ErrorMessage = "Description must not be empty")]
    public string Description{ get; set; }
}