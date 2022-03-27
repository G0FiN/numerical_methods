using System;
using System.Collections.Generic;

namespace Laba1 {
    class Program {

        public static void Main() {            
            Console.Write("Interval: ");
            string[] input = Console.ReadLine().Split();

            double leftLimit = double.Parse(input[0]);
            double rightLimit = double.Parse(input[1]);

            List<double> results = Limits(leftLimit, rightLimit);
            results = SecantsAlgoritm(results);

            foreach (var i in results) Console.WriteLine(i);
            Console.WriteLine("[END]");
        }

        public static double CulcEquation(double x) => x * x - 2 * x - 4;
        const double mathDX = 0.05;

        public static List<double> Limits(double leftLimit, double rightLimit) {
            double lengthOfValues = (rightLimit - leftLimit) / 40;
            List<double> result = new List<double>();

            double x1 = 0;
            double x2 = leftLimit;
            double y1 = 0;
            double aditional_y1 = 0;
            double y2 = CulcEquation(leftLimit);
            double aditional_y2 = (CulcEquation(leftLimit + mathDX) + y2) / mathDX;

            for (int i = 1; i < 41; i++) {
                x1 = x2;
                x2 = leftLimit + lengthOfValues * i;
                y1 = y2;
                y2 = CulcEquation(x2);
                aditional_y1 = aditional_y2;
                aditional_y2 = (CulcEquation(leftLimit + mathDX) + y2) / mathDX;
                if (y1 * y2 < 0 || (aditional_y1 * aditional_y2 < 0)) {
                    List<double> additionalLimits = AdditionalLimits(x1, x2);
                    foreach (var item in additionalLimits) result.Add(item);                    
                }
            }

            return result;
        }

        public static List<double> AdditionalLimits(double leftLimit, double rightLimit) {
            double lengthOfValues = (rightLimit - leftLimit) / 10;
            List<double> result = new List<double>();
            
            double x1 = 0;
            double x2 = leftLimit;
            double y1 = 0;
            double aditional_y1 = 0;
            double y2 = CulcEquation(leftLimit);
            double aditional_y2 = (CulcEquation(leftLimit + mathDX) + y2) / mathDX;

            for (int i = 1; i < 10; i++) {
                x1 = x2;
                x2 = leftLimit + lengthOfValues * i;
                y1 = y2;
                y2 = CulcEquation(x2);
                aditional_y1 = aditional_y2;
                aditional_y2 = CulcEquation(leftLimit + mathDX + y2) / mathDX;

                if (y1 * y2 < 0) {
                    result.Add(x1);
                    result.Add(x2);
                }
            }

            return result;
        }

        public static List<double> SecantsAlgoritm(List<double> data)
        {
            List<double> result = new List<double>();

            for (int i = 0; i < data.Count / 2; i++) {
                double aditional_x = 0;
                double x1 = data[i * 2 + 1];
                double y1 = CulcEquation(x1);
                double x2 = (data[i * 2] - x1) / 2 + data[i * 2];
                double y2 = CulcEquation(x2);

                while (y1 * y2 < 0) {
                    x2 -= (x2-x1)/2;
                    y2 = CulcEquation(x2);
                }
                
                while (true) {                    
                    aditional_x = x2 - y2 * (x2 - x1) / (y2 - y1);
                    double checker = CulcEquation(aditional_x);
                    if (checker < 0.01) {
                        break;
                    } else {
                        x1 = x2;
                        x2 = aditional_x;
                        y1 = y2;
                        y2 = CulcEquation(x2);
                    }
                }
                
                result.Add(aditional_x);
            }

            return result;
        }
    }
}
