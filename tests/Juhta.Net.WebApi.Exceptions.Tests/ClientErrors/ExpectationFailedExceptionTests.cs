﻿
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class ExpectationFailedExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException();
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ExpectationFailedException' was thrown.",
                    HttpStatusCode.ExpectationFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber);
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.ExpectationFailedException' was thrown.",
                    HttpStatusCode.ExpectationFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException("ExpectationFailedException Specified order number is invalid.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException3_ShouldReturn",
                    null,
                    "ExpectationFailedException Specified order number is invalid.",
                    HttpStatusCode.ExpectationFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_ExpectationFailedException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            ExpectationFailedException exception1 = null, exception2 = null;

            try
            {
                throw new ExpectationFailedException(ErrorCode.InvalidOrderNumber, "ExpectationFailedException Specified order number is invalid.");
            }

            catch (ExpectationFailedException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_ExpectationFailedException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "ExpectationFailedException Specified order number is invalid.",
                    HttpStatusCode.ExpectationFailed
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (ExpectationFailedException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
