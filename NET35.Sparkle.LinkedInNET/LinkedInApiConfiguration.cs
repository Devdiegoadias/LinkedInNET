﻿
namespace Sparkle.LinkedInNET
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Configuration object for LinkedIn API.
    /// </summary>
    public class LinkedInApiConfiguration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedInApiConfiguration"/> class.
        /// </summary>
        public LinkedInApiConfiguration()
        {
            this.BaseApiUrl = "https://api.linkedin.com";
            this.BaseOAuthUrl = "https://www.linkedin.com";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedInApiConfiguration"/> class.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        /// <param name="apiSecretKey">The API secret key.</param>
        public LinkedInApiConfiguration(string apiKey, string apiSecretKey)
            : this()
        {
            this.ApiKey = apiKey;
            this.ApiSecretKey = apiSecretKey;
        }

        /// <summary>
        /// Gets or sets the base API URL.
        /// </summary>
        public string BaseApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the base automatic authentication URL.
        /// </summary>
        public string BaseOAuthUrl { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the API secret key.
        /// </summary>
        public string ApiSecretKey { get; set; }

#if !ASYNCTASKS
        /// <summary>
        /// Creates a configuration object froms the application settings.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <returns></returns>
        [Obsolete("This method will be removed in future builds.")]
        public static LinkedInApiConfiguration FromAppSettings(string prefix)
        {
            if (prefix != null)
            {
                if (!prefix.EndsWith("."))
                    prefix += ".";
            }

            var me = new LinkedInApiConfiguration();
            me.ApiKey = ConfigurationManager.AppSettings[prefix + "ApiKey"];
            me.ApiSecretKey = ConfigurationManager.AppSettings[prefix + "ApiSecretKey"];

            return me;
        }
#endif

        internal LinkedInApiConfiguration Clone()
        {
            return new LinkedInApiConfiguration
            {
                BaseApiUrl = this.BaseApiUrl,
                BaseOAuthUrl = this.BaseOAuthUrl,
                ApiKey = this.ApiKey,
                ApiSecretKey = this.ApiSecretKey,
            };
        }
    }
}
