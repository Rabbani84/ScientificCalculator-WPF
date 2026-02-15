using System;
using System.Globalization;

namespace Rechner.Core
{
    public enum BinaryOp
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }

    public enum UnaryOp
    {
        Square,
        Sqrt,
        Reciprocal,
        Percent,
        Log10,
        Ln,
        Pow10,
        Exp,
        SinDeg,
        CosDeg,
        TanDeg,
        CotDeg
    }

    public sealed class CalcResult
    {
        public bool Ok { get; }
        public string? Error { get; }
        public double Value { get; }
        public string Expression { get; }

        private CalcResult(bool ok, double value, string expression, string? error)
        {
            Ok = ok;
            Value = value;
            Expression = expression;
            Error = error;
        }

        public static CalcResult Success(double value, string expression)
            => new CalcResult(true, value, expression, null);

        public static CalcResult Fail(string error, string expression = "")
            => new CalcResult(false, 0, expression, error);
    }

    public static class CalculatorEngine
    {
        // Parses numbers with current culture (German comma) and also accepts dot.
        public static bool TryParseNumber(string? input, out double value)
        {
            value = 0;

            if (string.IsNullOrWhiteSpace(input))
                return false;

            var s = input.Trim();

            // accept dot OR comma regardless of locale
            s = s.Replace(',', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0])
                 .Replace('.', CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0]);

            return double.TryParse(s, NumberStyles.Float, CultureInfo.CurrentCulture, out value);
        }

        public static CalcResult Evaluate(BinaryOp op, double a, double b)
        {
            switch (op)
            {
                case BinaryOp.Add:
                    return CalcResult.Success(a + b, $"{a} + {b} =");
                case BinaryOp.Subtract:
                    return CalcResult.Success(a - b, $"{a} - {b} =");
                case BinaryOp.Multiply:
                    return CalcResult.Success(a * b, $"{a} x {b} =");
                case BinaryOp.Divide:
                    if (b == 0) return CalcResult.Fail("Teilen durch 0 nicht möglich!", $"{a} ÷ {b} =");
                    return CalcResult.Success(a / b, $"{a} ÷ {b} =");
                default:
                    return CalcResult.Fail("Unbekannte Operation");
            }
        }

        public static CalcResult Evaluate(UnaryOp op, double x)
        {
            switch (op)
            {
                case UnaryOp.Square:
                    return CalcResult.Success(Math.Pow(x, 2), $"sqr({x}) =");
                case UnaryOp.Sqrt:
                    if (x >= 0) return CalcResult.Success(Math.Sqrt(x), $"√({x}) =");
                    // keep your idea of imaginary root, but do it clean:
                    return CalcResult.Success(Math.Sqrt(Math.Abs(x)), $"√({x}) = (imag)");
                case UnaryOp.Reciprocal:
                    if (x == 0) return CalcResult.Fail("Teilen durch 0 nicht möglich!", $"1/({x}) =");
                    return CalcResult.Success(1 / x, $"1/({x}) =");
                case UnaryOp.Percent:
                    return CalcResult.Success(x / 100.0, $"{x} % =");
                case UnaryOp.Log10:
                    if (x <= 0) return CalcResult.Fail("Ungültige Eingabe (log nur für x > 0).", $"log({x}) =");
                    return CalcResult.Success(Math.Log10(x), $"log({x}) =");
                case UnaryOp.Ln:
                    if (x <= 0) return CalcResult.Fail("Ungültige Eingabe (ln nur für x > 0).", $"ln({x}) =");
                    return CalcResult.Success(Math.Log(x), $"ln({x}) =");
                case UnaryOp.Pow10:
                    return CalcResult.Success(Math.Pow(10, x), $"10^({x}) =");
                case UnaryOp.Exp:
                    return CalcResult.Success(Math.Exp(x), $"e^({x}) =");

                case UnaryOp.SinDeg:
                    return CalcResult.Success(Math.Sin(ToRad(x)), $"sin({x}) =");
                case UnaryOp.CosDeg:
                    return CalcResult.Success(Math.Cos(ToRad(x)), $"cos({x}) =");
                case UnaryOp.TanDeg:
                    return CalcResult.Success(Math.Tan(ToRad(x)), $"tan({x}) =");
                case UnaryOp.CotDeg:
                    var t = Math.Tan(ToRad(x));
                    if (t == 0) return CalcResult.Fail("Ungültige Eingabe (cot nicht definiert).", $"cot({x}) =");
                    return CalcResult.Success(1 / t, $"cot({x}) =");

                default:
                    return CalcResult.Fail("Unbekannte Operation");
            }
        }

        private static double ToRad(double degrees) => degrees * (Math.PI / 180.0);

        public static string FormatResult(double value)
        {
            if (double.IsNaN(value) || double.IsInfinity(value))
                return "Error";

            // keep it readable for UI (adjust decimals later if you want)
            return value.ToString(CultureInfo.CurrentCulture);
        }
    }
}