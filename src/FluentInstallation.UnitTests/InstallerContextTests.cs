using System.Collections;
using NSubstitute;
using Xunit;

namespace FluentInstallation
{
    
    public class InstallerContextTests
    {
        public class SingleStringParameter
        {
            public string SiteName { get; set; }
        }

        [Fact]
        public void GetParameters_GivesParametersWhenSuppliedUsingCorrectCasing()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "SiteName", "MySite" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleStringParameter>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void GetParameters_GivesParametersWhenSuppliedUsingAnyCasing()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "sitename", "MySite" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleStringParameter>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void GetParameters_IgnoresExtraNotRequiredParameters()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "SiteName", "MySite" }, {"ApplicationPoolName", "AppPool1"} });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleStringParameter>();

            Assert.Equal("MySite", sut.SiteName);
        }

        public class SingleIntParameter
        {
            public int Port { get; set; }            
        }

        [Fact]
        public void GetParameters_ConvertsIntParamCorrectly()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "80" }});
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleIntParameter>();

            Assert.Equal(80, sut.Port);
        }

        [Fact]
        public void GetParameters_ThrowsWhenTypeNotCompatibleWithInt()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "blah" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = Catch.Exception(() => installerContext.GetParameters<SingleIntParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Int32 for parameter name Port", sut.Message);
        }

        public class SingleShortParameter
        {
            public short Port { get; set; }
        }

        [Fact]
        public void GetParameters_ConvertsShortParamCorrectly()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "80" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleShortParameter>();

            Assert.Equal(80, sut.Port);
        }

        [Fact]
        public void GetParameters_ThrowsWhenTypeNotCompatibleWithShort()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "blah" } });
            var installerContext = new InstallerContext { Command = command };
            
            var sut = Catch.Exception(() => installerContext.GetParameters<SingleShortParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Int16 for parameter name Port", sut.Message);
        }

        public class SingleLongParameter
        {
            public long Port { get; set; }
        }

        [Fact]
        public void GetParameters_ConvertsLongParamCorrectly()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "80" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleLongParameter>();

            Assert.Equal(80, sut.Port);
        }

        [Fact]
        public void GetParameters_ThrowsWhenTypeNotCompatibleWithLong()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "blah" } });
            var installerContext = new InstallerContext { Command = command };
            
            var sut = Catch.Exception(() => installerContext.GetParameters<SingleLongParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Int64 for parameter name Port", sut.Message);
        }

        public class SingleDoubleParameter
        {
            public double NetFramework { get; set; }
        }

        [Fact]
        public void GetParameters_ConvertsDoubleParamCorrectly()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "NetFramework", "4.5" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleDoubleParameter>();

            Assert.Equal(4.5D, sut.NetFramework);
        }

        [Fact]
        public void GetParameters_ThrowsWhenTypeNotCompatibleWithDouble()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "NetFramework", "blah" } });
            var installerContext = new InstallerContext { Command = command };

            
            var sut = Catch.Exception(() => installerContext.GetParameters<SingleDoubleParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Double for parameter name NetFramework", sut.Message);
        }

        public class SingleFloatParameter
        {
            public float NetFramework { get; set; }
        }

        [Fact]
        public void GetParameters_ConvertsFloatParamCorrectly()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "NetFramework", "4.5" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleFloatParameter>();

            Assert.Equal(4.5f, sut.NetFramework);
        }

        [Fact]
        public void GetParameters_ThrowsWhenTypeNotCompatibleWithFloat()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "NetFramework", "blah" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = Catch.Exception(() => installerContext.GetParameters<SingleFloatParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Single for parameter name NetFramework", sut.Message);
        }

        public class SingleDecimalParameter
        {
            public decimal NetFramework { get; set; }
        }

        [Fact]
        public void GetParameters_ConvertsDecimalParamCorrectly()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "NetFramework", "4.5" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<SingleDecimalParameter>();

            Assert.Equal(4.5M, sut.NetFramework);
        }

        [Fact]
        public void GetParameters_ThrowsWhenTypeNotCompatibleWithDecimal()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "NetFramework", "blah" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = Catch.Exception(() => installerContext.GetParameters<SingleDecimalParameter>());

            Assert.IsType<ParameterCastException>(sut);
            Assert.Equal("Cannot cast to type System.Decimal for parameter name NetFramework", sut.Message);
        }

        public class MultipleParameters
        {
            public int Port { get; set; }
            public string SiteName { get; set; }
            public string AppPoolName { get; set; }
        }

        [Fact]
        public void GetParameters_SetsAllParameters()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "80"}, {"SiteName", "MySite"}, {"AppPoolName", "AppPool1"}  });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<MultipleParameters>();

            Assert.Equal(80, sut.Port);
            Assert.Equal("MySite", sut.SiteName);
            Assert.Equal("AppPool1", sut.AppPoolName);
        }

        public class RequiredParameters
        {
            public int Port { get; set; }

            [Required]
            public string SiteName { get; set; }
        }

        [Fact]
        public void GetParameters_DoesNotThrowWhenNotRequiredParamNotGiven()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "SiteName", "MySite" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = installerContext.GetParameters<RequiredParameters>();

            Assert.Equal("MySite", sut.SiteName);
        }

        [Fact]
        public void GetParameters_ThrowsWhenRequiredParameterIsNotGiven()
        {
            var command = Substitute.For<ICommand>();
            command.Parameters.Returns(new Hashtable { { "Port", "80" } });
            var installerContext = new InstallerContext { Command = command };

            var sut = Catch.Exception(() => installerContext.GetParameters<RequiredParameters>());

            Assert.IsType<RequiredParameterNotGivenException>(sut);
            Assert.Equal("Parameter SiteName is required please specify it.", sut.Message);

        }
    }
}
