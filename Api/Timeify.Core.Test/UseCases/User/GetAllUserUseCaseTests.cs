using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using Bogus;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Entities;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.UseCases.User;

namespace Timeify.Core.Test.UseCases.User
{
    [TestFixture]
    public class GetAllUserUseCaseTests
    {
        private IUserRepository subUserRepository;

        [SetUp]
        public void SetUp()
        {
            this.subUserRepository = Substitute.For<IUserRepository>();

        }

        private GetAllUserUseCase CreateGetAllUserUseCase()
        {
            return new GetAllUserUseCase(
                this.subUserRepository);
        }

        [Test]
        public async Task GetAllUser_WhenTwoUsersAreInDatabase_ShoudReturnTwoUsers()
        {
            // Arrange
            var unitUnderTest = this.CreateGetAllUserUseCase();
            GetAllUserRequest message = new GetAllUserRequest();
            var entities = new Faker<UserEntity>().Generate(2);
            subUserRepository.ListAll().Returns(entities);

            IOutputPort<GetAllUserResponse> outputPort = 
                Substitute.For<IOutputPort<GetAllUserResponse>>();

            // Act
            var result = await unitUnderTest.Handle(
                message,
                outputPort);

            // Assert
            Assert.True(result);
                outputPort.Received(1).Handle(
                    Arg.Is<GetAllUserResponse>(x => x.Users.Count == 2));
        }
    }
}
