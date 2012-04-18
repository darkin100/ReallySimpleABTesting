using System;
using System.Collections.Generic;

namespace Website.Models
{
    /// <summary>
    ///  http://20bits.com/article/statistical-analysis-and-ab-testing
    /// </summary>
    public class ConfidenceCalculator
    {
        private readonly double _visitors;
        private readonly double _conversions;

        public ConfidenceCalculator(double visitors, double conversions)
        {
            _visitors = visitors;
            _conversions = conversions;
            Tests = new List<ConfidenceRow>();
        }

        public double ControlVisitors
        {
            get { return _visitors; }
        }
        public double ControlConversions
        {
            get { return _conversions; }
        }
        public double ControlConversionRate
        {
            get { return (ControlConversions / ControlVisitors) * 100; }
        }

        public IList<ConfidenceRow> Tests{get;set;}

        public void AddTest(double visited, double converted)
        {
            var row = new ConfidenceRow()
                                    {
                                        Visitors   = visited,
                                        Conversions = converted,
                                        ZScore = CalculateZscore(visited,converted),
                                        Confidence = CalculateConfidence(CalculateZscore(visited, converted))
                                    };
            Tests.Add(row);
        }

        /// <summary>
        /// http://www.johndcook.com/python_phi.html
        /// </summary>
        /// <param name="zScore"></param>
        /// <returns></returns>
        private static double CalculateConfidence(double zScore)
        {
            //constants
            var a1 = 0.254829592;
            var a2 = -0.284496736;
            var a3 = 1.421413741;
            var a4 = -1.453152027;
            var a5 = 1.061405429;
            var p = 0.3275911;

            var sign = 1;

            if (zScore < 0)
            {
                sign = -1;
            }

            var x = Math.Abs(zScore)/Math.Sqrt(2);

            var t = 1.0/(1.0 + p*x);
            var y = 1.0 - (((((a5*t + a4)*t) + a3)*t + a2)*t + a1)*t*Math.Exp((-x*x));

            return 0.5*(1.0 + sign*y);
        }


        public double CalculateZscore(double visited, double converted)
        {

            var pc = _conversions / _visitors;//Control conversion rate
            var nc = _visitors;

            var p = (converted / visited);
            var n = visited;

            var z = (p - pc);
            var a = (p * (1 - p)) / n;
            var b = (pc * (1 - pc)) / nc;

            var res = z / Math.Sqrt(a + b);


            return res;

        }
    }




    public class ConfidenceRow
    {
        public double Visitors { get; set; }
        public double Conversions { get; set; }
        public double Rate
        {
            get { return (Conversions/Visitors) * 100; }
        }

        public double ZScore { get; set; }

        public double Confidence { get; set; }
    }
}