create or replace view patches_unapplied_v as 
select 
 patch_name                   
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
from patches_dependency_v@PATCH_ADMIN_BACKWARD_DBLINK
where patch_name not in (select patch_name from patches_dependency_v)
and patch_name not in (select supersedes_patch from patch_supersedes)
and patch_name not in (select patch_name from patches where retired_yn = 'Y');
