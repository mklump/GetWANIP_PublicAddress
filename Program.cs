using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace GetWANIP
{
    class Program
    {
        static int Main(string[] args)
        {
            int errorCode = -1;
            string strIP = "", projPath = "", dirMSTest = "";
            foreach (string arg in args)
            {
                if (arg != "")
                    Console.WriteLine("The detected Public/External IP is: " + arg);
                strIP += arg;
            }
            System.Net.IPAddress theAddress = null;
            try
            {
                if (15 < strIP.Length || true != System.Net.IPAddress.TryParse(strIP, out theAddress))
                    throw new ApplicationException("The specified arguement is not an IP address.\n" +
                        "Please only provide a valid IP address.");
                else
                {
                    if (!Directory.Exists(Environment.GetEnvironmentVariable(
                        "VS2005PROJDIR", EnvironmentVariableTarget.Machine)) ||
                        !File.Exists(Environment.GetEnvironmentVariable(
                        "MSTest", EnvironmentVariableTarget.Machine) +
                        "MSTest.exe"))
                    {
                        string[] strDirs = Directory.GetDirectories(Environment.GetEnvironmentVariable("USERPROFILE",
                            EnvironmentVariableTarget.Process) + @"\Documents\", "*Visual Studio*", SearchOption.TopDirectoryOnly);
                        foreach (string str in strDirs)
                        {
                            string[] paths = Directory.GetDirectories(str, "GetWANIP", SearchOption.AllDirectories);
                            foreach (string subPath in paths)
                                if (true == subPath.EndsWith("Projects\\GetWANIP"))
                                    projPath = subPath;
                            if (string.Empty != projPath)
                                break;
                        }
                        projPath = projPath.Remove(projPath.Length - "GetWANIP".Length);
                        string[] dirsMSTest = Directory.GetDirectories(Environment.GetEnvironmentVariable("ProgramFiles(x86)",
                            EnvironmentVariableTarget.Process), "*Visual Studio*", SearchOption.TopDirectoryOnly);
                        foreach (string paths in dirsMSTest)
                        {
                            string[] subpaths = Directory.GetFiles(paths, "MSTest.exe", SearchOption.AllDirectories);
                            if (subpaths.Length > 0)
                                dirMSTest = subpaths[0];
                            if (true == dirMSTest.EndsWith("MSTest.exe"))
                                break;
                        }
                        dirMSTest = dirMSTest.Remove(dirMSTest.Length - "MSTest.exe".Length);
                        Environment.SetEnvironmentVariable("MSTest", dirMSTest, EnvironmentVariableTarget.Machine);
                        Environment.SetEnvironmentVariable("VS2005PROJDIR", projPath, EnvironmentVariableTarget.Machine);
                    }
                    // Write IP Address to machine environment variable
                    Environment.SetEnvironmentVariable("USERDNSDOMAINIP",
                        strIP, EnvironmentVariableTarget.Machine);
                    // Write IP Address to database PersonalInformation
                    SqlConnection conn = new SqlConnection();
                    conn.ConnectionString = "Data Source=MATT-SERVER;Initial Catalog=MySecureParameters;Integrated Security=True";
                    conn.Open();
                    SqlCommand command = conn.CreateCommand();
                    command.CommandText = "spUpdateIP";
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add(new SqlParameter("@username", SqlDbType.NVarChar, 50,
                        ParameterDirection.Input, false, ((byte)10), ((byte)0), "", DataRowVersion.Current,
                        /*VALUE*/ "mklump"));
                    command.Parameters.Add(new SqlParameter("@ipaddress", SqlDbType.NVarChar, 50,
                        ParameterDirection.Input, false, ((byte)10), ((byte)0), "", DataRowVersion.Current,
                        /*VALUE*/ strIP));
                    int rowsAffected = command.ExecuteNonQuery();
                    Console.WriteLine("Successfully executed spUpdateIP stored procedure with: (" +
                        rowsAffected + " row(s) affected).");
                    Console.WriteLine("\nThe number of rows affected by the IP update is: " + rowsAffected);
                    conn.Close();
                    errorCode = 0;
                }
            }
            catch (System.ApplicationException e)
            {
                errorCode = -1;
                Console.WriteLine(e.ToString());
            }
            if (errorCode == 0)
                Console.WriteLine("The USERDNSDOMAINIP environment variable update was successful.");
            Process msTest = new Process();
            msTest.StartInfo.UseShellExecute = true;
            msTest.StartInfo.CreateNoWindow = false;
            string startPath = msTest.StartInfo.WorkingDirectory = Environment.GetEnvironmentVariable(
                "MSTest", EnvironmentVariableTarget.Machine);
            msTest.StartInfo.FileName = startPath + "MSTest.exe";
            // Specifies to MSTest where to save the test results for the HomeDNSUpdate webtest.
            string saveTestResultFile = Environment.GetEnvironmentVariable("VS2005PROJDIR",
                EnvironmentVariableTarget.Machine) + "HomeDNSUpdate";
            try
            {
                // Check for existance of VS2005 old test result files, if yes, delete them.
                List<string> testResultFiles = new List<string>(Directory.GetFiles(saveTestResultFile, "*.webtestresult", SearchOption.TopDirectoryOnly));
                foreach (string resultFile in testResultFiles)
                {
                    if (true == File.Exists(resultFile))
                        File.Delete(resultFile);
                }
                // Get other VS2012 saved result file locations and delete them.
                string vs2012ResultDirs = Environment.GetEnvironmentVariable("VS2005PROJDIR", EnvironmentVariableTarget.Machine);
                testResultFiles.AddRange(Directory.GetDirectories(vs2012ResultDirs, "MATT-SERVER*", SearchOption.TopDirectoryOnly));
                testResultFiles.AddRange(Directory.GetFiles(vs2012ResultDirs, "HomeDNSUpdate*", SearchOption.TopDirectoryOnly));
                foreach (string vs2012ResultDir in testResultFiles)
                {
                    if (true == Directory.Exists(vs2012ResultDir))
                        Directory.Delete(vs2012ResultDir, true);
                    else if (true == File.Exists(vs2012ResultDir))
                        File.Delete(vs2012ResultDir);
                }
                if (true == Directory.Exists(vs2012ResultDirs + "GetWANIP\\TestResults"))
                    Directory.Delete(vs2012ResultDirs + "GetWANIP\\TestResults", true);
                msTest.StartInfo.Arguments = "/testcontainer:\"" +
                    Environment.GetEnvironmentVariable("VS2005PROJDIR",
                    EnvironmentVariableTarget.Machine) +
                    "HomeDNSUpdate\\bin\\debug\\HomeDNSUpdate.dll\"" +
                    " /resultsfile:\"" + saveTestResultFile + "\"";
                msTest.Start();
                msTest.WaitForExit();
                errorCode = 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                errorCode = -1;
                throw e;
            }
            Console.WriteLine("GetWAPIP application returned with error code: " + errorCode.ToString());
            return errorCode;
        }
    }
}