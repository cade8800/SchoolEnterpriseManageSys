using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolEnterpriseManageSys.Utilities.ExtensionHelper
{
    public static class MathExtension
    {
        public static double StandardDeviation(this IList<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Count == 0)
            {
                return double.NaN;
            }

            double variance = source.Variance();

            return Math.Sqrt(variance);
        }

        public static double SampleStandardDeviation(this IList<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Count == 0 || source.Count == 1)
            {
                return double.NaN;
            }

            double variance = source.SampleVariance();

            return Math.Sqrt(variance);
        }

        public static double Variance(this IList<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Count == 0)
            {
                return double.NaN;
            }

            int count = source.Count();
            double deviation = CalculateDeviation(source, count);

            return deviation / count;
        }

        public static double SampleVariance(this IList<double> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source"); ;
            }

            if (source.Count == 0 || source.Count == 1)
            {
                return double.NaN;
            }

            int count = source.Count();
            double deviation = CalculateDeviation(source, count);

            return deviation / (count - 1);
        }

        public static double WeightedAverage(this IList<double> source, IList<double> factors)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (source.Count != factors.Count)
            {
                throw new ArgumentException("source count is not equal to factors count.");
            }

            if (source.Count == 0)
            {
                return double.NaN;
            }

            double sum = factors.Sum();

            if (sum == 0)
            {
                return double.NaN;
            }

            double weight = 0;

            for (int index = 0; index < factors.Count; index++)
            {
                weight += source[index] * (factors[index] / sum);
            }

            return weight;
        }

        public static double AverageSafe(this IList<double> source) 
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (source.Count == 0)
            {
                return 0;
            }
            return source.Average();
        }

        private static double CalculateDeviation(IList<double> source, int count)
        {
            double avg = source.Average();
            double deviation = 0;

            for (int index = 0; index < count; index++)
            {
                deviation += (source[index] - avg) * (source[index] - avg);
            }

            return deviation;
        }
    }
}
