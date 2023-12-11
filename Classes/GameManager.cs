using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.VisualBasic;
using System.IO;

namespace GolfGame.Classes
{

    struct OptionsValues
    {
        public OptionsValues()
        {
            highScore = 0;
            frictionValue = 3f;
            hitPower = 8f;
        }

        public int highScore { get; set; }

        public float frictionValue { get; set; }

        public float hitPower { get; set; }
    }
    class GameManager
    {
        private static GameManager? instance = null;

        //Esta estrutura guarda os valores das opções 
        public OptionsValues optionsValues = new OptionsValues();


        //Declarar a class Gameplay/Game
        //->

        public static GameManager Instance
        {
            get
            {
                if (instance == null)
                {
                    throw new InvalidOperationException("Objeto não criado. Criar com GetManager(.)");

                }
                return instance;
            }

        }

        /// <summary>
        /// Retorna o Game Manager
        /// </summary>
        /// <returns></returns>
        public static GameManager GetManager()
        {
            if (instance == null)
            {
                instance = new GameManager();
            }

            return instance;
        }

        /// <summary>
        /// Esta função irá guardar as opçoes submetidas na pagina das definições
        /// </summary>
        public async Task  SaveOptions()
        {

            string currentPath = Directory.GetCurrentDirectory();

            string fileName = "optionsValues.json";
            string optionsFilePath = String.Concat(currentPath, "\\", fileName);

            try
            {
                //abro o ficheiro
                //Não é preciso fechar a Stream pq no fim do Scope do Using é fechada automaticamente
                using (FileStream createStream = new FileStream(optionsFilePath, FileMode.Open, FileAccess.Write))
                {

                    // Torna as opções em Json
                    string jsonData = JsonSerializer.Serialize(optionsValues, new JsonSerializerOptions { WriteIndented = true });

                    // Vejo o tamnho do ficheiro e escrevo o ficheiro
                    byte[] jsonDataBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
                    await createStream.WriteAsync(jsonDataBytes, 0, jsonDataBytes.Length);
                }

                Console.WriteLine($"Ficheiro alterado com sucesso: {optionsFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro a alterar o ficheiro: {ex.Message}");
            }
        }



        /// <summary>
        /// Esta função irá buscar os dados guardados se estes existirem
        /// </summary>
        public async Task LoadOptions()
        {
            string currentPath = Directory.GetCurrentDirectory();

            string fileName = "optionsValues.json";
            string optionsFilePath = String.Concat(currentPath, "\\", fileName);


            try
            {
                if (!File.Exists(optionsFilePath))
                {
                    //O ficheiro não existe

                    try
                    {
                        //Cria o ficheiro
                        //Não é preciso fechar a Stream pq no fim do Scope do Using é fechada automaticamente
                        using (FileStream createStream = new FileStream(optionsFilePath, FileMode.Create, FileAccess.Write))
                        {

                            // Torna as opções em Json
                            string jsonData = JsonSerializer.Serialize(optionsValues, new JsonSerializerOptions { WriteIndented = true });

                            // Vejo o tamnho do ficheiro e escrevo o ficheiro
                            byte[] jsonDataBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
                            await createStream.WriteAsync(jsonDataBytes, 0, jsonDataBytes.Length);
                        }

                        Console.WriteLine($"Ficheiro criado com sucesso: {optionsFilePath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro a criar o ficheiro: {ex.Message}");
                    }
                }
                else
                {
                    // O ficheiro já existe logo vamos processar-lo

                    try
                    {
                        //Abro o ficheiro
                        using (FileStream readStream = new FileStream(optionsFilePath, FileMode.Open, FileAccess.Read))
                        {

                            OptionsValues options = await JsonSerializer.DeserializeAsync<OptionsValues>(readStream);

                            this.optionsValues = options;

                            Console.WriteLine(options.ToString());
                        }


                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erro a ler o ficheiro: {ex.Message}");
                        OptionsErrorHandler();

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro a verificar a existencia do ficheiro: {ex.Message}");
            }
        }

        /// <summary>
        /// Esta função vai renicializar o ficheiro dos dados das opçoes e os os dados também
        /// </summary>
        void OptionsErrorHandler()
        {
            const int highScoreDefault = 0;
            const float frictionDefault = 3f;
            const float hitPowerDefault = 8f;

            optionsValues.highScore = highScoreDefault;
            optionsValues.frictionValue = frictionDefault;
            optionsValues.hitPower = hitPowerDefault;

        }

    }
}

