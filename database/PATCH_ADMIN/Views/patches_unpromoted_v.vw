CREATE OR REPLACE EDITIONABLE VIEW "PATCHES_UNPROMOTED_V"(
  "PATCH_ID"
  ,"PATCH_NAME"
  ,"DB_SCHEMA"
  ,"BRANCH_NAME"
  ,"TAG_FROM"
  ,"TAG_TO"
  ,"SUPPLEMENTARY"
  ,"PATCH_DESC"
  ,"PATCH_COMPONANTS"
  ,"PATCH_CREATE_DATE"
  ,"PATCH_CREATED_BY"
  ,"NOTE"
  ,"LOG_DATETIME"
  ,"COMPLETED_DATETIME"
  ,"SUCCESS_YN"
  ,"RETIRED_YN"
  ,"RERUNNABLE_YN"
  ,"WARNING_COUNT"
  ,"ERROR_COUNT"
  ,"USERNAME"
  ,"INSTALL_LOG"
  ,"CREATED_BY"
  ,"CREATED_ON"
  ,"LAST_UPDATED_BY"
  ,"LAST_UPDATED_ON"
  ,"PATCH_TYPE"
)AS
  SELECT "PATCH_ID"
         ,"PATCH_NAME"
         ,"DB_SCHEMA"
         ,"BRANCH_NAME"
         ,"TAG_FROM"
         ,"TAG_TO"
         ,"SUPPLEMENTARY"
         ,"PATCH_DESC"
         ,"PATCH_COMPONANTS"
         ,"PATCH_CREATE_DATE"
         ,"PATCH_CREATED_BY"
         ,"NOTE"
         ,"LOG_DATETIME"
         ,"COMPLETED_DATETIME"
         ,"SUCCESS_YN"
         ,"RETIRED_YN"
         ,"RERUNNABLE_YN"
         ,"WARNING_COUNT"
         ,"ERROR_COUNT"
         ,"USERNAME"
         ,"INSTALL_LOG"
         ,"CREATED_BY"
         ,"CREATED_ON"
         ,"LAST_UPDATED_BY"
         ,"LAST_UPDATED_ON"
         ,"PATCH_TYPE"
  FROM patches_dependency_v
  WHERE patch_name NOT IN(SELECT patch_name
                          FROM installed_patches_v@patch_admin_forward_dblink
                         );


--GRANTS


--SYNONYMS
