// Updated C# script for Tabular Editor 2 to export all measure descriptions (including table names) to a CSV file.
// This version prompts the user to select the save location using a SaveFileDialog.

// Import necessary namespaces
#r "System.Windows.Forms"
using System.Windows.Forms;
using Microsoft.Win32;
using System.Text;
using System.Windows;
using System.Data;

// Define the script to export measure descriptions
var sb = new StringBuilder();
// Add header row to the CSV
sb.AppendLine("\"Table\",\"Measure\",\"Description\"");

// Iterate through all measures in the model, ordered by table name and measure name
foreach (var m in Model.AllMeasures.OrderBy(m => m.Table.Name).ThenBy(m => m.Name))
{
    // Get the table name, measure name, and description
    string table = m.Table.Name;
    string name = m.Name;
    string desc = m.Description ?? "";  // Handle null descriptions as empty

    // Escape quotes in each field
    table = table.Replace("\"", "\"\"");
    name = name.Replace("\"", "\"\"");
    desc = desc.Replace("\"", "\"\"");
    // Append the formatted line to the StringBuilder
    sb.AppendLine("\"" + table + "\",\"" + name + "\",\"" + desc + "\"");
}

// Prompt user to select a file location to save the CSV
var dialog = new SaveFileDialog();
// Set the initial directory to the user's documents folder
dialog.Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*";
// Set the default file name
dialog.FileName = "measures_descriptions.csv";

// Show the save file dialog
dialog.Title = "Export Measure Descriptions";
if (dialog.ShowDialog() == DialogResult.OK)
{
    // Write the CSV content to the selected file
    System.IO.File.WriteAllText(dialog.FileName, sb.ToString());
    // Notify the user of successful export
    MessageBox.Show("Measure descriptions exported successfully to: " + dialog.FileName, "Export Complete", MessageBoxButton.OK, MessageBoxImage.Information);
}
