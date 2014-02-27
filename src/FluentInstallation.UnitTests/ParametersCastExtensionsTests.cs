using System;
using System.Collections;
using Xunit;

namespace FluentInstallation
{
    public class ParametersCastExtensionsTests
    {
        [Fact]
        public void Cast_GivesParametersWhenSuppliedUsingCorrectCasing()
        {
            var parameters = new Hashtable {{"SiteName", "MySite"}};

            var sut = parameters.Cast<InstallerContextTests.SingleStringParameter>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void Cast_GivesParametersWhenSuppliedUsingAnyCasing()
        {
            var parameters = new Hashtable {{"sitename", "MySite"}};

            var sut = parameters.Cast<InstallerContextTests.SingleStringParameter>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void Cast_IgnoresExtraNotRequiredParameters()
        {
            var parameters = new Hashtable {{"SiteName", "MySite"}, {"ApplicationPoolName", "AppPool1"}};

            var sut = parameters.Cast<InstallerContextTests.SingleStringParameter>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void Cast_ConvertsIntParamCorrectly()
        {
            var parameters = new Hashtable {{"Port", "80"}};

            var sut = parameters.Cast<SingleIntParameter>();

            Assert.Equal(80, sut.Port);
        }

        [Fact]
        public void Cast_ThrowsWhenTypeNotCompatibleWithInt()
        {
            var parameters = new Hashtable {{"Port", "blah"}};

            Exception sut = Catch.Exception(() => parameters.Cast<SingleIntParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Int32 for parameter name Port", sut.Message);
        }

        [Fact]
        public void Cast_ConvertsShortParamCorrectly()
        {
            var parameters = new Hashtable {{"Port", "80"}};

            var sut = parameters.Cast<SingleShortParameter>();

            Assert.Equal(80, sut.Port);
        }

        [Fact]
        public void Cast_ThrowsWhenTypeNotCompatibleWithShort()
        {
            var parameters = new Hashtable {{"Port", "blah"}};

            Exception sut = Catch.Exception(() => parameters.Cast<SingleShortParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Int16 for parameter name Port", sut.Message);
        }

        [Fact]
        public void Cast_ConvertsLongParamCorrectly()
        {
            var parameters = new Hashtable {{"Port", "80"}};

            var sut = parameters.Cast<SingleLongParameter>();

            Assert.Equal(80, sut.Port);
        }

        [Fact]
        public void Cast_ThrowsWhenTypeNotCompatibleWithLong()
        {
            var parameters = new Hashtable {{"Port", "blah"}};

            Exception sut = Catch.Exception(() => parameters.Cast<SingleLongParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Int64 for parameter name Port", sut.Message);
        }

        [Fact]
        public void Cast_ConvertsDoubleParamCorrectly()
        {
            var parameters = new Hashtable {{"NetFramework", "4.5"}};

            var sut = parameters.Cast<SingleDoubleParameter>();

            Assert.Equal(4.5D, sut.NetFramework);
        }

        [Fact]
        public void Cast_ThrowsWhenTypeNotCompatibleWithDouble()
        {
            var parameters = new Hashtable {{"NetFramework", "blah"}};

            Exception sut = Catch.Exception(() => parameters.Cast<SingleDoubleParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Double for parameter name NetFramework", sut.Message);
        }

        [Fact]
        public void Cast_ConvertsFloatParamCorrectly()
        {
            var parameters = new Hashtable {{"NetFramework", "4.5"}};

            var sut = parameters.Cast<SingleFloatParameter>();

            Assert.Equal(4.5f, sut.NetFramework);
        }

        [Fact]
        public void Cast_ThrowsWhenTypeNotCompatibleWithFloat()
        {
            var parameters = new Hashtable {{"NetFramework", "blah"}};

            Exception sut = Catch.Exception(() => parameters.Cast<SingleFloatParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Single for parameter name NetFramework", sut.Message);
        }

        [Fact]
        public void Cast_ConvertsDecimalParamCorrectly()
        {
            var parameters = new Hashtable {{"NetFramework", "4.5"}};
            var sut = parameters.Cast<SingleDecimalParameter>();

            Assert.Equal(4.5M, sut.NetFramework);
        }

        [Fact]
        public void Cast_ThrowsWhenTypeNotCompatibleWithDecimal()
        {
            var parameters = new Hashtable {{"NetFramework", "blah"}};

            Exception sut = Catch.Exception(() => parameters.Cast<SingleDecimalParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Decimal for parameter name NetFramework", sut.Message);
        }

        [Fact]
        public void Cast_SetsAllParameters()
        {
            var parameters = new Hashtable {{"Port", "80"}, {"SiteName", "MySite"}, {"AppPoolName", "AppPool1"}};

            var sut = parameters.Cast<MultipleParameters>();

            Assert.Equal(80, sut.Port);
            Assert.Equal("MySite", sut.SiteName);
            Assert.Equal("AppPool1", sut.AppPoolName);
        }

        [Fact]
        public void Cast_DoesNotThrowWhenNotRequiredParamNotGiven()
        {
            var parameters = new Hashtable {{"SiteName", "MySite"}};
            var sut = parameters.Cast<RequiredParameters>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void Cast_ThrowsWhenRequiredParameterIsNotGiven()
        {
            var parameters = new Hashtable {{"Port", "80"}};

            Exception sut = Catch.Exception(() => parameters.Cast<RequiredParameters>());

            Assert.IsType<RequiredParameterNotGivenException>(sut);
            Assert.Equal("Parameter SiteName is required please specify it.", sut.Message);
        }


        [Fact]
        public void Cast_OnlySetsWritableProperties()
        {
            var parameters = new Hashtable { { "Port", 80 } };
            var sut = parameters.Cast<ReadonlyParameter>();

            Assert.Equal(90, sut.Port);
        }

        [Fact]
        public void Cast_CastsParametersWithDotNotation()
        {
            var parameters = new Hashtable { { "Octopus.Machine.Name", "some machine name" } };
            var sut = parameters.Cast<DotNotationParameter>();

            Assert.Equal("some machine name", sut.OctopusMachineName);
        }

        public class MultipleParameters
        {
            public int Port { get; set; }
            public string SiteName { get; set; }
            public string AppPoolName { get; set; }
        }

        public class RequiredParameters
        {
            public int Port { get; set; }

            [Required]
            public string SiteName { get; set; }
        }

        public class SingleDecimalParameter
        {
            public decimal NetFramework { get; set; }
        }

        public class SingleDoubleParameter
        {
            public double NetFramework { get; set; }
        }

        public class SingleFloatParameter
        {
            public float NetFramework { get; set; }
        }

        public class SingleIntParameter
        {
            public int Port { get; set; }
        }

        public class SingleLongParameter
        {
            public long Port { get; set; }
        }

        public class SingleShortParameter
        {
            public short Port { get; set; }
        }

        public class ReadonlyParameter
        {
            public short Port { get { return 90; } }
        }

        public class DotNotationParameter
        {
            public string OctopusMachineName { get; set; }
        }
    }
}