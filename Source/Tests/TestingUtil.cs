﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PayPal.Api;

namespace PayPal.Testing
{
    public class TestingUtil
    {
        public static Dictionary<string, string> GetConfig()
        {
            return new Dictionary<string, string>
            {
                { BaseConstants.ClientId, "AWdYxxA6pvcbjhb51A4BUosqeNv8u6mlFX7CZ_d-D6WakmFVRGeG5X43FJql" },
                { BaseConstants.ClientSecret, "EFCQHhChJbtb4bjvQO593E3-shFeIXLh-a0Sxp7luqvqwzlOG10ueTly2EHf" },
                { BaseConstants.ApplicationModeConfig, BaseConstants.SandboxMode },
                { "connectionTimeout", "360000" },
                { "requestRetries", "1" }
            };
        }

        private static string GetAccessToken()
        {
            var oauth = new OAuthTokenCredential(GetConfig());
            return oauth.GetAccessToken();
        }

        public static PayPal.Api.APIContext GetApiContext()
        {
            var apiContext = new PayPal.Api.APIContext(GetAccessToken());
            apiContext.Config = GetConfig();
            return apiContext;
        }

        /// <summary>
        /// Invokes the specified action and checks whether or not the specified exception type is thrown. This allows for unit tests that more accurately
        /// capture when specific exceptions should be thrown.
        /// </summary>
        /// <typeparam name="T">The type of exception that is expected to be thrown from the given action.</typeparam>
        /// <param name="action">The action to be invoked.</param>
        public static void AssertThrownException<T>(Action action)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception ex)
            {
                if (typeof(T).Equals(ex.GetType()))
                {
                    return;
                }
                Assert.Fail("Expected " + typeof(T) + " to be thrown, but " + ex.GetType() + " was thrown instead.");
            }
            Assert.Fail("Expected " + typeof(T) + " to be thrown, but no exception was thrown.");
        }
    }
}
