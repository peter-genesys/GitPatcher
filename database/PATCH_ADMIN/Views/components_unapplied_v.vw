CREATE OR REPLACE FORCE EDITIONABLE VIEW "COMPONENTS_UNAPPLIED_V"(
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
  ,"PATCH_COMPONENT"
)AS
  SELECT pu."PATCH_NAME"
         ,pu."DB_SCHEMA"
         ,pu."BRANCH_NAME"
         ,pu."TAG_FROM"
         ,pu."TAG_TO"
         ,pu."SUPPLEMENTARY"
         ,pu."PATCH_DESC"
         ,pu."PATCH_CREATE_DATE"
         ,pu."PATCH_CREATED_BY"
         ,pu."NOTE"
         ,pu."RERUNNABLE_YN"
         ,pc.patch_component
  FROM patches_unapplied_v pu
       ,patches_components_v@patch_admin_backward_dblink pc
  WHERE pu.patch_name = pc.patch_name;
