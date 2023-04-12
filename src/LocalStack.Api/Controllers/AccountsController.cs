using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using LocalStack.Api.Contracts.Models;
using LocalStack.Api.Contracts.Requests;
using LocalStack.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LocalStack.Api.Controllers
{
    /// <summary>
    /// Manage Accounts
    /// </summary>
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsService _accountsService;

        /// <summary>
        /// Manage Accounts
        /// </summary>
        /// <param name="accountsService">Accounts Service</param>
        public AccountsController(IAccountsService accountsService)
        {
            _accountsService = accountsService;
        }

        /// <summary>
        /// Create new Account
        /// </summary>
        /// <param name="request">Create Account request</param>
        /// <returns>New <see cref="AccountV1"/></returns>
        /// <response code="201">Account created</response>
        /// <response code="400">Request is not valid</response>
        [HttpPost]
        [ProducesResponseType(typeof(AccountV1), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(CreateAccountRequestV1 request)
        {
            var account = new AccountV1
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                CreatedOn = DateTime.UtcNow
            };

            await this._accountsService.SaveAsync(account);

            return CreatedAtAction(nameof(GetById), new { id = account.Id }, account);
        }

        /// <summary>
        /// Get Account by ID
        /// </summary>
        /// <param name="id">Account ID</param>
        /// <returns><see cref="AccountV1"/></returns>
        /// <response code="200">Account returned</response>
        /// <response code="404">Account not found</response>
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AccountV1), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await this._accountsService.GetById(id);

            if (account == null)
            {
                return NotFound();
            }

            return Ok(account);
        }

        /// <summary>
        /// Get all Accounts
        /// </summary>
        /// <returns><see cref="List{AccountV1}"/></returns>
        /// <response code="200">Accounts returned</response>
        [HttpGet]
        [ProducesResponseType(typeof(List<AccountV1>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await this._accountsService.GetAll();

            return Ok(accounts);
        }
    }
}
