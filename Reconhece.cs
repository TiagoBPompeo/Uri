using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AnalisadorSintaxe
{
    class Reconhece
    {
        private List<List<string>> dadosTabela;
        int count = 0;
        char[] simbolos = null;
        public string sentenca;
        int qtdadeSimbolos;
        private Stack pilha;
        private Gramatica gramatica;

      
        private int indexOfInput = -1;
        //Stack
        Stack<string> strack = new Stack<string>();


        //tabela
        string[,] table = new string[,] 
        {

            {"aA",null,null,null},
            {"aBc",null,null,""},
            {null,null,"cCa",null},
            {null,"bB","a",null}

        };


        //estrutura da tabela
        string[] naoTerminais = { "S", "A", "B", "C"};
        string[] terminals = { "a", "b", "c", "$" };




        public Reconhece(string sentenca, List<List<string>> dadosTabela)
        {
            this.sentenca = sentenca;
            Console.WriteLine(this.sentenca);
            this.dadosTabela = dadosTabela;
            reconhecimento();
           
        }

        public void reconhecimento()
        {
            push(this.sentenca[0]+"");
            push("S");//empilha o primeiro simbolo nao terminal
        

            string token = read();//a
            string top = null;


            do
            {
                count++;
                if (strack!=null)
                {
                    top = this.strack.Pop();//recebe S que esta na pilha seu objetivo desempilhar
                
              
                //if top is non-terminal
                if (isNonTerminal(top))
                {
                    string rule = this.getRule(top, token);
                        if (rule!=null)
                        {
                            this.pushRule(rule);
                        }
                   
                }
                else if (isTerminal(top))
                {
                    if (!top.Equals(token))
                    {
                        error("this token is not corrent , By Grammer rule . Token : (" + token + ")");
                            Console.WriteLine("this token is not corrent, By Grammer rule.Token : (" + token + ")");
                        }
                    else
                    {
                        Console.WriteLine("Matching: Terminal :( " + token + " )");
                        token = read();
                        //top=pop();

                    }
                }
                else
                {
                    error("Never Happens , Because top : ( " + top + " )");
                        Console.WriteLine("Never Happens , Because top : ( " + top + " )");
                    }
                if (token.Equals("$"))
                {
                    break;
                }
                    //if top is terminal
                }
                else
                {
                    Console.WriteLine("Pilha Vazia");
                }
            } while (true);//out of the loop when $
                           //accept
            if (token.Equals("$"))
            {

                int countFinal = count;
                Console.WriteLine("Sentenca Aceita pela  Gramatica em "+countFinal.ToString()+" iterações");
                MessageBox.Show("Sentenca Aceita pela  Gramatica em " + countFinal.ToString() + " iterações");
            }
            else
            {

                Console.WriteLine("Sentenca  nao foi aceita  pela Gramatica");
            }
        }

        private bool isTerminal(string s)
        {
            for (int i = 0; i < this.terminals.Length; i++)
            {
                if (s.Equals(this.terminals[i]))
                {
                    return true;
                }

            }
            return false;
        }


        //lenght string diferente de count
        private void pushRule(string rule)
        {
            for (int i = rule.Length-1; i >= 0; i--)
            {
                char ch = rule[i];
                string str = ch.ToString();
                push(str);
            }
        }

        public string getRule(string non, string term)
        {

            int row = getnonTermIndex(non);
            int column = getTermIndex(term);
            string rule = table[row,column];

            if (rule == null)
            {
                error("sem regra pra isso , nao-Terminal(" + non + ") ,Terminal(" + term + ") ");
                Console.WriteLine("There is no Rule by this , Non-Terminal(" + non + ") ,Terminal(" + term + ") ");
            }
            return rule;
        }

        private int getnonTermIndex(string non)
        {
            for (int i = 0; i < this.naoTerminais.Length; i++)
            {
                if (non.Equals(this.naoTerminais[i]))
                {
                    return i;
                }
            }
            error(non + " is not NonTerminal");
            Console.WriteLine(non + " is not NonTerminal");
            return -1;
        }


        private int getTermIndex(String term)
        {
            for (int i = 0; i < this.terminals.Length; i++)
            {
                if (term.Equals(this.terminals[i]))
                {
                    return i;
                }
            }
            error(term + " is not Terminal");
            Console.WriteLine(term + " is not Terminal");
            return -1;
        }

        private void error(string message)
        {
           Console.WriteLine(message);
           // throw new RuntimeArgumentHandle(message);
        }

        private bool isNonTerminal(string s)
        {
            for (int i = 0; i < this.naoTerminais.Length; i++)
            {
                if (s.Equals(this.naoTerminais[i]))
                {
                    return true;
                }

            }
            return false;
        }

        private string read()
        {
            indexOfInput++;
            char ch = this.sentenca[indexOfInput];//sentenca na posicao 0 = a
            string str = ch.ToString();
            return str;
        }

        private void push(string s)
        {
            if (s != null)
            {
                this.strack.Push(s);
            }
         
        }


        private string pop()
        {
            return this.strack.Pop();
        }

        
    }
           

            

}

       
    

