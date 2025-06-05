using Model357App.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Model357App
{
    public class AskCrud
    {
        private readonly string dataFolderPath;
        private readonly string asksFilePath;
        private const string SEPARATOR = "<<";

        public AskCrud()
        {
            try
            {
                string projectPath = Application.StartupPath;
                dataFolderPath = Path.Combine(projectPath, "DataFiles");
                asksFilePath = Path.Combine(dataFolderPath, "asks.txt");

                CreateDataFiles();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al inicializar las rutas: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateDataFiles()
        {
            try
            {
                if (!Directory.Exists(dataFolderPath))
                    Directory.CreateDirectory(dataFolderPath);

                if (!File.Exists(asksFilePath))
                    File.Create(asksFilePath).Close();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al crear archivos: {ex.Message}");
            }
        }

        private int GetNextId()
        {
            try
            {
                if (!File.Exists(asksFilePath) || new FileInfo(asksFilePath).Length == 0)
                    return 1;

                var lastLine = File.ReadAllLines(asksFilePath).LastOrDefault();
                if (string.IsNullOrEmpty(lastLine))
                    return 1;

                string[] datos = lastLine.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                if (int.TryParse(datos[0], out int ultimoId))
                    return ultimoId + 1;

                return 1;
            }
            catch (Exception)
            {
                return 1;
            }
        }

        public List<Question> GetQuestions()
        {
            List<Question> questions = new List<Question>();
            try
            {
                if (!File.Exists(asksFilePath))
                {
                    MessageBox.Show("No hay preguntas disponibles.",
                        "Información",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    return questions;
                }

                string[] lines = File.ReadAllLines(asksFilePath);
                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] parts = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                        if (parts.Length >= 3) // Asegurarse de que tengamos al menos ID, pregunta y tipo
                        {
                            // parts[0] es ID, parts[1] es la pregunta, parts[2] es el tipo
                            questions.Add(new Question(parts[0], parts[1], parts[2]));
                        }
                    }
                }

                return questions;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar las preguntas: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return questions;
            }
        }

        public void CreateAsk(string question, string type, List<string> options = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(question) || string.IsNullOrWhiteSpace(type))
                {
                    MessageBox.Show("La pregunta y el tipo son obligatorios.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int id = GetNextId();
                string optionsStr = "";

                if (options != null && options.Count > 0)
                {
                    optionsStr = string.Join("|", options);
                }

                // Formato: ID<<PREGUNTA<<TIPO<<OPCIONES(separadas por |)
                string askLine = $"{id}{SEPARATOR}{question}{SEPARATOR}{type}{SEPARATOR}{optionsStr}";

                File.AppendAllText(asksFilePath, askLine + Environment.NewLine);
                MessageBox.Show("Pregunta guardada exitosamente.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al crear la pregunta: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void UpdateAsk(string id, string question, string type, List<string> options = null)
        {
            try
            {
                if (!File.Exists(asksFilePath))
                {
                    MessageBox.Show("No hay preguntas registradas.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] lines = File.ReadAllLines(asksFilePath);
                List<string> newLines = new List<string>();
                bool found = false;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                        if (data[0] == id)
                        {
                            string optionsStr = options != null ? string.Join("|", options) : "";
                            newLines.Add($"{id}{SEPARATOR}{question}{SEPARATOR}{type}{SEPARATOR}{optionsStr}");
                            found = true;
                        }
                        else
                        {
                            newLines.Add(line);
                        }
                    }
                }

                if (!found)
                {
                    MessageBox.Show("No se encontró la pregunta especificada.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                File.WriteAllLines(asksFilePath, newLines);
                MessageBox.Show("Pregunta actualizada exitosamente.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la pregunta: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void DeleteAsk(string id)
        {
            try
            {
                if (!File.Exists(asksFilePath))
                {
                    MessageBox.Show("No hay preguntas registradas.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string[] lines = File.ReadAllLines(asksFilePath);
                List<string> newLines = new List<string>();
                bool found = false;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                        if (data[0] != id)
                        {
                            newLines.Add(line);
                        }
                        else
                        {
                            found = true;
                        }
                    }
                }

                if (!found)
                {
                    MessageBox.Show("No se encontró la pregunta especificada.", 
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                File.WriteAllLines(asksFilePath, newLines);
                MessageBox.Show("Pregunta eliminada exitosamente.", 
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al eliminar la pregunta: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ListAsks(DataGridView dataGridView)
        {
            try
            {
                if (!File.Exists(asksFilePath))
                {
                    MessageBox.Show("No hay preguntas registradas.", 
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string[] lines = File.ReadAllLines(asksFilePath);

                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();

                // Configurar columnas
                dataGridView.Columns.Add("Id", "ID");
                dataGridView.Columns.Add("Question", "Pregunta");
                dataGridView.Columns.Add("Type", "Tipo");
                dataGridView.Columns.Add("Answer", "Respuestas");

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                        if (data.Length >= 4)
                        {
                            dataGridView.Rows.Add(
                                data[0],                // ID
                                data[1],                // Pregunta
                                data[2],                // Tipo
                                data[3].Replace("|", ", ") // Respuestas
                            );
                        }
                    }
                }

                dataGridView.AutoResizeColumns();

                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No hay preguntas registradas.", 
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar las preguntas: {ex.Message}", 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataGridView ListAsks()
        {
            try
            {
                // Crear el DataGridView dinámico
                DataGridView dataGridView = DinamicControls.CreateDataGrid("dataGridAsks", 0, 0, 0, 0);
                dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
                dataGridView.AllowUserToResizeColumns = true;
                dataGridView.AllowUserToResizeRows = false;
                dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
                dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
                dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                if (!File.Exists(asksFilePath))
                {
                    MessageBox.Show("No hay preguntas registradas.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return dataGridView;
                }

                string[] lines = File.ReadAllLines(asksFilePath);
                
                // Configurar columnas
                dataGridView.Columns.Add("Id", "ID");
                dataGridView.Columns.Add("Question", "Pregunta");
                dataGridView.Columns.Add("Type", "Tipo");

                // Configurar proporciones de las columnas
                dataGridView.Columns["Id"].FillWeight = 10; // 10% del espacio
                dataGridView.Columns["Question"].FillWeight = 70; // 70% del espacio
                dataGridView.Columns["Type"].FillWeight = 20; // 20% del espacio

                // Establecer propiedades mínimas de ancho para las columnas
                dataGridView.Columns["Id"].MinimumWidth = 50;
                dataGridView.Columns["Question"].MinimumWidth = 200;
                dataGridView.Columns["Type"].MinimumWidth = 100;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                        if (data.Length >= 3)
                        {
                            dataGridView.Rows.Add(
                                data[0],                // ID
                                data[1],                // Pregunta
                                data[2]                // Tipo
                                //data[3].Replace("|", ", ") // Respuestas
                            );
                        }
                    }
                }

                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No hay preguntas registradas.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                return dataGridView;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al listar las preguntas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        public void SaveUserResponses(string fullName, string program, Dictionary<string, string> responses)
        {
            try
            {
                // Crear carpeta de respuestas si no existe
                string responsesFolderPath = Path.Combine(dataFolderPath, "GraduateResponses");
                if (!Directory.Exists(responsesFolderPath))
                {
                    Directory.CreateDirectory(responsesFolderPath);
                }

                // Crear archivo para el usuario específico
                string userResponseFile = Path.Combine(responsesFolderPath, $"{fullName.Replace(" ","")}_responses.txt");

                // Obtener fecha y hora actual
                string timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                // Crear lista de líneas para escribir en el archivo
                List<string> linesToWrite = new List<string>
                {
                    $"Programa: {program}",
                    $"Nombre completo: {fullName}",
                    $"Fecha de respuesta: {timestamp}",
                    "----------------------------------------"
                };

                // Obtener todas las preguntas del archivo de preguntas
                Dictionary<string, string> questions = new Dictionary<string, string>();
                if (File.Exists(asksFilePath))
                {
                    foreach (string line in File.ReadAllLines(asksFilePath))
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] data = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                            if (data.Length >= 2)
                            {
                                questions[data[0]] = data[1]; // ID -> Pregunta
                            }
                        }
                    }
                }

                // Agregar cada pregunta y su respuesta
                foreach (var response in responses)
                {
                    if (questions.ContainsKey(response.Key))
                    {
                        linesToWrite.Add($"Pregunta: {questions[response.Key]}");
                        linesToWrite.Add($"Respuesta: {response.Value}");
                        linesToWrite.Add("----------------------------------------");
                    }
                }

                // Escribir al archivo
                File.WriteAllLines(userResponseFile, linesToWrite);

                MessageBox.Show("Respuestas guardadas exitosamente.",
                    "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar las respuestas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void RefreshDataGridView(DataGridView dataGridView)
        {
            try
            {
                if (dataGridView == null)
                    throw new ArgumentNullException(nameof(dataGridView));

                if (!File.Exists(asksFilePath))
                {
                    MessageBox.Show("No hay preguntas registradas.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string[] lines = File.ReadAllLines(asksFilePath);

                dataGridView.DataSource = null;
                dataGridView.Columns.Clear();
                dataGridView.Rows.Clear();

                // Configurar columnas
                dataGridView.Columns.Add("Id", "ID");
                dataGridView.Columns.Add("Question", "Pregunta");
                dataGridView.Columns.Add("Type", "Tipo");
                dataGridView.Columns.Add("Answer", "Respuestas");

                // Configurar anchos de columna
                dataGridView.Columns["Id"].Width = 50;
                dataGridView.Columns["Question"].Width = 500 - dataGridView.Width;
                dataGridView.Columns["Type"].Width = 150;
                dataGridView.Columns["Answer"].Width = 300;

                foreach (string line in lines)
                {
                    if (!string.IsNullOrEmpty(line))
                    {
                        string[] data = line.Split(new[] { SEPARATOR }, StringSplitOptions.None);
                        if (data.Length >= 4)
                        {
                            dataGridView.Rows.Add(
                                data[0],                // ID
                                data[1],                // Pregunta
                                data[2],                // Tipo
                                data[3].Replace("|", ", ") // Respuestas
                            );
                        }
                    }
                }

                if (dataGridView.Rows.Count == 0)
                {
                    MessageBox.Show("No hay preguntas registradas.",
                        "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al actualizar la lista de preguntas: {ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}