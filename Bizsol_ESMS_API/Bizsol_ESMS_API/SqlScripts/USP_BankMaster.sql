-- Bank Master: table + stored procedure
-- Run this script on your ESMS MySQL database before using Bank Master.

CREATE TABLE IF NOT EXISTS `bankmaster` (
  `Code` INT NOT NULL AUTO_INCREMENT,
  `BankName` VARCHAR(100) NOT NULL DEFAULT '',
  `AccountNo` VARCHAR(30) NOT NULL DEFAULT '',
  `IFSCCode` VARCHAR(11) NOT NULL DEFAULT '',
  `Branch` VARCHAR(100) NOT NULL DEFAULT '',
  `Type` VARCHAR(20) NOT NULL DEFAULT '',
  `DefaultCheck` CHAR(1) NOT NULL DEFAULT 'N',
  `IsActive` CHAR(1) NOT NULL DEFAULT 'Y',
  `CreatedBy` INT NULL DEFAULT 0,
  `CreatedOn` DATETIME NULL DEFAULT CURRENT_TIMESTAMP,
  `ModifiedBy` INT NULL DEFAULT 0,
  `ModifiedOn` DATETIME NULL,
  PRIMARY KEY (`Code`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

DROP PROCEDURE IF EXISTS `USP_BankMaster`;

DELIMITER $$

CREATE PROCEDURE `USP_BankMaster`(
    IN p_Mode VARCHAR(20),
    IN p_Code INT,
    IN p_BankName VARCHAR(100),
    IN p_AccountNo VARCHAR(30),
    IN p_IFSCCode VARCHAR(11),
    IN p_Branch VARCHAR(100),
    IN p_Type VARCHAR(20),
    IN p_DefaultCheck CHAR(1),
    IN p_UserMaster_Code INT
)
BEGIN
    DECLARE v_ExistingCount INT DEFAULT 0;
    DECLARE v_NewCode INT DEFAULT 0;

    IF p_Mode = 'SAVEDATA' THEN
        IF IFNULL(p_BankName, '') = '' THEN
            SELECT 'Please enter Bank Name.' AS Msg, 'N' AS Status, 0 AS Code;
        ELSEIF IFNULL(p_AccountNo, '') = '' THEN
            SELECT 'Please enter Account No.' AS Msg, 'N' AS Status, 0 AS Code;
        ELSEIF IFNULL(p_IFSCCode, '') = '' THEN
            SELECT 'Please enter IFSC Code.' AS Msg, 'N' AS Status, 0 AS Code;
        ELSEIF IFNULL(p_Branch, '') = '' THEN
            SELECT 'Please enter Branch.' AS Msg, 'N' AS Status, 0 AS Code;
        ELSEIF IFNULL(p_Type, '') = '' THEN
            SELECT 'Please select Type.' AS Msg, 'N' AS Status, 0 AS Code;
        ELSE
            IF p_Code > 0 THEN
                SELECT COUNT(1) INTO v_ExistingCount
                FROM bankmaster
                WHERE AccountNo = p_AccountNo
                  AND Code != p_Code
                  AND IsActive = 'Y';

                IF v_ExistingCount > 0 THEN
                    SELECT 'Account No already exists.' AS Msg, 'N' AS Status, p_Code AS Code;
                ELSE
                    IF IFNULL(p_DefaultCheck, 'N') = 'Y' THEN
                        UPDATE bankmaster SET DefaultCheck = 'N' WHERE IsActive = 'Y' AND Code != p_Code;
                    END IF;

                    UPDATE bankmaster SET
                        BankName = p_BankName,
                        AccountNo = p_AccountNo,
                        IFSCCode = p_IFSCCode,
                        Branch = p_Branch,
                        Type = p_Type,
                        DefaultCheck = IFNULL(p_DefaultCheck, 'N'),
                        ModifiedBy = p_UserMaster_Code,
                        ModifiedOn = NOW()
                    WHERE Code = p_Code;

                    SELECT 'Record updated successfully.' AS Msg, 'Y' AS Status, p_Code AS Code;
                END IF;
            ELSE
                SELECT COUNT(1) INTO v_ExistingCount
                FROM bankmaster
                WHERE AccountNo = p_AccountNo
                  AND IsActive = 'Y';

                IF v_ExistingCount > 0 THEN
                    SELECT 'Account No already exists.' AS Msg, 'N' AS Status, 0 AS Code;
                ELSE
                    IF IFNULL(p_DefaultCheck, 'N') = 'Y' THEN
                        UPDATE bankmaster SET DefaultCheck = 'N' WHERE IsActive = 'Y';
                    END IF;

                    INSERT INTO bankmaster
                        (BankName, AccountNo, IFSCCode, Branch, Type, DefaultCheck, IsActive, CreatedBy, CreatedOn)
                    VALUES
                        (p_BankName, p_AccountNo, p_IFSCCode, p_Branch, p_Type, IFNULL(p_DefaultCheck, 'N'), 'Y', p_UserMaster_Code, NOW());

                    SET v_NewCode = LAST_INSERT_ID();
                    SELECT 'Record saved successfully.' AS Msg, 'Y' AS Status, v_NewCode AS Code;
                END IF;
            END IF;
        END IF;

    ELSEIF p_Mode = 'DELETE' THEN
        UPDATE bankmaster
        SET IsActive = 'N',
            ModifiedBy = p_UserMaster_Code,
            ModifiedOn = NOW()
        WHERE Code = p_Code;

        SELECT 'Record deleted successfully.' AS Msg, 'Y' AS Status, p_Code AS Code;

    ELSEIF p_Mode = 'LOCATE' THEN
        SELECT
            Code,
            BankName AS `Bank Name`,
            AccountNo AS `Account No`,
            IFSCCode AS `IFSC Code`,
            Branch,
            Type,
            CASE WHEN DefaultCheck = 'Y' THEN 'Yes' ELSE 'No' END AS `Default Check`
        FROM bankmaster
        WHERE IsActive = 'Y'
        ORDER BY Code DESC;

    ELSEIF p_Mode = 'GETBYCODE' THEN
        SELECT
            Code,
            BankName,
            AccountNo,
            IFSCCode,
            Branch,
            Type,
            DefaultCheck
        FROM bankmaster
        WHERE Code = p_Code
          AND IsActive = 'Y';
    END IF;
END$$

DELIMITER ;
