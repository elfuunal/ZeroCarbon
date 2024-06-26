﻿using NeyeTech.ZeroCarbon.Core.DataAccess.EntityFramework;
using NeyeTech.ZeroCarbon.Core.Entities.Concrete;
using NeyeTech.ZeroCarbon.DataAccess.Abstract;
using NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;

namespace NeyeTech.ZeroCarbon.DataAccess.Concrete.EntityFramework
{
    public class UserRepository : EntityRepositoryBase<User, ProjectDbContext>, IUserRepository
    {
        public UserRepository(ProjectDbContext context)
            : base(context)
        {
        }

        public List<OperationClaim> GetClaims(long userId)
        {
            var result = (from user in Context.Users
                          join userGroup in Context.UserGroups on user.Id equals userGroup.UserId
                          join groupClaim in Context.GroupClaims on userGroup.GroupId equals groupClaim.GroupId
                          join operationClaim in Context.OperationClaims on groupClaim.OperationClaimId equals operationClaim.Id
                          where user.Id == userId
                          select new
                          {
                              operationClaim.Name
                          });

            return result.Select(x => new OperationClaim { Name = x.Name }).Distinct()
                .ToList();
        }

        public async Task<User> GetByRefreshToken(string refreshToken)
        {
            return await Context.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken && u.Status);
        }
    }
}
