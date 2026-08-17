using Bizsol_ESMS_API.Interface;
using Bizsol_ESMS_API.Model;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Net;

namespace Bizsol_ESMS_API.Service
{
    public class TicketMasterService : ITicketMaster
    {
        private readonly IConfiguration _configuration;

        public TicketMasterService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<CreateTicketResponse> CreateTicket( BizsolESMSConnectionDetails bizsolESMSConnectionDetails,CreateTicketRequest request, IEnumerable<IFormFile>? files)
        {
            if (request == null)
            {
                return Fail("Request data is required.");
            }

            if (string.IsNullOrWhiteSpace(request.CompanyCode))
            {
                return Fail("Company code is required.");
            }

            if (string.IsNullOrWhiteSpace(request.ProjectClient))
            {
                return Fail("Project client is required.");
            }

            if (string.IsNullOrWhiteSpace(request.RaisedBy))
            {
                return Fail("Raised by is required.");
            }

            if (string.IsNullOrWhiteSpace(request.ContactEMail))
            {
                return Fail("Contact email is required.");
            }

            if (string.IsNullOrWhiteSpace(request.Description))
            {
                return Fail("Description is required.");
            }

            if (!DateTime.TryParseExact(request.LogDate?.Trim(),"yyyy-MM-dd",CultureInfo.InvariantCulture,DateTimeStyles.None,out DateTime logDate))
            {
                return Fail("Please provide a valid log date in yyyy-MM-dd format (e.g. 2026-04-23).");
            }

            string companyConnectionString = GetCompanyConnectionString(request.CompanyCode.Trim());

            if (string.IsNullOrWhiteSpace(companyConnectionString) && !string.IsNullOrWhiteSpace(bizsolESMSConnectionDetails?.DefaultSQL))
            {
                companyConnectionString = bizsolESMSConnectionDetails.DefaultSQL;
            }

            if (string.IsNullOrWhiteSpace(companyConnectionString))
            {
                return Fail("Error To Fetch Connection String");
            }

            long.TryParse(request.TicketNo, out long ticketNo);
            int.TryParse(request.TicketType, out int ticketType);
            int.TryParse(request.Priority, out int priority);
            int.TryParse(request.Module, out int module);
            int.TryParse(request.Source, out int source);
            int.TryParse(request.TestedBy, out int testedBy);
            int.TryParse(request.UserModuleMaster_Code, out int userModuleMasterCode);

            using (var connection = new SqlConnection(companyConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("USP_InsertProject_Master", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;

                    command.Parameters.Add(new SqlParameter("@TicketType", ticketType));
                    command.Parameters.Add(new SqlParameter("@TicketNo", ticketNo));
                    command.Parameters.Add(new SqlParameter("@Priority", priority));
                    command.Parameters.Add(new SqlParameter("@ProjectClient", request.ProjectClient.Trim()));
                    command.Parameters.Add(new SqlParameter("@LogDate", logDate));
                    command.Parameters.Add(new SqlParameter("@Module", module));
                    command.Parameters.Add(new SqlParameter("@RaisedBy", request.RaisedBy.Trim()));
                    command.Parameters.Add(new SqlParameter("@ContactNo", request.ContactNo ?? ""));
                    command.Parameters.Add(new SqlParameter("@ContactEMail", request.ContactEMail.Trim()));
                    command.Parameters.Add(new SqlParameter("@Source", source));
                    command.Parameters.Add(new SqlParameter("@Description", request.Description.Trim()));
                    command.Parameters.Add(new SqlParameter("@PriorityText", ""));
                    command.Parameters.Add(new SqlParameter("@ModuleText", ""));
                    command.Parameters.Add(new SqlParameter("@SourceText", ""));
                    command.Parameters.Add(new SqlParameter("@CreateTicketBy", request.CreateTicketBy));
                    command.Parameters.Add(new SqlParameter("@TestedBy", testedBy));
                    command.Parameters.Add(new SqlParameter("@UserModuleMaster_Code", userModuleMasterCode));

                    var uidCode = new SqlParameter("@UIDCode", SqlDbType.BigInt)
                    {
                        Direction = ParameterDirection.Output
                    };
                    var autoCode = new SqlParameter("@AutoCode", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };

                    command.Parameters.Add(uidCode);
                    command.Parameters.Add(autoCode);

                    int result = await command.ExecuteNonQueryAsync();

                    if (result <= 0 || autoCode.Value == null || autoCode.Value == DBNull.Value)
                    {
                        return Fail("Ticket could not be created. A duplicate ticket may already exist.");
                    }

                    int ticketCode = Convert.ToInt32(autoCode.Value);
                    string generatedTicketNo = Convert.ToString(uidCode.Value) ?? "";

                    int attachmentCount = await SaveTicketAttachments(connection,ticketCode,generatedTicketNo,files);

                    bool emailSent = await SendTicketCreatedEmail(connection,request,logDate,generatedTicketNo,ticketCode);

                    bool assigned = false;
                    bool assignEmailSent = false;
                    if (request.Assigned > 0 && request.WorkType > 0)
                    {
                        (assigned, assignEmailSent) = await AssignTicketAfterCreate(connection,request,
                        ticketCode, generatedTicketNo,logDate);
                    }

                    string message;
                    if (emailSent && attachmentCount > 0)
                    {
                        message = "Ticket created, attachments uploaded and email sent successfully.";
                    }
                    else if (emailSent)
                    {
                        message = "Ticket created and email sent successfully.";
                    }
                    else if (attachmentCount > 0)
                    {
                        message = "Ticket created and attachments uploaded successfully, but email could not be sent.";
                    }
                    else
                    {
                        message = "Ticket created successfully, but email could not be sent.";
                    }

                    if (request.Assigned > 0 && request.WorkType > 0)
                    {
                        if (assigned && assignEmailSent)
                        {
                            message += " Ticket assigned and assignment email sent.";
                        }
                        else if (assigned)
                        {
                            message += " Ticket assigned, but assignment email could not be sent.";
                        }
                        else
                        {
                            message += " Ticket could not be assigned.";
                        }
                    }

                    return new CreateTicketResponse
                    {
                        Success = true,
                        Message = message,
                        TicketCode = ticketCode,
                        TicketNo = generatedTicketNo,
                        EmailSent = emailSent,
                        AttachmentCount = attachmentCount,
                        Assigned = assigned,
                        AssignEmailSent = assignEmailSent
                    };
                }
            }
        }
        private async Task<int> SaveTicketAttachments(SqlConnection connection, int ticketCode,string ticketNo, IEnumerable<IFormFile>? files)
        {
            if (files == null)
            {
                return 0;
            }

            var uploadFiles = files
                .Where(f => f != null && f.Length > 0 && !string.IsNullOrWhiteSpace(f.FileName))
                .ToList();

            if (uploadFiles.Count == 0)
            {
                return 0;
            }

            int totalAttachment = await GetExistingAttachmentCount(connection, ticketCode);
            int savedCount = 0;

            foreach (var file in uploadFiles)
            {
                totalAttachment++;

                string extension = Path.GetExtension(file.FileName);
                string fileName = totalAttachment < 10
                    ? ticketNo + "_0" + totalAttachment + extension
                    : ticketNo + "_" + totalAttachment + extension;

                byte[] bytes;
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                if (bytes.Length == 0)
                {
                    continue;
                }

                using (var command = new SqlCommand("USP_InsertCallTicketAttachment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@CallTicketMaster_Code", ticketCode);
                    command.Parameters.AddWithValue("@AttachmentFileName", fileName);

                    var attachmentParam = new SqlParameter("@Attachment", SqlDbType.VarBinary, -1)
                    {
                        Value = bytes
                    };
                    command.Parameters.Add(attachmentParam);

                    int insertResult = await command.ExecuteNonQueryAsync();
                    if (insertResult > 0)
                    {
                        savedCount++;
                    }
                }
            }

            return savedCount;
        }
        private async Task<int> GetExistingAttachmentCount(SqlConnection connection, int ticketCode)
        {
            using (var command = new SqlCommand(
                @"SELECT ISNULL(COUNT(*), 0)
                  FROM CallTicketAttachment
                  WHERE CallTicketMaster_Code = @CallTicketMaster_Code",
                connection))
            {
                command.Parameters.AddWithValue("@CallTicketMaster_Code", ticketCode);
                object? result = await command.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
        }
        private string GetCompanyConnectionString(string companyCode)
        {
            if (string.IsNullOrWhiteSpace(companyCode))
            {
                return string.Empty;
            }

            string? connectionString = _configuration.GetConnectionString("DefaultConnection")
                ?? _configuration.GetConnectionString("DefaultConnectionSQL");

            if (string.IsNullOrWhiteSpace(connectionString))
            {
                return string.Empty;
            }

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT sqladdress, userid, pwd, LoginDatabase FROM BizSolERPLoginDetails WHERE CompanyCode = @CompanyCode";
                using (var command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CompanyCode", companyCode);

                    using (SqlDataReader dr = command.ExecuteReader())
                    {
                        if (!dr.Read())
                        {
                            return string.Empty;
                        }

                        string sqlAddress = dr["sqladdress"]?.ToString() ?? "";
                        string userId = dr["userid"]?.ToString() ?? "";
                        string password = dr["pwd"]?.ToString() ?? "";
                        string loginDatabase = dr["LoginDatabase"]?.ToString() ?? "";

                        if (string.IsNullOrWhiteSpace(sqlAddress) ||
                            string.IsNullOrWhiteSpace(userId) ||
                            string.IsNullOrWhiteSpace(loginDatabase))
                        {
                            return string.Empty;
                        }

                        return
                            "Data Source=" + sqlAddress +
                            ";Connection Timeout=0;Persist Security Info=true;Initial Catalog=" + loginDatabase +
                            ";User ID=" + userId +
                            ";pwd=" + password +
                            ";Packet Size=32000;TrustServerCertificate=True;";
                    }
                }
            }
        }
        private async Task<bool> SendTicketCreatedEmail(SqlConnection connection,CreateTicketRequest request,DateTime logDate,string ticketNo,int ticketCode)
        {
            try
            {
                string emailBody =
                    "Dear <b>" + WebUtility.HtmlEncode(request.RaisedBy) +
                    "</b>,<br/><br/>" +
                    "Thank you for contacting us.<br/><br/>" +
                    "We have received your query and will get back to you soon." +
                    "<br/><br/>" +
                    "<table border='1' cellpadding='5' cellspacing='0'>" +
                    "<tr>" +
                    "<td>Client Name</td>" +
                    "<td><b>" +
                    WebUtility.HtmlEncode(request.ProjectClient) +
                    "</b></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>Ticket No</td>" +
                    "<td><b>" +
                    WebUtility.HtmlEncode(ticketNo) +
                    "</b></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>Log Date</td>" +
                    "<td><b>" +
                    logDate.ToString("dd-MM-yyyy") +
                    "</b></td>" +
                    "</tr>" +
                    "<tr>" +
                    "<td>Query Description</td>" +
                    "<td><b>" +
                    WebUtility.HtmlEncode(request.Description) +
                    "</b></td>" +
                    "</tr>" +
                    "</table><br/>" +
                    "Your concern is important to us. " +
                    "Thank you for being our customer.<br/><br/>" +
                    "Best Regards,<br/>" +
                    "Team BizSol<br/>" +
                    "Helpline Email: " +
                    "<a href='mailto:support@bizsol.in'>" +
                    "support@bizsol.in</a>";

                string sendEmailIds = await GetTicketRecipientEmails(connection,request.ProjectClient ?? "",request.ContactEMail ?? "");
                if (string.IsNullOrWhiteSpace(sendEmailIds))
                {
                    return false;
                }

                using (var command = new SqlCommand(
                    @"INSERT INTO EmailLogMaster
                      (CallTicketMaster_Code, MailTo, MailCC, [Subject], Body)
                      VALUES
                      (@CallTicketMaster_Code, @MailTo, @MailCC, @Subject, @Body)",
                    connection))
                {
                    command.Parameters.AddWithValue("@CallTicketMaster_Code", ticketCode);
                    command.Parameters.AddWithValue("@MailTo", sendEmailIds);
                    command.Parameters.AddWithValue("@MailCC", "support@bizsol.in");
                    command.Parameters.AddWithValue("@Subject", "Ticket " + ticketNo + " Received");
                    command.Parameters.AddWithValue("@Body", emailBody);

                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
        private async Task<string> GetTicketRecipientEmails(SqlConnection connection,string projectClient,string contactEmail)
        {
            var emailList = new List<string>();

            if (!string.IsNullOrWhiteSpace(contactEmail))
            {
                emailList.Add(contactEmail.Trim());
            }

            bool isBizSolEmail = !string.IsNullOrWhiteSpace(contactEmail) &&
                contactEmail.IndexOf("@bizsol.in", StringComparison.OrdinalIgnoreCase) >= 0;

            if (!isBizSolEmail)
            {
                using (var command = new SqlCommand("USP_GetClientDefaultEmails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ClientName", projectClient);

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            string? defaultEmails = null;

                            try
                            {
                                defaultEmails = Convert.ToString(reader["DefaultEmails"]);
                            }
                            catch
                            {
                                if (!reader.IsDBNull(0))
                                {
                                    defaultEmails = Convert.ToString(reader.GetValue(0));
                                }
                            }

                            if (!string.IsNullOrWhiteSpace(defaultEmails))
                            {
                                string[] emails = defaultEmails.Split(new[] { ';', ',' },StringSplitOptions.RemoveEmptyEntries);

                                emailList.AddRange(emails.Select(x => x.Trim()));
                            }
                        }
                    }
                }
            }
            return string.Join(";",emailList.Where(x => !string.IsNullOrWhiteSpace(x)).Distinct(StringComparer.OrdinalIgnoreCase));
        }
        public async Task<AttechedFileCheck[]> AttechedFileChecks(BizsolESMSConnectionDetails bizsolESMSConnectionDetails,string companyCode, long ticketNo)
        {
            string companyConnectionString = GetCompanyConnectionString(companyCode?.Trim() ?? "");

            if (string.IsNullOrWhiteSpace(companyConnectionString) &&
                !string.IsNullOrWhiteSpace(bizsolESMSConnectionDetails?.DefaultSQL))
            {
                companyConnectionString = bizsolESMSConnectionDetails.DefaultSQL;
            }

            if (string.IsNullOrWhiteSpace(companyConnectionString))
            {
                return Array.Empty<AttechedFileCheck>();
            }

            var list = new List<AttechedFileCheck>();

            using (var connection = new SqlConnection(companyConnectionString))
            {
                await connection.OpenAsync();

                using (var command = new SqlCommand("USP_GetCallTicketAttachment", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.Add("@TicketNo", SqlDbType.BigInt).Value = ticketNo;

                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            string attachmentBase64 = "";
                            if (!reader.IsDBNull(reader.GetOrdinal("Attachment")))
                            {
                                byte[] bytes = (byte[])reader["Attachment"];
                                attachmentBase64 = Convert.ToBase64String(bytes);
                            }

                            list.Add(new AttechedFileCheck
                            {
                                name = reader["AttachmentFileName"]?.ToString() ?? "",
                                Attachment = attachmentBase64
                            });
                        }
                    }
                }
            }

            return list.ToArray();
        }
        private async Task<(bool Assigned, bool EmailSent)> AssignTicketAfterCreate( SqlConnection connection,
            CreateTicketRequest request,
            int ticketCode,
            string ticketNo,
            DateTime logDate)
        {
            try
            {
                object committedDateValue = DBNull.Value;
                if (!string.IsNullOrWhiteSpace(request.CommittedDate) &&
                    DateTime.TryParse(request.CommittedDate.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime committedDate))
                {
                    committedDateValue = committedDate;
                }

                int employeeCode = request.Employee_Code > 0 ? request.Employee_Code : request.CreateTicketBy;

                using (var command = new SqlCommand("[dbo].[USP_CallTicketUpdate_Admin]", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 0;
                    command.Parameters.AddWithValue("@Employee_Code", employeeCode.ToString());
                    command.Parameters.AddWithValue("@Code", ticketCode);
                    command.Parameters.AddWithValue("@CommittedDate", committedDateValue);
                    command.Parameters.AddWithValue("@Assigned", request.Assigned);
                    command.Parameters.AddWithValue("@WorkType", request.WorkType);
                    command.Parameters.AddWithValue("@EstimatedTime", request.EstimatedTime ?? "");
                    command.Parameters.AddWithValue("@AssignedText", request.AssignedText ?? "");
                    command.Parameters.AddWithValue("@WorkTypeText", request.WorkTypeText ?? "");

                    int result = await command.ExecuteNonQueryAsync();
                    if (result <= 0)
                    {
                        return (false, false);
                    }
                }

                bool emailSent = await SendTicketAssignedEmail(connection, request, ticketCode, ticketNo, logDate);
                return (true, emailSent);
            }
            catch
            {
                return (false, false);
            }
        }

        private async Task<bool> SendTicketAssignedEmail(
            SqlConnection connection,
            CreateTicketRequest request,
            int ticketCode,
            string ticketNo,
            DateTime logDate)
        {
            try
            {
                string? emailId = await GetEmployeeScalar(connection,
                    "SELECT Email FROM [dbo].[EmployeeMaster] WHERE Code = @Code",
                    request.Assigned);

                if (string.IsNullOrWhiteSpace(emailId))
                {
                    return false;
                }

                string employeeName = await GetEmployeeScalar(connection, "SELECT EmployeeName FROM [dbo].[EmployeeMaster] WHERE Code = @Code",
                    request.Assigned) ?? "";

                string loggedByName = "";
                int createTicketBy = request.CreateTicketBy;
                if (createTicketBy <= 0)
                {
                    using (var command = new SqlCommand(
                        "SELECT CreateTicketBy_Code FROM [dbo].[CallTicketMaster] WHERE Code = @Code",
                        connection))
                    {
                        command.Parameters.AddWithValue("@Code", ticketCode);
                        object? createTicketByValue = await command.ExecuteScalarAsync();
                        if (createTicketByValue != null && createTicketByValue != DBNull.Value)
                        {
                            createTicketBy = Convert.ToInt32(createTicketByValue);
                        }
                    }
                }

                if (createTicketBy > 0)
                {
                    loggedByName = await GetEmployeeScalar(connection,
                        "SELECT EmployeeName FROM [dbo].[EmployeeMaster] WHERE Code = @Code",
                        createTicketBy) ?? "";
                }

                string committedDateDisplay = "";
                string committedDateRowStyle = "none";
                if (!string.IsNullOrWhiteSpace(request.CommittedDate) &&
                    DateTime.TryParse(request.CommittedDate.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime committedDate))
                {
                    committedDateDisplay = committedDate.ToString("dd-MM-yyyy");
                    committedDateRowStyle = "block";
                }

                string emailBody =
                    "Dear <b>" + WebUtility.HtmlEncode(employeeName) + "</b>,<br/><br/>" +
                    "New query has been assigned to you. Details are:<br/><br/>" +
                    "<table border='1' cellpadding='5' cellspacing='0'>" +
                    "<tr><td>Client Name</td><td><b>" + WebUtility.HtmlEncode(request.ProjectClient ?? "") + "</b></td></tr>" +
                    "<tr><td>Log Date</td><td><b>" + WebUtility.HtmlEncode(logDate.ToString("dd-MM-yyyy")) + "</b></td></tr>" +
                    "<tr><td>Logged By</td><td><b>" + WebUtility.HtmlEncode(loggedByName) + "</b></td></tr>" +
                    "<tr><td>Assigned By</td><td><b>" + WebUtility.HtmlEncode(request.EmployeeName ?? "") + "</b></td></tr>" +
                    "<tr><td>Query Description</td><td><b>" + WebUtility.HtmlEncode(request.Description ?? "") + "</b></td></tr>" +
                    "<tr style='display: " + committedDateRowStyle + "'><td>Expected Resolution Date</td><td><b>" +
                    WebUtility.HtmlEncode(committedDateDisplay) + "</b></td></tr>" +
                    "</table><br/>" +
                    "Please try to resolve the query as soon as possible.<br/><br/>" +
                    "Best Regards,<br/>" +
                    "Team BizSol<br/>" +
                    "Helpline Email: <a href='mailto:support@bizsol.in'>support@bizsol.in</a>";

                using (var command = new SqlCommand(
                    @"INSERT INTO EmailLogMaster
                      (CallTicketMaster_Code, MailTo, MailCC, [Subject], Body)
                      VALUES
                      (@CallTicketMaster_Code, @MailTo, @MailCC, @Subject, @Body)",
                    connection))
                {
                    command.Parameters.AddWithValue("@CallTicketMaster_Code", ticketCode);
                    command.Parameters.AddWithValue("@MailTo", emailId);
                    command.Parameters.AddWithValue("@MailCC", "support@bizsol.in");
                    command.Parameters.AddWithValue("@Subject", "Ticket " + ticketNo + " Assigned");
                    command.Parameters.AddWithValue("@Body", emailBody);
                    await command.ExecuteNonQueryAsync();
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        private static async Task<string?> GetEmployeeScalar(SqlConnection connection, string sql, int code)
        {
            using (var command = new SqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@Code", code);
                object? result = await command.ExecuteScalarAsync();
                return result == null || result == DBNull.Value ? null : Convert.ToString(result);
            }
        }

        private static CreateTicketResponse Fail(string message)
        {
            return new CreateTicketResponse
            {
                Success = false,
                Message = message
            };
        }
    }
}
