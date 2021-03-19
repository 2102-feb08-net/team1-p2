using LooseLeaf.Business.IRepositories;
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
        public async Task<IActionResult> GetAllLoans()
        {
            throw new NotImplementedException();
        }

        [HttpPost("api/loans")]
        public async Task<IActionResult> SubmitLoanRequest(DTOs.LoanRequest req)
        {
            throw new NotImplementedException();
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