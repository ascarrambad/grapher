using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher {

    static class DataAnalysis {

        // Module
        // double[,] data: [numero pacchetto, (x,y,z)]
        // return modarr[numero pacchetto]: array di moduli

        public static double[] ComputeModules(double[,] data) {
            int size = data.GetLength(0);

            double[] modarr = new double[size];

            for (int i = 0; i < size; i++) {
                modarr[i] = ComputeModule(data[i, 0], data[i, 1], data[i, 2]);
            }

            return modarr;
        }

        private static double ComputeModule(double x, double y, double z) {
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2) + Math.Pow(z, 2));
        }

        // Smoothing
        // return smoothed[pacchetto,sensore,tipo,(x,y,z,w)]

        public static List<Packet> SmoothData(List<Packet> sampwin, int range) {
            int size = sampwin.Count();
            int sensNum = sampwin.First().SensorsNumber;

            List<Packet> smoothed = new List<Packet>();

            for (int p = 0; p < size; p++) {
                Packet pa = new Packet();
                pa.SensorsNumber = sensNum;
                pa.Sensors = new Sensor[sensNum];
                smoothed.Add(pa);

                for (int s = 0; s < sensNum; s++) {
                    Sensor se = new Sensor();
                    se.acc = new double[3];
                    se.gyr = new double[3];
                    se.mag = new double[3];
                    se.q = new double[4];
                    pa.Sensors[s] = se;

                    int sx = Math.Max(0, p - range);
                    int dx = Math.Min(size - 1, p + range);
                    int num = dx - sx + 1;
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
                        pa.Sensors[s].acc[i] = accmean[i] / (double)num;
                        pa.Sensors[s].gyr[i] = gyrmean[i] / (double)num;
                        pa.Sensors[s].mag[i] = magmean[i] / (double)num;
                        pa.Sensors[s].q[i] = qmean[i] / (double)num;
                    }
                    pa.Sensors[s].q[3] = qmean[3] / (double)num;
                }
            }

            return smoothed;
        }

        // Derivative
        // return modarr[pacchetto,sensore,tipo(0:acc,1:gyr)]

        public static double[] ComputeDerivatives(double[] mod, int freq) {
            int size = mod.Count();
            double[] derivated = new double[size];

            for (int i = 0; i < size - 1; i++) {
                derivated[i] = ComputeDerivative(freq, mod[i], mod[i + 1]);
            }
            return derivated;
        }

        private static double ComputeDerivative(int freq, double v1, double v2) {
            return (v1 - v2) / ((double)1 / freq);
        }

        // Standard Deviation

        public static double[] ComputeStandardDeviation(double[] mod, int range) {
            int size = mod.Count();
            double[] derivated = new double[size];

            for (int i = 0; i < size - 1; i++) {

                int sx = Math.Max(0, i - range);
                int dx = Math.Min(size - 1, i + range);
                int num = dx - sx + 1;

                for (int j = sx; j < dx; j++) {

                }
            }
            return derivated;
        }
    }
}
