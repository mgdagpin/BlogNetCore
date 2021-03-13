﻿using Arcturus.Application.Common.Extensions;
using Arcturus.Commands.UsersCmds;
using Arcturus.Domain.Entities;
using Arcturus.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using TasqR;

namespace Arcturus.Application.Handlers.Commands.UserCmds
{
    public class NewUserCmdHandler : TasqHandlerAsync<NewUserCmd, User>
    {
        private readonly IArcturusDbContext dbContext;
        private readonly DbContext dbContextWriter;

        public NewUserCmdHandler(IArcturusDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbContextWriter = dbContext as DbContext;
        }

        public override Task<User> RunAsync(NewUserCmd request, CancellationToken cancellationToken = default)
        {
            User newUser = new User
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                MiddleName = request.MiddleName,
                Gender = request.Gender
            };

            dbContext.Users.AsDbSet().Add(newUser);

            return dbContextWriter.SaveChangesAsync().ContinueWith(a =>
            {
                return newUser;
            });
        }

        public class NewUserCmdValidator : AbstractValidator<NewUserCmd>
        {
            public NewUserCmdValidator()
            {
                RuleFor(a => a.FirstName)
                    .NotNull()
                    .MaximumLength(250);

                RuleFor(a => a.LastName)
                    .NotNull()
                    .MaximumLength(250);

                RuleFor(a => a.Gender)
                    .NotNull();
            }
        }
    }
}
