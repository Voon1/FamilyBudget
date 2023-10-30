using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Domain.Entities;
/*
    Decode - Holds dictionary definitions
 */
public class BudgetCategoryType
{
    public int BudgetCategoryTypeId { get; set; }

    [Required]
    [StringLength(100)]
    public string BudgetCategoryTypeName { get; set; }
}
