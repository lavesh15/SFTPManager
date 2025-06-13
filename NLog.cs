[TestFixture]
public class NLogLearningTests
{
    private Logger _logger;

    [SetUp]
    public void Setup()
    {
        LogManager.Configuration = new LoggingConfiguration();
        _logger = LogManager.GetLogger("TestLogger");
    }

    [Test]
    public void BasicLoggerProducesNothing()
    {
        // Learning: Just getting a logger doesn't enable logging
        _logger.Info("hello"); // No output - no targets configured!
        // This test documents that NLog needs explicit configuration
    }

    [Test]
    public void ConsoleTargetNeedsLayout()
    {
        // Learning: Console target needs layout configuration
        var target = new ConsoleTarget();
        var config = new LoggingConfiguration();
        config.AddTarget("console", target);
        config.AddRuleForAllLevels(target);
        LogManager.Configuration = config;
        
        _logger.Info("hello"); // Still no output - no layout!
    }

    [Test]
    public void WorkingConsoleLogger()
    {
        // Learning: This combination works
        var target = new ConsoleTarget()
        {
            Layout = "${level:uppercase=true} ${message}"
        };
        
        var config = new LoggingConfiguration();
        config.AddTarget("console", target);
        config.AddRuleForAllLevels(target);
        LogManager.Configuration = config;
        
        _logger.Info("hello"); // Success! Shows: "INFO hello"
    }

    [Test]
    public void SimpleConsoleConfiguration()
    {
        // Learning: NLog.config or programmatic simple setup
        var config = new LoggingConfiguration();
        var consoleTarget = new ConsoleTarget("logconsole")
        {
            Layout = "${level} ${message} ${exception}"
        };
        config.AddRule(LogLevel.Info, LogLevel.Fatal, consoleTarget);
        LogManager.Configuration = config;
        
        _logger.Info("Simple setup works!");
    }
}

// Encapsulation after learning NLog complexity
public class SimpleNLogWrapper 
{
    private readonly Logger _logger;
    
    public SimpleNLogWrapper(string loggerName)
    {
        // Hide NLog configuration complexity
        if (LogManager.Configuration?.AllTargets?.Count == 0)
        {
            var config = new LoggingConfiguration();
            var consoleTarget = new ConsoleTarget("console")
            {
                Layout = "${time} [${level:uppercase=true}] ${message}"
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, consoleTarget);
            LogManager.Configuration = config;
        }
        
        _logger = LogManager.GetLogger(loggerName);
    }
    
    public void Info(string message) => _logger.Info(message);
    public void Error(string message) => _logger.Error(message);
    public void Debug(string message) => _logger.Debug(message);
}
