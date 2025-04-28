using System;
using Bogus;
using JurassicCode.DataAccess.Interfaces;
using NSubstitute;

namespace JurassicCode.Tests.ParkService
{
    /// <summary>
    /// Base class for all ParkService tests that provides shared setup logic
    /// </summary>
    public abstract class ParkServiceTestBase : IDisposable
    {
        protected readonly JurassicCode.ParkService ParkService;
        protected readonly IDataAccessLayer MockDataAccess;
        protected readonly Faker Faker;
        
        protected ParkServiceTestBase()
        {
            // Create a mock of IDataAccessLayer
            MockDataAccess = Substitute.For<IDataAccessLayer>();
            
            // Create ParkService with the mock and disable initialization
            ParkService = new JurassicCode.ParkService(MockDataAccess, false);

            // Initialize Bogus faker
            Faker = new Faker();
        }

        public void Dispose()
        {
            // Clean up resources if needed
        }
    }
}