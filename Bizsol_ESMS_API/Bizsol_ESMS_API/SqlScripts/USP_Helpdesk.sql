-- Run this script manually in MySQL (webiz_demo).
-- Adds TicketNo column and updates USP_Helpdesk to save / show it.

USE `webiz_demo`;

-- Add column if it does not already exist
SET @col_exists := (
    SELECT COUNT(*)
    FROM INFORMATION_SCHEMA.COLUMNS
    WHERE TABLE_SCHEMA = 'webiz_demo'
      AND TABLE_NAME = 'GenerateTicketMaster'
      AND COLUMN_NAME = 'TicketNo'
);

SET @sql := IF(
    @col_exists = 0,
    'ALTER TABLE `GenerateTicketMaster` ADD COLUMN `TicketNo` VARCHAR(50) NULL AFTER `Code`;',
    'SELECT ''TicketNo column already exists'' AS Msg;'
);
PREPARE stmt FROM @sql;
EXECUTE stmt;
DEALLOCATE PREPARE stmt;

DROP PROCEDURE IF EXISTS `USP_Helpdesk`;

DELIMITER $$
CREATE DEFINER=`sa`@`%` PROCEDURE `USP_Helpdesk`(
   IN p_Mode VARCHAR(20),
   IN p_Code INT,
   IN p_UserMaster_Code INT,
   IN p_jsonData JSON
)
BEGIN
    DECLARE v_TicketNo VARCHAR(50);

    SET v_TicketNo = COALESCE(
        NULLIF(TRIM(JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.TicketNo'))), ''),
        NULLIF(TRIM(JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.ticketNo'))), '')
    );

    IF p_Mode = 'SAVE' THEN
        IF p_Code > 0 THEN
            UPDATE GenerateTicketMaster SET
                usermodulemaster_Code = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.userModuleMaster_Code')),
                TicketNo = v_TicketNo,
                Description = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.description')),
                ModifiedDate = NOW(),
                ModifiedBy = p_UserMaster_Code
            WHERE GenerateTicketMaster.Code = p_Code;
            SELECT 'Data Updated Successfully' AS Msg, 'Y' AS Status;
        ELSE
            INSERT INTO webiz_demo.GenerateTicketMaster
                (usermodulemaster_Code, TicketNo, Description, CreatedDate, CreatedBy)
            VALUES (
                JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.userModuleMaster_Code')),
                v_TicketNo,
                JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.description')),
                NOW(),
                p_UserMaster_Code
            );
            SELECT 'Data Save Successfully' AS Msg, 'Y' AS Status;
        END IF;
    ELSEIF p_Mode = 'DELETE' THEN
        DELETE FROM GenerateTicketMaster WHERE GenerateTicketMaster.Code = p_Code;
        SELECT 'Data Deleted Successfully' AS Msg, 'Y' AS Status;
    ELSEIF p_Mode = 'SHOWDATA' THEN
        SELECT Code, UserModuleMaster_Code, TicketNo, Description
        FROM GenerateTicketMaster
        WHERE Code = p_Code;
    ELSEIF p_Mode = 'LOCATE' THEN
        SELECT
            ROW_NUMBER() OVER (ORDER BY GenerateTicketMaster.Code) AS 'SNo',
            GenerateTicketMaster.Code,
            GenerateTicketMaster.TicketNo AS 'Ticket No',
            ModuleDesp AS 'Page',
            Description AS 'Description',
            UserName AS 'Raised By',
            DATE_FORMAT(CreatedDate, '%d-%m-%Y') AS 'Raised Date',
            CASE WHEN GenerateTicketMaster.Status = 'P' THEN 'Pending' ELSE 'Completed' END AS Status
        FROM GenerateTicketMaster
        LEFT JOIN UserModuleMaster ON UserModuleMaster.Code = GenerateTicketMaster.UserModuleMaster_Code
        LEFT JOIN UserMaster ON UserMaster.Code = GenerateTicketMaster.CreatedBy;
    ELSEIF p_Mode = 'COMPLETE' THEN
        UPDATE GenerateTicketMaster
        SET Status = 'C',
            CompletedBy = p_UserMaster_Code,
            CompletedDate = CURRENT_DATE()
        WHERE GenerateTicketMaster.Code = p_Code;
        SELECT 'Status change successfully' AS Msg, 'Y' AS Status;
    END IF;
END$$
DELIMITER ;
