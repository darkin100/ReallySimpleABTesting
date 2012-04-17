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

        public double ControlVisitors { get; set; }
        public double ControlConversions { get; set; }

        public IList<ConfidenceRow> Tests{get;set;}

        public void AddTest(double visited, double converted)
        {
            var row = new ConfidenceRow()
                                    {
                                        Visitors   = visited,
                                        Conversions = converted,
                                        ZScore = CalculateZscore(visited,converted)
                                    };
            Tests.Add(row);
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

        
    }
}