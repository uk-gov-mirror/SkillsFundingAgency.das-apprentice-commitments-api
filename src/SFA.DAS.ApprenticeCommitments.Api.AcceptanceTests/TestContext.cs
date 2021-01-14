using System;
using System.Net.Http;

namespace SFA.DAS.ApprenticeCommitments.Api.AcceptanceTests
{
    public class TestContext : IDisposable
    {
        private bool isDisposed;

        public TestContext()
        {
            isDisposed = false;
        }
        public HttpClient Api { get; set; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed) return;

            if (disposing)
            {
                Api?.Dispose();
            }

            isDisposed = true;
        }
    }
}