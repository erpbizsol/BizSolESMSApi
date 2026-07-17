USE `db_dadasales_test`;

DROP PROCEDURE IF EXISTS `USP_ItemMaster`;

DELIMITER $$

CREATE DEFINER=`sa`@`%` PROCEDURE `USP_ItemMaster`(
   IN p_Mode VARCHAR(20),
   IN p_Code INT,
   IN p_UserMaster_Code INT,
   IN p_jsonData JSON
)
BEGIN
    DECLARE v_ExistingCount INT DEFAULT 0;
    DECLARE v_ItemMaster_Code INT DEFAULT 0;
    DECLARE v_LocationCodes TEXT;
    DECLARE v_FirstLocationCode INT DEFAULT NULL;
    DECLARE v_Pos INT DEFAULT 1;
    DECLARE v_NextPos INT DEFAULT 0;
    DECLARE v_CodeStr VARCHAR(50);
    DECLARE v_DetailJson JSON;
    DECLARE v_DetailCount INT DEFAULT 0;
    DECLARE v_DetailIdx INT DEFAULT 0;
    DECLARE v_WarehouseCode INT DEFAULT 0;
    DECLARE v_LocationCode INT DEFAULT 0;

    SET v_LocationCodes = COALESCE(
        NULLIF(TRIM(JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.LocationMaster_Codes'))), ''),
        NULLIF(TRIM(JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.locationMaster_Codes'))), '')
    );

    SET v_DetailJson = COALESCE(
        JSON_EXTRACT(p_jsonData, '$.ItemWarehouseLocationDetails'),
        JSON_EXTRACT(p_jsonData, '$.itemWarehouseLocationDetails')
    );
    SET v_DetailCount = IFNULL(JSON_LENGTH(v_DetailJson), 0);
    SET v_FirstLocationCode = NULL;

    IF v_DetailCount > 0 THEN
        SET v_FirstLocationCode = CAST(COALESCE(
            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, '$[0].LocationMaster_Code')),
            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, '$[0].locationMaster_Code'))
        ) AS UNSIGNED);
        IF v_FirstLocationCode = 0 THEN
            SET v_FirstLocationCode = NULL;
        END IF;
    ELSEIF v_LocationCodes IS NOT NULL AND v_LocationCodes <> '' THEN
        SET v_CodeStr = TRIM(SUBSTRING_INDEX(v_LocationCodes, ',', 1));
        IF v_CodeStr <> '' THEN
            SET v_FirstLocationCode = CAST(v_CodeStr AS UNSIGNED);
        END IF;
    END IF;

    IF p_Mode = 'SAVE' THEN
        IF p_Code > 0 THEN
            SELECT MIN(Code) INTO v_ExistingCount
            FROM db_dadasales_test.itemmaster
            WHERE itemmaster.ItemName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemName'))
              AND itemmaster.ItemBarCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemBarCode'))
              AND itemmaster.ItemCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemCode'))
              AND Code != p_Code
              AND IsActive = 'Y';

            IF v_ExistingCount > 0 THEN
                SELECT 'Duplicate Record not updated!.' AS Msg, 'N' AS Status;
            ELSE
            BEGIN
                IF v_FirstLocationCode IS NULL THEN
                    SELECT MIN(Code) INTO v_FirstLocationCode
                    FROM db_dadasales_test.locationmaster
                    WHERE locationmaster.LocationName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.locationName'))
                      AND IsActive = 'Y';
                END IF;

                UPDATE db_dadasales_test.itemmaster SET
                    ItemCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemCode')),
                    ItemName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemName')),
                    DisplayName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.displayName')),
                    ItemBarCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemBarCode')),
                    UOMMaster_Code = (SELECT Code FROM db_dadasales_test.uommaster WHERE uommaster.UOMName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.uOMName')) AND IsActive = 'Y'),
                    HSNCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.hSNCode')),
                    CategoryMaster_Code = (SELECT Code FROM db_dadasales_test.categorymaster WHERE categorymaster.CategoryName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.categoryName')) AND IsActive = 'Y'),
                    GroupMaster_Code = (SELECT Code FROM db_dadasales_test.groupmaster WHERE groupmaster.GroupName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.groupName')) AND IsActive = 'Y'),
                    SubGroupMaster_Code = (SELECT Code FROM db_dadasales_test.subgroupmaster WHERE subgroupmaster.SubGroupName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.subGroupName')) AND IsActive = 'Y'),
                    BrandMaster_Code = (SELECT Code FROM db_dadasales_test.brandmaster WHERE brandmaster.BrandName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.brandName')) AND IsActive = 'Y'),
                    ReorderLevel = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.reorderLevel')),
                    ReorderQty = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.reorderQty')),
                    LocationMaster_Code = v_FirstLocationCode,
                    BatchApplicable = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.batchApplicable')),
                    MaintainExpiry = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.maintainExpiry')),
                    BoxPacking = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.boxPacking')),
                    QtyInBox = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.qtyInBox')),
                    ModifiedBy = p_UserMaster_Code,
                    ModifiedOn = NOW(),
                    DataImported = 'N',
                    MRPNo = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.mRPNo')),
                    IsActive = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.isActive'))
                WHERE Code = p_Code;

                SET v_ItemMaster_Code = p_Code;

                DELETE FROM db_dadasales_test.ItemLocationDetails
                WHERE ItemMaster_Code = v_ItemMaster_Code;

                IF v_DetailCount > 0 THEN
                    SET v_DetailIdx = 0;
                    detail_update_loop: WHILE v_DetailIdx < v_DetailCount DO
                        SET v_WarehouseCode = CAST(COALESCE(
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].WarehouseMaster_Code'))),
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].warehouseMaster_Code'))),
                            '0'
                        ) AS UNSIGNED);
                        SET v_LocationCode = CAST(COALESCE(
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].LocationMaster_Code'))),
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].locationMaster_Code'))),
                            '0'
                        ) AS UNSIGNED);

                        IF v_LocationCode > 0 THEN
                            INSERT INTO db_dadasales_test.ItemLocationDetails (ItemMaster_Code, LocationMaster_Code, WarehouseMaster_Code)
                            SELECT v_ItemMaster_Code, lm.Code, IF(v_WarehouseCode > 0, v_WarehouseCode, lm.WarehouseMaster_Code)
                            FROM db_dadasales_test.locationmaster lm
                            WHERE lm.Code = v_LocationCode
                              AND (v_WarehouseCode = 0 OR lm.WarehouseMaster_Code = v_WarehouseCode)
                              AND lm.IsActive = 'Y'
                            LIMIT 1;
                        END IF;

                        SET v_DetailIdx = v_DetailIdx + 1;
                    END WHILE;
                ELSEIF v_LocationCodes IS NOT NULL AND v_LocationCodes <> '' THEN
                    SET v_LocationCodes = CONCAT(v_LocationCodes, ',');
                    SET v_Pos = 1;
                    loc_update_loop: WHILE v_Pos <= CHAR_LENGTH(v_LocationCodes) DO
                        SET v_NextPos = LOCATE(',', v_LocationCodes, v_Pos);
                        IF v_NextPos = 0 THEN
                            LEAVE loc_update_loop;
                        END IF;

                        SET v_CodeStr = TRIM(SUBSTRING(v_LocationCodes, v_Pos, v_NextPos - v_Pos));
                        IF v_CodeStr <> '' THEN
                            INSERT INTO db_dadasales_test.ItemLocationDetails (ItemMaster_Code, LocationMaster_Code, WarehouseMaster_Code)
                            SELECT v_ItemMaster_Code, lm.Code, lm.WarehouseMaster_Code
                            FROM db_dadasales_test.locationmaster lm
                            WHERE lm.Code = CAST(v_CodeStr AS UNSIGNED)
                              AND lm.IsActive = 'Y'
                            LIMIT 1;
                        END IF;

                        SET v_Pos = v_NextPos + 1;
                    END WHILE;
                ELSEIF v_FirstLocationCode IS NOT NULL THEN
                    INSERT INTO db_dadasales_test.ItemLocationDetails (ItemMaster_Code, LocationMaster_Code, WarehouseMaster_Code)
                    SELECT v_ItemMaster_Code, lm.Code, lm.WarehouseMaster_Code
                    FROM db_dadasales_test.locationmaster lm
                    WHERE lm.Code = v_FirstLocationCode
                      AND lm.IsActive = 'Y'
                    LIMIT 1;
                END IF;

                SELECT 'Data Updated Successfully' AS Msg, 'Y' AS Status, v_ItemMaster_Code AS Code;
            END;
            END IF;
        ELSE
            SELECT MIN(Code) INTO v_ExistingCount
            FROM db_dadasales_test.itemmaster
            WHERE itemmaster.ItemName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemName'))
              AND itemmaster.ItemBarCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemBarCode'))
              AND itemmaster.ItemCode = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemCode'))
              AND IsActive = 'Y';

            IF v_ExistingCount > 0 THEN
                SELECT 'Duplicate  Record not inserted!.' AS Msg, 'N' AS Status;
            ELSE
            BEGIN
                IF v_FirstLocationCode IS NULL THEN
                    SELECT MIN(Code) INTO v_FirstLocationCode
                    FROM db_dadasales_test.locationmaster
                    WHERE locationmaster.LocationName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.locationName'))
                      AND IsActive = 'Y';
                END IF;

                INSERT INTO db_dadasales_test.itemmaster (
                    ItemCode, ItemName, DisplayName, ItemBarCode, UOMMaster_Code, HSNCode,
                    CategoryMaster_Code, GroupMaster_Code, SubGroupMaster_Code, BrandMaster_Code,
                    ReorderLevel, ReorderQty, LocationMaster_Code, BatchApplicable, MaintainExpiry,
                    BoxPacking, QtyInBox, CreatedBy, CreatedOn, IsActive, MRPNo
                )
                VALUES (
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemCode')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemName')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.displayName')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.itemBarCode')),
                    (SELECT Code FROM db_dadasales_test.uommaster WHERE uommaster.UOMName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.uOMName')) AND IsActive = 'Y'),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.hSNCode')),
                    (SELECT Code FROM db_dadasales_test.categorymaster WHERE categorymaster.CategoryName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.categoryName')) AND IsActive = 'Y'),
                    (SELECT Code FROM db_dadasales_test.groupmaster WHERE groupmaster.GroupName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.groupName')) AND IsActive = 'Y'),
                    (SELECT Code FROM db_dadasales_test.subgroupmaster WHERE subgroupmaster.SubGroupName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.subGroupName')) AND IsActive = 'Y'),
                    (SELECT Code FROM db_dadasales_test.brandmaster WHERE brandmaster.BrandName = JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.brandName')) AND IsActive = 'Y'),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.reorderLevel')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.reorderQty')),
                    v_FirstLocationCode,
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.batchApplicable')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.maintainExpiry')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.boxPacking')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.qtyInBox')),
                    p_UserMaster_Code,
                    NOW(),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.isActive')),
                    JSON_UNQUOTE(JSON_EXTRACT(p_jsonData, '$.mRPNo'))
                );

                SET v_ItemMaster_Code = LAST_INSERT_ID();

                IF v_DetailCount > 0 THEN
                    SET v_DetailIdx = 0;
                    detail_insert_loop: WHILE v_DetailIdx < v_DetailCount DO
                        SET v_WarehouseCode = CAST(COALESCE(
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].WarehouseMaster_Code'))),
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].warehouseMaster_Code'))),
                            '0'
                        ) AS UNSIGNED);
                        SET v_LocationCode = CAST(COALESCE(
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].LocationMaster_Code'))),
                            JSON_UNQUOTE(JSON_EXTRACT(v_DetailJson, CONCAT('$[', v_DetailIdx, '].locationMaster_Code'))),
                            '0'
                        ) AS UNSIGNED);

                        IF v_LocationCode > 0 THEN
                            INSERT INTO db_dadasales_test.ItemLocationDetails (ItemMaster_Code, LocationMaster_Code, WarehouseMaster_Code)
                            SELECT v_ItemMaster_Code, lm.Code, IF(v_WarehouseCode > 0, v_WarehouseCode, lm.WarehouseMaster_Code)
                            FROM db_dadasales_test.locationmaster lm
                            WHERE lm.Code = v_LocationCode
                              AND (v_WarehouseCode = 0 OR lm.WarehouseMaster_Code = v_WarehouseCode)
                              AND lm.IsActive = 'Y'
                            LIMIT 1;
                        END IF;

                        SET v_DetailIdx = v_DetailIdx + 1;
                    END WHILE;
                ELSEIF v_LocationCodes IS NOT NULL AND v_LocationCodes <> '' THEN
                    SET v_LocationCodes = CONCAT(v_LocationCodes, ',');
                    SET v_Pos = 1;
                    loc_insert_loop: WHILE v_Pos <= CHAR_LENGTH(v_LocationCodes) DO
                        SET v_NextPos = LOCATE(',', v_LocationCodes, v_Pos);
                        IF v_NextPos = 0 THEN
                            LEAVE loc_insert_loop;
                        END IF;

                        SET v_CodeStr = TRIM(SUBSTRING(v_LocationCodes, v_Pos, v_NextPos - v_Pos));
                        IF v_CodeStr <> '' THEN
                            INSERT INTO db_dadasales_test.ItemLocationDetails (ItemMaster_Code, LocationMaster_Code, WarehouseMaster_Code)
                            SELECT v_ItemMaster_Code, lm.Code, lm.WarehouseMaster_Code
                            FROM db_dadasales_test.locationmaster lm
                            WHERE lm.Code = CAST(v_CodeStr AS UNSIGNED)
                              AND lm.IsActive = 'Y'
                            LIMIT 1;
                        END IF;

                        SET v_Pos = v_NextPos + 1;
                    END WHILE;
                ELSEIF v_FirstLocationCode IS NOT NULL THEN
                    INSERT INTO db_dadasales_test.ItemLocationDetails (ItemMaster_Code, LocationMaster_Code, WarehouseMaster_Code)
                    SELECT v_ItemMaster_Code, lm.Code, lm.WarehouseMaster_Code
                    FROM db_dadasales_test.locationmaster lm
                    WHERE lm.Code = v_FirstLocationCode
                      AND lm.IsActive = 'Y'
                    LIMIT 1;
                END IF;

                SELECT 'Data Save Successfully' AS Msg, 'Y' AS Status, v_ItemMaster_Code AS Code;
            END;
            END IF;
        END IF;
    ELSEIF p_Mode = 'DELETE' THEN
        DELETE FROM db_dadasales_test.ItemLocationDetails WHERE ItemMaster_Code = p_Code;
        DELETE FROM db_dadasales_test.itemmaster WHERE Code = p_Code;
        SELECT 'Data Deleted Successfully' AS Msg, 'Y' AS Status;
    ELSEIF p_Mode = 'SHOWDATA' THEN
        SELECT
            itemmaster.Code,
            itemmaster.ItemCode,
            itemmaster.ItemName,
            itemmaster.DisplayName,
            itemmaster.ItemBarCode,
            UOMMaster.UomName,
            itemmaster.HSNCode,
            categorymaster.CategoryName,
            GroupMaster.GroupName,
            subgroupmaster.SubGroupName,
            brandmaster.BrandName,
            itemmaster.ReorderLevel,
            itemmaster.ReorderQty,
            GROUP_CONCAT(DISTINCT locationmaster.LocationName ORDER BY locationmaster.LocationName SEPARATOR ', ') AS locationName,
            GROUP_CONCAT(DISTINCT ItemLocationDetails.LocationMaster_Code ORDER BY ItemLocationDetails.LocationMaster_Code SEPARATOR ', ') AS LocationMaster_Codes,
            IFNULL(
                (
                    SELECT JSON_ARRAYAGG(
                        JSON_OBJECT(
                            'WarehouseMaster_Code', IFNULL(ild.WarehouseMaster_Code, lm.WarehouseMaster_Code),
                            'LocationMaster_Code', ild.LocationMaster_Code
                        )
                    )
                    FROM db_dadasales_test.ItemLocationDetails ild
                    LEFT JOIN db_dadasales_test.locationmaster lm ON ild.LocationMaster_Code = lm.Code
                    WHERE ild.ItemMaster_Code = itemmaster.Code
                ),
                JSON_ARRAY()
            ) AS ItemWarehouseLocationDetails,
            itemmaster.BoxPacking,
            itemmaster.BatchApplicable,
            itemmaster.MaintainExpiry,
            itemmaster.QtyInBox,
            itemmaster.IsActive,
            itemmaster.MRPNo
        FROM db_dadasales_test.itemmaster
        LEFT JOIN db_dadasales_test.GroupMaster GroupMaster ON itemmaster.GroupMaster_Code = GroupMaster.Code
        LEFT JOIN db_dadasales_test.UOMMaster UOMMaster ON itemmaster.UOMMaster_Code = UOMMaster.Code
        LEFT JOIN db_dadasales_test.categorymaster categorymaster ON itemmaster.CategoryMaster_Code = categorymaster.Code
        LEFT JOIN db_dadasales_test.subgroupmaster subgroupmaster ON itemmaster.SubGroupMaster_Code = subgroupmaster.Code
        LEFT JOIN db_dadasales_test.brandmaster brandmaster ON itemmaster.BrandMaster_Code = brandmaster.Code
        LEFT JOIN db_dadasales_test.ItemLocationDetails ItemLocationDetails ON ItemLocationDetails.ItemMaster_Code = itemmaster.Code
        LEFT JOIN db_dadasales_test.locationmaster locationmaster ON ItemLocationDetails.LocationMaster_Code = locationmaster.Code
        WHERE itemmaster.Code = p_Code
        GROUP BY
            itemmaster.Code, itemmaster.ItemCode, itemmaster.ItemName, itemmaster.DisplayName,
            itemmaster.ItemBarCode, UOMMaster.UomName, itemmaster.HSNCode, categorymaster.CategoryName,
            GroupMaster.GroupName, subgroupmaster.SubGroupName, brandmaster.BrandName,
            itemmaster.ReorderLevel, itemmaster.ReorderQty, itemmaster.BoxPacking,
            itemmaster.BatchApplicable, itemmaster.MaintainExpiry, itemmaster.QtyInBox,
            itemmaster.IsActive, itemmaster.MRPNo;
    ELSEIF p_Mode = 'GetItemDetails' THEN
        SELECT
            itemmaster.Code,
            itemmaster.ItemCode,
            itemmaster.ItemName,
            itemmaster.ItemBarCode,
            UOMMaster.UomName,
            GROUP_CONCAT(DISTINCT locationmaster.locationName ORDER BY locationmaster.locationName SEPARATOR ', ') AS locationName,
            itemmaster.QtyInBox
        FROM db_dadasales_test.itemmaster
        LEFT JOIN db_dadasales_test.UOMMaster UOMMaster ON itemmaster.UOMMaster_Code = UOMMaster.Code
        LEFT JOIN db_dadasales_test.ItemLocationDetails ItemLocationDetails ON ItemLocationDetails.ItemMaster_Code = itemmaster.Code
        LEFT JOIN db_dadasales_test.locationmaster locationmaster ON ItemLocationDetails.LocationMaster_Code = locationmaster.Code
        WHERE itemmaster.IsActive = 'Y'
        GROUP BY
            itemmaster.Code, itemmaster.ItemCode, itemmaster.ItemName,
            itemmaster.ItemBarCode, UOMMaster.UomName, itemmaster.QtyInBox;
    ELSEIF p_Mode = 'LOCATE' THEN
        SELECT
            ROW_NUMBER() OVER (ORDER BY itemmaster.DataImported DESC, itemmaster.ItemName, itemmaster.Code) AS `S.No`,
            itemmaster.Code,
            IFNULL(itemmaster.ItemCode,'') AS 'Item Code',
            IFNULL(itemmaster.ItemName,'') AS 'Item Name',
            IFNULL(itemmaster.DisplayName,'') AS 'Display Name',
            IFNULL(itemmaster.ItemBarCode,'') AS 'Item Bar Code',
            GROUP_CONCAT(DISTINCT IFNULL(locationmaster.LocationName, '')) AS 'Location Name',
            IFNULL(UOMMaster.UomName, '') AS 'UomName',
            IFNULL(itemmaster.HSNCode, '') AS 'HSN Code',
            IFNULL(categorymaster.CategoryName, '') AS 'Category Name',
            IFNULL(GroupMaster.GroupName, '') AS 'Group Name',
            IFNULL(subgroupmaster.SubGroupName, '') AS 'Sub Group Name',
            IFNULL(brandmaster.BrandName, '') AS 'Brand Name',
            itemmaster.ReorderLevel AS 'Reorder Level',
            itemmaster.ReorderQty AS 'Reorder Qty',
            IFNULL(itemmaster.BoxPacking, '') AS 'Box Packing',
            IFNULL(itemmaster.BatchApplicable, '') AS 'Batch Applicable',
            IFNULL(itemmaster.MaintainExpiry, '') AS 'Maintain Expiry',
            itemmaster.QtyInBox AS 'Qty In Box',
            itemmaster.DataImported,
            itemmaster.MRPNo AS MRP
        FROM db_dadasales_test.itemmaster
        LEFT JOIN db_dadasales_test.GroupMaster GroupMaster ON itemmaster.GroupMaster_Code = GroupMaster.Code
        LEFT JOIN db_dadasales_test.UOMMaster UOMMaster ON itemmaster.UOMMaster_Code = UOMMaster.Code
        LEFT JOIN db_dadasales_test.categorymaster categorymaster ON itemmaster.CategoryMaster_Code = categorymaster.Code
        LEFT JOIN db_dadasales_test.subgroupmaster subgroupmaster ON itemmaster.SubGroupMaster_Code = subgroupmaster.Code
        LEFT JOIN db_dadasales_test.brandmaster brandmaster ON itemmaster.BrandMaster_Code = brandmaster.Code
        LEFT JOIN db_dadasales_test.ItemLocationDetails ItemLocationDetails ON ItemLocationDetails.ItemMaster_Code = itemmaster.Code
        LEFT JOIN db_dadasales_test.locationmaster locationmaster ON ItemLocationDetails.LocationMaster_Code = locationmaster.Code
        GROUP BY itemmaster.Code;
    END IF;
END$$

DELIMITER ;
