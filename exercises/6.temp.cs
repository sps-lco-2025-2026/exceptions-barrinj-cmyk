class TemperatureException : Exception
{
     public double AttemptedValue { get; }
    public TemperatureException(double attemptedValue) : base("Temperature is below absolute zero.")
    {
        AttemptedValue = attemptedValue;
    }

    public TemperatureException(double attemptedValue, string message) : base(message)
    {
        AttemptedValue = attemptedValue;
    }

    public TemperatureException(double attemptedValue, string message, Exception inner) : base(message, inner)
    {
        AttemptedValue = attemptedValue;
    }
}


class Program
{
    const double AbsoluteZero = -273.15;
    static double CelsiusToFahrenheit(double celsius)
    {
        if (celsius < AbsoluteZero) throw new TemperatureException(celsius);
        return celsius * 9.0 / 5.0 + 32;
    }

    static void Main()
    {
        string[] inputs = { "100", "-273.15", "-300", "abc", "0", "-274" };

        foreach (string input in inputs)
        {
            Console.Write($"Input {input}: ");
            try
            {
                double celsius = double.Parse(input);
                double fahrenheit = CelsiusToFahrenheit(celsius);
                Console.WriteLine($"{celsius} °C = {fahrenheit} °F");
            }
            catch (TemperatureException ex)
            {
                Console.WriteLine($"{ex.AttemptedValue} is below absolute zero (minimum: {AbsoluteZero})");
            }
            catch (FormatException)
            {
                Console.WriteLine($"[FormatException] {input} is not a valid number.");
            }
        }
    }
}