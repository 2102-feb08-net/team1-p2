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
        [HttpGet("api/loans")]
        public IActionResult GetAllLoans()
        {
            return Ok();
        }

        [HttpPost("api/loans")]
        public IActionResult SubmitLoanRequest(InterfaceModels.LoanRequest req)
        {
            return Ok();
        }

        [HttpGet("api/loans/{loanId}")]
        public IActionResult GetLoanById(int loanId)
        {
            return Ok();
        }

        [HttpPatch("api/loans/{loanId}")]
        public IActionResult UpdateLoanRequestStatus(int loanId, InterfaceModels.LoanRequest req)
        {
            return Ok();
        }
    }
}
