﻿
using Juhta.Net.WebApi.Exceptions.ClientErrors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Net;

namespace Juhta.Net.WebApi.Exceptions.Tests.ClientErrors
{
    [TestClass]
    public class LengthRequiredExceptionTests : WebApiExceptionTests
    {
        #region Test Methods

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException1_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException();
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException1_ShouldReturn",
                    null,
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.LengthRequiredException' was thrown.",
                    HttpStatusCode.LengthRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException2_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber);
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException2_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "Exception of type 'Juhta.Net.WebApi.Exceptions.ClientErrors.LengthRequiredException' was thrown.",
                    HttpStatusCode.LengthRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException3_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException("LengthRequiredException Specified order number is invalid.");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException3_ShouldReturn",
                    null,
                    "LengthRequiredException Specified order number is invalid.",
                    HttpStatusCode.LengthRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        [TestMethod]
        public void ThrowAndSerialize_LengthRequiredException4_ShouldReturn()
        {
            ClientError clientError1, clientError2;
            LengthRequiredException exception1 = null, exception2 = null;

            try
            {
                throw new LengthRequiredException(ErrorCode.InvalidOrderNumber, "LengthRequiredException Specified order number is invalid.");
            }

            catch (LengthRequiredException ex)
            {
                AssertException(
                    ex,
                    "ThrowAndSerialize_LengthRequiredException4_ShouldReturn",
                    ErrorCode.InvalidOrderNumber.ToString(),
                    "LengthRequiredException Specified order number is invalid.",
                    HttpStatusCode.LengthRequired
                );

                clientError1 = ex.ToClientError();

                exception1 = ex;
            }

            clientError2 = JsonConvert.DeserializeObject<ClientError>(JsonConvert.SerializeObject(clientError1));

            try
            {
                clientError2.Throw();
            }

            catch (LengthRequiredException ex)
            {
                exception2 = ex;
            }

            AssertExceptions(exception1, exception2);
        }

        #endregion
    }
}
