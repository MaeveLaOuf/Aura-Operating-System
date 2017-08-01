﻿using System;
using System.Collections.Generic;
using System.Text;
using Alve_OS.System.Users;
using System.IO;
using Sys = Cosmos.System;
using L = Alve_OS.System.Translation;

namespace Alve_OS.System
{
    class Setup
    {

        /// <summary>
        /// Vérifie que l'installation d'Alve est complète
        /// </summary>
        public void SetupVerifyCompleted()
        {
            if (!File.Exists(@"0:\System\setup"))
            {
                StartSetup();
            }

        }


        /// <summary>
        /// Void appelé lors d'une erreur lors de l'installation
        /// </summary>
        public void ErrorDuringSetup(string error = "Setup Error")
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  Error during installation");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("  Error: " + error);
            Console.WriteLine();
        }


        /// <summary>
        /// Démarre l'installation
        /// </summary>
        public void StartSetup()
        {
            Step1();
        }

        private void Step1()
        {
            //creating folders

            try
            {
                if (!Directory.Exists(@"0:\System"))
                {
                    Directory.CreateDirectory(@"0:\System");
                }

                if (!Directory.Exists(@"0:\System\Users"))
                {
                    Directory.CreateDirectory(@"0:\System\Users");
                }

                if (!Directory.Exists(@"0:\Users"))
                {
                    Directory.CreateDirectory(@"0:\Users");
                }

                if (!Directory.Exists(@"0:\Users\root"))
                {
                    Directory.CreateDirectory(@"0:\Users\root");
                }

                if ((Directory.Exists(@"0:\System")) && (Directory.Exists(@"0:\System\Users")) && (Directory.Exists(@"0:\Users")) && (Directory.Exists(@"0:\Users\root")))
                {
                    Step2();
                }
            }
            catch
            {
                ErrorDuringSetup("Creating system folders");
            }
        }


        /// <summary>
        /// Méthode appelé pour créer 'root' et proposer la création d'un utilisateur de base.
        /// </summary>
        private void Step2()
        {
            try
            {
                if (!File.Exists(@"0:\System\Users\root.usr"))
                {
                    File.Create(@"0:\System\Users\root.usr");

                    try
                    {
                        if (File.Exists(@"0:\System\Users\root.usr"))
                        {
                            string text = "root";
                            File.WriteAllText(@"0:\System\Users\root.usr", text + "|admin");

                            Step3();
                        }
                    }
                    catch
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("[ERROR] Root writing file");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            catch
            {
                ErrorDuringSetup("Creating root");
            }
        }


        /// <summary>
        /// Demande de la disposition du clavier
        /// </summary>
        private void Step3()
        {
            Console.Clear();
            Console.WriteLine();
            L.Text.Display("languageask");
            Console.WriteLine();
            L.Text.Display("availablelanguage");
            Console.WriteLine();
            Console.Write("> ");
            var cmd = Console.ReadLine();
            if ((cmd.Equals("en_US")) || cmd.Equals("en-US"))
            {
                Kernel.langSelected = "en_US";
                L.Keyboard.Init();

                File.Create(@"0:\System\lang");

                if (File.Exists(@"0:\System\lang"))
                {
                    File.WriteAllText(@"0:\System\lang", Kernel.langSelected);
                }
                else
                {
                    ErrorDuringSetup("Lang Register");
                }

                Step4();
            }
            else if ((cmd.Equals("fr_FR")) || cmd.Equals("fr-FR"))
            {
                Kernel.langSelected = "fr_FR";
                L.Keyboard.Init();

                File.Create(@"0:\System\lang");

                if (File.Exists(@"0:\System\lang"))
                {
                    File.WriteAllText(@"0:\System\lang", Kernel.langSelected);
                }
                else
                {
                    ErrorDuringSetup("Lang Register");
                }

                Step4();
            }
            else
            {
                L.Text.Display("unknownlanguage");
                L.Text.Display("availablelanguage");
            }
        }

        private void Step4()
        {
            try
            {

                Console.Clear();

                Console.WriteLine();
                L.Text.Display("chooseyourusername");
                Console.WriteLine();
                Console.Write("> ");
                var username = Console.ReadLine();

                if (File.Exists(@"0:\Users\" + username + ".usr"))
                {
                    L.Text.Display("alreadyuser");

                }
                else
                {
                    Console.WriteLine();
                    L.Text.Display("alreadyuser", username);
                    Console.WriteLine();
                    Console.Write("> ");

                    Console.ForegroundColor = ConsoleColor.Black;
                    var password = Console.ReadLine();
                    Console.ForegroundColor = ConsoleColor.White;

                    Console.WriteLine();

                    File.Create(@"0:\System\Users\" + username + ".usr");
                    Directory.CreateDirectory(@"0:\Users\" + username);

                    if (File.Exists(@"0:\System\Users\" + username + ".usr"))
                    {
                        File.WriteAllText(@"0:\System\Users\" + username + ".usr", password + "|standard");
                        
                        if (Directory.Exists(@"0:\System\System"))
                        {
                            Step5();
                        }
                    }
                    else
                    {
                        ErrorDuringSetup("Creating user");
                    }
                }

            }
            catch
            {
                ErrorDuringSetup("Creating user");
            }
        }

        private void Step5()
        {
            File.Create(@"0:\System\setup");
            Console.Clear();
        }

    }
}
