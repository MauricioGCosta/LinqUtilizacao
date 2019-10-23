using System;
using System.IO;
using System.Globalization;
using System.Collections.Generic;
using LinqUtilizacao.Entidades;
using System.Linq;

namespace LinqUtilizacao
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Entre com o nome do arquivo, junto com sua pasta: ");
            string arquivo = Console.ReadLine();

            Console.Write("Entre com o salário: ");
            double salario = double.Parse(Console.ReadLine(), CultureInfo.InvariantCulture);

            List<Funcionario> funcionarios = new List<Funcionario>();

            using (StreamReader sr = File.OpenText(arquivo))
            {
                while (!sr.EndOfStream)
                {
                    string[] campos = sr.ReadLine().Split(',');
                    string nomeFunc = campos[0];
                    string emailFunc = campos[1];
                    double salarioFunc = double.Parse(campos[2], CultureInfo.InvariantCulture);

                    funcionarios.Add(new Funcionario(nomeFunc, emailFunc, salarioFunc));
                }
            }


            //var emailResult = funcionarios.Where(obj => obj.Salario > salario).OrderBy(obj => obj.Email).Select(obj => obj.Email);

            var emailResult =
                from p in funcionarios
                where p.Salario > salario
                orderby p.Nome
                select p.Email;

            Console.WriteLine("E-mail dos funcionários com salário maior que {0}", salario.ToString("F2", CultureInfo.InvariantCulture));

            foreach (string email in emailResult)
            {
                Console.WriteLine(email);
            }

            /*
            Forma com Lambda
            var soma = funcionarios.Where(obj => obj.Nome[0] == 'M').Sum(obj => obj.Salario);
            Forma com Linq
            */
            var soma = (from p in funcionarios where p.Nome[0] == 'M' select p).Sum(x => x.Salario);

            Console.WriteLine("Soma dos salarios de pessoas cujo nome começa com 'M': " + soma.ToString());
        }
    }
}
