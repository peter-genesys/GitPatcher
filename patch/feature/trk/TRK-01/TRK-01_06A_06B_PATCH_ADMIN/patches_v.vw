CREATE OR REPLACE EDITIONABLE VIEW "PATCHES_V"(
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
  ,"TRACKING_YN"
  ,"PREREQ_COUNT"
  ,"IS_PREREQ_COUNT"
)AS
  SELECT p."PATCH_ID"
         ,p."PATCH_NAME"
         ,p."DB_SCHEMA"
         ,p."BRANCH_NAME"
         ,p."TAG_FROM"
         ,p."TAG_TO"
         ,p."SUPPLEMENTARY"
         ,p."PATCH_DESC"
         ,p."PATCH_COMPONANTS"
         ,p."PATCH_CREATE_DATE"
         ,p."PATCH_CREATED_BY"
         ,p."NOTE"
         ,p."LOG_DATETIME"
         ,p."COMPLETED_DATETIME"
         ,p."SUCCESS_YN"
         ,p."RETIRED_YN"
         ,p."RERUNNABLE_YN"
         ,p."WARNING_COUNT"
         ,p."ERROR_COUNT"
         ,p."USERNAME"
         ,p."INSTALL_LOG"
         ,p."CREATED_BY"
         ,p."CREATED_ON"
         ,p."LAST_UPDATED_BY"
         ,p."LAST_UPDATED_ON"
         ,p."PATCH_TYPE"
         ,p."TRACKING_YN"
         ,(SELECT COUNT(*)
    FROM patch_prereqs pr
    WHERE pr.patch_name = p.patch_name
          )prereq_count --has prereq count
         ,(SELECT COUNT(*)
    FROM patch_prereqs pr
    WHERE pr.prereq_patch = p.patch_name
          )is_prereq_count
  FROM patches p;


--GRANTS


--SYNONYMS
