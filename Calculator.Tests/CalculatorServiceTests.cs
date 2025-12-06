using CalculatorApp.Services;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorServiceTests
    {
        private ICalculatorService _service = null!;

        [SetUp]
        public void Setup()
        {
            _service = new CalculatorService();
        }

        [Test]
        public void Add_ReturnsCorrectSum()
        {
            Assert.That(_service.Add(4, 5), Is.EqualTo(9.0).Within(1e-9));
            Assert.That(_service.Add(-5, 3), Is.EqualTo(-2.0).Within(1e-9));
            Assert.That(_service.Add(2.5, -2.5), Is.EqualTo(0.0).Within(1e-9));
        }

        [Test]
        public void Add_WithLargeNumbers_ReturnsCorrectSum()
        {
            Assert.That(_service.Add(1000000, 2000000), Is.EqualTo(3000000.0).Within(1e-9));
        }

        [Test]
        public void Add_WithDecimals_ReturnsCorrectSum()
        {
            Assert.That(_service.Add(0.1, 0.2), Is.EqualTo(0.3).Within(1e-9));
        }

        [Test]
        public void Subtract_ReturnsCorrectDifference()
        {
            Assert.That(_service.Subtract(6, 4), Is.EqualTo(2.0).Within(1e-9));
            Assert.That(_service.Subtract(-5, 3), Is.EqualTo(-8.0).Within(1e-9));
            Assert.That(_service.Subtract(2.5, -2.5), Is.EqualTo(5.0).Within(1e-9));
        }

        [Test]
        public void Subtract_WithSameNumbers_ReturnsZero()
        {
            Assert.That(_service.Subtract(5, 5), Is.EqualTo(0.0).Within(1e-9));
        }

        [Test]
        public void Multiply_ReturnsCorrectProduct()
        {
            Assert.That(_service.Multiply(6, 4), Is.EqualTo(24.0).Within(1e-9));
            Assert.That(_service.Multiply(-5, 3), Is.EqualTo(-15.0).Within(1e-9));
            Assert.That(_service.Multiply(2.5, -2.5), Is.EqualTo(-6.25).Within(1e-9));
        }

        [Test]
        public void Multiply_WithZero_ReturnsZero()
        {
            Assert.That(_service.Multiply(100, 0), Is.EqualTo(0.0).Within(1e-9));
            Assert.That(_service.Multiply(0, 100), Is.EqualTo(0.0).Within(1e-9));
        }

        [Test]
        public void Multiply_WithOne_ReturnsSameNumber()
        {
            Assert.That(_service.Multiply(42, 1), Is.EqualTo(42.0).Within(1e-9));
        }

        [Test]
        public void Divide_ReturnsCorrectQuotient()
        {
            Assert.That(_service.Divide(6, 4), Is.EqualTo(1.5).Within(1e-9));
            Assert.That(_service.Divide(-15, 3), Is.EqualTo(-5.0).Within(1e-9));
            Assert.That(_service.Divide(2.5, -2.5), Is.EqualTo(-1.0).Within(1e-9));
        }

        [Test]
        public void Divide_WithSameNumber_ReturnsOne()
        {
            Assert.That(_service.Divide(5, 5), Is.EqualTo(1.0).Within(1e-9));
        }

        [Test]
        public void Divide_ByZero_ThrowsDivideByZeroException()
        {
            var ex = Assert.Throws<DivideByZeroException>(() => _service.Divide(5, 0));
            Assert.That(ex?.Message, Is.EqualTo("Cannot divide by zero"));
        }

        [Test]
        public void Divide_ZeroByNumber_ReturnsZero()
        {
            Assert.That(_service.Divide(0, 5), Is.EqualTo(0.0).Within(1e-9));
        }

        [TestCase(10, 5, 15)]
        [TestCase(-10, -5, -15)]
        [TestCase(0, 0, 0)]
        [TestCase(100.5, 50.5, 151)]
        public void Add_WithTestCases_ReturnsExpectedResult(double a, double b, double expected)
        {
            Assert.That(_service.Add(a, b), Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(10, 5, 5)]
        [TestCase(-10, -5, -5)]
        [TestCase(0, 5, -5)]
        public void Subtract_WithTestCases_ReturnsExpectedResult(double a, double b, double expected)
        {
            Assert.That(_service.Subtract(a, b), Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(10, 5, 50)]
        [TestCase(-10, -5, 50)]
        [TestCase(10, 0, 0)]
        public void Multiply_WithTestCases_ReturnsExpectedResult(double a, double b, double expected)
        {
            Assert.That(_service.Multiply(a, b), Is.EqualTo(expected).Within(1e-9));
        }

        [TestCase(10, 5, 2)]
        [TestCase(-10, -5, 2)]
        [TestCase(0, 5, 0)]
        public void Divide_WithTestCases_ReturnsExpectedResult(double a, double b, double expected)
        {
            Assert.That(_service.Divide(a, b), Is.EqualTo(expected).Within(1e-9));
        }
    }
}