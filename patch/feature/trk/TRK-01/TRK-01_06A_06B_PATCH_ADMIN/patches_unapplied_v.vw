CREATE OR REPLACE EDITIONABLE VIEW "PATCHES_UNAPPLIED_V"(
  "PATCH_NAME"
  ,"DB_SCHEMA"
  ,"BRANCH_NAME"
  ,"TAG_FROM"
  ,"TAG_TO"
  ,"SUPPLEMENTARY"
  ,"PATCH_DESC"
  ,"PATCH_CREATE_DATE"
  ,"PATCH_CREATED_BY"
  ,"NOTE"
  ,"RERUNNABLE_YN"
)AS
  SELECT patch_name
         ,db_schema
         ,branch_name
         ,tag_from
         ,tag_to
         ,supplementary
         ,patch_desc
         ,patch_create_date
         ,patch_created_by
         ,note
         ,rerunnable_yn
  FROM patches_dependency_v@patch_admin_backward_dblink
  WHERE patch_name NOT IN(SELECT patch_name
                          FROM installed_patches_v
                         );


--GRANTS

--SYNONYMS
