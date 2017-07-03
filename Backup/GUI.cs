using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CargaPesada
{
    class GUI
    {
        /// <summary>
        /// M�todo que recebe uma lista de caixas e retorna um bitmap com 
        /// desenho das posi��es das mesmas.
        /// </summary>
        /// <param name="cargas"></param>
        /// <returns></returns>
        public static Bitmap DesenhaCarga(List<Carga> cargas)
        {
            Bitmap b = new Bitmap(502, 502);
            Graphics g = Graphics.FromImage(b);
            // Desenhando espa�os do caminh�o
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    g.DrawRectangle(new Pen(Brushes.Gray), i * 25, j * 25, 25, 25);
                }
            }

            // Agora as caixas
            foreach (Carga c in cargas)
            {
                g.DrawRectangle(new Pen(Brushes.Red), c.X* 25+1, c.Y * 25+1, c.Largura * 25-2, c.Altura * 25-2);
            }
            
            // Retorno
            return b;

        }
    }
}
