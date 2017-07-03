using System;
using System.Collections.Generic;
using System.Text;

namespace CargaPesada
{
    /// <summary>
    /// Classe onde deve ser implementado o algoritmo gen�tico
    /// </summary>
    public class GA
    {
        private int numTipoA;
        private int numTipoB;
        private int numTipoC;
        private double pior_fitness = 200;
        private List<Chromosome> population;
        // Tamanho da popula��o
        private int populationSize;
        // N�mero m�ximo de gera��es e gera��o atual 
        private int generations, generation;
        // Taxa de muta��o
        private double mutation_rate;
        // Gerador de n�meros rand�micos
        private static Random random = new Random(DateTime.Now.Millisecond);
        /// <summary>
        /// Propriedade para expor gera��o
        /// </summary>
        public int Generation
        {
            get { return generation; }
        }
        
        /// <summary>
        /// Construtor padr�o que recebe e armazena o n�mero de caixas de cada tipo que devem ser armazenadas
        /// </summary>
        /// <param name="numTipoA"></param>
        /// <param name="numTipoB"></param>
        /// <param name="numTipoC"></param>
        public GA(int numTipoA, int numTipoB, int numTipoC)
        {
            this.numTipoA = numTipoA;
            this.numTipoB = numTipoB;
            this.numTipoC = numTipoC;
            this.populationSize = 5000;
            this.population = new List<Chromosome>(5000);
            this.generations = 1000;
            this.mutation_rate = 0.25;
            this.generation = 0;
            InitializePopulation();
        }

        /// <summary>
        /// Busca a solu��o, criando as sucessivas gera��es da popula��o. 
        /// </summary>
        /// <returns> Retorna o melhor indiv�duo encontrado quando os crit�rios foram alcan�ados. </returns>
        public List<Carga> FindSolution()
        {
            AvaliatePopulation();

            Chromosome best = GetBestIndividual();
            if (best == null) return null;
            while (generation < generations && best.GetFitness() > 0)
            {
                Selection(populationSize / 2);
                GenerateChildren();
                AvaliatePopulation();
                best = GetBestIndividual();
                Console.WriteLine(generation + " - " +  best.Fitness);
                generation++;
            }

            return best.Caixas;
        }


        /// <summary>
        /// Retorna o indiv�duo com o melhor fitness
        /// </summary>
        /// <returns></returns>
        public Chromosome GetBestIndividual()
        {
            int min = 0;
            int max = 0;
            for (int i = 1; i < populationSize; i++)
            {
                if (population[i].Fitness < population[min].Fitness)
                {
                    min = i;
                }
                if (population[i].Fitness > population[max].Fitness)
                {
                    max = i;
                }
            }
            pior_fitness = population[max].Fitness;
            return population[min];
        }

        /// <summary>
        /// Inicializa a popula��o
        /// </summary>
        public void InitializePopulation()
        {
            for (int i = 0; i < this.populationSize; i++)
            {
                population.Add(Chromosome.CreateRandomChromosome(numTipoA, numTipoB, numTipoC));
            }
        }

        /// <summary>
        /// Avalia a popula��o calculando o fitness de todos os indiv�duos
        /// </summary>
        public void AvaliatePopulation()
        {
            foreach (Chromosome eqc in population)
            {
                eqc.GetFitness();
            }
        }



        /// <summary>
        /// Opera��o de sele��o, que elimina um n�mero de indiv�duos da popula��o. 
        /// </summary>
        /// <param name="killnumber"> Quantos indiv�duos devem ser eliminados. </param>
        public void Selection(int killnumber)
        {
            while (killnumber > 0)
            {
                Chromosome candidato = population[random.Next(population.Count)];
                double fator = candidato.Fitness / pior_fitness;
                if (random.NextDouble() < fator)
                {
                    population.Remove(candidato);
                    killnumber--;
                }
            }
        }

        /// <summary>
        /// Gerando os filhos da popula��o atual utilizando operadores de crossover e muta��o.
        /// Para controlar o tamanho da popula��o, o n�mero de filhos gerados n�o deve ultrapassar
        ///  o de indiv�duos eliminados pela sele��o.
        /// </summary>
        public void GenerateChildren()
        {
            while (population.Count < populationSize)
            {
                Chromosome pai = population[random.Next(population.Count)];
                Chromosome mae = population[random.Next(population.Count)];
                Chromosome filho = pai.Crossover(mae);
                filho.Mutate(mutation_rate);
                population.Add(filho);
            }
        }



    }
}
