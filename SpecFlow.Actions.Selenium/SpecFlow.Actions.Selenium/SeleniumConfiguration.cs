﻿using SpecFlow.Actions.Configuration;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpecFlow.Actions.Selenium
{
    public interface ISeleniumConfiguration
    {
        Browser Browser { get; }

        string[]? Arguments { get; }

        Dictionary<string, string>? Capabilities { get; }

        double? DefaultTimeout { get; }

        double? PollingInterval { get; }

        string? TestPlatform { get; }
    }

    public class SeleniumConfiguration : ISeleniumConfiguration
    {
        private readonly ISpecFlowActionJsonLoader _specFlowActionJsonLoader;

        private class SpecFlowActionJson
        {
            [JsonInclude]
            public SeleniumSpecFlowJsonPart Selenium { get; private set; } = new SeleniumSpecFlowJsonPart();
        }

        private class SeleniumSpecFlowJsonPart
        {
            [JsonInclude]
            public Browser Browser { get; private set; }

            [JsonInclude]
            public string[]? Arguments { get; private set; }

            [JsonInclude]
            public Dictionary<string, string>? Capabilities { get; private set; }

            [JsonInclude]
            public double? DefaultTimeout { get; private set; }

            [JsonInclude]
            public double? PollingInterval { get; private set; }

            [JsonInclude]
            public string? TestPlatform { get; private set; }
        }

        private readonly Lazy<SpecFlowActionJson> _specflowJsonPart;

        private SpecFlowActionJson LoadSpecFlowJson()
        {
            var json = _specFlowActionJsonLoader.Load();

            if (string.IsNullOrWhiteSpace(json))
            {
                return new SpecFlowActionJson();
            }

            var jsonSerializerOptions = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            jsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

            var specflowActionConfig = JsonSerializer.Deserialize<SpecFlowActionJson>(json, jsonSerializerOptions);

            return specflowActionConfig ?? new SpecFlowActionJson();
        }

        /// <summary>
        /// Provides the configuration details for the webdriver instance
        /// </summary>
        /// <param name="specFlowActionJsonLoader"></param>
        public SeleniumConfiguration(ISpecFlowActionJsonLoader specFlowActionJsonLoader)
        {
            _specFlowActionJsonLoader = specFlowActionJsonLoader;
            _specflowJsonPart = new Lazy<SpecFlowActionJson>(LoadSpecFlowJson);
        }

        /// <summary>
        /// The browser specified in the configuration
        /// </summary>
        public Browser Browser => _specflowJsonPart.Value.Selenium.Browser; 

        /// <summary>
        /// Arguments used to configure the webdriver
        /// </summary>
        public string[]? Arguments => _specflowJsonPart.Value.Selenium.Arguments;

        /// <summary>
        /// Capabilities used to configure the webdriver
        /// </summary>
        public Dictionary<string, string>? Capabilities => _specflowJsonPart.Value.Selenium.Capabilities;

        /// <summary>
        /// The default timeout used to configure the webdriver
        /// </summary>
        public double? DefaultTimeout => _specflowJsonPart.Value.Selenium.DefaultTimeout;

        /// <summary>
        /// The default polling interval used to configure the webdriver
        /// </summary>
        public double? PollingInterval => _specflowJsonPart.Value.Selenium.PollingInterval;

        /// <summary>
        /// The test platform to execute against
        /// </summary>
        public string TestPlatform => _specflowJsonPart.Value.Selenium.TestPlatform ?? "local";
    }
}