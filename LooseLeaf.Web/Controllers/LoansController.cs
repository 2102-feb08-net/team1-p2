using LooseLeaf.Business.IRepositories;
using LooseLeaf.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LooseLeaf.Web.Controllers
{
    [ApiController]
    public class LoansController : ControllerBase
    {
        private readonly ILoanRepository _loanRepo;

        public LoansController(ILoanRepository loanRepo)
        {
            _loanRepo = loanRepo;
        }

        [HttpGet("api/loans")]
        [Authorize]
        public async Task<IActionResult> GetAllLoans(int? lender, int? borrower, int? book, int? ownedBook, int? loanStatus)
        {
            ILoanSearchParams searchParams = new LoanSearchParams { LenderId = lender, BorrowerId = borrower, BookId = book, OwnedBookId = ownedBook, LoanStatus = (LoanStatus?)loanStatus };
            IEnumerable<ILoanResult> loans = await _loanRepo.GetLoansAsync(searchParams);
            return Ok(loans);
        }

        [HttpPost("api/loans")]
        public async Task<IActionResult> SubmitLoanRequest(DTOs.LoanRequest req)
        {
            ILoan loan = new Loan(req.LenderId, req.BorrowerId, req.Message, req.StartDate, req.EndDate, req.AddressId, req.OwnedBookIds, LoanStatus.Requested);
            await _loanRepo.AddLoanAsync(loan);
            await _loanRepo.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("api/loans/{loanId}")]
        public async Task<IActionResult> GetLoanById(int loanId)
        {
            throw new NotImplementedException();
        }

        [HttpPatch("api/loans/{loanId}")]
        public async Task<IActionResult> UpdateLoanRequestStatus(int loanId, DTOs.LoanRequest req)
        {
            throw new NotImplementedException();
        }
    }
}