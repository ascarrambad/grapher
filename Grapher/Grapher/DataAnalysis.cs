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

        public static double[] ComputeSquare(double[] data, int freq , double cutOff, double epsilon)
        {
            int size = data.Count();
            double[] squared = new double[size];
            int initialState = 0;
            int currentstate = 0;
            Boolean changeState = false;
            int start = 10;
            
            // inizialmente salto i primi tot pacchetti
            for (int i = start; i < size; ++i) { // -30 perche potrebbe andarmi in exception
                // e' cambiato lo stato attuale??

                if (initialState == currentstate) {
                    for (int k = 0; k <= start; ++k) {
                        squared[k] = data[i];
                    }
                    
                    currentstate = data[i] < cutOff ? 1 : 2;
                }
                else {
                    // 1 <
                    // 2 >

                    if ((data[i] < cutOff && currentstate == 1) || (data[i] >= cutOff && currentstate == 2)) {
                        // non ce cambio, non faccio niente
                        squared[i] = squared[i - 1];
                    } else {
                        if (data[i] < cutOff) {
                            //changeState = true;
                            int next = (int)(i + 1 / freq * epsilon); // 0.25 e' un quarto di secondo
                            if (data[next] < cutOff) {
                                squared[i] = data[next];
                                currentstate = 1;
                                changeState = true;
                            } else {
                                squared[i] = squared[i - 1]; // falso moto
                            }
                        } else {
                            int next = (int)(i + 1 / freq * epsilon);
                            if (data[next] > cutOff) {
                                squared[i] = data[next];
                                currentstate = 2;
                                changeState = true;
                            } else {
                                squared[i] = squared[i - 1];
                            }
                        }
                    }
                }

                /*if (data[i] < cutOff) {
                    if (initialState != currentstate && changeState) {
                        int next = (int)(i + 1 / freq * 0.25); // 0.25 e' un quarto di secondo
                        if (data[next] < cutOff) {
                            squared[i] = data[next];
                        }
                        else {
                            // controllo il next di next
                            int more = (int)(i + 1 / freq * 0.25);
                            if (data[more] < cutOff) {
                                squared[i] = squared[i - 1];
                            }
                            else {
                                squared[i] = data[next];
                            }
                        }
                    }
                }*/
            }
            return squared;
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

        public static List<Packet> ComputeLowPass(List<Packet> sampwin)
        {
            int size = sampwin.Count();
            int sensNum = sampwin.First().SensorsNumber;

            List<Packet> lowfilter = new List<Packet>();

            Packet pa1 = new Packet() {
                SensorsNumber = sensNum,
                Sensors = new Sensor[sensNum]
            };
            lowfilter.Add(pa1);

            for (int s = 0; s < sensNum; s++) {
                Sensor se = new Sensor();

                se.acc = sampwin[0].Sensors[s].acc;
                se.gyr = sampwin[0].Sensors[s].gyr;
                se.mag = sampwin[0].Sensors[s].mag;
                se.q = sampwin[0].Sensors[s].q;

                pa1.Sensors[s] = se;
            }

            for (int p = 1; p < size; p++) {
                Packet pa = new Packet() {
                    SensorsNumber = sensNum,
                    Sensors = new Sensor[sensNum]
                };
                lowfilter.Add(pa);

                for (int s = 0; s < sensNum; s++) {
                    Sensor se = new Sensor();

                    se.acc = ComputeLowPass(sampwin[p].Sensors[s].acc, lowfilter[p - 1].Sensors[s].acc, 0.1);
                    se.gyr = ComputeLowPass(sampwin[p].Sensors[s].gyr, lowfilter[p - 1].Sensors[s].gyr, 0.1);
                    se.mag = ComputeLowPass(sampwin[p].Sensors[s].mag, lowfilter[p - 1].Sensors[s].mag, 0.1);
                    se.q = ComputeLowPass(sampwin[p].Sensors[s].q, lowfilter[p - 1].Sensors[s].q, 0.1);

                    pa.Sensors[s] = se;
                }
            }

            return lowfilter;
        }

        private static double[] ComputeLowPass(double[] data, double[] prec, double a)
        {
            int size = data.Count();
            double[] filteredValues = new double[size];
            for (int i = 0; i < size; i++) {
                filteredValues[i] = data[i] * a + prec[i] * (1.0 - a);
            }
            return filteredValues;
        }

        public static List<Packet> ComputeHighPass(List<Packet> sampwin)
        {
            int size = sampwin.Count();
            int sensNum = sampwin.First().SensorsNumber;

            List<Packet> lowfilter = new List<Packet>();

            Packet pa1 = new Packet() {
                SensorsNumber = sensNum,
                Sensors = new Sensor[sensNum]
            };
            lowfilter.Add(pa1);

            for (int s = 0; s < sensNum; s++) {
                Sensor se = new Sensor();

                se.acc = sampwin[0].Sensors[s].acc;
                se.gyr = sampwin[0].Sensors[s].gyr;
                se.mag = sampwin[0].Sensors[s].mag;
                se.q = sampwin[0].Sensors[s].q;

                pa1.Sensors[s] = se;
            }

            for (int p = 1; p < size; p++) {
                Packet pa = new Packet() {
                    SensorsNumber = sensNum,
                    Sensors = new Sensor[sensNum]
                };
                lowfilter.Add(pa);

                for (int s = 0; s < sensNum; s++) {
                    Sensor se = new Sensor();

                    se.acc = ComputeHighPass(sampwin[p].Sensors[s].acc, lowfilter[p - 1].Sensors[s].acc, 0.5);
                    se.gyr = ComputeHighPass(sampwin[p].Sensors[s].gyr, lowfilter[p - 1].Sensors[s].gyr, 0.5);
                    se.mag = ComputeHighPass(sampwin[p].Sensors[s].mag, lowfilter[p - 1].Sensors[s].mag, 0.5);
                    se.q = ComputeHighPass(sampwin[p].Sensors[s].q, lowfilter[p - 1].Sensors[s].q, 0.5);

                    pa.Sensors[s] = se;
                }
            }

            return lowfilter;
        }

        private static double[] ComputeHighPass(double[] data, double[] prec, double a)
        {
            int size = data.Count();
            double[] filteredValues = new double[size];
            for (int i = 0; i < size; i++) {
                filteredValues[i] = prec[i] * a + a * (data[i] - prec[i]);
            }
            return filteredValues;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////

        //Dobbiamo prendere gia l' array con i dati del singolo sensore
        //dobbiamo prendere in ingresso un parametro per la standard deviation che si sceglie dall' interfaccia

        //Funzione MotoStazionamento, ritorna un array di Stringhe contentente lo stato di moto di ogni singola rilevazione
        //"Fermo" oppure "In-Piedi"

        public static String[] MotoStazionamento(double[,] providedData, int window)
        {
            double[] module = DataAnalysis.ComputeModules(providedData);
            double[] sd = DataAnalysis.ComputeStandardDeviations(module, window);
            int sd_size = sd.Count();
            String[] state = new String[sd_size];
            for (int i = 0; i < sd_size; i++) {
                if (sd[i] <= 1)
                    state[i] = "Fermo";
                else
                    state[i] = "Non-Fermo";
            }
            return state;
        }

        //FunzioneOrientamento prende in ingresso 2 array, corrispondenti all' asse y e z del magnetometro
        //restituisce il valore theta della variazione di angoli senza salti

        public static double[] FunzioneOrientamento(double[] y, double[] z)
        {
            int size = y.Count();
            double[] theta = new double[size];
            theta[0] = Math.Atan(y[0] / z[0]);
            int[] sfasam = new int[size];
            for (int i = 1; i < size; i++) {
                double value = Math.Atan(y[i] / z[i]);
                double diff = value - theta[i - 1];
                //La differenza tra gli angoli deve essere minore di 2.5 se no e' considerata un salto
                if (Math.Abs(diff) > 2.5) {
                    if (diff > 0) {
                        sfasam[i] -= 1;
                    }
                    else {
                        sfasam[i] += 1;
                    }
                }
                theta[i] = value + sfasam[i] * Math.PI;

            }
            return theta;
        }

        //Girata prende in ingresso un' array di due dimensioni [numero di pacchetto][asse x y z del magnetometro]
        //

        public static String[] Girata(double[,] providedData)
        {

            int size = providedData.GetLength(0);
            double[] thetaArray = new double[size];
            double[] yArray = new double[size];
            double[] zArray = new double[size];
            double[] gradesArray = new double[size];
            String[] turnArray = new String[size];
            for (int i = 0; i < size; i++) {
                yArray[i] = providedData[i, 1];
                zArray[i] = providedData[i, 2];
            }

            thetaArray = FunzioneOrientamento(yArray, zArray);
            //Pi:180 = theta: x;
            for (int i = 0; i < size; i++) {
                gradesArray[i] = 180 * thetaArray[i] / Math.PI;
            }
            for (int i = 1; i < size; i++) {
                double diff = Math.Round((gradesArray[i] - gradesArray[i - 1]), MidpointRounding.AwayFromZero);
                //double diff = Math.Round(thetaArray[i] - thetaArray[i - 1], MidpointRounding.AwayFromZero);
                //girata sinistra
                // 10 gradi = 0.15 radianti
                if (diff >= 10) {
                    turnArray[i] = diff.ToString();
                    //turnArray[i] = "Girata SX di °" + diff;
                }
                else if (diff <= -10) {
                    turnArray[i] = diff.ToString();
                    //turnArray[i] = "Girata DX di °" + -diff;
                }
                else turnArray[i] = "Continua ";
            }

            return turnArray;
        }
        //Metodo Lay/Stand/Sit, ritorna un array di Stringhe contenente lo stato di ogni singola rilevazione
        //"Lay" o "LaySit" o "Sit" o "Stand"
        //Prende in ingresso l' array delle x dell' accelerometro del PRIMO SENSORE
        //perche' la x e' rivolta verso l' alto
        //Per convenzione mettiamo l' uguale nei minori

        public static String[] LayStandSit(double[] providedData)
        {

            int size = providedData.Count();
            String[] state = new String[size];
            for (int i = 0; i < size; i++) {
                if (providedData[i] <= 2.7)
                    state[i] = "Sdraiato";
                else if (providedData[i] > 2.7 && providedData[i] <= 3.7)
                    state[i] = "Sdraiato-Seduto";
                else if (providedData[i] > 3.7 && providedData[i] <= 7)
                    state[i] = "Seduto";
                else if (providedData[i] > 7)
                    state[i] = "In-Piedi";
            }
            return state;
        }


        public static double ComputeAverage(double[] vector)
        {
            double avg = 0;
            for (int i = 0; i < vector.Count(); i++) {
                avg += vector[i];
            }
            return avg / (vector.Count());
        }


    }
}