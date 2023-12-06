using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using NuGet.Configuration;

namespace BudgetApp.Areas.Identity.Data;

// Add profile data for application users by adding properties to the BudgetAppUser class
public class BudgetAppUser : IdentityUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? MiddleName { get; set; }
    public string Email { get; set; }
    public string PhoneNum { get; set; }
    public string PostalCode { get; set; }
    public decimal Budget { get; set; }
    public ICollection<Transaction> Transactions { get; set; }

}


