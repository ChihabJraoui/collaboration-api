using System;
using System.Collections.Generic;
using System.Text;

using Core.Entities;
using Application.Workspace.Commands;
using FluentAssertions;
using Application.Common.Exceptions;
using NUnit.Framework;

namespace IntegrationTests.Workspace.Commands
{
    using static Testing;
    public class CreateWorkspaceTests :TestBase
    {
        [Test]
        public void ShoudRequireTheCommandProprieties()
        {
            //Arrage
            var command = new CreateWorkspaceCommand
            {
                Name="",
            };
               
            //Act

            //Assert
            FluentActions.Invoking(() => SendAsync(command))
                .Should().Throw<BusinessRuleException>();
        }
        
    }
}
