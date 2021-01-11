﻿
namespace Sparkle.LinkedInNET
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Sparkle.LinkedInNET.OAuth2;
    using Sparkle.LinkedInNET.Internals;

    /// <summary>
    /// LinkedIn API client.
    /// </summary>
    public partial class LinkedInApi
    {
        private readonly LinkedInApiConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinkedInApi"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <exception cref="System.ArgumentNullException">configuration</exception>
        public LinkedInApi(LinkedInApiConfiguration configuration)
            : base(null)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            this.configuration = configuration.Clone();
        }

        /// <summary>
        /// Gets the OAuth2 API.
        /// </summary>
        public OAuth2Api OAuth2
        {
            get { return new OAuth2Api(this); }
        }

        internal LinkedInApiConfiguration Configuration
        {
            get { return this.configuration; }
        }

        /// <summary>
        /// Executes a custom GET API query.
        /// </summary>
        /// <param name="path">The URL path and query (ex: /v1/companies?whatnot=stuff).</param>
        /// <returns>The resulting JSON response.</returns>
        public string RawGetJsonQuery(string path)
        {
            return this.RawGetJsonQuery(path, null);
        }

        /// <summary>
        /// Executes a custom GET API query.
        /// </summary>
        /// <param name="path">The URL path and query (ex: /v1/companies?whatnot=stuff).</param>
        /// <param name="user">The user access token.</param>
        /// <returns>The resulting JSON response.</returns>
        public string RawGetJsonQuery(string path, UserAuthorization user)
        {
            var context = new RequestContext();
            context.UserAuthorization = user;
            context.Method = "GET";

            if (path.StartsWith("https:") || path.StartsWith("http:"))
            {
                context.UrlPath = path;
            }
            else
            {
                context.UrlPath = this.Configuration.BaseApiUrl + path;
            }

            if (!this.ExecuteQuery(context))
                this.HandleJsonErrorResponse(context);

            var result = this.HandleJsonRawResponse(context);
            return result;
        }

        /// <summary>
        /// Executes a custom POST API query.
        /// </summary>
        /// <param name="path">The URL path and query (ex: /v1/companies?whatnot=stuff).</param>
        /// <param name="content">The JSON content to POST.</param>
        /// <returns>The resulting JSON response.</returns>
        public string RawPostJsonQuery(string path, string content)
        {
            return this.RawPostJsonQuery(path, content, null);
        }

        /// <summary>
        /// Executes a custom POST API query.
        /// </summary>
        /// <param name="path">The URL path and query (ex: /v1/companies?whatnot=stuff).</param>
        /// <param name="user">The user access token.</param>
        /// <param name="content">The JSON content to POST.</param>
        /// <returns>The resulting JSON response.</returns>
        public string RawPostJsonQuery(string path, string content, UserAuthorization user)
        {
            var context = new RequestContext();
            context.UserAuthorization = user;
            context.Method = "POST";

            if (path.StartsWith("https:") || path.StartsWith("http:"))
            {
                context.UrlPath = path;
            }
            else
            {
                context.UrlPath = this.Configuration.BaseApiUrl + path;
            }

            this.CreateJsonPostStream(context, content);

            if (!this.ExecuteQuery(context))
                this.HandleJsonErrorResponse(context);

            var result = this.HandleJsonRawResponse(context);
            return result;
        }

        /// <summary>
        /// Executes a custom query. You may need to read the library's source codes to understand how that works.
        /// </summary>
        /// <param name="context">HTTP request description</param>
        /// <param name="responseHandling">One of JSON / XML / RAW</param>
        public string RawQuery(RequestContext context, string responseHandling)
        {
            if (context == null)
                throw new ArgumentNullException("context");
            if (string.IsNullOrEmpty(context.UrlPath))
                throw new ArgumentException("The UrlPath cannot be empty", "context.UrlPath");

            if (context.UrlPath.StartsWith("https:") || context.UrlPath.StartsWith("http:"))
            {
                // let it go
            }
            else
            {
                context.UrlPath = this.Configuration.BaseApiUrl + context.UrlPath;
            }

            if (!this.ExecuteQuery(context))
                this.HandleJsonErrorResponse(context);

            if ("JSON".Equals(responseHandling, StringComparison.OrdinalIgnoreCase))
            {
                var result = this.HandleJsonRawResponse(context);
                return result;
            }
            else if ("XML".Equals(responseHandling, StringComparison.OrdinalIgnoreCase))
            {
                var result = this.HandleXmlRawResponse(context);
                return result;
            }
            else
            {
                var result = this.HandleRawResponse(context, Encoding.UTF8);
                return result;
            }
        }
    }
}
