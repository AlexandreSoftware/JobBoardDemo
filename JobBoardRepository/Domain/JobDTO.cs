using System.ComponentModel.DataAnnotations;
using ExpressiveAnnotations.Attributes;
using Microsoft.VisualBasic;

namespace JobBoardRepository.Domain;

public class JobDTO
{
    [Key]
    [Display(Name="Id")]
    public int ProductId;
    [Required]
    [Display(Name="Title")]
    [StringLength(100,ErrorMessage = "Title Must not Be Empty")]
    public string Title;
    [Display(Name = "SubTitle")]
    public string SubTitle;
    [Required]
    [Range(1,Int32.MaxValue,ErrorMessage ="Value Must be greater than 1")]
    public double MinPay;
    [AssertThat("MaxPay > MinPay")]
    public double MaxPay;
    [Required]
    [StringLength(900,ErrorMessage = "Description must not be empty")]
    public string Description;
}