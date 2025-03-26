using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Bizsol_ESMS_API.Controllers.Master
{
    [Route("api/[controller]")]
    [ApiController]
    public class BackUpController : Controller
    {

        [HttpGet]
        [Route("GetDesignationMasterList")]
        public async Task<IActionResult> GetDesignationMasterList([FromBody] tblBackupModel BackupModel)
        {
            //string backupFolder = @"C:\Users\Meetu\OneDrive\Desktop\Daily Backup MySql DataBase";
            string backupFolder = BackupModel.BackupFolderPath;
            string backupFile = Path.Combine(backupFolder, DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss") + "-backup.sql");

            try
            {
                Directory.CreateDirectory(backupFolder);

                //string server = "220.158.165.98";
                //string port = "65448";
                //string database = "bizsolesms_test";
                //string user = "sa";
                //string password = "biz1981";
                string server = BackupModel.Server.Trim();
                string port = BackupModel.Port.Trim();
                string database = BackupModel.Database.Trim();
                string user = BackupModel.User.Trim();
                string password = BackupModel.Password.Trim();

                string mysqldumpPath = @"C:\Program Files\MySQL\MySQL Server 8.0\bin\mysqldump.exe";
                string command = $"\"{mysqldumpPath}\" --host={server} --port={port} --user={user} --password={password} " +
                                 $"--routines --events --triggers --databases {database} > \"{backupFile}\"";

                ExecuteCommand(command);

                return Ok(new { message = "Backup completed successfully!", filePath = backupFile });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Backup failed", details = ex.Message });
            }
        }
        private void ExecuteCommand(string command)
        {
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(psi))
            {
                using (StreamWriter sw = process.StandardInput)
                {
                    if (sw.BaseStream.CanWrite)
                    {
                        sw.WriteLine(command);
                    }
                }

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine("CMD Output: " + output);
                Console.WriteLine("CMD Error: " + error);
            }
        }
    }
}
