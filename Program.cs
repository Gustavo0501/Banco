using Banco.AccountContext;
using Banco.SystemContext;
using System.ComponentModel.DataAnnotations;

namespace Banco
{
    public class Program
    {
        static void Main(string[] args)
        {
            Home();

        }

        public static void Home()
        {
            int choice = -1;
            bool login = false;

            User user = null;

            do
            {
                Console.WriteLine("\nPressione qualquer tecla para continuar");
                Console.ReadKey();
                Console.Clear();
                Console.WriteLine("Selecione uma opção: ");
                Console.WriteLine("[1] - Criar conta");
                Console.WriteLine("[2] - Entrar");
                Console.WriteLine("[0] - Sair");

                try
                {
                    choice = int.Parse(Console.ReadLine());
                    
                    switch (choice)
                    {
                        case 1:
                            RegisterSystem.CreateAccount();
                            break;
                        case 2:
                            user = RegisterSystem.Login();

                            if (user != null)
                            {
                                Console.WriteLine("Entrando...");
                                login = true;
                            }
                            else
                            {
                                Console.WriteLine("Login inválido!");
                            }
                            break;
                        case 0:
                            Console.WriteLine("Saindo...");
                            break;
                        default:
                            Console.WriteLine("Opção inválida");
                            break;
                    }

                }
                catch(FormatException) {
                    Console.WriteLine("Você precisa digitar um número!\n\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("Algo deu errado!");
                }

            } while (choice != 0 && (!login));

            if (login && user != null)
            {
                LoginSucessful(user);
            }
        }

        private static void LoginSucessful(User user)
        {
            var menu = new Menu(user);

            menu.ShowOptions();
        }
    }
}