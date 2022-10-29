using Memory;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Input;

/*
 * Referencias: https://github.com/erfg12/memory.dll
*/

namespace Example_PangYa
{
    internal class Program
    {
        static Funcoes f = new Funcoes();
        static Mem m = new Mem();
        static double tee1Mem, tee2Mem, tee3Mem, pin1Mem, pin2Mem, pin3Mem, eixoxMem, eixoyMem, cosBolaMem, senoBolaMem, spinMem, curvaMem, gridPersonagemMem, cosAnguloMem, senoAnguloMem;
        static double accuracyPixelMem, spinMax, curvaMax, linhaXMem, linhaZMem, assistXMem, assistZMem, radiusAssistMem, radianseixoMem, assistDiametroAtual, linhaAssistTotal, linhaAssistPixel;
        static double assistEixoX, assistEixoY, assistEixoZ;

        static int mapa = 0;
        static string ventoMem;
        static int terrenoMem, driverMem;
        //static int estadoBarra, readyCheck;
        static bool Aberto = false;
        static bool Rodando = true;
        static bool Auto = false, AutoLimpar = false;
        static int opcao = 0, contador = 0;
        static Thread TC = new Thread(Teclado);

        static void Main(string[] args)
        {
            Console.WriteLine("Key win: " + getKey());
            Aberto = m.OpenProcess("ProjectG");
            if (Aberto)
            {
                TC.SetApartmentState(ApartmentState.STA);
                TC.Start();
                TC.Suspend();
                Console.WriteLine("PangYa aberto");
                do
                {
                    Console.WriteLine("Escolha a opção: ");
                    Console.WriteLine("[1] - Dados Gerais.");
                    Console.WriteLine("[2] - Dados do Hole.");
                    Console.WriteLine("[3] - Dados da Bola.");
                    Console.WriteLine("[4] - Assist.");
                    if (Auto == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[5] - Modo Semi-Auto.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[5] - Modo Semi-Auto.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    if (AutoLimpar == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[6] - Auto limpar.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("[6] - Auto limpar.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.WriteLine("[0] - Para fechar o programa.");
                    Console.Write("Opção: ");
                    opcao = int.Parse(Console.ReadLine());
                    if (Auto == false)
                    {
                        Console.WriteLine("");
                        switch (opcao)
                        {
                            case 1:
                                Gerais();
                                break;
                            case 2:
                                Hole();
                                break;
                            case 3:
                                Bola();
                                break;
                            case 4:
                                Assist();
                                break;
                            case 5:
                                TC.Resume();
                                Auto = true;
                                Console.WriteLine("Modo Semi-Auto ligado.");
                                Console.Clear();
                                break;
                            case 6:
                                if (contador % 2 == 0)
                                {
                                    AutoLimpar = true;
                                    contador++;
                                }
                                else
                                {
                                    AutoLimpar = false;
                                    contador--;
                                }
                                Console.Clear();
                                break;
                            default:
                                Console.WriteLine("Opcao Invalida");
                                break;
                        }
                    }
                } while (opcao != 0);
                Console.WriteLine("Encerrando Programa.");
                Rodando = false;
                Thread.Sleep(1000);
            }
            else
            {
                Console.WriteLine("PangYa não encontrado.");
                Thread.Sleep(1000);
                Console.WriteLine("Tentando novamente em:");
                Console.WriteLine("3");
                Thread.Sleep(1000);
                Console.WriteLine("2");
                Thread.Sleep(1000);
                Console.WriteLine("1");
                Thread.Sleep(1000);
                Console.Clear();
                Main(args);
            }
        }
        private static void Teclado()
        {
            while (Rodando)
            {
                Thread.Sleep(50);
                if ((Keyboard.GetKeyStates(Key.NumPad1) & KeyStates.Down) > 0 & Auto == true)
                {
                    Gerais();
                    Thread.Sleep(1000);
                }
                else if ((Keyboard.GetKeyStates(Key.NumPad2) & KeyStates.Down) > 0 & Auto == true)
                {
                    Hole();
                    Thread.Sleep(1000);
                }
                else if ((Keyboard.GetKeyStates(Key.NumPad3) & KeyStates.Down) > 0 & Auto == true)
                {
                    Bola();
                    Thread.Sleep(1000);
                }
                else if ((Keyboard.GetKeyStates(Key.NumPad4) & KeyStates.Down) > 0 & Auto == true)
                {
                    Assist();
                    Thread.Sleep(1000);
                }
                else if ((Keyboard.GetKeyStates(Key.NumPad5) & KeyStates.Down) > 0 & Auto == true)
                {
                    Auto = false;
                    Console.WriteLine(" ");
                    Console.WriteLine("Modo Semi-Auto desligado.");
                    Thread.Sleep(1200);
                    Console.Clear();
                    Console.WriteLine("Escolha a opção: ");
                    Console.WriteLine("[1] - Dados Gerais.");
                    Console.WriteLine("[2] - Dados do Hole.");
                    Console.WriteLine("[3] - Dados da Bola.");
                    Console.WriteLine("[4] - Assist.");
                    Console.WriteLine("[0] - Para fechar o programa.");
                    Console.Write("Opção: ");
                    TC.Suspend();
                }
            }
        }
        private static void Memorias()
        {
            mapa = m.ReadByte("ProjectG.exe+A47E28");
            if (Aberto == true && mapa != 0)
            {
                accuracyPixelMem = m.ReadFloat("ProjectG.exe+A79014", "", false);
                spinMax = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0xC,0x28,0x2C,0x30,0x0,0x24", "", false);
                curvaMax = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0xC,0x28,0x2C,0x30,0x0,0x20", "", false);
                tee1Mem = m.ReadFloat("ProjectG.exe+A47E30", "", false);
                tee2Mem = m.ReadFloat("ProjectG.exe+A47F00", "", false);
                tee3Mem = m.ReadFloat("ProjectG.exe+A47F04", "", false);
                pin1Mem = m.ReadFloat("ProjectG.exe+AFD154", "", false);
                pin2Mem = m.ReadFloat("ProjectG.exe+AFD158", "", false);
                pin3Mem = m.ReadFloat("ProjectG.exe+AFD15C", "", false);
                linhaXMem = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x2C,0x28,0x14,0x0,0x0,0x68", "", false);
                linhaZMem = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x2C,0x28,0x14,0x0,0x0,0x70", "", false);
                cosAnguloMem = m.ReadFloat("ProjectG.exe+00A73E60,0x34,0x18,0x10,0x30,0x0,0x234,0xAC", "", false);
                senoAnguloMem = m.ReadFloat("ProjectG.exe+00A73E60,0x34,0x18,0x10,0x30,0x0,0x234,0xB4", "", false);
                eixoxMem = m.ReadFloat("ProjectG.exe+00A73E60,0x34,0x18,0x10,0x30,0x0,0x21C,0x1C", "", false);
                eixoyMem = m.ReadFloat("ProjectG.exe+00B006E8,0x8,0x10,0x30,0x0,0x21C,0x24", "", false);
                radianseixoMem = m.ReadFloat("ProjectG.exe+00A73E60,0x34,0x18,0x10,0x30,0x0,0x21C,0x74", "", false);
                cosBolaMem = m.ReadFloat("ProjectG.exe+B024A0", "", false);
                senoBolaMem = m.ReadFloat("ProjectG.exe+B024A8", "", false);
                ventoMem = m.ReadString("ProjectG.exe+00B006E8,0x8,0x10,0x30,0x0,0x220,0x28,0x0", "");
                //estadoBarra = m.ReadByte("ProjectG.exe+00B006E8,0x20,0x10,0xB4,0x30,0x8,0x128,0xF8");
                //readyCheck = m.ReadByte("ProjectG.exe+00B006E8,0x20,0x10,0xB4,0x30,0x8,0x128,0x158"); //ESTADO DO JOGADOR!
                gridPersonagemMem = m.ReadFloat("ProjectG.exe+00A73E60,0x34,0xB4,0x28,0x14,0x30,0x0,0x7C", "", false);
                terrenoMem = m.ReadInt("ProjectG.exe+00B006E8,0x8,0xC,0xC,0x30,0x0,0x21C,0xAC");
                driverMem = m.ReadByte("ProjectG.exe+A79011");
                spinMem = m.ReadFloat("ProjectG.exe+00A73E60,0x34,0x18,0x10,0x3C,0x30,0x0,0x1C");
                curvaMem = m.ReadFloat("ProjectG.exe+00B006E8,0x8,0xC,0xC,0x40,0x0,0x0,0x18");
                assistXMem = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x70");
                assistZMem = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x78");
                radiusAssistMem = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x7C");
                assistDiametroAtual = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0xA4");
                linhaAssistTotal = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x90");
                linhaAssistPixel = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x9C");
                assistEixoX = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x21C");
                assistEixoY = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x220");
                assistEixoZ = m.ReadFloat("ProjectG.exe+00B006E8,0x1C,0x34,0x24,0x28,0x0,0x0,0x224");
            }
            else
            {
                Console.WriteLine("Nenhum mapa aberto.");
            }

        }
        private static void Gerais()
        {
            Memorias();
            if (mapa != 0)
            {
                if (AutoLimpar == true)
                    Console.Clear();
                //Players Status
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine(" _______________________\t");
                Console.WriteLine("|     Player Status     |");
                Console.WriteLine(" _______________________\t");
                Console.WriteLine("| Forca: \t" + " \t|");
                Console.WriteLine("| Controle: \t" + " \t|");
                Console.WriteLine("| Precisao: \t" + " \t|");
                Console.WriteLine("| Spin: \t" + spinMax + "\t|");
                Console.WriteLine("| Curva: \t" + curvaMax + "\t|");
                Console.WriteLine("|_______________________|");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
                //Dados Gerais
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Distancia: \t" + f.Distancia(pin1Mem, tee1Mem, pin3Mem, tee3Mem) + "y");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Altura: \t" + f.Altura(tee2Mem, pin2Mem));
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.WriteLine("Vento: \t\t" + ventoMem);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Angulo: \t" + Math.Round(f.anguloDegree(senoAnguloMem, cosAnguloMem), 2));
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Quebra: \t" + f.quebraBola(senoBolaMem, cosBolaMem, eixoxMem, eixoyMem));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Terreno: \t" + f.Terreno(terrenoMem) + "%");
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Spin: \t\t" + Math.Round(spinMem, 2) + "/" + spinMax.ToString());
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Curva: \t\t" + Math.Round(curvaMem, 2) + "/" + curvaMax.ToString());
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("PB: \t\t" + f.pbTirado(pin1Mem, tee1Mem, pin3Mem, tee3Mem, gridPersonagemMem));
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Driver: \t" + f.Driver(driverMem, linhaXMem, tee1Mem, linhaZMem, tee3Mem));
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void Hole()
        {
            Memorias();
            if (mapa != 0)
            {
                if (AutoLimpar == true)
                    Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Posição do Hole X: " + Math.Round(pin1Mem, 4));
                Console.WriteLine("Posição do Hole Y: " + Math.Round(pin2Mem, 4));
                Console.WriteLine("Posição do Hole Z: " + Math.Round(pin3Mem, 4));
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void Bola()
        {
            Memorias();
            if (mapa != 0)
            {
                if (AutoLimpar == true)
                    Console.Clear();
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("Posição da Bola X: " + Math.Round(tee1Mem, 4));
                Console.WriteLine("Posição da Bola Y: " + Math.Round(tee2Mem, 4));
                Console.WriteLine("Posição da Bola Z: " + Math.Round(tee3Mem, 4));
                Console.WriteLine("Eixo X:\t\t   " + Math.Round(eixoxMem, 4));
                Console.WriteLine("Eixo Y:\t\t   " + Math.Round(eixoxMem, 4));
                Console.WriteLine("Radianos:\t   " + Math.Round(radianseixoMem, 4));
                Console.WriteLine("Sen: \t\t   " + Math.Round(senoBolaMem, 4));
                Console.WriteLine("Cos: \t\t   " + Math.Round(cosBolaMem, 4));
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        private static void Assist()
        {
            Memorias();
            if (mapa != 0)
            {
                if (AutoLimpar == true)
                    Console.Clear();
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine("Linha eixo [X]: \t" + Math.Round(assistEixoX, 2));
                Console.WriteLine("Linha eixo [Y]: \t" + Math.Round(assistEixoY, 2));
                Console.WriteLine("Linha eixo [Z]: \t" + Math.Round(assistEixoZ, 2));
                Console.WriteLine("Deslocamento eixo [X]: \t" + Math.Round(assistXMem, 2));
                Console.WriteLine("Deslocamento eixo [Z]: \t" + Math.Round(assistZMem, 2));
                Console.WriteLine("Diametro Assist: \t" + Math.Round(assistDiametroAtual, 2));
                Console.WriteLine("Radius Assist: \t\t" + Math.Round(radiusAssistMem, 2));
                Console.WriteLine("Linha Total: \t\t" + Math.Round(linhaAssistTotal, 2) + "%");
                Console.WriteLine("Linha Pixel: \t\t" + Math.Round(linhaAssistPixel, 2) + "px");
                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
        //KEY WINDOWS
        private static string getKey()
        {
            string text = "SOFTWARE\\Microsoft\\Cryptography";
            string text2 = "MachineGuid";
            using (RegistryKey registrykey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            {
                using (RegistryKey registryKey2 = registrykey.OpenSubKey(text))
                {
                    bool flag = registryKey2 == null;
                    if (flag)
                    {
                        throw new KeyNotFoundException(String.Format("Key not found {0}", text));
                    }
                    object value = registryKey2?.GetValue(text2);
                    bool flag2 = value == null;
                    if (flag2)
                    {
                        throw new IndexOutOfRangeException(String.Format("Index not found: {^0}", text2));
                    }
                    return value.ToString();
                    
                }
            }
        }
    }
}
