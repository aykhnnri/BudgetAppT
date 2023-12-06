using BudgetApp.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetApp.Areas.Identity.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly DBContext _context;
        private readonly UserManager<BudgetAppUser> _userManager;

        public TransactionsModel(DBContext context, UserManager<BudgetAppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<Transaction> AllTransactions { get; set; }
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

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return RedirectToPage("/Login");
            }

            AllTransactions = await _context.Transactions
                                            .Where(t => t.Id == user.Id)
                                            .OrderByDescending(t => t.Date)
                                            .ToListAsync();

            return Page();
        }

        public IActionResult OnGetEdit(int id)
        {
            var transaction = _context.Transactions.Find(id);

            if (transaction == null)
            {
                return NotFound();
            }

            ViewData["TransactionId"] = transaction.TransactionId;
            ViewData["Date"] = transaction.Date.ToShortDateString();
            ViewData["Amount"] = transaction.Amount.ToString("C");
            ViewData["Category"] = transaction.Category;
            ViewData["Description"] = transaction.Description;

            return Page();
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

        public IActionResult OnPostEdit(int transactionId)
        {
            var transaction = _context.Transactions.Find(transactionId);

            if (transaction == null)
            {
                return Page();
            }

            ViewData["TransactionId"] = transactionId;
            ViewData["Amount"] = transaction.Amount;
            ViewData["Category"] = transaction.Category;
            ViewData["Description"] = transaction.Description;
            ViewData["Date"] = transaction.Date.ToString("yyyy-MM-dd");

            return Page();
        }

        public IActionResult OnPostSaveEdit(int transactionId, InputModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var transaction = _context.Transactions.Find(transactionId);

            if (transaction == null)
            {
                return Page();
            }

            transaction.Amount = inputModel.Amount;
            transaction.Category = inputModel.Category;
            transaction.Description = inputModel.Description;
            transaction.Date = inputModel.Date;

            _context.SaveChanges();

            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int transactionId)
        {
            var transaction = _context.Transactions.Find(transactionId);

            if (transaction == null)
            {

                return Page();
            }

            _context.Transactions.Remove(transaction);
            _context.SaveChanges();

            return RedirectToPage();
        }
    }


}
