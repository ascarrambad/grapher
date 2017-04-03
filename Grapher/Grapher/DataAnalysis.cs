using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher
{

    static class DataAnalysis
    {

        // Module
        // double[,] data: [numero pacchetto, (x,y,z)]
        // return modarr[numero pacchetto]: array di moduli

        public static double[] ComputeModules(double[,] data)
        {
            int size = data.GetLength(0);

            double[] modarr = new double[size];

            for (int i = 0; i < size; i++) {
                modarr[i] = ComputeModule(data[i, 0], data[i, 1], data[i, 2]);
            }

            return modarr;
        }

        private static double ComputeModule(double x, double y, double z)
        {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        // Smoothing
        // return smoothed[pacchetto,sensore,tipo,(x,y,z,w)]

        public static List<Packet> SmoothData(List<Packet> sampwin, int range)
        {
            int size = sampwin.Count();
            int sensNum = sampwin.First().SensorsNumber;

            List<Packet> smoothed = new List<Packet>();

            for (int p = 0; p < size; p++) {
                Packet pa = new Packet() {
                    SensorsNumber = sensNum,
                    Sensors = new Sensor[sensNum]
                };
                smoothed.Add(pa);

                for (int s = 0; s < sensNum; s++) {
                    Sensor se = new Sensor() {
                        acc = new double[3],
                        gyr = new double[3],
                        mag = new double[3],
                        q = new double[4]
                    };
                    pa.Sensors[s] = se;

                    int sx = Math.Max(0, p - range);
                    int dx = Math.Min(size - 1, p + range);
                    double winSize = dx - sx + 1;
                    double[] accmean = new double[3];
                    double[] gyrmean = new double[3];
                    double[] magmean = new double[3];
                    double[] qmean = new double[4];

                    for (int m = sx; m <= dx; m++) {
                        for (int i = 0; i < 3; i++) {
                            accmean[i] += sampwin[m].Sensors[s].acc[i];
                            gyrmean[i] += sampwin[m].Sensors[s].gyr[i];
                            magmean[i] += sampwin[m].Sensors[s].mag[i];
                            qmean[i] += sampwin[m].Sensors[s].q[i];
                        }
                        qmean[3] += sampwin[m].Sensors[s].q[3];
                    }

                    for (int i = 0; i < 3; i++) {
                        pa.Sensors[s].acc[i] = accmean[i] / winSize;
                        pa.Sensors[s].gyr[i] = gyrmean[i] / winSize;
                        pa.Sensors[s].mag[i] = magmean[i] / winSize;
                        pa.Sensors[s].q[i] = qmean[i] / winSize;
                    }
                    pa.Sensors[s].q[3] = qmean[3] / winSize;
                }
            }

            return smoothed;
        }

        // Derivative
        // return modarr[pacchetto,sensore,tipo(0:acc,1:gyr)]

        public static double[] ComputeDerivatives(double[] mod, int freq)
        {
            int size = mod.Count();
            double[] derivated = new double[size];

            for (int i = 0; i < size - 1; i++) {
                derivated[i] = ComputeDerivative(freq, mod[i], mod[i + 1]);
            }
            return derivated;
        }

        private static double ComputeDerivative(int freq, double v1, double v2)
        {
            return (v1 - v2) / ((double)1 / freq);
        }

        // Standard Deviation

        public static double[] ComputeStandardDeviations(double[] data, int range)
        {
            int size = data.Count();
            double[] derivated = new double[size];

            for (int i = 0; i < size; i++) {

                int sx = Math.Max(0, i - range);
                int dx = Math.Min(size - 1, i + range);
                int winSize = dx - sx + 1;

                double mean = 0;

                for (int j = sx; j < dx; j++) {
                    mean += data[j];
                }
                mean /= winSize;

                double devStd = 0;

                for (int j = sx; j < dx; j++) {
                    devStd += Math.Pow(data[j] - mean, 2);
                }

                devStd /= winSize;

                derivated[i] = Math.Sqrt(devStd);
            }
            return derivated;
        }

        // Euler angles

        public static double[,] RemoveDiscontinuities(double[,] data)
        {
            int size = data.GetLength(0);
            int valueSize = data.GetLength(1);

            double[,] cont = new double[size, valueSize];

            for (int j = 0; j < valueSize; j++) {
                cont[0, j] = data[0, j];
            }

            int[] sfasam = new int[valueSize];
            for (int i = 1; i < size; i++) {
                for (int j = 0; j < valueSize; j++) {
                    double diff = data[i, j] - data[i - 1, j];
                    double absDiff = Math.Abs(diff);
                    if (absDiff > 2.5) { sfasam[j] += diff > 0 ? -1 : 1; }
                    cont[i, j] = data[i, j] + Math.PI * sfasam[j];
                }
            }

            return cont;
        }

        public static double[,] ComputeEulerAngles(double[,] data)
        {

            int size = data.GetLength(0);

            double[,] euler = new double[size, 3];

            for (int i = 0; i < size; i++) {
                double q0 = data[i, 0];
                double q1 = data[i, 1];
                double q2 = data[i, 2];
                double q3 = data[i, 3];

                double r32 = (2.0 * q2 * q3) + (2.0 * q0 * q1);
                double r33 = (2.0 * Math.Pow(q0, 2)) + (2.0 * Math.Pow(q3, 2)) - 1.0;

                double r31 = (2.0 * q1 * q3) - (2.0 * q0 * q2);

                double r21 = (2.0 * q1 * q2) + (2.0 * q0 * q3);
                double r11 = (2.0 * Math.Pow(q0, 2)) + (2.0 * Math.Pow(q1, 2)) - 1.0;

                euler[i, 0] = Math.Atan(r32 / r33);
                euler[i, 1] = -Math.Asin(r31);
                euler[i, 2] = Math.Atan(r21 / r11);
            }

            return euler;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////


    }
}