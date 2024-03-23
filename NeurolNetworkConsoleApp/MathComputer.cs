namespace NeurolNetworkConsoleApp
{
    static class MathComputer
    { 
        static public double GetSigmoid(double x)
        {
            return Convert.ToDouble(1 / (1 + Math.Pow(Math.E, -x )));
        }

        static public double GetSigmoid_dx(double x)
        {
            double sigmoidX = GetSigmoid(x);

            double result = sigmoidX * (1 - sigmoidX);


            return result;
        }
    }
}
