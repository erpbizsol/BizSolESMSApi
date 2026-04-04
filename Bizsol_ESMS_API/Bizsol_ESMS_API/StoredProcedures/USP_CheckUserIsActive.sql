DELIMITER $$

CREATE PROCEDURE `USP_CheckUserIsActive`(
    IN p_Mode VARCHAR(50),
    IN p_Code INT
)
BEGIN
    DECLARE v_IsActive VARCHAR(1) DEFAULT 'N';
    DECLARE v_Status VARCHAR(50) DEFAULT '';
    DECLARE v_UserName VARCHAR(255) DEFAULT '';
    DECLARE v_UserID VARCHAR(255) DEFAULT '';
    
    -- Check if user exists and get status
    SELECT 
        Status,
        UserName,
        UserID
    INTO 
        v_Status,
        v_UserName,
        v_UserID
    FROM tblUserMaster
    WHERE Code = p_Code
    LIMIT 1;
    
    -- Determine if user is active
    -- Assuming Status = 'Y' or 'Active' means active, 'N' or 'Inactive' means inactive
    IF v_Status = 'Y' OR v_Status = 'Active' OR UPPER(v_Status) = 'ACTIVE' THEN
        SET v_IsActive = 'Y';
    ELSE
        SET v_IsActive = 'N';
    END IF;
    
    -- Return result
    SELECT 
        p_Code AS UserMaster_Code,
        v_UserID AS UserID,
        v_UserName AS UserName,
        v_Status AS Status,
        v_IsActive AS IsActive,
        CASE 
            WHEN v_Status IS NULL THEN 'User not found'
            WHEN v_IsActive = 'Y' THEN 'User is active'
            ELSE 'User is inactive'
        END AS Message;
END$$

DELIMITER ;




