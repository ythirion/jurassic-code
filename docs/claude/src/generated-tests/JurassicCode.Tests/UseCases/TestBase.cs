using System;
using JurassicCode.Db2;
using Xunit;

namespace JurassicCode.Tests.UseCases
{
    public abstract class TestBase : IDisposable
    {
        protected readonly ParkService ParkService;

        protected TestBase()
        {
            // Initialize a fresh database for each test
            DataAccessLayer.Init(new Database());
            ParkService = new ParkService();
        }

        public void Dispose()
        {
            // Clean up after test if needed
        }
    }
}