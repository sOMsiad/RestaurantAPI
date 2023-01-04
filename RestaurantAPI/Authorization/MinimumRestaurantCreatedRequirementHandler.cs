using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using RestaurantAPI.Entities;
using RestaurantAPI.Services;

namespace RestaurantAPI.Authorization
{
    public class MinimumRestaurantCreatedRequirementHandler : AuthorizationHandler<MinimumRestaurantCreatedRequirement>
    {
        private readonly RestaurantDbContext _dbContext;
        private readonly ILogger<MinimumAgeRequirementHandler> _logger;
        private readonly IUserContextService _userContextService;

        public MinimumRestaurantCreatedRequirementHandler(RestaurantDbContext dbContext, ILogger<MinimumAgeRequirementHandler> logger, IUserContextService userContextService)
        {
            _dbContext = dbContext;
            _logger = logger;
            _userContextService = userContextService;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, MinimumRestaurantCreatedRequirement requirement)
        {
            //var amountRestaurantCreatedByUser =
            var amountRestaurantCreatedByUser = _dbContext
                .Restaurants
                .Count(r => r.CreatedById == _userContextService.GetUserId);
            if (amountRestaurantCreatedByUser >= requirement.MinimumRestaurantCreted)
            {
                _logger.LogInformation("Authorization succedded");
                context.Succeed(requirement);
            }
            else
            {
                _logger.LogInformation("Authorization failed");
            }
            return Task.CompletedTask;
        }
    }
}
