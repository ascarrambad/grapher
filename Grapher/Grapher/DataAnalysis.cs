using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher {

    static class DataAnalysis {

        // Module
        // return modarr[pacchetto,sensore,tipo(0:acc,1:gyr)]

        public static double[,,] ComputeModules(List<Packet> sampwin) {
            int sensNum = sampwin.First().SensorsNumber;
            int dim = sampwin.Count * sensNum * 3;

            double[,,] modarr = new double[sampwin.Count,sensNum,2];

            for (int i = 0; i < sampwin.Count; i++) {
                for(int j = 0; j < sensNum; j++) {
                    Sensor s = sampwin[i].Sensors[j];
                    modarr[i, j, 0] = ComputeModule(s.acc[0], s.acc[1], s.acc[2]);
                    modarr[i, j, 1] = ComputeModule(s.gyr[0], s.gyr[1], s.gyr[2]);
                }
            }

            return modarr;
        }

        private static double ComputeModule(double x, double y, double z) {
            return Math.Sqrt(Math.Pow(x,2) + Math.Pow(y, 2) + Math.Pow(z, 2));
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
                        pa.Sensors[s].acc[i] = accmean[i] / num;
                        pa.Sensors[s].gyr[i] = gyrmean[i] / num;
                        pa.Sensors[s].mag[i] = magmean[i] / num;
                        pa.Sensors[s].q[i] = qmean[i] / num;
                    }
                    pa.Sensors[s].q[3] = qmean[3] / num;
                }
            }

            return smoothed;
        }

    }
}
