using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using System;
using BudgetApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BudgetApp.Areas.Identity.Pages
{
    public class AddTransactionModel : PageModel
    {
        private readonly DBContext _context;
        private readonly UserManager<BudgetAppUser> _userManager;

        public AddTransactionModel(DBContext context, UserManager<BudgetAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public Transaction Transaction { get; set; }
		public List<SelectListItem> Categories => GetCategories();

		public List<SelectListItem> GetCategories()
		{
			var categories = new List<SelectListItem>
	        {
				new SelectListItem { Value = "Grocery", Text = "Grocery" },
			    new SelectListItem { Value = "Rent", Text = "Rent" },
			    new SelectListItem { Value = "Utilities", Text = "Utilities" },
			    new SelectListItem { Value = "Dining", Text = "Dining" },
			    new SelectListItem { Value = "Shopping", Text = "Shopping" },
			    new SelectListItem { Value = "Entertainment", Text = "Entertainment" },
			    new SelectListItem { Value = "Health", Text = "Health" },
			    new SelectListItem { Value = "Transportation", Text = "Transportation" },
			    new SelectListItem { Value = "Other", Text = "Other" },
			};

			return categories;
		}


		public class InputModel
        {
            [Required]
            [Display(Name = "Amount")]
            public decimal Amount { get; set; }

            [Required]
            [Display(Name = "Category")]
            public string Category { get; set; }

            [Required]
            [Display(Name = "Description")]
            public string Description { get; set; }

			[Required]
			[Display(Name = "Date")]
            public DateTime Date { get; set; }
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }

                return Page();
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            } 

            Transaction.Id = user.Id;
            Transaction.Amount = Input.Amount;
            Transaction.Category = Input.Category;
            Transaction.Description = Input.Description;
            Transaction.Date = Input.Date;
            Transaction.User = user;



			_context.Transactions.Add(Transaction);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Transactions");
        }
    }

}
