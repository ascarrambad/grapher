using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grapher
{
    static class DataWriter
    {

        public static void DataWrite(string[] moto_staz, string[] lay_stand_sit, double[] girata, double freq, String mydocpath)
        {
            int size = moto_staz.Count();
            String[,] print_moto = new String[3, size];
            String[,] print_lay = new String[3, size];
            String[,] print_gir = new String[3, size];

            print_moto[0, 0] = DateTime.Now.ToString("h:mm:ss.fff tt");
            print_moto[2, 0] = moto_staz[0];

            print_lay[0, 0] = DateTime.Now.ToString("h:mm:ss.fff tt");
            print_lay[2, 0] = lay_stand_sit[0];

            print_gir[0, 0] = DateTime.Now.ToString("h:mm:ss.fff tt");
            print_gir[2, 0] = RitornaGirata(girata[0]);

            int count_m = 0, count_l = 0, count_g = 0;
            DateTime dateSum = DateTime.Now;
            DateTime dateSum2 = DateTime.Now;

            for (int i = 1; i < size; i++)
            {
                int k = i + 1;

                //if Moto_Stazionamento
                if (print_moto[2, count_m] != moto_staz[i])
                {
                    double sec = 1 / freq * i;
                    dateSum = dateSum.AddSeconds(sec);
                    print_moto[1, count_m] = dateSum.ToString("h:mm:ss.fff tt");
                    print_moto[0, count_m + 1] = dateSum.ToString("h:mm:ss.fff tt");
                    print_moto[2, count_m + 1] = moto_staz[i];
                    count_m++;
                    dateSum = dateSum.AddSeconds(-sec);
                }
                else if (k == size)
                {
                    double sec = 1 / freq * k;
                    dateSum2 = dateSum.AddSeconds(sec);
                    print_moto[1, count_m] = dateSum.AddSeconds(sec).ToString("h:mm:ss.fff tt");
                    print_moto[2, count_m] = moto_staz[i];
                }

                //if Lay_Stand_Sit
                if (print_lay[2, count_l] != lay_stand_sit[i])
                {
                    double sec = 1 / freq * i;
                    dateSum = dateSum.AddSeconds(sec);
                    print_lay[1, count_l] = dateSum.ToString("h:mm:ss.fff tt");
                    print_lay[0, count_l + 1] = dateSum.ToString("h:mm:ss.fff tt");
                    print_lay[2, count_l + 1] = lay_stand_sit[i];
                    count_l++;
                    dateSum = dateSum.AddSeconds(-sec);
                }
                else if (k == size)
                {
                    double sec = 1 / freq * k;
                    dateSum2 = dateSum.AddSeconds(sec);
                    print_lay[1, count_l] = dateSum.AddSeconds(sec).ToString("h:mm:ss.fff tt");
                    print_lay[2, count_l] = lay_stand_sit[i];
                }

                //if Girata
                if (print_gir[2, count_g] != RitornaGirata(girata[i]))
                {
                    double sec = 1 / freq * i;
                    dateSum = dateSum.AddSeconds(sec);
                    print_gir[1, count_g] = dateSum.ToString("h:mm:ss.fff tt");
                    print_gir[0, count_g + 1] = dateSum.ToString("h:mm:ss.fff tt");
                    print_gir[2, count_g + 1] = RitornaGirata(girata[i]);
                    count_g++;
                    dateSum = dateSum.AddSeconds(-sec);
                }
                else if (k == size)
                {
                    double sec = 1 / freq * k;
                    dateSum2 = dateSum.AddSeconds(sec);
                    print_gir[1, count_g] = dateSum.AddSeconds(sec).ToString("h:mm:ss.fff tt");
                    print_gir[2, count_g + 1] = RitornaGirata(girata[i]);
                }
            }

            int size_str = 0;
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < size; j++)
                {
                    if (print_moto[i, j] != null)
                        size_str++;
                    if (print_lay[i, j] != null)
                        size_str++;
                    if (print_gir[i, j] != null)
                        size_str++;
                }

            String[] str = new String[size];
            String fine = "";
            int cont = 0;
            for (int i = 0; i < size_str; i++)
            {
                if (print_moto[0, i] != null)
                {
                    if (print_moto[2, i + 1] != null)
                        str[cont] = print_moto[0, i] + " " + print_moto[1, i] + " " + print_moto[2, i];
                    else
                    {
                        fine = print_moto[0, i] + " " + print_moto[1, i] + " " + print_moto[2, i];
                        cont--;
                    }
                    cont++;
                }
                if (print_lay[0, i] != null)
                {
                    str[cont] = print_lay[0, i] + " " + print_lay[1, i] + " " + print_lay[2, i];
                    cont++;
                }
                if (print_gir[0, i] != null)
                {
                    if (print_gir[2, i] != "Prosegue")
                    {
                        str[cont] = print_gir[0, i] + " " + print_gir[1, i] + " " + print_gir[2, i];
                        cont++;
                    }
                }
            }
            str[cont] = fine;
            int t = 0;
            while (File.Exists(mydocpath + @"\WriteLine_" + t + ".txt"))
            {
                t++;
            }
            System.IO.File.WriteAllLines(mydocpath + @"\WriteLine_" + t + ".txt", str);
        }


        public static String RitornaGirata(double girata)
        {
            if (girata > 0)
                return "Girata DX";
            else if (girata < 0)
                return "Girata SX";
            else
                return "Prosegue";
        }

        public static void PrintPacketsToFile(List<Packet> sampwin, String path)
        {
            StreamWriter stream = new StreamWriter(path + @"\packet.txt", false);

            int size = sampwin.Count;
            int sensNum = sampwin.First().SensorsNumber;

            for (int p = 0; p < size; p++)
            {
                for (int s = 0; s < sensNum; s++)
                {

                    PrintSensorOnStream(stream, sampwin[p].Sensors[s].acc);
                    PrintSensorOnStream(stream, sampwin[p].Sensors[s].gyr);
                    PrintSensorOnStream(stream, sampwin[p].Sensors[s].mag);
                    PrintSensorOnStream(stream, sampwin[p].Sensors[s].q);
                    stream.Write(";; ");

                }
                stream.WriteLine();
            }

            stream.Close();
        }

        private static void PrintSensorOnStream(StreamWriter stream, double[] sens)
        {
            int size = sens.Count();
            for (int i = 0; i < size; i++)
            {
                stream.Write("{0}; ", sens[i]);
            }
        }

    }
}


