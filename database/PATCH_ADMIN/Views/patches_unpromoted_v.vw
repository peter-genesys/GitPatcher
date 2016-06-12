--Unpromoted patches are those from the THIS DB
--that have not been installed to the FORWARD DB.
create or replace force view patches_unpromoted_v as
select *    
from patches_dependency_v 
where patch_name not in (select patch_name from installed_patches_v@PATCH_ADMIN_FORWARD_DBLINK);
 