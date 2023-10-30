using System.ComponentModel.DataAnnotations;

namespace FamilyBudget.Domain.Entities;
/*
    Decode - Holds dictionary definitions
 */
public class BudgetCategoryType
{
    public int BudgetCategoryTypeId { get; set; }

  
    public string BudgetCategoryTypeName { get; set; }
}
