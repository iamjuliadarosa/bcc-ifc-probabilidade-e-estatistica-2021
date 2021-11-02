using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace esperanca_matematica
{
    class Program
    {
        public class Model {
            public string ID;
            public float ocorrencia;
            public float probabilidade;
            public float projecao;
            public float aposta;

            public Model(string id, float ocorrencia, float p) {
                this.ID = id;
                this.ocorrencia = ocorrencia;
                this.probabilidade = p;
            }
        }
        static void Main(string[] args) {
            try {
                int variaveis = 0;

                Console.WriteLine("Informe a quantidade de estados de dados obsevados: ");
                string inputDados = Console.ReadLine();
                if (int.TryParse(inputDados, out int value)) {
                    variaveis = value;
                } else {
                    throw new Exception("Valor invalido! Tente novamente.");
                }
                if (variaveis >= 2 ) {
                    Dictionary<string, float> valoresObservados = new Dictionary<string, float>();
                    for (int i = 0; i < variaveis; i++) {
                        Console.WriteLine("Informe a identificação do dado "+i+": ");
                        string ID = Console.ReadLine();
                        Console.WriteLine("Informe a quantidade de vezes que esse dado foi obsevados: ");
                        string rep = Console.ReadLine();
                        if (float.TryParse(rep, out float value1)) {
                            valoresObservados.Add(ID, value1);
                        } else {
                            throw new Exception("Valor invalido! Tente novamente.");
                        }
                    }
                    float totalObservado = valoresObservados.Values.Sum();

                    List< Model> probabilidade = new List<Model>();
                    foreach(KeyValuePair<string, float> item in valoresObservados) {
                        float prob = item.Value / totalObservado;
                        Model Item = new Model(item.Key, item.Value, prob);
                        probabilidade.Add(Item);
                    }
                    Console.Clear();
                    foreach (Model item in probabilidade) {
                        Console.WriteLine("\nO valor observado \"" + item.ID + "\" ocorreu " + item.ocorrencia + " vezes em uma amostragem de " + totalObservado + " lances.\nTendo sua probabilidade de ocorrencia igual á "+item.probabilidade+".");
                    }
                    Console.WriteLine("Aperte qualquer tecla para prosseguir.");
                    Console.ReadKey();
                    Console.Clear();
                    int lanceProjecao = 0;
                    Console.WriteLine("Informe a quantidade de lances a serem projetados: ");
                    string inputAposta = Console.ReadLine();
                    if (int.TryParse(inputAposta, out int value4)) {
                        lanceProjecao = value4;
                    } else {
                        throw new Exception("Valor invalido! Tente novamente.");
                    }
                    foreach (Model item in probabilidade) {
                        item.projecao = item.probabilidade * lanceProjecao;
                        Console.WriteLine("O valor observado \"" + item.ID + "\" ocorreu " + item.ocorrencia + " " +
                            "vezes em uma amostragem de " + totalObservado + " lances.\nTendo sua probabilidade de ocorrencia igual á " + item.probabilidade 
                            + ".\nTendo sua projeção de ocorrencia igual á " + item.projecao + " em "+lanceProjecao+" lances.");
                    }
                    Console.WriteLine("Aperte qualquer tecla para prosseguir.");
                    Console.ReadKey();
                    Console.Clear();
                    float esperança = 0f;
                    float esperançaporaposta = 0f;
                    foreach (Model item in probabilidade) {
                        Console.WriteLine("\nO valor observado \"" + item.ID + "\" ocorreu " + item.ocorrencia + " " +
                            "vezes em uma amostragem de " + totalObservado + " lances.\nTendo sua probabilidade de ocorrencia igual á " + item.probabilidade
                            + ".\nTendo sua projeção de ocorrencia igual á " + item.projecao + " em " + lanceProjecao + " lances.\n");

                        Console.WriteLine("Informe o ganho/perca na aposta desse valor: ");
                        string aposta = Console.ReadLine();
                        if (float.TryParse(aposta, out float value3)) {
                            item.aposta = value3;
                        } else {
                            throw new Exception("Valor invalido! Tente novamente.");
                        }
                        esperança += item.projecao * item.aposta;
                        esperançaporaposta += item.probabilidade * item.aposta;
                    }
                    Console.WriteLine("\nÉ esperado ganhar ou perder o valor de "+esperança+".");
                    Console.WriteLine("É esperado ganhar ou perder o valor de "+esperança/lanceProjecao+" por aposta.");
                    Console.WriteLine("É esperado ganhar ou perder o valor de "+ esperançaporaposta + " por aposta.");
                    Console.WriteLine("Aperte qualquer tecla para finalizar.");
                    Console.ReadKey();
                }
            } catch (Exception err) {
                Console.WriteLine(err.Message);
            }
        }
    }
}
