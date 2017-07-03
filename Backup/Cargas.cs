using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CargaPesada
{

    /// <summary>
    /// Classe que retorna as cargas poss�veis no problema 
    /// </summary>
    public class Carga
    {
        private int largura;
        private int altura;
        private int x;
        private int y;
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Altura
        {
            get { return altura; }
            set { altura = value; }
        }
        public int Largura
        {
            get { return largura; }
            set { largura = value; }
        }

        public Carga(int x, int y, int largura, int altura)
        {
            this.x = x;
            this.y = y;
            this.altura = altura;
            this.largura = largura;
        }

    }


}
