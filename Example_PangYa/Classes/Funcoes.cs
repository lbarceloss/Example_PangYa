using System;

namespace Example_PangYa
{
    internal class Funcoes
    {
        public double quebraBola(double x, double y, double bolax, double bolay)
        {
            double radianusSeno, radianusCos, senoInverso, radianusPosicao, posicao, resultadoAutoquebra, cos;
            radianusSeno = Math.Asin(x) * 180 / Math.PI;
            radianusCos = Math.Acos(y) * 180 / Math.PI;
            if (radianusSeno < 0.0)
            {
                posicao = 180 - (radianusCos - 180);
            }
            else
            {
                posicao = radianusCos;
            }
            radianusPosicao = posicao * Math.PI / 180;
            radianusPosicao *= -1;
            senoInverso = Math.Sin(radianusPosicao) * -1;
            cos = Math.Cos(radianusPosicao);
            resultadoAutoquebra = Math.Round(((bolax * cos) + (bolay * senoInverso)) * -1 * (1 / 0.00875), 2);
            return resultadoAutoquebra;
        }

        public double pbTirado(double x1, double x2, double z1, double z2, double gridPersonagemMem)
        {
            double anguloCamera, distanciaRaiz, rad2, rad, pb2;

            anguloCamera = Math.Atan2(x2 - x1, z2 - z1);
            distanciaRaiz = Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(z2 - z1, 2));
            rad2 = gridPersonagemMem;
            rad = Math.Abs(rad2) % 6.28318530717659;
            if (rad2 <= 0)
                rad *= -1;

            pb2 = ((distanciaRaiz * 0.3125) * Math.Tan(rad + anguloCamera)) / 1.5 / 0.2167 * -1;
            if (pb2 < 0)
            {
                pb2 *= -1;
            }
            return Math.Round(pb2, 2);
        }

        public double anguloDegree(double x, double y)
        {
            if (x > 0.00 && y < 0.00)
                return Math.Acos(x) * 180 / Math.PI;
            else if (x < 0.00 && y < 0.00)
                return Math.Acos(x) * 180 / Math.PI;
            else if (x > 0.00 && y > 0.00)
                return ((Math.Acos(x) * 180 / Math.PI) - 360) * -1;
            else
                return ((Math.Acos(x) * 180 / Math.PI) - 360) * -1;
        }

        public double Distancia(double x1, double x2, double y1, double y2)
        {
            return Math.Round(Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2)) * 0.312495, 2);
        }

        public double Altura(double x1, double x2)
        {
            return Math.Round((x2 - x1 + 0.14) * (0.312495 * 0.914), 1);
        }

        public int Terreno(int x)
        {
            x = 100 - x;
            return x;
        }

        public string Driver(int driverMem, double linhaXMem, double tee1Mem, double linhaZMem, double tee3Mem)
        {
            if (driverMem >= 0 && driverMem <= 2)
                return Convert.ToString(Distancia(linhaXMem, tee1Mem, linhaZMem, tee3Mem) + "y/" + (driverMem + 1) + "w");
            else if (driverMem > 2 && driverMem <= 10)
                return Convert.ToString(Distancia(linhaXMem, tee1Mem, linhaZMem, tee3Mem)) + "y/" + ((driverMem - 2) + 1) + "l";
            else if (driverMem == 11)
                return Convert.ToString(Distancia(linhaXMem, tee1Mem, linhaZMem, tee3Mem)) + "y/" + "PW";
            else if (driverMem == 12)
                return Convert.ToString(Distancia(linhaXMem, tee1Mem, linhaZMem, tee3Mem)) + "y/" + "SW";
            else
                return Convert.ToString(Distancia(linhaXMem, tee1Mem, linhaZMem, tee3Mem)) + "y/" + "PT";
        }

        public double RadiansParaDegrees(double radians)
        {
            double degrees = (180 / Math.PI) * radians;
            return (degrees);
        }

    }
}
