using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AnalisadorSintaxe;

namespace AnalisadorSintaxe
{
    //É uma forma de representar as regras para formação de uma linguagem.
    class Gramatica
    {
        List<string> regras = new List<string>();    /*contem as regras (produções) da gramática. */
        List<string> produtor = new List<string>();    /* armazena  produtor da gramática. */
        public static List<string> alfabeto = new List<string>();/*É um conjunto de símbolos*/
     

        public Gramatica(List<string> producoes, string entrada)
        {
        
            int conta_glc;
            int qtdadeSimbolos;
            int qtd_letras_alfabeto;
            string[] aux = new string[2];
            char [] letras = null;

            for (conta_glc = 0; conta_glc < producoes.Count; conta_glc++)
            {
                aux = Regex.Split(producoes[conta_glc], "::=");
                this.produtor.Add(aux[0]);
                Console.WriteLine("Produtor adicionado: " + produtor[produtor.Count - 1]);
                this.regras.Add(aux[1]);
                Console.WriteLine("Regra adicionada: " + regras[conta_glc]);

            }

            if (!string.IsNullOrWhiteSpace(entrada))
            {
                letras = entrada.ToCharArray();
                qtd_letras_alfabeto = letras.Count();
             

                //Adiciona alfabeto
                for (conta_glc = 0; conta_glc < qtd_letras_alfabeto; conta_glc++)
                {
                    alfabeto.Add(letras[conta_glc].ToString());
                }
            }
        }



   
        public List<String> getRegras()
        {
            return regras;
        }

       
        public List<string> getHandle()
        {
            return produtor;
        }

       
        public List<string> getAlfabeto()
        {
            return alfabeto;
        }


    }
}
