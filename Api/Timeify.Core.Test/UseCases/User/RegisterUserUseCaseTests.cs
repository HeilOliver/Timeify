using System.Linq;
using NSubstitute;
using NUnit.Framework;
using System.Threading.Tasks;
using Castle.Components.DictionaryAdapter;
using Timeify.Core.Dto;
using Timeify.Core.Dto.GatewayResponses;
using Timeify.Core.Dto.UseCaseRequests;
using Timeify.Core.Dto.UseCaseResponse;
using Timeify.Core.Interfaces;
using Timeify.Core.Interfaces.Gateways;
using Timeify.Core.UseCases.User;

namespace Timeify.Core.Test.UseCases.User
{
    [TestFixture]
    public class RegisterUserUseCaseTests
    {
        private IUserRepository subUserRepository;

        [SetUp]
        public void SetUp()
        {
            this.subUserRepository = Substitute.For<IUserRepository>();
        }

        private RegisterUserUseCase CreateRegisterUserUseCase()
        {
            return new RegisterUserUseCase(
                this.subUserRepository);
        }

        [Test]
        public async Task Handle_WhereUserCanCreated_ExpectSuccessResultWithId()
        {
            // Arrange
            var unitUnderTest = this.CreateRegisterUserUseCase();
            string id = "123";
            RegisterUserRequest message = new RegisterUserRequest("First", "Last", "abc@blob.con", "Sample", "Test");
            subUserRepository
                .Create(default, default, default, default, default)
                .ReturnsForAnyArgs(new CreateUserResponse(id, true));
            IOutputPort<RegisterUserResponse> outputPort = Substitute.For<IOutputPort<RegisterUserResponse>>();

            // Act
            bool result = await unitUnderTest.Handle(
                message,
                outputPort);

            // Assert
            Assert.True(result);
            outputPort
                .Received(1)
                .Handle(Arg.Is<RegisterUserResponse>(x => x.Success));

            outputPort
                .Received(1)
                .Handle(Arg.Is<RegisterUserResponse>(x => x.Id.Equals(id)));
        }

        [Test]
        public async Task Handle_WhereUserCanNotCreated_ExpectFailedResultWithoutId()
        {
            // Arrange
            var unitUnderTest = this.CreateRegisterUserUseCase();
            RegisterUserRequest message = new RegisterUserRequest("First", "Last", "abc@blob.con", "Sample", "Test");
            subUserRepository
                .Create(default, default, default, default, default)
                .ReturnsForAnyArgs(new CreateUserResponse(default, false, new []{new ApplicationError("Fail", ""), }));
            IOutputPort<RegisterUserResponse> outputPort = Substitute.For<IOutputPort<RegisterUserResponse>>();

            // Act
            bool result = await unitUnderTest.Handle(
                message,
                outputPort);

            // Assert
            Assert.False(result);
            outputPort
                .Received(1)
                .Handle(Arg.Is<RegisterUserResponse>(x => !x.Success));

            outputPort
                .Received(1)
                .Handle(Arg.Is<RegisterUserResponse>(x => string.IsNullOrEmpty(x.Id)));

            outputPort
                .Received(1)
                .Handle(Arg.Is<RegisterUserResponse>(x => x.Errors.Any()));
        }
    }
}
