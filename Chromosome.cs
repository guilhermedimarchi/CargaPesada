using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace CargaPesada
{

    /// <summary>
    /// Classe que representa um cromossomo do problema das 8 rainhas
    /// </summary>
    public class Chromosome
    {
        // Vetor com as posições das colunas de cada rainha. A linha é o índice do vetor. 
        private List<Carga> caixas = null;
        private List<Carga> caixasPossiveis = null;
        // Objeto random para geração de números randômicos
        private static Random random = new Random(DateTime.Now.Millisecond);
        // Fitness
        private int fitness;

        public int Fitness
        {
            get { return fitness; }
        }
	

        // Propriedade que expõe o vetor para leitura
        public List<Carga> Caixas
        {
            get
            {
                return caixas;
            }
        }

        // Construtor - recebe o vetor com as posições
        public Chromosome(List<Carga> caixas)
        {
            this.caixas = caixas;

            this.caixasPossiveis = new List<Carga>( );
            this.caixasPossiveis.Add( new Carga( 0, 0, 3, 3 ) );
            this.caixasPossiveis.Add( new Carga( 0, 0, 1, 5 ) );
            this.caixasPossiveis.Add( new Carga( 0, 0, 5, 1 ) );
            this.caixasPossiveis.Add( new Carga( 0, 0, 2, 4 ) );
            this.caixasPossiveis.Add( new Carga( 0, 0, 4, 2 ) );
        }

        // Método estático que instancia um cromossomo com valores randômicos
        public static Chromosome CreateRandomChromosome(int numTipoA, int numTipoB, int numTipoC)
        {

            List<Carga> caixas = new List<Carga>(numTipoA + numTipoB + numTipoC);
            // Tipos A
            for (int i = 0; i < numTipoA; i++)
                caixas.Add(new Carga(random.Next(8), random.Next(8), 3, 3));
            // Tipos B
            for ( int i = 0; i < numTipoB; i++ )
                caixas.Add( new Carga( random.Next( 6 ), random.Next( 10 ), 5, 1 ) );
            // Tipos C
            for ( int i = 0; i < numTipoC; i++ )
                caixas.Add( new Carga( random.Next( 9 ), random.Next( 7 ), 2, 4 ) );
            return new Chromosome( caixas );
        }

        /// <summary>
        /// Operação de crossover entre dois cromossomos. 
        /// </summary>
        /// <param name="pair"> Recebe um cromossomo (par) que "cruzará" como atual.  </param>
        /// <returns> Retorna um filho com genes do cromossomo atual e do par. </returns>
        public Chromosome Crossover(Chromosome pair)
        {
            List<Carga> filho = new List<Carga>();

            for (int i = 0; i < Caixas.Count; i++)
            {
                filho.Add (i % 2 == 0 ? Caixas[i] : pair.Caixas[i]);
            }
            Chromosome eqc = new Chromosome(filho);
            return eqc;
        }


        /// <summary>
        /// Mutação - uma pequena chance de realizar mutação no cromossomo, mudando um de seus genes.
        /// </summary>
        /// <param name="rate"> Percentual da chance de mutação, número entre 0 e 1. </param>
        public void Mutate(double rate)
        {
            double d = random.NextDouble();
            if (d < rate)
            {
                int sorteio = random.Next(caixas.Count);
                int Altura, Largura;
                if ( random.Next( 2 )==0 )
                {
                    Altura = caixas [sorteio].Altura;
                    Largura  = caixas [sorteio].Largura;
                }
                else
                {
                    Altura = caixas [sorteio].Largura;
                    Largura  = caixas [sorteio].Altura;
                }
                int newx = random.Next( 11 - Largura );
                int newy = random.Next( 11 - Altura );
                caixas [sorteio] = new Carga( newx, newy, Largura, Altura );
                // Caixas B e C
            }
        }

        /// <summary>
        /// Fitness, o cálculo de quanto um cromossomo é bom
        /// </summary>
        /// <returns> Retorna a avaliação deste cromossomo. Valor "zero" é o cromossomo ideal, o objetivo da busca. </returns>
        public double GetFitness()
        {
            int [,] mat = new int[10, 10];

            // COntando
            foreach (Carga c in caixas)
            {
                for (int i = c.X; i < c.X + c.Largura; i++)
                {
                    for (int j = c.Y; j < c.Y + c.Altura; j++)
                    {
                        mat[i, j]++;
                    }
                }
            }

            // Descontando 1 que não vale...
            int fit = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (mat[i, j]>1)
                        fit += mat[i, j];
                }
                
            }



            fitness = fit;
            return fit;
            
        }


    }


}


